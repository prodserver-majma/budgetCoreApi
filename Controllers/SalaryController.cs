using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System.Globalization;
using System.Text.RegularExpressions;

namespace mahadalzahrawebapi.Controllers
{
    public class KeyDisplayName
    {
        public string Key { get; set; }
        public string DisplayName { get; set; }
    }

    public class ValueFromKey
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public string DisplayName { get; set; }
    }

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        private static readonly string WafdAlHuffaz = "Wafd al-Huffaz";
        private static readonly string MahadAlZahra_KHDGZ = "Mahad al-Zahra KHDGZ";

        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly HelperService _helperService;
        private readonly NotificationService _notificationService;
        private readonly globalConstants _globalConstants;
        private readonly WhatsAppApiService _whatsAppApiService;
        private readonly SalaryService _salaryService;

        public SalaryController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _helperService = new HelperService(context);
            _notificationService = new NotificationService();
            _globalConstants = new globalConstants();
            _whatsAppApiService = new WhatsAppApiService(context);
            _salaryService = new SalaryService(context);
        }

        [Route("pakagesalaries")]
        [HttpPost]
        public async Task<ActionResult> pakageSalaries(List<PayrollProcessingModel> salaries)
        {
            string api = "pakagesalaries";
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

                string paymentFrom = salaries.FirstOrDefault().paymentFrom;

                List<PayrollProcessingModel> salariesHijri = salaries.Where(x => x.isHijri == true).ToList();
                List<PayrollProcessingModel> salariesgregorian = salaries.Where(x => x.isHijri == false).ToList();
                bool isHijri = true;
                string monthName = "";
                string pName = "";

                if (salariesHijri.Count != 0 && salariesgregorian.Count != 0)
                {
                    return BadRequest(new { message = "both gregorian and hijri salaries cannot be select together" });
                }

                if (salariesgregorian.Count > 0)
                {
                    isHijri = false;
                    monthName = new DateTime(2022, salariesgregorian.First().month, 1).ToString("MMMM");
                    pName = "Gregorian salary ( " + monthName + " - " + salariesgregorian.First().year + " )";
                }
                else
                {
                    int m = salariesHijri.First().month;

                    hijri_months months = _context.hijri_months.Where(x => x.id == m).FirstOrDefault();

                    monthName = months.hijriMonthName;
                    pName = "Hijri salary ( " + monthName + " - " + salariesHijri.First().year + " )";
                }

                payroll_salary_packages p = new payroll_salary_packages
                {
                    name = pName,
                    amount = 0,
                    paymentFrom = "Kotak Mahindra Bank",
                };

                _context.payroll_salary_packages.Add(p);
                _context.SaveChanges(); //requied id to be generated
                if (isHijri)
                {
                    foreach (PayrollProcessingModel prm in salariesHijri)
                    {
                        salary_allocation_hijri sh = _context.salary_allocation_hijri.Where(x => x.id == prm.id).FirstOrDefault();
                        sh.tds = prm.tds;
                        sh.netEarnings = _salaryService.netSalary(sh);
                        sh.packageId = p.id;
                        p.amount += sh.netEarnings;
                    }
                }
                else
                {
                    foreach (PayrollProcessingModel prm in salariesgregorian)
                    {
                        salary_allocation_gegorian sg = _context.salary_allocation_gegorian.Where(x => x.id == prm.id).FirstOrDefault();
                        sg.tds = prm.tds;
                        sg.netEarnings = _salaryService.netSalary(sg);
                        p.amount += sg.netEarnings;
                        sg.packageId = p.id;
                    }
                }
                _context.SaveChanges();

                return Ok("packages created successfully");

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        //[Route("reversepackage/{id}")]
        //[HttpGet]
        //public async Task<ActionResult> reversePakage(int id)
        //{
        //    string api = "pakagesalaries";
        //    try
        //    {

        //        string token = _tokenService.ExtractTokenFromRequest(HttpContext);
        //        AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
        //        //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

        //        payroll_salary_packages p = _context.payroll_salary_packages.Where(x => x.id == id).FirstOrDefault();
        //        p.salary_allocation_gegorian.ToList().ForEach(x => x.packageId = null);
        //        p.salary_allocation_hijri.ToList().ForEach(x => x.packageId = null);
        //        _context.payroll_salary_packages.Remove(p);
        //        _context.SaveChanges();


        //        return Ok("packages successfully reversed");

        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}


        [Route("addpaymentdate")]
        [HttpPost]
        public async Task<ActionResult> addPaymentDate(AddPaymentModel apm)
        {
            string api = "addPayementDate";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);
                int id = apm.id;
                DateTime paymentDate = apm.paymentDate;

                payroll_salary_packages psp = _context.payroll_salary_packages
                    .Include(x => x.salary_allocation_gegorian).ThenInclude(x => x.its).ThenInclude(x => x.employee_salary)
                    .Include(x => x.salary_allocation_gegorian).ThenInclude(x => x.its).ThenInclude(x => x.employee_bank_details)
                    .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.its).ThenInclude(x => x.employee_salary)
                    .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.its).ThenInclude(x => x.employee_bank_details)
                    .Where(x => x.id == id).FirstOrDefault();

                psp.paymentDate = paymentDate;
                psp.salary_allocation_gegorian.ToList().ForEach(x =>
                {
                    x.paymentDate = paymentDate;
                    x.its.employee_salary.lastSalaryDate = x.salaryTo;
                });
                psp.salary_allocation_hijri.ToList().ForEach(x =>
                {
                    x.paymentDate = paymentDate;
                    x.its.employee_salary.lastSalaryDate = x.salaryTo;
                });
                _context.SaveChanges();

                //Printable_ReportService generatePaySlips = new Printable_ReportService();
                //string result = generatePaySlips.generatePaySlips(psp);
                generatePaySlips(psp);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }


        }

        private static List<ValueFromKey> GetValuesFromKeys(dynamic obj, List<KeyDisplayName> keys)
        {
            // Reflection to handle properties dynamically
            var type = obj.GetType();

            return keys
                .Select(item =>
                {
                    var propertyInfo = type.GetProperty(item.Key);
                    object value = null;
                    if (propertyInfo != null)
                    {
                        value = propertyInfo.GetValue(obj, null);
                    }

                    return new ValueFromKey
                    {
                        Key = item.Key,
                        Value = value,
                        DisplayName = item.DisplayName
                    };
                })
                .Where(item => item.Value != null && !item.Value.Equals(0))
                .ToList();
        }

        private string generatePaySlips(payroll_salary_packages psp)
        {
            //try
            //{
            List<SalaryAllocation> sa = new List<SalaryAllocation>();
            psp.salary_allocation_gegorian.ToList().ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));
            psp.salary_allocation_hijri.ToList().ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));

            int c = 1;
            string monthName = "";
            int year;
            if (sa.FirstOrDefault().isHijri)
            {
                hijri_months months = _context.hijri_months.Where(x => x.id == psp.salary_allocation_hijri.FirstOrDefault().month).FirstOrDefault();
                monthName = months.hijriMonthName;
                year = psp.salary_allocation_hijri.FirstOrDefault().year;

            }
            else
            {
                monthName = new DateTime(2015, psp.salary_allocation_gegorian.FirstOrDefault().month, 01).ToString("MMMM");
                year = psp.salary_allocation_gegorian.FirstOrDefault().year;
            }

            List<int> itsId = sa.Select(x => x.itsId).ToList();
            List<khidmat_guzaar> kh = _context.khidmat_guzaar.Where(x => itsId.Contains(x.itsId)).ToList();

            var earningsKeys = new List<KeyDisplayName>
                {
                    new KeyDisplayName {Key = "salary", DisplayName = "Basic Salary"},
                    new KeyDisplayName {Key = "rentAllowance", DisplayName = "Rent Allowance"},
                    new KeyDisplayName {Key = "fmbAllowance", DisplayName = "FMB Allowance"},
                    new KeyDisplayName {Key = "marriageAllowance", DisplayName = "Marriage Allowance"},
                    new KeyDisplayName {Key = "convenienceAllowance", DisplayName = "Convenience Allowance"},
                    new KeyDisplayName {Key = "arrears", DisplayName = "Arrears"},
                    new KeyDisplayName {Key = "overtime", DisplayName = "Overtime"},

                };

            var deductionsKeys = new List<KeyDisplayName>
                {
                    new KeyDisplayName {Key = "sabeel", DisplayName = "Sabeel"},
                    new KeyDisplayName {Key = "marafiqKhairiyah", DisplayName = "Marafiq Khairiyah"},
                    new KeyDisplayName {Key = "qardanHasanahRefundable", DisplayName = "Qardan Hasanah (Refundable)"},
                    new KeyDisplayName {Key = "qardanHasanahNonRefundable", DisplayName = "Qardan Hasanah (Non Refundable)"},
                    new KeyDisplayName {Key = "withHoldings", DisplayName = "Withholdings"},
                    new KeyDisplayName {Key = "shortfall", DisplayName = "Shortfall"},
                    new KeyDisplayName {Key = "tds", DisplayName = "TDS"},
                    new KeyDisplayName {Key = "professionTax", DisplayName = "Profession Tax"},
                    new KeyDisplayName {Key = "localTax", DisplayName = "Local Tax"}
                };



            foreach (SalaryAllocation w in sa)
            {

                var earnings = GetValuesFromKeys(w, earningsKeys);
                var deductions = GetValuesFromKeys(w, deductionsKeys);

                var totalEarning = earnings.Sum(item => Convert.ToDecimal(item.Value));
                var totalDeduction = deductions.Sum(item => Convert.ToDecimal(item.Value));

                var netSalary = totalEarning - totalDeduction;

                string html = @"<!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>Payment Slip</title>
                        <style>
                            body {
                                font-family: ""Helvetica Neue"", Arial, sans-serif;
                                color: #333;
                                margin: 0;
                                padding: 15px;
                                font-size: 0.8em;
                            }

                            .container {
                                background-color: #f4f4f4;
                                padding: 15px;
                                margin: auto;
                                width: 100%;
                                max-width: 800px;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                page-break-inside: auto;
                            }

                            .header {
                                background-color: #34495e;
                                /* Navy Blue */
                                color: #fff;
                                padding: 8px;
                                display: flex;
                                align-items: center;
                                justify-content: space-around;
                                border-radius: 5px 5px 0 0;
                                text-align: center;
                            }

                            .logo img {
                                max-width: 80px;
                                /* Slightly smaller logo for a more compact header */
                            }

                            .header-info {
                                widht: 100%;
                                text-align: center;
                            }

                            h2,
                            h3,
                            h4,
                            h5,
                            h6 {
                                margin: 5px 0;
                                font-size: 0.9em;
                                /* Reduced margin for compact spacing */
                            }

                            .section-header {
                                background-color: #bdc3c7;
                                /* Light Gray */
                                color: #333;
                                /* Dark text for contrast */
                                padding: 0.5px;
                                padding-left: 7px;
                                margin-top: 15px;
                                border-radius: 5px;
                                page-break-after: avoid;
                            }

                            .section-header h6 {
                                margin: 0;
                                padding: 5px;
                                font-size: 1.1em;
                                font-weight: bold;
                            }

                            table {
                                width: 100%;
                                border-collapse: collapse;
                                margin-top: 5px;
                                page-break-inside: auto;
                            }

                            th,
                            td {
                                border: 1px solid #ddd;
                                text-align: left;
                                padding: 4px;
                                page-break-inside: avoid;
                            }

                            th {
                                background-color: #f2f2f2;
                            }

                            .description {
                                width: 75%;
                            }

                            .amount {
                                width: 25%;
                                text-align: right;
                            }

                            .summary td {
                                font-weight: bold;
                                background-color: #ecf0f1;
                                /* Very light gray for summary rows */
                            }

                            .notes,
                            .footer {
                                text-align: center;
                                margin-top: 20px;
                                font-size: 0.9em;
                                page-break-before: avoid;
                            }

                            .footer {
                                margin-bottom: 0;
                                padding: 10px;
                                background-color: #ecf0f1;
                                border-radius: 0 0 5px 5px;
                            }

                            @media print {

                                body,
                                .container {
                                    padding: 0;
                                    margin: 0;
                                    box-shadow: none;
                                    width: auto;
                                    max-width: none;
                                    page-break-after: auto;
                                }
                            }
                        </style>
                    </head>

                    <body>
                            <div class=""container"" id=""payslipContent"">
                                <div class=""header"">
                                    <div class=""header-info"" style=""width: 100%;"">
                                        <h5>Mahad al-Zahra</h5>
                                        <h6>Payment Slip for the Month of " + monthName + @" - " + w.year + @"</h6>
                                    </div>
                                </div>
                                <table>
                                    <tr>
                                        <td style=""width: 20%;"">Name:</td>
                                        <td><b>" + w.accountName + @"</b></td>
                                    </tr>
                                    <tr>
                                        <td style=""width: 20%;"">Employee Code:</td>
                                        <td><b>" + w.itsId + @"</b></td>
                                    </tr>
                                </table>

                                <div class=""section-header"">
                                    <h6>Earnings</h6>
                                </div>
                                <table id=""earnings"">
                                    <tr>
                                        <th class=""description"">Description</th>
                                        <th class=""amount"">Amount</th>
                                    </tr>";
                foreach (ValueFromKey e in earnings)
                {

                    html +=
                    @"<tr>
                                        <td>" + e.DisplayName + @"</td>
                                        <td class=""amount"">" + e.Value + @"</td>
                                    </tr>";
                }
                html += @"
                                    <!-- Dynamically insert earnings here -->
                                </table>

                                <div class=""section-header"">
                                    <h6>Deductions</h6>
                                </div>
                                <table id=""deductions"">
                                <tr>
                                    <th class=""description"">Description</th>
                                    <th class=""amount"">Amount</th>
                                </tr>";

                foreach (ValueFromKey e in deductions)
                {
                    html += @"<tr>
                                        <td>" + e.DisplayName + @"</td>
                                        <td class=""amount"">" + e.Value + @"</td>
                                    </tr>";
                }
                html += @"<!-- Dynamically insert deductions here -->
                                </table>

                                <div class=""section-header"">
                                    <h6>Summary</h6>
                                </div>
                                <table>
                                    <tr class=""summary"">
                                        <td>Total Earnings</td>
                                        <td class=""amount"">" + totalEarning + @"</td>
                                    </tr>
                                    <tr class=""summary"">
                                        <td>Total Deductions</td>
                                        <td class=""amount"">" + totalDeduction + @"</td>
                                    </tr>
                                    <tr class=""summary"">
                                        <td>Net Payable Amount</td>
                                        <td class=""amount"">" + w.netEarnings + @"</td>
                                    </tr>
                                </table>

                                <div class=""notes"">
                                    Note: This Salary Slip is Computer Generated.
                                    <br>For any queries contact your branch admin.
                                </div>

                                <div class=""footer"">
                                    Payment Date: " + w.paymentDate?.ToString("dd/MM/yyyy") + @"
                                </div>
                            </div>
                    </body>

                    </html>";

                khidmat_guzaar k = kh.Where(x => x.itsId == w.itsId).FirstOrDefault();
                string email = string.IsNullOrEmpty(k.officialEmailAddress) ? k.emailAddress : k.officialEmailAddress;
                if (!string.IsNullOrEmpty(email))
                {
                    _notificationService.SendEmail("Payment Slip : " + psp.name, html, email, "noreply.accounts@mahadalzahra.com");
                }
                //_notificationService.SendEmail("Payment Slip : "+psp.name, html, new List<string> { "hatimn219@gmail.com","husain.bekhushi@mahadalzahra.com" });


                string msg = "Salam jameel,\n*" + k.fullName + "*\n\nYour payslip for the month of *" + monthName + "_" + w.year + "* has been emailed to you at " + email + ". Please check your email for additional information. Alternatively, you can download the *payslip PDF* by following the instructions below:\nVisit www.mahadalzahra.org > HR Login > Personal > Profile > Wazifa Payslip.\n\nShukran,\nWa al-Salaam";
                //_whatsAppApiService.sendGeneralMessageToSpecific(k.mobileNo, msg, "Payslip Notification");
                _whatsAppApiService.sendStarMarketingGeneralWhatsapp(new List<string> { k.c_codeMobile + k.mobileNo }, msg, "Payslip Notification");
            }
            return "success";

            //} catch (Exception e)
            //{
            //    return e.ToString();
            //}
        }

        //[Route("getsalarypackages")]
        //[HttpGet]
        //public async Task<ActionResult> getSalaryPackages()
        //{
        //    List<PackagePayrollModel> pkgList = new List<PackagePayrollModel>();

        //    try
        //    {
        //        List<payroll_salary_packages> psp = _context.payroll_salary_packages.Where(x => x.paymentDate == null).ToList();
        //        foreach (payroll_salary_packages p in psp)
        //        {
        //            List<SalaryAllocation> sa = new List<SalaryAllocation>();

        //            p.salary_allocation_gegorian.ToList().ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));
        //            p.salary_allocation_hijri.ToList().ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));

        //            pkgList.Add(new PackagePayrollModel
        //            {
        //                id = p.id,
        //                name = p.name,
        //                salaries = sa,
        //                paymentFrom = p.paymentFrom,
        //                totalPaymentAmount = p.amount
        //            });
        //        }
        //        return Ok(pkgList);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }

        //}

        [Route("resetdeductionallowance")]
        [HttpGet]
        public async Task<ActionResult> resetDeductionallowance()
        {
            string api = "resetdeductionallowance";

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

                List<employee_salary> psp = _context.employee_salary.Where(x => x.extraAllowance > 0 || x.lessDeduction > 0).ToList();
                foreach (employee_salary p in psp)
                {
                    p.extraAllowance = 0;
                    p.lessDeduction = 0;

                }
                _context.SaveChanges();
                return Ok("Reset succesfull");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("getPendingSalaries/{isHijri}/{hMonth}/{hYear}")]
        [HttpGet]
        public async Task<ActionResult> getPendingSalaries(bool isHijri, int hMonth, int hYear)
        {

            string api = "getPendingSalaries/" + isHijri + "/" + hMonth + "/" + hYear;

            List<PayrollProcessingModel> reportModel = new List<PayrollProcessingModel>();
            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

                List<SalaryAllocation> sa = new List<SalaryAllocation>();
                if (isHijri)
                {
                    List<salary_allocation_hijri> hijriSalaryAllocation = new List<salary_allocation_hijri>();

                    hijriSalaryAllocation = _context.salary_allocation_hijri.Where(x => x.month == hMonth && x.year == hYear && x.packageId == null).ToList();

                    hijriSalaryAllocation.ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));
                }
                else
                {
                    List<salary_allocation_gegorian> hijriSalaryAllocation = new List<salary_allocation_gegorian>();

                    hijriSalaryAllocation = _context.salary_allocation_gegorian.Where(x => x.month == hMonth && x.year == hYear && x.packageId == null).ToList();

                    hijriSalaryAllocation.ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));
                }
                List<EmployeeModel> li = (from hsa in sa
                                          join kh in _context.khidmat_guzaar on hsa.itsId equals kh.itsId
                                          select Translator.khtoModel(kh)).ToList();
                List<EmployeeModel> l = li.OrderBy(x => x.basicDetails.employeeType).ThenBy(x => x.basicDetails.mz_idara).ThenBy(x => x.basicDetails.fullName).ToList();
                int c = 1;
                hijri_months months = _context.hijri_months.Where(x => x.id == hMonth).FirstOrDefault();
                foreach (var w in l)
                {

                    SalaryAllocation i = sa.Where(x => x.itsId == w.basicDetails.itsId).FirstOrDefault();
                    if (i.salary != 0)
                    {
                        var r = new PayrollProcessingModel
                        {
                            id = i.id,
                            employeeType = w.basicDetails.employeeType,
                            ctc = i.ctc,
                            currency = i.currency,
                            netEarnings = i.netEarnings,
                            paymentDate = i.paymentDate,
                            professionTax = i.professionTax ?? 0,
                            tds = i.tds ?? 0,
                            itsId = w.basicDetails.itsId,
                            name = w.basicDetails.fullName,
                            //pan = w.basicDetails.panCardNo,
                            account_Number = "\t" + w.bankDetails[0].bankAccountNumber,
                            bank_AccountName = w.bankDetails[0].bankAccountName,
                            salary = i.salary,
                            bankName = w.bankDetails[0].bankName,
                            ifsc = w.bankDetails[0].ifsc,
                            isHijri = isHijri,
                            month = hMonth,
                            year = hYear,
                        };
                        reportModel.Add(r);
                    }

                }


                return Ok(reportModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        struct dropdownStructure
        {
            public int id;
            public int secondaryid;
            public String name;
        }
        struct namedList
        {
            public string name;
            public List<dropdownStructure> month;
        }

        [Route("getPending/options")]
        [HttpGet]
        public async Task<ActionResult> getPendingOptions()
        {

            string api = "getPending/options";

            List<namedList> reportModel = new List<namedList>();
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

                List<dropdownStructure> eng = new List<dropdownStructure>();
                List<dropdownStructure> hij = new List<dropdownStructure>();

                List<salary_allocation_hijri> hijriSalaryAllocation = new List<salary_allocation_hijri>();

                hijriSalaryAllocation = _context.salary_allocation_hijri.Where(x => x.packageId == null && x.salary != 0).GroupBy(x => new { x.month, x.year }).Select(x => x.FirstOrDefault()).ToList();

                List<salary_allocation_gegorian> gregorianSalaryAllocation = new List<salary_allocation_gegorian>();

                gregorianSalaryAllocation = _context.salary_allocation_gegorian.Where(x => x.salary != 0 && x.packageId == null).GroupBy(x => new { x.month, x.year }).Select(x => x.FirstOrDefault()).ToList();


                foreach (salary_allocation_hijri sh in hijriSalaryAllocation)
                {
                    hijri_months months = _context.hijri_months.Where(x => x.id == sh.month).FirstOrDefault();
                    dropdownStructure e = new dropdownStructure();
                    e.id = sh.month;
                    e.secondaryid = sh.year;
                    e.name = months.hijriMonthName + " " + sh.year;
                    hij.Add(e);
                }
                foreach (salary_allocation_gegorian sg in gregorianSalaryAllocation)
                {
                    string fullMonthName = new DateTime(sg.year, sg.month, 1).ToString("Y", CultureInfo.CreateSpecificCulture("en-US"));
                    dropdownStructure e = new dropdownStructure();
                    e.id = sg.month;
                    e.secondaryid = sg.year;
                    e.name = fullMonthName;
                    eng.Add(e);
                }

                reportModel.Add(new namedList
                {
                    name = "Hijri",
                    month = hij
                });
                reportModel.Add(new namedList
                {
                    name = "Gregorian",
                    month = eng
                });
                return Ok(reportModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("getallsalarylogs")]
        [HttpGet]
        public async Task<ActionResult> getAllSalaryLogs()
        {
            string api = "api/salary/getAllsalaryquery";
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

                List<salary_querylogs> logs = new List<salary_querylogs>();

                logs = _context.salary_querylogs.Where(x => x.type.Equals("Hijri") || x.type.Equals("Gregorian")).ToList();

                return Ok(logs);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("getsalaryreports/{type}")]
        [HttpGet]
        public async Task<ActionResult> getSalaryReport([FromRoute] string type, [FromQuery] string packageIds)
        {
            string api = "api/salary/getSabeelsalaryreport/" + type;
            List<SalarySpecificHeadReportFormat> reportModel = new List<SalarySpecificHeadReportFormat>();

            try
            {
                List<int> packageIdList = _helperService.parseIds(packageIds);

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


                foreach (int pckId in packageIdList)
                {
                    payroll_salary_packages psp = _context.payroll_salary_packages.Where(x => x.id == pckId)
                        .Include(x => x.salary_allocation_gegorian).ThenInclude(x => x.salary_generation_gegorgian).ThenInclude(x => x.salaryTypeNavigation)
                        .Include(x => x.salary_allocation_gegorian).ThenInclude(x => x.salary_generation_gegorgian).ThenInclude(x => x.deptVenue)
                        .Include(x => x.salary_allocation_gegorian).ThenInclude(x => x.its)
                        .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.salary_generation_hijri).ThenInclude(x => x.salaryTypeNavigation)
                        .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.salary_generation_hijri).ThenInclude(x => x.deptVenue)
                        .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.its)
                        .FirstOrDefault();

                    if (psp == null)
                    {
                        continue;
                    }

                    List<salary_allocation_hijri> hijriSalaryAllocation = psp.salary_allocation_hijri.ToList();
                    List<salary_allocation_gegorian> gregorianSalaryAllocation = psp.salary_allocation_gegorian.ToList();

                    List<SalaryAllocation> genSalaryAllocation = new List<SalaryAllocation>();

                    bool isHijri = hijriSalaryAllocation.Count > 0;

                    if (isHijri)
                    {

                        psp.salary_allocation_hijri.ToList().ForEach(x => genSalaryAllocation.Add(_mapper.Map<SalaryAllocation>(x)));
                    }
                    else
                    {
                        psp.salary_allocation_hijri.ToList().ForEach(x => genSalaryAllocation.Add(_mapper.Map<SalaryAllocation>(x)));
                    }


                    switch (type)
                    {
                        case "rent":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.rentAllowance > 0).ToList();
                            break;
                        case "fmb":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.fmbAllowance > 0).ToList();
                            break;
                        case "marriage":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.marriageAllowance > 0).ToList();
                            break;
                        case "conveyance":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.convenienceAllowance > 0).ToList();
                            break;
                        case "arrears":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.arrears > 0).ToList();
                            break;
                        case "overtime":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.overtime > 0).ToList();
                            break;
                        case "shortfall":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.shortfall > 0).ToList();
                            break;
                        case "sabeel":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.sabeel > 0).ToList();
                            break;
                        case "marafiq":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.marafiqKhairiyah > 0).ToList();
                            break;
                        case "qhrefundable":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.qardanHasanahRefundable > 0).ToList();
                            break;
                        case "qhnonrefundable":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.qardanHasanahNonRefundable > 0).ToList();
                            break;
                        case "withholdings":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.withHoldings > 0).ToList();
                            break;
                        case "tds":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.tds > 0).ToList();
                            break;
                        case "professiontax":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.professionTax > 0).ToList();
                            break;
                        case "localtax":
                            genSalaryAllocation = genSalaryAllocation.Where(x => x.localTax > 0).ToList();
                            break;
                    }


                    List<int> itsIds = genSalaryAllocation.Select(x => x.itsId).ToList();

                    List<khidmat_guzaar> kg = _context.khidmat_guzaar.Where(x => itsIds.Contains(x.itsId)).ToList();

                    List<khidmat_guzaar> l = kg.OrderBy(x => x.employeeType).ThenBy(x => x.mz_idara).ThenBy(x => x.fullName).ToList();
                    int c = 1;

                    string monthName = "";
                    int year = 0;

                    if (isHijri)
                    {
                        hijri_months months = _context.hijri_months.Where(x => x.id == hijriSalaryAllocation.FirstOrDefault().month).FirstOrDefault();
                        monthName = months.hijriMonthName;
                        year = hijriSalaryAllocation.FirstOrDefault().year;
                    }
                    else
                    {
                        monthName = new DateTime(2015, gregorianSalaryAllocation.FirstOrDefault().month, 01).ToString("MMMM");
                        year = gregorianSalaryAllocation.FirstOrDefault().year;
                    }

                    foreach (var w in l)
                    {

                        SalaryAllocation i = genSalaryAllocation.Where(x => x.itsId == w.itsId).FirstOrDefault();
                        var r = new SalarySpecificHeadReportFormat
                        {
                            srNo = c,
                            itsId = w.itsId,
                            name = w.fullName,
                            hijriMonth = monthName + "-" + year,
                            panCard = w.panCardNo,
                            packageId = psp.id
                        };
                        switch (type)
                        {
                            case "rent":
                                r.amount = i.rentAllowance;
                                break;
                            case "fmb":
                                r.amount = i.fmbAllowance;
                                break;
                            case "marriage":
                                r.amount = i.marriageAllowance;
                                break;
                            case "conveyance":
                                r.amount = i.convenienceAllowance;
                                break;
                            case "arrears":
                                r.amount = i.arrears;
                                break;
                            case "overtime":
                                r.amount = i.overtime;
                                break;
                            case "shortfall":
                                r.amount = i.shortfall;
                                break;
                            case "sabeel":
                                r.amount = i.sabeel;
                                break;
                            case "marafiq":
                                r.amount = i.marafiqKhairiyah;
                                break;
                            case "qhrefundable":
                                r.amount = i.qardanHasanahRefundable;
                                break;
                            case "qhnonrefundable":
                                r.amount = i.qardanHasanahNonRefundable;
                                break;
                            case "withholdings":
                                r.amount = i.withHoldings;
                                break;
                            case "tds":
                                r.amount = i.tds;
                                break;
                            case "professiontax":
                                r.amount = i.professionTax;
                                break;
                            case "localtax":
                                r.amount = i.localTax;
                                break;

                        }
                        reportModel.Add(r);
                        c = c + 1;

                    }

                }
                return Ok(reportModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public struct csvStruct
        {
            public string csvIts;
        }

        //Get Salary Packages

        [Route("getSalaryAllocation")]
        [HttpGet]
        public async Task<ActionResult> getSalaryAllocations()
        {
            string api = "api/salary/getSalaryPackages/";
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);
                List<SalaryAllocation> sa = new List<SalaryAllocation>();
                payroll_salary_packages psp = new payroll_salary_packages();

                if (authUser.qismId == 0)
                {
                    psp = _context.payroll_salary_packages
                        .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.its).ThenInclude(x => x.employee_academic_details)
                        .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.its).ThenInclude(x => x.employee_academic_details)
                        .FirstOrDefault();
                }
                else
                {
                    psp = _context.payroll_salary_packages.Where(x => x.qismId == authUser.qismId)
                        .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.its).ThenInclude(x => x.employee_academic_details)
                        .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.its).ThenInclude(x => x.employee_academic_details)
                        .FirstOrDefault();
                }

                psp.salary_allocation_gegorian.ToList().ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));
                psp.salary_allocation_hijri.ToList().ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));

                sa.Sort((x, y) =>
                {

                    // Sort by mz_idara based on idaraSortingOrder
                    int indexX = _globalConstants.idaraSortingOrder.IndexOf(x.mzIdara);
                    int indexY = _globalConstants.idaraSortingOrder.IndexOf(y.mzIdara);
                    if (indexX == -1) indexX = int.MaxValue;
                    if (indexY == -1) indexY = int.MaxValue;
                    if (indexX != indexY) return indexX.CompareTo(indexY);

                    // Sort by batchId in descending order
                    int batchIdComparison = (y.batchId ?? 0).CompareTo(x.batchId ?? 0);
                    if (batchIdComparison != 0) return batchIdComparison;

                    // Sort by category in ascending order
                    int categoryComparison = string.Compare(x.category, y.category, StringComparison.Ordinal);
                    if (categoryComparison != 0) return categoryComparison;

                    // Sort by farigDarajah in descending order
                    int farigDarajahComparison = (y.farigDarajah ?? 0).CompareTo(x.farigDarajah ?? 0);
                    if (farigDarajahComparison != 0) return farigDarajahComparison;

                    // Sort by age in descending order
                    return (y.age).CompareTo(x.age);
                });

                return Ok(sa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("getMySalaryAllocation")]
        [HttpGet]
        public async Task<ActionResult> getMySalaryAllocations()
        {
            string api = "api/salary/getSalaryPackages/";
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);
                List<SalaryAllocation> sa = new List<SalaryAllocation>();

                _context.salary_allocation_gegorian.Where(x => x.itsId == authUser.ItsId && x.paymentDate != null)
                    .Include(x => x.its).ThenInclude(x => x.employee_academic_details)
                    .Include(x => x.its).ThenInclude(x => x.employee_bank_details)
                    .ToList().ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));
                _context.salary_allocation_hijri.Where(x => x.itsId == authUser.ItsId && x.paymentDate != null)
                    .Include(x => x.its).ThenInclude(x => x.employee_academic_details)
                    .Include(x => x.its).ThenInclude(x => x.employee_bank_details)
                    .ToList().ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));

                sa.Sort((x, y) =>
                {

                    // Sort by mz_idara based on idaraSortingOrder
                    int indexX = _globalConstants.idaraSortingOrder.IndexOf(x.mzIdara);
                    int indexY = _globalConstants.idaraSortingOrder.IndexOf(y.mzIdara);
                    if (indexX == -1) indexX = int.MaxValue;
                    if (indexY == -1) indexY = int.MaxValue;
                    if (indexX != indexY) return indexX.CompareTo(indexY);

                    // Sort by batchId in descending order
                    int batchIdComparison = (y.batchId ?? 0).CompareTo(x.batchId ?? 0);
                    if (batchIdComparison != 0) return batchIdComparison;

                    // Sort by category in ascending order
                    int categoryComparison = string.Compare(x.category, y.category, StringComparison.Ordinal);
                    if (categoryComparison != 0) return categoryComparison;

                    // Sort by farigDarajah in descending order
                    int farigDarajahComparison = (y.farigDarajah ?? 0).CompareTo(x.farigDarajah ?? 0);
                    if (farigDarajahComparison != 0) return farigDarajahComparison;

                    // Sort by age in descending order
                    return (y.age).CompareTo(x.age);
                });

                return Ok(sa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("getSalaryPackages")]
        [HttpGet]
        public async Task<ActionResult> getSalaryPackages([FromQuery] string? qismIds = null)
        {
            string api = "api/salary/getSalaryPackages/";
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

                List<int> qids = _helperService.parseIds(qismIds);

                if (qids.Count == 0)
                {
                    qids.Add(authUser.qismId);
                }

                if (authUser.qismId == 0)
                {
                    qids = _context.qism_al_tahfeez.Select(x => x.id).ToList();
                }

                List<payroll_salary_packages> psp = _context.payroll_salary_packages.Where(x => qids.Contains(x.qismId)).ToList();

                List<PackagePayrollModel> pkgList = new List<PackagePayrollModel>();
                foreach (var p in psp)
                {
                    PackagePayrollModel temp = new PackagePayrollModel();
                    temp = _mapper.Map<PackagePayrollModel>(p);
                    CalenderModel fromHD = _helperService.getHijriDate(temp.fromDate ?? DateTime.Now);
                    CalenderModel toHD = _helperService.getHijriDate(temp.toDate ?? DateTime.Now);
                    temp.fromDateHijri = fromHD.hijDay + "/" + fromHD.hijMonth + "/" + fromHD.hijYear;
                    temp.toDateHijri = toHD.hijDay + "/" + toHD.hijMonth + "/" + toHD.hijYear;
                    pkgList.Add(temp);
                }

                return Ok(pkgList.OrderByDescending(x => x.id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("getSalaryAllocations/{id}")]
        [HttpGet]
        public async Task<ActionResult> getSalaryAllocations(int id)
        {
            string api = "api/salary/getSalaryPackages/";
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);
                List<SalaryAllocation> sa = new List<SalaryAllocation>();
                payroll_salary_packages psp = _context.payroll_salary_packages.Where(x => x.id == id)
                    .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.its).ThenInclude(x => x.employee_academic_details)
                    .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.its).ThenInclude(x => x.employee_bank_details.OrderBy(x => x.isDefault))
                    .Include(x => x.salary_allocation_gegorian).ThenInclude(x => x.its).ThenInclude(x => x.employee_academic_details)
                    .Include(x => x.salary_allocation_gegorian).ThenInclude(x => x.its).ThenInclude(x => x.employee_bank_details.OrderBy(x => x.isDefault))
                    .FirstOrDefault();
                psp.salary_allocation_gegorian.ToList().ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));
                psp.salary_allocation_hijri.ToList().ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));

                sa.Sort((x, y) =>
                {

                    // Sort by mz_idara based on idaraSortingOrder
                    int indexX = _globalConstants.idaraSortingOrder.IndexOf(x.mzIdara);
                    int indexY = _globalConstants.idaraSortingOrder.IndexOf(y.mzIdara);
                    if (indexX == -1) indexX = int.MaxValue;
                    if (indexY == -1) indexY = int.MaxValue;
                    if (indexX != indexY) return indexX.CompareTo(indexY);

                    // Sort by batchId in descending order
                    int batchIdComparison = (y.batchId ?? 0).CompareTo(x.batchId ?? 0);
                    if (batchIdComparison != 0) return batchIdComparison;

                    // Sort by category in ascending order
                    int categoryComparison = string.Compare(x.category, y.category, StringComparison.Ordinal);
                    if (categoryComparison != 0) return categoryComparison;

                    // Sort by farigDarajah in descending order
                    int farigDarajahComparison = (y.farigDarajah ?? 0).CompareTo(x.farigDarajah ?? 0);
                    if (farigDarajahComparison != 0) return farigDarajahComparison;

                    // Sort by age in descending order
                    return (y.age).CompareTo(x.age);
                });

                return Ok(sa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("getSalaryGenerations/{allocationid}/{isHijri}")]
        [HttpGet]
        public async Task<ActionResult> getSalaryGenerations(int allocationid, bool isHijri)
        {
            string api = "api/salary/getSalaryGenerations/";
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);
                List<SalaryGeneration> sa = new List<SalaryGeneration>();
                if (isHijri)
                {
                    List<salary_generation_hijri> sgh = _context.salary_generation_hijri.Include(x => x.deptVenue).Include(x => x.salaryTypeNavigation).Where(x => x.allocationId == allocationid).ToList();
                    sgh.ForEach(x => sa.Add(_mapper.Map<SalaryGeneration>(x)));
                }
                else
                {
                    List<salary_generation_gegorgian> sgg = _context.salary_generation_gegorgian.Include(x => x.deptVenue).Include(x => x.salaryTypeNavigation).Where(x => x.allocationId == allocationid).ToList();
                    sgg.ForEach(x => sa.Add(_mapper.Map<SalaryGeneration>(x)));
                }
                return Ok(sa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("generatesalariesspecifix/{isHijri}/{hMonth}/{hYear}")]
        [HttpPost]
        public async Task<ActionResult> generateSalariesSpecifix(bool isHijri, int hMonth, int hYear, csvStruct csvModel)
        {
            List<int> itsIds = _helperService.parseItsId(csvModel.csvIts);

            string api = "api/salary/generatesalaries/ " + isHijri + "/" + hMonth + "/" + hYear;


            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {

                List<dept_venue> depts = _context.dept_venue.ToList();

                CalenderModel f = new CalenderModel
                {
                    hijDay = 21,
                    hijMonth = hMonth == 1 ? 12 : hMonth - 1,
                    hijYear = hMonth == 1 ? hYear - 1 : hYear,
                };

                CalenderModel t = new CalenderModel
                {
                    hijDay = 20,
                    hijMonth = hMonth,
                    hijYear = hYear,
                };

                DateTime from = _helperService.getEngDate(f);
                DateTime to = _helperService.getEngDate(t);
                if (!isHijri)
                {
                    from = new DateTime(hMonth == 1 ? hYear - 1 : hYear, hMonth == 1 ? 12 : hMonth - 1, 20);
                    to = new DateTime(hYear, hMonth, 20);
                }

                salary_querylogs logs = _context.salary_querylogs.Where(x => x.type.Equals(isHijri ? "Hijri" : "Gregorian") && x.hijriMonth == hMonth && x.hijriYear == hYear).FirstOrDefault();
                if (logs == null)
                {
                    return BadRequest(new { message = "this function is allowed only after bulk generation" });
                }
                List<khidmat_guzaar> kgs = _context.khidmat_guzaar.ToList();
                kgs = kgs.Where(x => itsIds.Any(y => y == x.itsId)).ToList();

                if (isHijri)
                {
                    if (kgs.Any(x => x.salary_allocation_hijri.Where(y => y.month == hMonth && y.year == hYear).FirstOrDefault()?.packageId != null))
                    {
                        return BadRequest(new { message = "Salary for the Following month has been processed" });
                    }

                }
                else
                {
                    if (kgs.Any(x => x.salary_allocation_gegorian.Where(y => y.month == hMonth && y.year == hYear).FirstOrDefault()?.packageId != null))
                    {
                        return BadRequest(new { message = "Salary for the Following month has been processed" });
                    }
                }

                //List<azwaaj_minentry> azwaajmin = _context.azwaaj_minentry.Where(x => x.date >= from && x.date <= to).ToList();
                List<EmployeeSalaryDetailsModel> employeeSalaryDetailsList = kgs
                    .Select(x => new EmployeeSalaryDetailsModel()
                    {
                        bqhs = x.employee_salary.bqhs,
                        conveyanceAllowance = x.employee_salary.conveyanceAllowance,
                        extraAllowance = x.employee_salary.extraAllowance,
                        grossSalary = x.employee_salary.grossSalary,
                        husainiQardanHasanah = x.employee_salary.husainiQardanHasanah,
                        installmentDeduction_Qardan = x.employee_salary.installmentDeduction_Qardan,
                        isHijriAllowence = x.employee_salary.isHijriAllowence,
                        isHusainiQardan = x.employee_salary.isHusainiQardan,
                        isMahadSalary = x.employee_salary.isMahadSalary,
                        itsId = x.employee_salary.itsId,
                        lessDeduction = x.employee_salary.lessDeduction,
                        marafiqKhairiyah = x.employee_salary.marafiqKhairiyah,
                        marriageAllowance = x.employee_salary.marriageAllowance,
                        mohammediQardanHasanah = x.employee_salary.mohammediQardanHasanah,
                        mumbaiAllowance = x.employee_salary.mumbaiAllowance,
                        professionTax = x.employee_salary.professionTax,
                        qardanHasanah = x.employee_salary.qardanHasanah,
                        rentAllowance = x.employee_salary.rentAllowance,
                        sabeel = x.employee_salary.sabeel,
                        tds = x.employee_salary.tds,
                        fmbAllowance = x.employee_salary.fmbAllowance,
                        fmbDeduction = x.employee_salary.fmbDeduction,
                    }
                    ).ToList();

                foreach (EmployeeSalaryDetailsModel item in employeeSalaryDetailsList)
                {
                    khidmat_guzaar processingKg = kgs.Where(x => x.itsId == item.itsId).FirstOrDefault();
                    if (processingKg == null)
                    {
                        continue;
                    }

                    EmployeeSalaryModel i = new EmployeeSalaryModel();
                    i.salaryDetails = item;
                    List<EmployeeDeptSalaries> edsAll = (from eds in _context.employee_dept_salary
                                                         where eds.itsId == i.salaryDetails.itsId && eds.hasSalary == true
                                                         select new EmployeeDeptSalaries
                                                         {
                                                             deptVenueId = eds.deptVenueId,
                                                             hasSalary = eds.hasSalary,
                                                             isHijriSalary = eds.isHijriSalary,
                                                             itsId = eds.itsId,
                                                             salaryAmount = eds.salaryAmount,
                                                             salaryTypeId = eds.salaryTypeId,
                                                             workingMin = eds.workingMin,
                                                             dept_venue = eds.deptVenue,
                                                         }).ToList();

                    i.deptSalaries = edsAll.Where(x => x.isHijriSalary == isHijri).ToList();

                    List<EmployeeDeptSalaries> toGenerate = new List<EmployeeDeptSalaries>();

                    if (isHijri)
                    {
                        List<salary_generation_hijri> toDeleteGenerations = processingKg.salary_generation_hijri.Where(x => x.year == hYear && x.month == hMonth).ToList();
                        toDeleteGenerations.ForEach(x =>
                        {
                            _context.salary_generation_hijri.Remove(x);
                        });
                        _context.SaveChanges();
                        salary_allocation_hijri toDeleteAllocation = processingKg.salary_allocation_hijri.Where(x => x.year == hYear && x.month == hMonth).FirstOrDefault();
                        _context.salary_allocation_hijri.Remove(toDeleteAllocation);
                    }
                    else
                    {
                        List<salary_generation_gegorgian> toDeleteGenerations = processingKg.salary_generation_gegorgian.Where(x => x.year == hYear && x.month == hMonth).ToList();
                        toDeleteGenerations.ForEach(x =>
                        {
                            _context.salary_generation_gegorgian.Remove(x);
                        });
                        _context.SaveChanges();
                        salary_allocation_gegorian toDeleteAllocation = processingKg.salary_allocation_gegorian.Where(x => x.year == hYear && x.month == hMonth).FirstOrDefault();
                        _context.salary_allocation_gegorian.Remove(toDeleteAllocation);

                    }

                    _context.SaveChanges();

                    if (i.deptSalaries.Count == 0 || processingKg.employee_salary?.isMahadSalary == false)
                    {
                        continue;
                    }

                    foreach (EmployeeDeptSalaries j in i.deptSalaries)
                    {
                        if (j.salaryTypeId == 4)
                        {
                            continue;
                        }
                        if (j.salaryTypeId == 2 || j.salaryTypeId == 3)
                        {
                            List<azwaaj_minentry> azwaajmin1 = kgs.Where(x => x.itsId == j.itsId).FirstOrDefault()?.azwaaj_minentry.Where(x => x.date >= DateOnly.FromDateTime(from) && x.date <= DateOnly.FromDateTime(to) && x.deptVenueId == j.deptVenueId && x.policyId == j.salaryTypeId).ToList();

                            if (azwaajmin1.Count == 0)
                            {
                                continue;
                            }

                            int totalMin = 0;
                            float value = 0;
                            dept_venue d = depts.Where(x => x.id == j.deptVenueId).FirstOrDefault();

                            foreach (var k in azwaajmin1)
                            {
                                totalMin = totalMin + (k.min ?? 0);
                            }

                            if (totalMin == 0)
                            {
                                continue;
                            }

                            if (j.deptVenueId == 17)
                            {
                                float period = (float)((totalMin == 0 ? 35 : totalMin) / 35.00);

                                if (period > 0 && totalMin % 35 != 0)
                                {
                                    int x = Convert.ToInt32(Math.Floor(period) + 1);
                                    float x1 = (float)((x - period) * 35.00);
                                    int x2 = Convert.ToInt32(Math.Round(x1));
                                    if (x2 != 0)
                                    {
                                        totalMin += x2;
                                        var aa = new azwaaj_minentry
                                        {
                                            itsid = j.itsId,
                                            date = DateOnly.FromDateTime(to),
                                            min = x2,
                                            deptVenueId = j.deptVenueId,
                                            createdBy = "Grace :- " + authUser.Name,
                                            createdOn = DateTime.Now,
                                            policyId = j.salaryTypeId,
                                            rate = j.salaryAmount,
                                            allotedMin = j.workingMin
                                        };

                                        processingKg.azwaaj_minentry.Add(aa);
                                    }

                                }
                            }

                            j.workingMin = totalMin;
                            j.salaryAmount = totalMin * j.salaryAmount;

                        }
                        else
                        {
                            var tmin = j.workingMin * 30;
                            j.workingMin = tmin;
                        }
                        toGenerate.Add(j);
                    }

                    i.salaryDetails.grossSalary = _salaryService.applicableSalary(toGenerate);
                    var ctc = i.salaryDetails.grossSalary;
                    var netS = i.salaryDetails.grossSalary;
                    var allowences = 0;


                    if (i.salaryDetails.isHijriAllowence == isHijri)
                    {
                        ctc = _salaryService.caculateCTC(i.salaryDetails);
                        i.salaryDetails.professionTax = _salaryService.getProfessionalTax(ctc);
                        netS = _salaryService.netSalary(i.salaryDetails);
                        allowences = ctc - i.salaryDetails.grossSalary;
                    }


                    salary_allocation_hijri sah = new salary_allocation_hijri();
                    salary_allocation_gegorian sag = new salary_allocation_gegorian();

                    DateTime dt = DateTime.Now;
                    string currency = _salaryService.getCurrency(i.deptSalaries.FirstOrDefault()?.dept_venue.venueId ?? 0);
                    if (isHijri)
                    {
                        sah = new salary_allocation_hijri()
                        {
                            itsId = i.salaryDetails.itsId,
                            createdBy = authUser.Name,
                            salary = i.salaryDetails.grossSalary,
                            bqhs = i.salaryDetails.bqhs,
                            convenienceAllowance = i.salaryDetails.conveyanceAllowance,
                            createdOn = dt,
                            ctc = ctc,
                            currency = currency,
                            extraAllowance = i.salaryDetails.extraAllowance,
                            husaini_qardanHasanah = i.salaryDetails.husainiQardanHasanah,
                            installmentDeduction_Qardan = i.salaryDetails.installmentDeduction_Qardan,
                            lessDeduction = i.salaryDetails.lessDeduction,
                            marafiqKhairiyah = i.salaryDetails.marafiqKhairiyah,
                            marriageAllowance = i.salaryDetails.marriageAllowance,
                            mohammedi_qardanHasanah = i.salaryDetails.mohammediQardanHasanah,
                            month = hMonth,
                            year = hYear,
                            mumbaiAllowance = i.salaryDetails.mumbaiAllowance,
                            netEarnings = netS,
                            professionTax = i.salaryDetails.professionTax,
                            qardanHasanah = i.salaryDetails.qardanHasanah,
                            rentAllowance = i.salaryDetails.rentAllowance,
                            sabeel = i.salaryDetails.sabeel,
                            fmbAllowance = i.salaryDetails.fmbAllowance,
                            fmbDeduction = i.salaryDetails.fmbDeduction,
                        };
                        processingKg.salary_allocation_hijri.Add(sah);
                    }
                    else
                    {
                        sag = new salary_allocation_gegorian()
                        {
                            itsId = i.salaryDetails.itsId,
                            createdBy = authUser.Name,
                            salary = i.salaryDetails.grossSalary,
                            bqhs = i.salaryDetails.bqhs,
                            convenienceAllowance = i.salaryDetails.conveyanceAllowance,
                            createdOn = dt,
                            ctc = ctc,
                            currency = currency,
                            extraAllowance = i.salaryDetails.extraAllowance,
                            husaini_qardanHasanah = i.salaryDetails.husainiQardanHasanah,
                            installmentDeduction_Qardan = i.salaryDetails.installmentDeduction_Qardan,
                            lessDeduction = i.salaryDetails.lessDeduction,
                            marafiqKhairiyah = i.salaryDetails.marafiqKhairiyah,
                            marriageAllowance = i.salaryDetails.marriageAllowance,
                            mohammedi_qardanHasanah = i.salaryDetails.mohammediQardanHasanah,
                            month = hMonth,
                            year = hYear,
                            mumbaiAllowance = i.salaryDetails.mumbaiAllowance,
                            netEarnings = netS,
                            professionTax = i.salaryDetails.professionTax,
                            qardanHasanah = i.salaryDetails.qardanHasanah,
                            rentAllowance = i.salaryDetails.rentAllowance,
                            sabeel = i.salaryDetails.sabeel,
                            fmbAllowance = i.salaryDetails.fmbAllowance,
                            fmbDeduction = i.salaryDetails.fmbDeduction,
                        };
                        processingKg.salary_allocation_gegorian.Add(sag);
                    }

                    _context.SaveChanges();
                    int netAmount = 0;

                    foreach (EmployeeDeptSalaries eds in toGenerate)
                    {
                        if (eds.deptVenueId != 17)
                        {
                            if (allowences != 0)
                            {
                                float percent = ((eds.salaryAmount ?? 0) * 100) / i.salaryDetails.grossSalary;
                                float partialAllowence = ((percent * allowences) / 100);
                                eds.salaryAmount = eds.salaryAmount + partialAllowence;
                            }
                            netAmount += (int)Math.Round(eds.salaryAmount ?? 0);
                        }
                        if (isHijri)
                        {
                            salary_generation_hijri sgh = new salary_generation_hijri()
                            {
                                createdBy = authUser.Name,
                                itsId = eds.itsId,
                                salaryType = eds.salaryTypeId,
                                allocationId = sah.id,
                                deptVenueId = eds.deptVenueId,
                                month = hMonth,
                                year = hYear,
                                createdOn = DateTime.Now,
                                netSalary = (int)Math.Round(eds.salaryAmount ?? 0),
                                quantity = eds.workingMin ?? 0,
                            };
                            processingKg.salary_generation_hijri.Add(sgh);
                        }
                        else
                        {
                            salary_generation_gegorgian sgg = new salary_generation_gegorgian()
                            {
                                createdBy = authUser.Name,
                                itsId = eds.itsId,
                                salaryType = eds.salaryTypeId,
                                allocationId = sag.id,
                                deptVenueId = eds.deptVenueId,
                                month = hMonth,
                                year = hYear,
                                createdOn = DateTime.Now,
                                netSalary = (int)Math.Round(eds.salaryAmount ?? 0),
                                quantity = eds.workingMin ?? 0,
                            };
                            processingKg.salary_generation_gegorgian.Add(sgg);
                        }

                    }
                    if (isHijri)
                    {
                        if (((sah.ctc - netAmount) != 0) && (sah.ctc != 0 && netAmount != 0))
                        {
                            processingKg.salary_generation_hijri.Where(x => x.itsId == i.salaryDetails.itsId && x.month == hMonth && x.year == hYear && x.deptVenueId != 17).OrderByDescending(x => x.netSalary).ToList().FirstOrDefault().netSalary += (netAmount - sah.ctc);
                        }
                    }
                    else
                    {
                        if (((sah.ctc - netAmount) != 0) && (sah.ctc != 0 && netAmount != 0))
                        {
                            processingKg.salary_generation_gegorgian.Where(x => x.itsId == i.salaryDetails.itsId && x.month == hMonth && x.year == hYear && x.deptVenueId != 17).OrderByDescending(x => x.netSalary).ToList().FirstOrDefault().netSalary += (netAmount - sag.ctc);
                        }
                    }

                }

                //_context.salary_querylogs.Add(
                //         new salary_querylogs
                //         {
                //             hijriMonth = hMonth,
                //             hijriYear = hYear,
                //             type = isHijri ? "Hijri" : "Gregorian",
                //             createdOn = DateTime.Now,
                //             createdBy = authUser.Name,

                //             fromDate = from,
                //             toDate = to,
                //         }
                //         );

                _context.SaveChanges();


                return Ok("succesfully Allocated");
            }
            catch (DbUpdateException ex)
            {
                string err = "";
                foreach (var entry in ex.Entries)
                {
                    // entry.Entity will give you the entity that caused the exception
                    // Now you can inspect the entity's state and properties to determine the problematic parameter
                    var entity = entry.Entity;
                    var currentState = entry.State;

                    // Log the information about the entity and the state for further analysis
                    Console.WriteLine($"Entity of type {entity.GetType().Name} in state {currentState} caused the exception");

                    // Check individual properties if necessary
                    foreach (var property in entry.OriginalValues.Properties)
                    {
                        var originalValue = entry.OriginalValues[property];
                        var currentValue = entry.CurrentValues[property];
                        // You can log these values or perform additional checks

                        err += originalValue + " -> " + currentValue + ", ";
                    }
                }
                return BadRequest(ex + err);
            }
        }

        [Route("v2/generatesalaries/gregorian")]
        [HttpPost]
        public async Task<ActionResult> generateSalariesGregorianV2(PayrollProcessing payrollProcessing)
        {
            try
            {
                // Extract Token and Authenticate User
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                // Parse qismId and Get Associated Venues
                payrollProcessing.qismIdList = _helperService.parseIds(payrollProcessing.qismIds);
                int qismId = payrollProcessing.qismIdList.FirstOrDefault();

                List<venue> venues = _context.venue.Include(x => x.qism).Where(x => x.qismId == qismId).ToList();

                // Create Salary Package
                payroll_salary_packages payrollSalaryPackage = new payroll_salary_packages()
                {
                    fromDate = payrollProcessing.from,
                    toDate = payrollProcessing.to,
                    createdOn = DateTime.UtcNow,
                    qismId = qismId,
                    paymentFrom = "Kotak Mahindra Bank",
                    createdBy = authUser.ItsId,
                    amount = 0,
                    description = $"{venues.First().qism.name} ( Gregorian Wazifa - {payrollProcessing.to:MMMM}/{payrollProcessing.to.Year} )",
                    name = $"{venues.First().qism.name} ( Gregorian Wazifa - {payrollProcessing.to:MMMM}/{payrollProcessing.to.Year} )"
                };

                DateOnly fromDate = DateOnly.FromDateTime(payrollProcessing.from);
                DateOnly toDate = DateOnly.FromDateTime(payrollProcessing.to);

                List<azwaaj_minentry> attendance = await _context.azwaaj_minentry
                    .Include(x => x.deptVenue)
                    .Where(x => x.deptVenue.qismId > 0 &&
                                x.date >= fromDate &&
                                x.date <= toDate &&
                                payrollProcessing.qismIdList.Contains(x.deptVenue.qismId ?? 0) &&
                                x.deptVenueId != 17)
                    .ToListAsync();

                // Fetch Employees Based on Attendance
                List<int?> attendies = attendance.Select(x => x.itsid).Distinct().ToList();

                List<khidmat_guzaar> employees = await _context.khidmat_guzaar
                    .Where(x => attendies.Contains(x.itsId) &&
                                x.salaryCalender == "Gregorian")
                    .Include(x => x.employee_salary)
                    .Include(x => x.employee_dept_salary.Where(y => y.deptVenueId != 17 && !(y.isHijriSalary ?? false)))
                    .Include(x => x.salary_allocation_gegorian)
                    .ThenInclude(x => x.salary_generation_gegorgian)
                    .ToListAsync();

                // Generate Salaries
                List<salary_allocation_gegorian> salaryAllocations = _salaryService.GenerateSalaries(employees, attendance, payrollProcessing, authUser);

                // Save Salary Package
                payrollSalaryPackage.salary_allocation_gegorian.AddRange(salaryAllocations);
                payrollSalaryPackage.amount = salaryAllocations.Sum(x => x.netEarnings);

                await _context.payroll_salary_packages.AddAsync(payrollSalaryPackage);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred while processing salaries.", Exception = ex.ToString() });
            }
        }

        [Route("generatesalaries/gregorian")]
        [HttpPost]
        public async Task<ActionResult> generateSalariesGregorian(PayrollProcessing payrollProcessing)
        {
            string debugMsg = "setp - 1";
            try
            {
                string api = "api/salary/generatesalaries/gregorian/" + payrollProcessing.hMonth + "/" + payrollProcessing.hYear;

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                int qismId = _helperService.parseIds(payrollProcessing.qismIds).FirstOrDefault();

                var venues = _context.venue.Include(x => x.qism).Where(x => x.qismId > 0 && qismId == x.qismId);
                //List<int> venueIds = venues.Select(x => x.Id).ToList();


                payroll_salary_packages payroll_Salary_Packages = new payroll_salary_packages()
                {
                    fromDate = payrollProcessing.from,
                    toDate = payrollProcessing.to,
                    createdOn = DateTime.UtcNow,
                    qismId = qismId,
                    paymentFrom = "Kotak Mahindra Bank",
                    createdBy = authUser.ItsId,
                    amount = 0,
                    description = "",
                    name = venues.First().qism.name + " ( Gregorian Wazifa - " + payrollProcessing.to.ToString("MMMM") + "/" + payrollProcessing.to.Year + " )"
                };
                DateOnly toDate = DateOnly.FromDateTime(payrollProcessing.to);
                DateOnly fromDate = DateOnly.FromDateTime(payrollProcessing.from);

                payrollProcessing.qismIdList = _helperService.parseIds(payrollProcessing.qismIds);

                debugMsg = "setp - 2";

                List<azwaaj_minentry> attendence = await _context.azwaaj_minentry.Where(
                    x => x.deptVenueId != 17 && x.deptVenue.qismId > 0
                    && x.date >= fromDate
                    && x.date <= toDate
                    && payrollProcessing.qismIdList.Contains(x.deptVenue.qismId ?? 0)
                    ).Include(x => x.deptVenue).ToListAsync();

                attendence = attendence.Where(x => payrollProcessing.qismIdList.Any(y => y == x.deptVenue.qismId)).ToList();

                debugMsg = "setp - 3";

                List<int?> attendies = attendence.Select(x => x.itsid).Distinct().ToList();

                string itsinstring = string.Join(",", attendies);

                List<khidmat_guzaar> khidmat_Guzaars = _context.khidmat_guzaar.Where(x => x.salaryCalender == "Gregorian" && attendies.Contains(x.itsId))
                    .Include(x => x.employee_salary)
                    .Include(x => x.employee_dept_salary.Where(y => y.deptVenueId != 17 && y.isHijriSalary == false))
                    .ThenInclude(x => x.salaryType)
                    .Include(x => x.mauzeNavigation)
                    .Include(x => x.salary_allocation_gegorian)
                    .ThenInclude(x => x.salary_generation_gegorgian)
                    .ToList();

                debugMsg = "setp - 4";

                List<khidmat_guzaar> regular = khidmat_Guzaars.Where(x =>
                        attendies.Contains(x.itsId) && x.employee_salary != null &&
                        (x.employee_salary.lastSalaryDate == null ||
                        x.employee_salary.lastSalaryDate < payrollProcessing.from)
                    ).ToList();
                //List<khidmat_guzaar> inactive = khidmat_Guzaars.Where(x => attendence.Any(y => y.itsid == x.itsId) && x.activeStatus == false && ).ToList();

                int daysConsidered = (payrollProcessing.to - payrollProcessing.from).Days + 1;

                debugMsg = "setp - 5";

                foreach (khidmat_guzaar kg in regular)
                {
                    int selfDaysConsidered = daysConsidered;
                    debugMsg = "generate salary for " + kg.itsId;

                    salary_allocation_gegorian sag = new salary_allocation_gegorian()
                    {
                        itsId = kg.itsId,
                        createdBy = authUser.Name,
                        createdOn = DateTime.Now,
                        salary_generation_gegorgian = new List<salary_generation_gegorgian>(),
                        timeDelta = 0,
                        fmbAllowance = kg.employee_salary?.fmbAllowance ?? 0,
                        mumbaiAllowance = kg.employee_salary?.mumbaiAllowance ?? 0,
                        rentAllowance = kg.employee_salary?.rentAllowance ?? 0,
                        marriageAllowance = kg.employee_salary?.marriageAllowance ?? 0,
                        qardanHasanahNonRefundable = kg.employee_salary?.qardanHasanahNonRefundable ?? 0,
                        qardanHasanahRefundable = kg.employee_salary?.qardanHasanahRefundable ?? 0,
                        sabeel = kg.employee_salary?.sabeel ?? 0,
                        marafiqKhairiyah = kg.employee_salary?.marafiqKhairiyah ?? 0,
                        convenienceAllowance = kg.employee_salary?.conveyanceAllowance ?? 0,
                        currency = kg.employee_salary?.currency ?? "",
                        systemRemarks = "",
                        salaryFrom = payrollProcessing.from,
                        salaryTo = payrollProcessing.to,
                        month = payrollProcessing.to.Month,
                        year = payrollProcessing.to.Year,
                        netEarnings = 0,
                        professionTax = 0,
                        ctc = 0,
                        salary = 0,
                        overtime = 0,
                        arrears = kg.employee_salary?.arrears,
                        shortfall = 0,
                        withHoldings = kg.employee_salary?.withHoldings,
                        lessDeduction = 0,
                        bqhs = 0,
                        dayDelta = 0,
                        extraAllowance = 0,
                        installmentDeduction_Qardan = 0,
                        mohammedi_qardanHasanah = 0,
                        fmbDeduction = 0,
                        husaini_qardanHasanah = 0,
                        qardanHasanah = 0,
                        tds = 0
                    };

                    //int selfFixedWorkingMin = kg.employee_dept_salary.Where(x => x.salaryTypeId == 1).Sum(y => y.workingMin ?? 0);
                    List<azwaaj_minentry> kgattendence = attendence.Where(x => x.itsid == kg.itsId).ToList();
                    List<azwaaj_minentry> kgDeptSalaryGrp = kgattendence.GroupBy(x => new { x.deptVenueId, x.policyId }).Select(y => y.First()).ToList();
                    int devisor = kgattendence.GroupBy(x => x.date).Select(x => x.FirstOrDefault()).Count();
                    int selfFixedWorkingMin = 0;
                    selfFixedWorkingMin = kgattendence.Where(x => x.policyId == 1).Sum(y => y.allotedMin ?? 0);
                    float perMinRate;
                    float perMinRate2;
                    float averageSal = 0;
                    float averagedailyMin = 0;
                    if (devisor != 0)
                    {
                        averagedailyMin = kgattendence.Where(x => x.policyId == 1).Sum(y => y.min ?? 0) / devisor;
                        averageSal = kgattendence.Where(x => x.policyId == 1).Sum(y => y.rate ?? 0) / devisor;
                    }

                    if (selfFixedWorkingMin == 0)
                    {
                        perMinRate = 0;
                        perMinRate2 = 0;
                    }
                    else
                    {
                        perMinRate = (float)kg.employee_salary?.grossSalary / (selfFixedWorkingMin);
                        perMinRate2 = (float)averageSal / (selfFixedWorkingMin);
                    }

                    bool hasArrears = false;

                    debugMsg += "allocating var set";

                    if (kg.employee_salary?.lastSalaryDate == null)
                    {
                        sag.salaryFrom = kg.dojGregorian ?? payrollProcessing.from;
                        sag.salaryTo = payrollProcessing.to;

                        if (kg.dojGregorian > payrollProcessing.from)
                        {
                            selfDaysConsidered = (payrollProcessing.to - (kg.dojGregorian ?? DateTime.Now)).Days + 1;
                        }
                        else if (kg.dojGregorian < payrollProcessing.from)
                        {
                            hasArrears = true;
                            if (kg.workType == "Fixed")
                            {
                                sag.dayDelta = (payrollProcessing.from - (kg.dojGregorian ?? DateTime.Now)).Days + 1;
                                sag.arrears += (int)Math.Round((float)(perMinRate * averagedailyMin * sag.dayDelta));
                                selfDaysConsidered = (payrollProcessing.to - (kg.dojGregorian ?? payrollProcessing.from)).Days + 1;
                                sag.systemRemarks = "| Arrears: " + sag.dayDelta + " days of wazifa is pending ";
                            }
                        }
                    }
                    else if (kg.employee_salary?.lastSalaryDate < payrollProcessing.from.AddDays(-1))
                    {
                        sag.salaryFrom = kg.employee_salary?.lastSalaryDate ?? payrollProcessing.from;
                        sag.salaryTo = payrollProcessing.to;

                        hasArrears = true;

                        if (kg.workType == "Fixed")
                        {
                            sag.dayDelta = (payrollProcessing.from - (kg.employee_salary?.lastSalaryDate ?? DateTime.Now)).Days + 1;
                            sag.arrears += (int)Math.Round((float)(perMinRate * averagedailyMin * sag.dayDelta));
                            selfDaysConsidered = (payrollProcessing.to - (kg.employee_salary?.lastSalaryDate ?? payrollProcessing.from)).Days + 1;
                            sag.systemRemarks = "| Arrears: " + sag.dayDelta + " days of wazifa is pending ";
                        }
                    }

                    int applicableSalary = _salaryService.caculateCTC(kg.employee_salary);

                    debugMsg += " applicable salary set";

                    //foreach (employee_dept_salary eds in kg.employee_dept_salary)
                    foreach (azwaaj_minentry eds in kgDeptSalaryGrp)
                    {

                        debugMsg = "salary generation for " + eds.deptVenueId + " " + eds.itsid;

                        salary_generation_gegorgian sgg = new salary_generation_gegorgian();
                        List<azwaaj_minentry> entries = kgattendence.Where(x => x.deptVenueId == eds.deptVenueId && x.policyId == eds.policyId).ToList();
                        List<azwaaj_minentry> arrearsEntries = new List<azwaaj_minentry>();
                        if (hasArrears && kg.workType != "Fixed")
                        {
                            DateOnly sagFromdate = DateOnly.FromDateTime(sag.salaryFrom);
                            DateOnly sagTodate = DateOnly.FromDateTime(payrollProcessing.from);
                            arrearsEntries = _context.azwaaj_minentry.Where(x => x.itsid == kg.itsId && x.deptVenueId == eds.deptVenueId && x.policyId == eds.policyId && x.date > sagFromdate && x.date <= sagTodate).ToList();
                        }

                        int daysCount = entries.Count();
                        float rate = entries.Sum(x => (x.rate ?? 0)) / daysCount;
                        int allotedMin = entries.Sum(x => (x.allotedMin ?? 0)) / daysCount;

                        sgg.itsId = kg.itsId;
                        sgg.createdBy = authUser.Name;
                        sgg.deptVenueId = eds.deptVenueId ?? 0;
                        sgg.salaryType = eds.policyId ?? 0;
                        sgg.createdOn = DateTime.Now;
                        sgg.quantity = 0;
                        sgg.netSalary = 0;
                        sgg.year = payrollProcessing.to.Year;
                        sgg.month = payrollProcessing.to.Month;

                        var deltaT = 0;

                        debugMsg += " salary generation set";
                        if (eds.rate != 0)
                        {
                            switch (eds.policyId)
                            {

                                case 1:
                                    sgg.quantity = entries.Sum(x => (x.min ?? 0));
                                    var perMinWage = (double)(rate / (selfFixedWorkingMin * daysCount));
                                    deltaT = sgg.quantity - ((allotedMin) * daysCount);
                                    //sgg.netSalary = (int)(rate );
                                    if (deltaT != 0)
                                    {
                                        sgg.netSalary = (int?)Math.Round(perMinRate * sgg.quantity);

                                    }
                                    else if (deltaT == 0)
                                    {
                                        sgg.netSalary = (int?)Math.Round((double)((rate)));
                                    }
                                    // if(daysCount < selfDaysConsidered)
                                    // {
                                    //   float diff = selfDaysConsidered - daysCount;
                                    //   deltaT -= (int)Math.Round(diff* averagedailyMin);
                                    // }
                                    break;
                                case 2:
                                    if (hasArrears)
                                    {
                                        sag.dayDelta += arrearsEntries.Count();
                                        sag.arrears += (int)Math.Round((decimal)(rate * arrearsEntries.Sum(x => (x.min ?? 0))));
                                    }
                                    sgg.quantity = entries.Sum(x => (x.min ?? 0));
                                    sgg.netSalary = (int)Math.Round((decimal)(rate * sgg.quantity));
                                    break;
                                case 3:
                                    if (hasArrears)
                                    {
                                        sag.dayDelta += arrearsEntries.Count();
                                        sag.arrears += (int)Math.Round((decimal)(rate * arrearsEntries.Sum(x => (x.min ?? 0))));
                                    }
                                    sgg.quantity = entries.Sum(x => (x.min ?? 0));
                                    sgg.netSalary = (int)Math.Round((decimal)(rate * sgg.quantity));
                                    break;
                            }
                        }
                        debugMsg += " salary generation completed";

                        sag.timeDelta += deltaT;
                        sag.salary_generation_gegorgian.Add(sgg);
                    }

                    int fixedTimeWorked = (sag.salary_generation_gegorgian.Where(x => x.salaryType == 1).Sum(y => y.quantity));
                    int requiredAmountRedistribution = 0;
                    if (sag.timeDelta > 0)
                    {
                        int thrusholdTime = daysConsidered * 480;
                        if (selfFixedWorkingMin > 480)
                        {
                            thrusholdTime = selfFixedWorkingMin * selfDaysConsidered;
                        }

                        //int overtimeMin = fixedTimeWorked - (selfFixedWorkingMin * selfDaysConsidered);

                        int timeEceedinglimit = fixedTimeWorked - thrusholdTime;
                        if (timeEceedinglimit > 0)
                        {
                            sag.systemRemarks += "| Overtime: " + sag.timeDelta + " minutes extra wazifa allotted " + timeEceedinglimit + "(*1.5)";
                            requiredAmountRedistribution = (int)Math.Round((float)timeEceedinglimit * (perMinRate2 * 0.5));

                            sag.overtime = (int)Math.Round(((float)sag.timeDelta * perMinRate2)) + requiredAmountRedistribution;
                        }
                        else
                        {
                            sag.systemRemarks += "| Overtime: " + sag.timeDelta + " minutes extra wazifa allotted ";
                            sag.overtime = (int)Math.Round(((float)sag.timeDelta * perMinRate2));
                        }
                    }
                    else if (sag.timeDelta < 0)
                    {
                        sag.systemRemarks += "| Shortfall: " + sag.timeDelta + " minutes deducted from wazifa";
                        sag.shortfall += (int)(-1 * (sag.timeDelta * perMinRate2));
                    }

                    debugMsg += " salary allocation overtime/shortfall calculated";

                    switch (kg.workType)
                    {
                        case "Fixed":
                            sag.salary = kg.employee_salary?.grossSalary ?? 0;
                            break;
                        default:
                            sag.salary = sag.salary_generation_gegorgian.Sum(x => x.netSalary ?? 0);
                            break;
                    }

                    if (kg.workType != "Fixed" && hasArrears)
                    {
                        sag.systemRemarks += "| Arrears: " + sag.dayDelta + " days of wazifa is pending";
                    }


                    sag.ctc = (int)(_salaryService.caculateCTC(sag));
                    sag.professionTax = _salaryService.getProfessionalTax(sag.ctc);

                    sag.netEarnings = _salaryService.netSalary(sag);

                    requiredAmountRedistribution = sag.salary - sag.ctc;

                    //issue - redistribute the amount equally
                    if (requiredAmountRedistribution > 0 && sag.salary != 0)
                    {
                        foreach (salary_generation_gegorgian salaryGeneration in sag.salary_generation_gegorgian)
                        {
                            salaryGeneration.netSalary += (requiredAmountRedistribution / sag.salary) * salaryGeneration.netSalary;
                        }
                    }

                    if (requiredAmountRedistribution > 0 && sag.salary == 0)
                    {
                        foreach (var salaryGeneration in sag.salary_generation_gegorgian)
                        {
                            salaryGeneration.netSalary += requiredAmountRedistribution / sag.salary_generation_gegorgian.Count();
                        }
                    }

                    payroll_Salary_Packages.amount += sag.netEarnings;

                    debugMsg += " salary allocation added to package";

                    payroll_Salary_Packages.salary_allocation_gegorian.Add(sag);

                }

                debugMsg = "setp - 6 add and save package";

                await _context.payroll_salary_packages.AddAsync(payroll_Salary_Packages);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (InvalidOperationException ex)
                {

                    var errorDetails = new
                    {
                        Message = "An error occurred while processing your request.",
                        ExceptionMessage = ex.Message, // Be cautious with sharing internal exception details
                        ExceptionType = ex.GetType().ToString(),
                        StackTrace = ex.StackTrace // Consider including this only in development environment
                    };

                    Console.WriteLine(errorDetails);

                    return BadRequest(ex + debugMsg);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var err = "";
                    foreach (var entry in ex.Entries)
                    {
                        var entityType = entry.Entity.GetType().Name;

                        var currentValues = entry.CurrentValues;
                        var originalValues = entry.OriginalValues;
                        var databaseValues = entry.GetDatabaseValues();

                        err += $"Concurrency conflict in entity type {entityType}. Current values: {currentValues}, Original values: {originalValues}, Database values: {databaseValues}";
                    }

                    return BadRequest(err + debugMsg);
                }
                catch (DbUpdateException ex)
                {
                    var err = "";

                    foreach (var entry in ex.Entries)
                    {
                        var entityType = entry.Entity.GetType().Name;

                        var currentValues = entry.CurrentValues;

                        err += $"Database update error for entity type {entityType}. Current values: {currentValues}";
                    }
                    return BadRequest(err + " " + debugMsg + " " + ex);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex + debugMsg);
            }
        }

        [Route("getlastallocationdate/{qismId}")]
        [HttpGet]
        public async Task<ActionResult> getLastAllocationDate([FromRoute] int qismId)
        {
            try
            {
                payroll_salary_packages? lastPayrollHijri = await _context.payroll_salary_packages.Where(x => x.qismId == qismId && x.name.Contains("Hijri") && x.paymentDate != null).OrderByDescending(x => x.toDate).FirstOrDefaultAsync();
                payroll_salary_packages? lastPayrollGregorian = await _context.payroll_salary_packages.Where(x => x.qismId == qismId && x.name.Contains("Gregorian") && x.paymentDate != null).OrderByDescending(x => x.toDate).FirstOrDefaultAsync();

                return Ok(new { hijri = lastPayrollHijri?.toDate, gregorian = lastPayrollGregorian?.toDate });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("v2/generatesalaries/hijri")]
        [HttpPost]
        public async Task<ActionResult> generateSalariesHijriV2(PayrollProcessing payrollProcessing)
        {
            string api = "api/salary/generatesalaries/" + payrollProcessing.isHijri + "/" + payrollProcessing.hMonth + "/" + payrollProcessing.hYear;
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                DateTime curr = DateTime.UtcNow;
                payrollProcessing.qismIdList = _helperService.parseIds(payrollProcessing.qismIds);

                // Fetch Hijri calendar dates
                hijri_calender? fromHij = _context.hijri_calender.Where(x => x.hijri_month == payrollProcessing.hMonth && x.hijri_year == payrollProcessing.hYear).FirstOrDefault();
                hijri_calender toHij = _context.hijri_calender.Where(x => x.hijri_month == payrollProcessing.hMonth && x.hijri_year == payrollProcessing.hYear).OrderBy(x => x.hijri_day).Last();

                int hijriDays = toHij.hijri_day ?? 30; // Get the number of days in the Hijri month (default to 30 if not available)

                DateTime fromEng = new DateTime(fromHij?.english_year ?? 0, fromHij?.english_month ?? 0, fromHij?.english_day ?? 0);
                DateTime toEng = new DateTime(toHij.english_year ?? 0, toHij.english_month ?? 0, toHij.english_day ?? 0);
                int numberOfDay = toHij.hijri_day ?? 29;

                payrollProcessing.from = fromEng;
                payrollProcessing.to = toEng;
                string hijriMonthName = _helperService.getHijriMonthName(toHij.hijri_month ?? 0);

                foreach (int qism in payrollProcessing.qismIdList)
                {
                    // Get department venues
                    var venues = _context.venue.Include(x => x.qism).Where(x => x.qismId > 0 && qism == x.qismId);
                    List<int> venueIds = venues.Select(x => x.Id).ToList();

                    payroll_salary_packages payroll_Salary_Packages = new payroll_salary_packages()
                    {
                        fromDate = fromEng,
                        toDate = toEng,
                        createdOn = curr,
                        qismId = qism,
                        paymentFrom = "Kotak Mahindra Bank",
                        createdBy = authUser.ItsId,
                        amount = 0,
                        description = "",
                        name = venues.First().qism.name + " ( Hijri Wazifa - " + hijriMonthName + "/" + toHij.hijri_year + " )"
                    };

                    // Fetch employee list based on active status and department
                    List<khidmat_guzaar> employees = _context.khidmat_guzaar
                        .Include(x => x.mauzeNavigation)
                        .Include(x => x.employee_salary)
                        .Include(x => x.employee_dept_salary.Where(y => y.isHijriSalary == true && y.hasSalary == true && y.deptVenueId != 17))
                        .ThenInclude(x => x.deptVenue)
                        .Where(x => x.activeStatus == true && x.mauzeNavigation.qismId > 0 && venueIds.Contains(x.mauze ?? 0) && x.employee_salary != null && x.employee_dept_salary.Count > 0)
                        .ToList();

                    foreach (khidmat_guzaar item1 in employees)
                    {
                        try
                        {
                            // Employee salary details
                            employee_salary item = item1.employee_salary;
                            item.its = item1;
                            EmployeeSalaryModel i = new EmployeeSalaryModel();
                            i.salaryDetails = _mapper.Map<EmployeeSalaryDetailsModel>(item);

                            // Fetch department salaries for the employee
                            List<employee_dept_salary> edsAll = item1.employee_dept_salary.ToList();
                            i.deptSalaries = _mapper.Map<List<EmployeeDeptSalaries>>(edsAll);

                            // Filter salaries based on department and type
                            List<EmployeeDeptSalaries> toGenerate = i.deptSalaries
                                .Where(j => j.salaryTypeId != 4) // Skip specific salary type
                                .ToList();

                            if (toGenerate.Count == 0)
                            {
                                continue; // Skip employee if no department salaries need to be generated
                            }

                            // Create salary allocation for Hijri salary
                            salary_allocation_hijri sah = _salaryService.CreateSalaryAllocationHijri(item1, i, fromEng, toEng, numberOfDay, authUser, payrollProcessing);

                            // If arrears or shortfall need to be adjusted, handle here
                            if (item1.employee_salary.lastSalaryDate > fromEng)
                            {
                                sah = _salaryService.HandleShortfallHijri(sah, item1, fromEng, numberOfDay, payrollProcessing);
                            }
                            else if (item1.employee_salary.lastSalaryDate < fromEng.AddDays(-1))
                            {
                                sah = await _salaryService.HandleArrearsHijri(sah, item1, fromEng, payrollProcessing, venues, hijriDays);
                            }

                            // Calculate CTC, professional tax, and net earnings
                            sah.ctc = _salaryService.caculateCTC(sah);
                            sah.professionTax = _salaryService.getProfessionalTax(sah.ctc);
                            sah.netEarnings = _salaryService.netSalary(sah);

                            payroll_Salary_Packages.salary_allocation_hijri.Add(sah);

                            // Generate salary for each department and adjust allowances
                            await _salaryService.GenerateSalariesForDepartments(toGenerate, sah, i.salaryDetails.grossSalary, authUser, sah.ctc);

                        }
                        catch (Exception e)
                        {
                            // Log error and continue for next employee
                            // Log the exception (if required) or proceed to the next employee
                            return BadRequest(e.ToString());
                        }
                    }

                    // Update payroll package with the total amount
                    payroll_Salary_Packages.amount = payroll_Salary_Packages.salary_allocation_hijri.Sum(x => x.netEarnings);
                    _context.payroll_salary_packages.Add(payroll_Salary_Packages);
                    await _context.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("generatesalaries/hijri")]
        [HttpPost]
        public async Task<ActionResult> generateSalariesHijri(PayrollProcessing payrollProcessing)
        {
            string api = "api/salary/generatesalaries/" + payrollProcessing.isHijri + "/" + payrollProcessing.hMonth + "/" + payrollProcessing.hYear;

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {

                DateTime curr = DateTime.UtcNow;
                payrollProcessing.qismIdList = _helperService.parseIds(payrollProcessing.qismIds);

                List<dept_venue> depts = _context.dept_venue.Where(x => x.qismId > 0).ToList();

                hijri_calender? fromHij = _context.hijri_calender.Where(x => x.hijri_month == payrollProcessing.hMonth && x.hijri_year == payrollProcessing.hYear).FirstOrDefault();
                hijri_calender toHij = _context.hijri_calender.Where(x => x.hijri_month == payrollProcessing.hMonth && x.hijri_year == payrollProcessing.hYear).OrderBy(x => x.hijri_day).Last();

                DateTime fromEng = new DateTime(fromHij?.english_year ?? 0, fromHij?.english_month ?? 0, fromHij?.english_day ?? 0);
                DateTime toEng = new DateTime(toHij.english_year ?? 0, toHij.english_month ?? 0, toHij.english_day ?? 0);
                int numberOfDay = toHij.hijri_day ?? 29;

                payrollProcessing.from = fromEng;
                payrollProcessing.to = toEng;

                string hijriMonthName = _helperService.getHijriMonthName(toHij.hijri_month ?? 0);

                foreach (int qism in payrollProcessing.qismIdList)
                {

                    var venues = _context.venue.Include(x => x.qism).Where(x => x.qismId > 0 && qism == x.qismId);
                    List<int> venueIds = venues.Select(x => x.Id).ToList();

                    payroll_salary_packages payroll_Salary_Packages = new payroll_salary_packages()
                    {
                        fromDate = fromEng,
                        toDate = toEng,
                        createdOn = curr,
                        qismId = qism,
                        paymentFrom = "Kotak Mahindra Bank",
                        createdBy = authUser.ItsId,
                        amount = 0,
                        description = "",
                        name = venues.First().qism.name + " ( Hijri Wazifa - " + hijriMonthName + "/" + toHij.hijri_year + " )"
                    };

                    List<khidmat_guzaar> employees = _context.khidmat_guzaar
                        .Include(x => x.mauzeNavigation)
                        .Include(x => x.employee_salary)
                        .Include(x => x.employee_dept_salary.Where(y => y.isHijriSalary == true && y.hasSalary == true && y.deptVenueId != 17))
                        .ThenInclude(x => x.deptVenue)
                        .Where(x => x.activeStatus == true && x.mauzeNavigation.qismId > 0 && venueIds.Contains(x.mauze ?? 0) && x.employee_salary != null && x.employee_dept_salary.Count > 0)
                        .ToList();

                    // foreach (employee_salary item in employeeSalaryDetailsList)
                    foreach (khidmat_guzaar item1 in employees)
                    {

                        try
                        {
                            employee_salary item = item1.employee_salary;
                            item.its = item1;
                            EmployeeSalaryModel i = new EmployeeSalaryModel();
                            i.salaryDetails = _mapper.Map<EmployeeSalaryDetailsModel>(item);

                            List<employee_dept_salary> edsAll = item1.employee_dept_salary.ToList();

                            i.deptSalaries = _mapper.Map<List<EmployeeDeptSalaries>>(edsAll);

                            List<EmployeeDeptSalaries> toGenerate = new List<EmployeeDeptSalaries>();

                            if (i.deptSalaries.Count == 0)
                            {
                                continue;
                            }

                            foreach (EmployeeDeptSalaries j in i.deptSalaries)
                            {
                                if (j.salaryTypeId == 4)
                                {
                                    continue;
                                }
                                if (j.salaryTypeId == 2 || j.salaryTypeId == 3)
                                {

                                    int totalMin = 0;
                                    float value = 0;
                                    dept_venue d = j.dept_venue;
                                    if (j.salaryTypeId == 2)
                                    {
                                        totalMin = numberOfDay * (j.workingMin ?? 0);
                                    }
                                    else
                                    {
                                        totalMin = j.workingMin ?? 0;
                                    }

                                    j.workingMin = totalMin;
                                    j.salaryAmount = totalMin * j.salaryAmount;
                                }
                                else
                                {
                                    var tmin = j.workingMin * 30;
                                    j.workingMin = tmin;
                                }
                                toGenerate.Add(j);
                            }

                            i.salaryDetails.grossSalary = item1.employee_salary.grossSalary;
                            var ctc = i.salaryDetails.grossSalary;
                            var netS = i.salaryDetails.grossSalary;
                            var allowences = 0;

                            salary_allocation_hijri sah = new salary_allocation_hijri();

                            DateTime dt = DateTime.Now;
                            string currency = item1.mauzeNavigation.currency;

                            sah = new salary_allocation_hijri()
                            {
                                itsId = i.salaryDetails.itsId,
                                createdBy = authUser.Name,
                                salary = i.salaryDetails.grossSalary,
                                bqhs = i.salaryDetails.bqhs,
                                convenienceAllowance = i.salaryDetails.conveyanceAllowance ?? 0,
                                createdOn = dt,
                                ctc = 0,
                                currency = currency,
                                extraAllowance = i.salaryDetails.extraAllowance ?? 0,
                                husaini_qardanHasanah = 0,
                                installmentDeduction_Qardan = 0,
                                lessDeduction = 0,
                                marafiqKhairiyah = i.salaryDetails.marafiqKhairiyah ?? 0,
                                marriageAllowance = i.salaryDetails.marriageAllowance ?? 0,
                                mohammedi_qardanHasanah = 0,
                                month = payrollProcessing.hMonth ?? 0,
                                year = payrollProcessing.hYear ?? 0,
                                mumbaiAllowance = 0,
                                netEarnings = 0,
                                professionTax = i.salaryDetails.professionTax ?? 0,
                                qardanHasanah = 0,
                                rentAllowance = i.salaryDetails.rentAllowance ?? 0,
                                sabeel = i.salaryDetails.sabeel ?? 0,
                                fmbAllowance = i.salaryDetails.fmbAllowance ?? 0,
                                fmbDeduction = 0,
                                salaryFrom = fromEng,
                                salaryTo = toEng,
                                arrears = i.salaryDetails.arrears ?? 0,
                                dayDelta = 0,
                                shortfall = 0,
                                overtime = 0,
                                timeDelta = 0,
                                incomeTax = 0,
                                systemRemarks = "",
                                localTax = 0,
                                qardanHasanahNonRefundable = i.salaryDetails.qardanHasanahNonRefundable ?? 0,
                                qardanHasanahRefundable = i.salaryDetails.qardanHasanahRefundable ?? 0,
                                qismRemarks = "",
                                tds = 0,
                                withHoldings = i.salaryDetails.withHoldings ?? 0,
                            };

                            if (item1.employee_salary.lastSalaryDate == null)
                            {
                                item1.employee_salary.lastSalaryDate = item1.dojGregorian ?? DateTime.UtcNow;
                            }
                            if (item1.employee_salary.lastSalaryDate > fromEng)
                            {
                                sah.salaryFrom = item1.employee_salary.lastSalaryDate?.AddDays(1) ?? payrollProcessing.from;
                                int daysPaid = (int)Math.Round(((TimeSpan)(item1.employee_salary.lastSalaryDate - payrollProcessing.from)).TotalDays);

                                sah.shortfall = (sah.salary / numberOfDay) * daysPaid;
                                allowences = allowences - (sah.shortfall ?? 0);
                                sah.dayDelta = 0 - daysPaid;
                                sah.systemRemarks = "Shortfall: Wazifa for " + daysPaid + " days has been allotted previously";
                            }

                            if (item1.employee_salary.lastSalaryDate < fromEng.AddDays(-1))
                            {
                                List<azwaaj_minentry> attendence = await _context.azwaaj_minentry.Where(
                                                                    x => x.itsid == item1.itsId
                                                                    && x.date >= DateOnly.FromDateTime(item1.employee_salary.lastSalaryDate ?? DateTime.UtcNow)
                                                                    && x.date <= DateOnly.FromDateTime(payrollProcessing.from)
                                                                    && venues.Any(y => y.Id == x.deptVenue.venueId)
                                                                ).Include(x => x.deptVenue).ToListAsync();
                                attendence = attendence.GroupBy(x => x.date).Select(x => x.FirstOrDefault()).ToList();

                                sah.salaryFrom = item1.employee_salary.lastSalaryDate?.AddDays(1) ?? payrollProcessing.from;
                                int daysPaid = attendence.Count;
                                sah.arrears += (sah.salary / numberOfDay) * daysPaid;
                                allowences = allowences + (sah.arrears ?? 0);
                                sah.dayDelta = daysPaid;
                                sah.systemRemarks = "Arrears: Wazifa for " + daysPaid + " days is pending since last allocation date";

                            }

                            sah.ctc = _salaryService.caculateCTC(sah);
                            sah.professionTax = _salaryService.getProfessionalTax(sah.ctc);
                            sah.netEarnings = _salaryService.netSalary(sah);
                            allowences = sah.ctc - i.salaryDetails.grossSalary;

                            payroll_Salary_Packages.salary_allocation_hijri.Add(sah);

                            int netAmount = 0;
                            salary_generation_hijri sgh = new salary_generation_hijri();
                            foreach (EmployeeDeptSalaries eds in toGenerate)
                            {
                                if (eds.deptVenueId != 17)
                                {
                                    if (allowences != 0)
                                    {
                                        float percent = ((eds.salaryAmount ?? 0) * 100) / i.salaryDetails.grossSalary;
                                        float partialAllowence = ((percent * allowences) / 100);
                                        eds.salaryAmount = eds.salaryAmount + partialAllowence;
                                    }
                                    netAmount += (int)Math.Round(eds.salaryAmount ?? 0);
                                }
                                sgh = new salary_generation_hijri()
                                {
                                    createdBy = authUser.Name,
                                    itsId = eds.itsId,
                                    salaryType = eds.salaryTypeId,
                                    allocationId = sah.id,
                                    deptVenueId = eds.deptVenueId,
                                    month = payrollProcessing.hMonth ?? 0,
                                    year = payrollProcessing.hYear ?? 0,
                                    createdOn = DateTime.Now,
                                    netSalary = (int)Math.Round(eds.salaryAmount ?? 0),
                                    quantity = eds.workingMin ?? 0,
                                };
                                sah.salary_generation_hijri.Add(sgh);


                            }

                            if (((sah.ctc - netAmount) != 0) && (sah.ctc != 0 && netAmount != 0))
                            {
                                sgh.netSalary += (netAmount - sah.ctc);
                            }


                        }
                        catch (Exception e)
                        {
                            return BadRequest(e.ToString());
                        }

                    }

                    payroll_Salary_Packages.amount = payroll_Salary_Packages.salary_allocation_hijri.Sum(x => x.netEarnings);

                    _context.payroll_salary_packages.Add(payroll_Salary_Packages);
                    _context.SaveChanges();
                }

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("generatesalaries/gregorianadvance")]
        [HttpPost]
        public async Task<ActionResult> generateSalariesGregorianAdvance(PayrollProcessing payrollProcessing)
        {
            string api = "api/salary/generatesalaries/" + payrollProcessing.isHijri + "/" + payrollProcessing.hMonth + "/" + payrollProcessing.hYear;

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {

                DateTime curr = DateTime.UtcNow;
                payrollProcessing.qismIdList = _helperService.parseIds(payrollProcessing.qismIds);

                List<dept_venue> depts = _context.dept_venue.Where(x => x.qismId > 0).ToList();

                DateTime fromEng = new DateTime(payrollProcessing.hYear ?? 0, payrollProcessing.hMonth ?? 0, 1);
                // to last day of the month
                int totalDays = DateTime.DaysInMonth(payrollProcessing.hYear ?? 0, payrollProcessing.hMonth ?? 0);
                DateTime toEng = new DateTime(payrollProcessing.hYear ?? 0, payrollProcessing.hMonth ?? 0, totalDays);
                int numberOfDay = totalDays;

                payrollProcessing.from = fromEng;
                payrollProcessing.to = toEng;

                //string hijriMonthName = _helperService.getHijriMonthName(toHij.hijri_month ?? 0);

                foreach (int qism in payrollProcessing.qismIdList)
                {

                    var venues = _context.venue.Include(x => x.qism).Where(x => x.qismId > 0 && qism == x.qismId);
                    List<int> venueIds = venues.Select(x => x.Id).ToList();

                    payroll_salary_packages payroll_Salary_Packages = new payroll_salary_packages()
                    {
                        fromDate = fromEng,
                        toDate = toEng,
                        createdOn = curr,
                        qismId = qism,
                        paymentFrom = payrollProcessing.paymentFrom,
                        createdBy = authUser.ItsId,
                        amount = 0,
                        description = "",
                        name = venues.First().qism.name + " ( Gregorian Wazifa - " + toEng.ToString("MMMM") + "/" + toEng.Year + " )"
                    };

                    List<khidmat_guzaar> employees = _context.khidmat_guzaar
                        .Include(x => x.mauzeNavigation)
                        .Include(x => x.employee_salary)
                        .Include(x => x.employee_dept_salary.Where(y => y.isHijriSalary == false && y.hasSalary == true && y.deptVenueId != 17))
                        .ThenInclude(x => x.deptVenue)
                        .Where(x => x.activeStatus == true && x.mauzeNavigation.qismId > 0 && venueIds.Contains(x.mauze ?? 0) && x.employee_salary != null && x.employee_dept_salary.Count > 0)
                        .ToList();

                    // foreach (employee_salary item in employeeSalaryDetailsList)
                    foreach (khidmat_guzaar item1 in employees)
                    {

                        try
                        {
                            employee_salary item = item1.employee_salary;
                            item.its = item1;
                            EmployeeSalaryModel i = new EmployeeSalaryModel();
                            i.salaryDetails = _mapper.Map<EmployeeSalaryDetailsModel>(item);

                            List<employee_dept_salary> edsAll = item1.employee_dept_salary.ToList();

                            i.deptSalaries = _mapper.Map<List<EmployeeDeptSalaries>>(edsAll);

                            List<EmployeeDeptSalaries> toGenerate = new List<EmployeeDeptSalaries>();

                            if (i.deptSalaries.Count == 0)
                            {
                                continue;
                            }

                            foreach (EmployeeDeptSalaries j in i.deptSalaries)
                            {
                                if (j.salaryTypeId == 4)
                                {
                                    continue;
                                }
                                if (j.salaryTypeId == 2 || j.salaryTypeId == 3)
                                {

                                    int totalMin = 0;
                                    float value = 0;
                                    dept_venue d = j.dept_venue;
                                    if (j.salaryTypeId == 2)
                                    {
                                        totalMin = numberOfDay * (j.workingMin ?? 0);
                                    }
                                    else
                                    {
                                        totalMin = j.workingMin ?? 0;
                                    }

                                    j.workingMin = totalMin;
                                    j.salaryAmount = totalMin * j.salaryAmount;
                                }
                                else
                                {
                                    var tmin = j.workingMin * 30;
                                    j.workingMin = tmin;
                                }
                                toGenerate.Add(j);
                            }

                            i.salaryDetails.grossSalary = _salaryService.applicableSalary(toGenerate);
                            var ctc = i.salaryDetails.grossSalary;
                            var netS = i.salaryDetails.grossSalary;
                            var allowences = 0;

                            salary_allocation_gegorian sag = new salary_allocation_gegorian();

                            DateTime dt = DateTime.Now;
                            string currency = item1.mauzeNavigation.currency;

                            sag = new salary_allocation_gegorian()
                            {
                                itsId = i.salaryDetails.itsId,
                                createdBy = authUser.Name,
                                salary = i.salaryDetails.grossSalary,
                                bqhs = i.salaryDetails.bqhs,
                                convenienceAllowance = i.salaryDetails.conveyanceAllowance ?? 0,
                                createdOn = dt,
                                ctc = 0,
                                currency = currency,
                                extraAllowance = i.salaryDetails.extraAllowance ?? 0,
                                husaini_qardanHasanah = 0,
                                installmentDeduction_Qardan = 0,
                                lessDeduction = 0,
                                marafiqKhairiyah = i.salaryDetails.marafiqKhairiyah ?? 0,
                                marriageAllowance = i.salaryDetails.marriageAllowance ?? 0,
                                mohammedi_qardanHasanah = 0,
                                month = payrollProcessing.hMonth ?? 0,
                                year = payrollProcessing.hYear ?? 0,
                                mumbaiAllowance = 0,
                                netEarnings = 0,
                                professionTax = i.salaryDetails.professionTax ?? 0,
                                qardanHasanah = 0,
                                rentAllowance = i.salaryDetails.rentAllowance ?? 0,
                                sabeel = i.salaryDetails.sabeel ?? 0,
                                fmbAllowance = i.salaryDetails.fmbAllowance ?? 0,
                                fmbDeduction = 0,
                                salaryFrom = fromEng,
                                salaryTo = toEng,
                                arrears = i.salaryDetails.arrears ?? 0,
                                dayDelta = 0,
                                shortfall = 0,
                                overtime = 0,
                                timeDelta = 0,
                                incomeTax = 0,
                                systemRemarks = "",
                                localTax = 0,
                                qardanHasanahNonRefundable = i.salaryDetails.qardanHasanahNonRefundable ?? 0,
                                qardanHasanahRefundable = i.salaryDetails.qardanHasanahRefundable ?? 0,
                                qismRemarks = "",
                                tds = 0,
                                withHoldings = i.salaryDetails.withHoldings ?? 0,
                            };

                            if (item1.employee_salary.lastSalaryDate == null)
                            {
                                item1.employee_salary.lastSalaryDate = item1.dojGregorian ?? DateTime.UtcNow;
                            }
                            if (item1.employee_salary.lastSalaryDate > fromEng)
                            {
                                sag.salaryFrom = item1.employee_salary.lastSalaryDate?.AddDays(1) ?? payrollProcessing.from;
                                int daysPaid = (int)Math.Round(((TimeSpan)(item1.employee_salary.lastSalaryDate - payrollProcessing.from)).TotalDays);

                                sag.shortfall = (sag.salary / numberOfDay) * daysPaid;
                                allowences = allowences - (sag.shortfall ?? 0);
                                sag.dayDelta = 0 - daysPaid;
                                sag.systemRemarks = "Shortfall: Wazifa for " + daysPaid + " days has been allotted previously";
                            }

                            if (item1.employee_salary.lastSalaryDate < fromEng.AddDays(-1))
                            {
                                List<azwaaj_minentry> attendence = await _context.azwaaj_minentry.Where(
                                                                    x => x.itsid == item1.itsId
                                                                    && x.date >= DateOnly.FromDateTime(item1.employee_salary.lastSalaryDate ?? DateTime.UtcNow)
                                                                    && x.date <= DateOnly.FromDateTime(payrollProcessing.from)
                                                                    && venues.Any(y => y.Id == x.deptVenue.venueId)
                                                                ).Include(x => x.deptVenue).ToListAsync();
                                attendence = attendence.GroupBy(x => x.date).Select(x => x.FirstOrDefault()).ToList();

                                sag.salaryFrom = item1.employee_salary.lastSalaryDate?.AddDays(1) ?? payrollProcessing.from;
                                int daysPaid = attendence.Count;
                                sag.arrears += (sag.salary / numberOfDay) * daysPaid;
                                allowences = allowences + (sag.arrears ?? 0);
                                sag.dayDelta = daysPaid;
                                sag.systemRemarks = "Arrears: Wazifa for " + daysPaid + " days is pending since last allocation date";

                            }

                            sag.ctc = _salaryService.caculateCTC(sag);
                            sag.professionTax = _salaryService.getProfessionalTax(sag.ctc);
                            sag.netEarnings = _salaryService.netSalary(sag);
                            allowences = sag.ctc - i.salaryDetails.grossSalary;

                            payroll_Salary_Packages.salary_allocation_gegorian.Add(sag);

                            int netAmount = 0;
                            salary_generation_gegorgian sgg = new salary_generation_gegorgian();
                            foreach (EmployeeDeptSalaries eds in toGenerate)
                            {
                                if (eds.deptVenueId != 17)
                                {
                                    if (allowences != 0)
                                    {
                                        float percent = ((eds.salaryAmount ?? 0) * 100) / i.salaryDetails.grossSalary;
                                        float partialAllowence = ((percent * allowences) / 100);
                                        eds.salaryAmount = eds.salaryAmount + partialAllowence;
                                    }
                                    netAmount += (int)Math.Round(eds.salaryAmount ?? 0);
                                }
                                sgg = new salary_generation_gegorgian()
                                {
                                    createdBy = authUser.Name,
                                    itsId = eds.itsId,
                                    salaryType = eds.salaryTypeId,
                                    allocationId = sag.id,
                                    deptVenueId = eds.deptVenueId,
                                    month = payrollProcessing.hMonth ?? 0,
                                    year = payrollProcessing.hYear ?? 0,
                                    createdOn = DateTime.Now,
                                    netSalary = (int)Math.Round(eds.salaryAmount ?? 0),
                                    quantity = eds.workingMin ?? 0,
                                };
                                sag.salary_generation_gegorgian.Add(sgg);


                            }

                            if (((sag.ctc - netAmount) != 0) && (sag.ctc != 0 && netAmount != 0))
                            {
                                sgg.netSalary += (netAmount - sag.ctc);
                            }


                        }
                        catch (Exception e)
                        {
                            return BadRequest(e.ToString());
                        }

                    }

                    payroll_Salary_Packages.amount = payroll_Salary_Packages.salary_allocation_gegorian.Sum(x => x.netEarnings);

                    _context.payroll_salary_packages.Add(payroll_Salary_Packages);
                    _context.SaveChanges();
                }

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("getsalaryallocationreport")]
        [HttpGet]
        public async Task<ActionResult> getDPTSalaryReport([FromQuery] string packageIds)
        {

            string api = "getDPTsalaryreport/{isHijri}/{hMonth}/{hYear}";

            List<SalaryExportModel> reportModel = new List<SalaryExportModel>();
            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

                List<int> ints = _helperService.parseIds(packageIds);

                foreach (int packageId in ints)
                {

                    payroll_salary_packages payroll_Salary_Packages = _context.payroll_salary_packages.Where(x => x.id == packageId)
                        .Include(x => x.salary_allocation_gegorian).ThenInclude(x => x.salary_generation_gegorgian).ThenInclude(x => x.salaryTypeNavigation)
                        .Include(x => x.salary_allocation_gegorian).ThenInclude(x => x.salary_generation_gegorgian).ThenInclude(x => x.deptVenue)
                        .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.salary_generation_hijri).ThenInclude(x => x.salaryTypeNavigation)
                        .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.salary_generation_hijri).ThenInclude(x => x.deptVenue)
                        .FirstOrDefault();

                    if (payroll_Salary_Packages == null)
                    {
                        continue;
                    }

                    List<salary_allocation_hijri> hijriSalaryAllocation = payroll_Salary_Packages.salary_allocation_hijri.ToList();
                    List<salary_allocation_gegorian> gregorianSalaryAllocation = payroll_Salary_Packages.salary_allocation_gegorian.ToList();

                    bool isHijri = hijriSalaryAllocation.Count > 0;

                    if (isHijri)
                    {
                        salary_allocation_hijri firstAllocation = payroll_Salary_Packages.salary_allocation_hijri.FirstOrDefault();
                        List<int> itsIds = hijriSalaryAllocation.Select(x => x.itsId).ToList();

                        List<khidmat_guzaar> kg = _context.khidmat_guzaar.Where(x => itsIds.Contains(x.itsId))
                            .Include(its => its.employee_academic_details)
                            .Include(its => its.mauzeNavigation)
                            .Include(its => its.employee_bank_details.OrderBy(x => x.isDefault))
                            .Include(its => its.employee_salary).ToList();

                        List<EmployeeModel> li = new List<EmployeeModel>();
                        kg.ForEach(x => li.Add(Translator.khtoModel(x)));

                        List<EmployeeModel> l = li.OrderBy(x => x.basicDetails.employeeType).ThenBy(x => x.basicDetails.mz_idara).ThenBy(x => x.basicDetails.fullName).ToList();

                        if (l.Any(x => x.bankDetails == null || x.bankDetails.Count == 0))
                        {
                            return BadRequest("Missing Bank details of an employee");
                        }

                        hijri_months months = _context.hijri_months.Where(x => x.id == firstAllocation.month).FirstOrDefault();
                        foreach (var w in l)
                        {

                            salary_allocation_hijri i = hijriSalaryAllocation.Where(x => x.itsId == w.basicDetails.itsId).FirstOrDefault();
                            //if (i.netEarnings != 0)
                            //{
                            var r = new SalaryExportModel
                            {
                                employeeType = w.basicDetails.employeeType,
                                convenienceAllowance = i.convenienceAllowance ?? 0,
                                ctc = i.ctc,
                                currency = i.currency,
                                qardanHasanahRefundable = (i.qardanHasanahRefundable ?? 0) * -1,
                                qardanNonHasanahRefundable = (i.qardanHasanahNonRefundable ?? 0) * -1,
                                shortfall = (i.shortfall ?? 0) * -1,
                                withholdings = (i.withHoldings ?? 0) * -1,
                                marafiqKhairiyah = (i.marafiqKhairiyah ?? 0) * -1,
                                marriageAllowance = i.marriageAllowance ?? 0,
                                netEarnings = i.netEarnings,
                                paymentDate = i.paymentDate,
                                professionTax = (i.professionTax ?? 0) * -1,
                                rentAllowance = i.rentAllowance ?? 0,
                                sabeel = (i.sabeel ?? 0) * -1,
                                tds = (i.tds ?? 0) * -1,
                                Month_Year = months.hijriMonthName + " - " + firstAllocation.year,
                                itsId = w.basicDetails.itsId,
                                name = w.basicDetails.fullName,
                                account_Number = "\t" + w.bankDetails[0]?.bankAccountNumber,
                                bank_AccountName = w.bankDetails[0]?.bankAccountName,
                                salary = i.salary,
                                fmbAllowance = i.fmbAllowance ?? 0,
                                arrears = i.arrears ?? 0,
                                localTax = (i.localTax ?? 0) * -1,
                                overtime = i.overtime ?? 0,
                                pan = w.bankDetails[0].panCard,
                                farigDarajah = w.academicDetails.farigDarajah ?? 0,
                                category = w.academicDetails.category,
                                batchId = w.academicDetails.batchId,
                                age = w.basicDetails.age ?? 0,
                                designation = w.basicDetails.designation,
                                mzIdara = w.basicDetails.mz_idara,
                                workType = w.basicDetails.workType,
                                packageId = packageId
                            };
                            reportModel.Add(r);
                            //}

                        }
                    }
                    else
                    {
                        salary_allocation_gegorian firstAllocation = payroll_Salary_Packages.salary_allocation_gegorian.FirstOrDefault();

                        List<int> itsIds = gregorianSalaryAllocation.Select(x => x.itsId).ToList();

                        List<khidmat_guzaar> kg = _context.khidmat_guzaar.Where(x => itsIds.Contains(x.itsId))
                            .Include(its => its.employee_academic_details)
                            .Include(its => its.mauzeNavigation)
                            .Include(its => its.employee_bank_details)
                            .ToList();

                        List<EmployeeModel> li = new List<EmployeeModel>();
                        kg.ForEach(x => li.Add(Translator.khtoModel(x)));

                        List<EmployeeModel> l = li.OrderBy(x => x.basicDetails.employeeType).ThenBy(x => x.basicDetails.mz_idara).ThenBy(x => x.basicDetails.fullName).ToList();

                        string fullMonthName = new DateTime(2015, firstAllocation.month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("en-US"));

                        if (l.Any(x => x.bankDetails == null || x.bankDetails.Count == 0))
                        {
                            return BadRequest("Missing Bank details of an employee");
                        }

                        foreach (var w in l)
                        {

                            salary_allocation_gegorian i = gregorianSalaryAllocation.Where(x => x.itsId == w.basicDetails.itsId).FirstOrDefault();
                            //if (i.netEarnings != 0)
                            //{
                            var r = new SalaryExportModel
                            {
                                employeeType = w.basicDetails.employeeType,
                                convenienceAllowance = i.convenienceAllowance ?? 0,
                                ctc = i.ctc,
                                currency = i.currency,
                                qardanHasanahRefundable = (i.qardanHasanahRefundable ?? 0) * -1,
                                qardanNonHasanahRefundable = (i.qardanHasanahNonRefundable ?? 0) * -1,
                                shortfall = (i.shortfall ?? 0) * -1,
                                withholdings = (i.withHoldings ?? 0) * -1,
                                marafiqKhairiyah = (i.marafiqKhairiyah ?? 0) * -1,
                                marriageAllowance = i.marriageAllowance ?? 0,
                                netEarnings = i.netEarnings,
                                paymentDate = i.paymentDate,
                                professionTax = (i.professionTax ?? 0) * -1,
                                rentAllowance = i.rentAllowance ?? 0,
                                sabeel = (i.sabeel ?? 0) * -1,
                                tds = (i.tds ?? 0) * -1,
                                Month_Year = fullMonthName + " - " + firstAllocation.year,
                                itsId = w.basicDetails.itsId,
                                name = w.basicDetails.fullName,
                                account_Number = "\t" + w.bankDetails[0]?.bankAccountNumber,
                                bank_AccountName = w.bankDetails[0]?.bankAccountName,
                                salary = i.salary,
                                fmbAllowance = i.fmbAllowance ?? 0,
                                arrears = i.arrears ?? 0,
                                localTax = (i.localTax ?? 0) * -1,
                                overtime = i.overtime ?? 0,
                                pan = w.bankDetails[0]?.panCard ?? "",
                                farigDarajah = w.academicDetails?.farigDarajah ?? 0,
                                category = w.academicDetails?.category ?? "",
                                batchId = w.academicDetails?.batchId ?? 0,
                                age = w.basicDetails.age ?? 0,
                                designation = w.basicDetails.designation,
                                mzIdara = w.basicDetails.mz_idara,
                                workType = w.basicDetails.workType,
                                packageId = packageId
                            };
                            reportModel.Add(r);
                            //}

                        }
                    }
                }

                reportModel.Sort((x, y) =>
                {

                    // Sort by mz_idara based on idaraSortingOrder
                    int indexX = _globalConstants.idaraSortingOrder.IndexOf(x.mzIdara);
                    int indexY = _globalConstants.idaraSortingOrder.IndexOf(y.mzIdara);
                    if (indexX == -1) indexX = int.MaxValue;
                    if (indexY == -1) indexY = int.MaxValue;
                    if (indexX != indexY) return indexX.CompareTo(indexY);

                    // Sort by batchId in descending order
                    int batchIdComparison = (y.batchId ?? 0).CompareTo(x.batchId ?? 0);
                    if (batchIdComparison != 0) return batchIdComparison;

                    // Sort by category in ascending order
                    int categoryComparison = string.Compare(x.category, y.category, StringComparison.Ordinal);
                    if (categoryComparison != 0) return categoryComparison;

                    // Sort by farigDarajah in descending order
                    int farigDarajahComparison = (y.farigDarajah ?? 0).CompareTo(x.farigDarajah ?? 0);
                    if (farigDarajahComparison != 0) return farigDarajahComparison;

                    // Sort by age in descending order
                    return (y.age).CompareTo(x.age);
                });

                int c = 1;
                foreach (var w in reportModel)
                {
                    w.srNo = c;
                    c = c + 1;
                }

                return Ok(reportModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("getbankreport/{pkgId}/{paymentFrom}")]
        [HttpGet]
        public async Task<ActionResult> getKotakbankreport(int pkgId, string paymentFrom)
        {
            string api = "api/salary/getazwaajqtmBANKsalaryreport/{pkgId}";

            List<KotakBankExportModel> kotakReportModel = new List<KotakBankExportModel>();
            List<OtherBankBasicExportModel> barodaReportModel = new List<OtherBankBasicExportModel>();

            //string pattern = "[^a-zA-Z0-9]";
            //string replacement = "";
            //Regex rgx = new Regex(pattern);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

                if (!(paymentFrom.Contains("Kotak") || paymentFrom.Contains("Baroda")))
                {
                    return BadRequest(new { message = "please recheck payment from bank" });
                }

                List<SalaryAllocation> sa = new List<SalaryAllocation>();

                payroll_salary_packages? p = _context.payroll_salary_packages.Where(x => x.id == pkgId)
                .Include(x => x.salary_allocation_gegorian).ThenInclude(x => x.salary_generation_gegorgian).ThenInclude(x => x.its)
                .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.salary_generation_hijri).ThenInclude(x => x.its)
                .FirstOrDefault();

                if (p == null)
                {
                    return BadRequest(new { message = "Package not found" });
                }
                p?.salary_allocation_gegorian.ToList().ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));
                p?.salary_allocation_hijri.ToList().ForEach(x => sa.Add(_mapper.Map<SalaryAllocation>(x)));

                List<int> itsids = sa.Select(x => x.itsId).ToList();
                List<EmployeeModel> employeeList = _context.khidmat_guzaar.Where(x => itsids.Contains(x.itsId))
                  .Include(x => x.employee_bank_details)
                  .Include(x => x.employee_salary)
                  .AsEnumerable()
                  .Select(x => Translator.khtoModel(x)).ToList();

                List<EmployeeModel> l = employeeList.OrderBy(x => x.basicDetails.employeeType).ThenBy(x => x.basicDetails.mz_idara).ThenBy(x => x.basicDetails.fullName).ToList();
                int c = 1;
                foreach (var w in l)
                {

                    var i = sa.Where(x => x.itsId == w.basicDetails.itsId).FirstOrDefault();

                    if (i.netEarnings != 0)
                    {
                        string paymentMode = "";

                        if (i.netEarnings > 200000)
                        {
                            paymentMode = "RTGS";
                        }
                        else if (w.bankDetails[0]?.bankName != null)
                        {
                            if (w.bankDetails[0].bankName.ToLower().Contains("kotak"))
                            {
                                paymentMode = "IFT";
                            }
                            else
                            {
                                paymentMode = "NEFT";
                            }

                        }
                        else
                        {
                            paymentMode = "";
                        }

                        Regex reg = new Regex("[^a-zA-Z0-9\\s]");

                        var k = new KotakBankExportModel
                        {
                            Amount = i.netEarnings,
                            Bank_Code_Indicator = "M",
                            Beneficiary_Acc_No = w.bankDetails[0].bankAccountNumber?.ToUpper(),
                            Beneficiary_Email = w.basicDetails.emailAddress?.ToUpper(),
                            Beneficiary_Mobile = (w.basicDetails.mobileNo?.Length > 3) ? w.basicDetails.mobileNo?.Substring(3) : w.basicDetails.mobileNo,
                            Beneficiary_Name = reg.Replace(w.bankDetails[0].bankAccountName?.ToUpper() ?? "", string.Empty),
                            Beneficiary_Bank = reg.Replace(w.bankDetails[0].bankName?.ToUpper() ?? "", string.Empty),
                            Client_Code = "DAWATMAZ",
                            Credit_Narration = "FROM MAHAD AL ZAHRA SURAT",
                            Debit_Narration = w.basicDetails.fullName?.ToUpper(),
                            Dr_Ac_No = "6112824176",
                            Beneficiary_Branch_IFSC_Code = w.bankDetails[0].ifsc?.ToUpper(),
                            Payment_Type = paymentMode,
                            Product_Code = "SALPAY",
                            Enrichment_1 = "",
                            Enrichment_2 = "",
                            Payment_Date = "",
                        };
                        kotakReportModel.Add(k);

                        var b = new OtherBankBasicExportModel
                        {
                            accountNumber = w.bankDetails[0].bankAccountNumber,
                            bankAccountName = w.bankDetails[0].bankAccountName,
                            ifscCode = w.bankDetails[0].ifsc,
                            name = w.basicDetails.fullName,
                            nameOfTheBank = w.bankDetails[0].bankName,
                            netSalary = i.netEarnings,
                            srNo = c,
                        };
                        barodaReportModel.Add(b);
                        c = c + 1;
                    }

                }

                if (paymentFrom.Contains("Kotak"))
                {
                    return Ok(kotakReportModel);
                }

                return Ok(barodaReportModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("gettahfeezsalaryreport/{hMonth}/{hYear}")]
        [HttpGet]
        public async Task<ActionResult> getTahfeezDPTSalaryReport(int hMonth, int hYear)
        {
            string api = "api/salary/getazwaajtahfeezDPTsalaryreport/{hMonth}/{hYear}";
            List<JameaTahfeezReportModel> reportModel = new List<JameaTahfeezReportModel>();
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

                List<salary_generation_hijri> hijriSalaryAllocation = new List<salary_generation_hijri>();

                hijriSalaryAllocation = _context.salary_generation_hijri.Where(x => x.month == hMonth && x.year == hYear && x.deptVenueId == 17).ToList();

                List<EmployeeModel> li = (from hsa in hijriSalaryAllocation
                                          join kh in _context.khidmat_guzaar on hsa.itsId equals kh.itsId
                                          select Translator.khtoModel(kh)).ToList();

                List<EmployeeModel> l = li.OrderBy(x => x.basicDetails.fullName).ToList();
                int c = 1;
                hijri_months? months = _context.hijri_months.Where(x => x.id == hMonth).FirstOrDefault();
                foreach (var w in l)
                {

                    salary_generation_hijri? i = hijriSalaryAllocation.Where(x => x.itsId == w.basicDetails.itsId).FirstOrDefault();
                    if (i?.netSalary != 0)
                    {
                        var r = new JameaTahfeezReportModel
                        {
                            srNo = c,
                            itsId = w.basicDetails.itsId,
                            name = w.basicDetails.fullName,
                            //pan = w.basicDetails.panCardNo,
                            account_Number = "\t" + w.bankDetails[0].bankAccountNumber,
                            bank_AccountName = w.bankDetails[0].bankAccountName,
                            salary = i.netSalary ?? 0,
                            periods = (i.quantity / 35),
                            hijri_Month_Year = months.hijriMonthName + " " + hYear,
                        };
                        reportModel.Add(r);
                        c = c + 1;
                    }

                }



                return Ok(reportModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("deletesalarylog/{id}")]
        [HttpGet]
        public async Task<ActionResult> deleteSalaryLog(int id)
        {
            string api = "deletesalarylog/{id}";

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            //ServiceFactory.getHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                var delete = _context.salary_querylogs.Where(x => x.id == id).FirstOrDefault();

                bool deleted = false;
                if (delete.type == "Hijri")
                {

                    List<salary_generation_hijri> toDelete = _context.salary_generation_hijri.Where(x => x.month == delete.hijriMonth && x.year == delete.hijriYear).ToList();
                    List<salary_allocation_hijri> toDelete1 = _context.salary_allocation_hijri.Where(x => x.month == delete.hijriMonth && x.year == delete.hijriYear).ToList();
                    if (toDelete.Count > 0)
                    {

                        var createdFirst = toDelete1.FirstOrDefault().createdOn;
                        var createdLast = toDelete[toDelete.Count() - 1].createdOn;
                        List<azwaaj_minentry> toDelete2 = _context.azwaaj_minentry.Where(x => (x.createdOn >= createdFirst && x.createdOn <= createdLast) && x.createdBy.Contains("Grace")).ToList();

                        foreach (var i in toDelete)
                        {
                            _context.salary_generation_hijri.Remove(i);
                        }

                        foreach (var i in toDelete2)
                        {
                            _context.azwaaj_minentry.Remove(i);
                        }

                    }

                    foreach (var i in toDelete1)
                    {
                        _context.salary_allocation_hijri.Remove(i);
                    }
                    _context.SaveChanges();
                    deleted = true;
                }
                else if (delete.type == "Gregorian")
                {
                    List<salary_generation_gegorgian> toDelete = _context.salary_generation_gegorgian.Where(x => x.month == delete.hijriMonth && x.year == delete.hijriYear).ToList();
                    List<salary_allocation_gegorian> toDelete1 = _context.salary_allocation_gegorian.Where(x => x.month == delete.hijriMonth && x.year == delete.hijriYear).ToList();
                    if (toDelete.Count > 0)
                    {

                        var createdFirst = toDelete.FirstOrDefault().createdOn;
                        var createdLast = toDelete[toDelete.Count() - 1].createdOn;
                        List<azwaaj_minentry> toDelete2 = _context.azwaaj_minentry.Where(x => (x.createdOn >= createdFirst && x.createdOn <= createdLast) && x.createdBy.Contains("Grace")).ToList();

                        foreach (var i in toDelete)
                        {
                            _context.salary_generation_gegorgian.Remove(i);
                        }

                        foreach (var i in toDelete2)
                        {
                            _context.azwaaj_minentry.Remove(i);
                        }
                    }

                    foreach (var i in toDelete1)
                    {
                        _context.salary_allocation_gegorian.Remove(i);
                    }
                    _context.SaveChanges();
                    deleted = true;

                }
                if (deleted)
                {
                    _context.salary_querylogs.Remove(delete);
                    _context.SaveChanges();
                    return Ok("Log is succesfully deleted");
                }

                return BadRequest(new { message = "Salaries were not deleted plz contact development team" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Exception" });
            }
        }

        [Route("deptvssalaryreport")]
        [HttpGet]
        public async Task<ActionResult> deptVsSalaryReport([FromQuery] string packageIds)
        {
            string api = "api/salary/deptvssalaryreport";

            List<dynamic> reportModel = new List<dynamic>();

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<int> ints = _helperService.parseIds(packageIds);

                List<payroll_salary_packages> payroll_Salary_Packages = _context.payroll_salary_packages.Where(x => ints.Contains(x.id))
                    .Include(x => x.salary_allocation_gegorian).ThenInclude(x => x.salary_generation_gegorgian).ThenInclude(x => x.deptVenue)
                    .Include(x => x.salary_allocation_hijri).ThenInclude(x => x.salary_generation_hijri).ThenInclude(x => x.deptVenue)
                    .ToList();

                List<salary_generation_hijri> report = new List<salary_generation_hijri>();
                List<salary_generation_gegorgian> reportg = new List<salary_generation_gegorgian>();

                report = payroll_Salary_Packages.SelectMany(x => x.salary_allocation_hijri.SelectMany(y => y.salary_generation_hijri)).ToList();
                reportg = payroll_Salary_Packages.SelectMany(x => x.salary_allocation_gegorian.SelectMany(y => y.salary_generation_gegorgian)).ToList();

                var c = 1;
                bool isHijri = report.Count > 0;

                if (report.Count > 0)
                {

                    List<dept_venue> dvids = report.GroupBy(x => x.deptVenueId).Select(x => x.FirstOrDefault()).ToList().Select(x => x.deptVenue).ToList();

                    var tempSGH = report.GroupBy(x => x.itsId).Select(x => x.FirstOrDefault().itsId).ToList();

                    List<khidmat_guzaar> w = _context.khidmat_guzaar.Where(x => tempSGH.Contains(x.itsId))
                        .Include(its => its.employee_academic_details)
                        .Include(its => its.mauzeNavigation)
                        .Include(its => its.employee_bank_details).ToList();


                    foreach (var i1 in w)
                    {
                        hijri_months months = _context.hijri_months.Where(x => x.id == (report.FirstOrDefault().month)).FirstOrDefault();

                        Dictionary<string, string> r = new Dictionary<string, string>();
                        r.Add("srNo", c.ToString());
                        r.Add("hijri_Month_Year", months.hijriMonthName + " - " + report.FirstOrDefault().year);
                        r.Add("itsId", i1.itsId.ToString());
                        r.Add("Employee Type", i1.employeeType);
                        r.Add("name", i1.fullName);
                        //r.Add("pan", i1.basicDetails.panCardNo);
                        r.Add("account_Number", "\t" + i1.employee_bank_details.FirstOrDefault()?.bankAccountNumber);
                        r.Add("bank_AccountName", i1.employee_bank_details.FirstOrDefault()?.bankAccountName);
                        r.Add("packageId", payroll_Salary_Packages.Where(x => x.salary_allocation_hijri.Any(y => y.itsId == i1.itsId)).FirstOrDefault().id.ToString());

                        foreach (var h in dvids)
                        {
                            int sumOfVal = report.Where(x => x.itsId == i1.itsId && x.deptVenueId == h.id).Sum(x => x.netSalary) ?? 0;
                            r.Add(h.deptName.ToUpper() + "_" + h.venueName.ToUpper(), sumOfVal.ToString());
                        }

                        reportModel.Add(r);
                        c += 1;
                    }
                }

                if (reportg.Count > 0)
                {

                    List<dept_venue> dvids = reportg.GroupBy(x => x.deptVenueId).Select(x => x.FirstOrDefault()).ToList().Select(x => x.deptVenue).ToList();

                    var tempSGG = reportg.GroupBy(x => x.itsId).Select(x => x.FirstOrDefault()).ToList();

                    List<khidmat_guzaar> w = _context.khidmat_guzaar.Where(x => tempSGG.Select(x => x.itsId).Contains(x.itsId))
                        .Include(its => its.employee_academic_details)
                        .Include(its => its.mauzeNavigation)
                        .Include(its => its.employee_bank_details).ToList();

                    foreach (var i1 in w)
                    {
                        DateTime date = new DateTime(reportg.FirstOrDefault().year, reportg.FirstOrDefault().month, 1);

                        Dictionary<string, string> r = new Dictionary<string, string>();
                        r.Add("srNo", c.ToString());
                        r.Add("Month_Year", date.ToString("MMM") + " - " + date.ToString("yyyy"));
                        r.Add("itsId", i1.itsId.ToString());
                        r.Add("Employee Type", i1.employeeType);
                        r.Add("name", i1.fullName);
                        //r.Add("pan", i1.basicDetails.panCardNo);
                        r.Add("account_Number", "\t" + i1.employee_bank_details.FirstOrDefault().bankAccountNumber);
                        r.Add("bank_AccountName", i1.employee_bank_details.FirstOrDefault().bankAccountName);
                        r.Add("packageId", payroll_Salary_Packages.Where(x => x.salary_allocation_gegorian.Any(y => y.itsId == i1.itsId)).FirstOrDefault().id.ToString());

                        foreach (var h in dvids)
                        {
                            int sumOfVal = reportg.Where(x => x.itsId == i1.itsId && x.deptVenueId == h.id).Sum(x => x.netSalary) ?? 0;
                            r.Add(h.deptName.ToUpper() + "_" + h.venueName.ToUpper(), sumOfVal.ToString());
                        }

                        reportModel.Add(r);
                        c += 1;
                    }
                }


                return Ok(reportModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("removePayrollPackage/{packageId}")]
        [HttpGet]
        public async Task<ActionResult> removePayrollPackage(int packageId)
        {
            try
            {
                payroll_salary_packages payroll_Salary_Packages = _context.payroll_salary_packages.Where(x => x.id == packageId).FirstOrDefault();
                if (payroll_Salary_Packages != null)
                {
                    _context.salary_allocation_gegorian.RemoveRange(payroll_Salary_Packages.salary_allocation_gegorian);
                    _context.salary_allocation_hijri.RemoveRange(payroll_Salary_Packages.salary_allocation_hijri);
                    _context.payroll_salary_packages.Remove(payroll_Salary_Packages);
                    _context.SaveChanges();
                    return Ok();
                }
                return BadRequest(new { message = "Package not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Route("refreshPayrollPackage/{packageId}")]
        //[HttpGet]
        //public async Task<ActionResult> refreshPayrollPackage(int packageId)
        //{
        //    try
        //    {
        //        payroll_salary_packages payroll_Salary_Packages = _context.payroll_salary_packages.Where(x => x.id == packageId).FirstOrDefault();
        //        if (payroll_Salary_Packages != null)
        //        {
        //            payroll_Salary_Packages.amount = payroll_Salary_Packages.salary_allocation_gegorian.Sum(x => x.netEarnings);
        //            _context.SaveChanges();
        //            return Ok("Package refreshed successfully");
        //        }
        //        return BadRequest( new { message = "Package not found");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [Route("deleteSalaryAllocation/{isHijri}/{allocationId}")]
        [HttpGet]
        public async Task<ActionResult> deleteSalaryAllocation(bool isHijri, int allocationId)
        {
            try
            {
                if (isHijri)
                {
                    salary_allocation_hijri salary_Allocation_Hijri = _context.salary_allocation_hijri.Where(x => x.id == allocationId).FirstOrDefault();
                    if (salary_Allocation_Hijri != null)
                    {
                        _context.salary_allocation_hijri.Remove(salary_Allocation_Hijri);
                        _context.SaveChanges();
                        return Ok();
                    }
                }
                else
                {
                    salary_allocation_gegorian salary_Allocation_Gegorian = _context.salary_allocation_gegorian.Where(x => x.id == allocationId).FirstOrDefault();
                    if (salary_Allocation_Gegorian != null)
                    {
                        _context.salary_allocation_gegorian.Remove(salary_Allocation_Gegorian);
                        _context.SaveChanges();
                        return Ok();
                    }
                }
                return BadRequest(new { message = "Allocation not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("updateSalariesBulk")]
        [HttpPost]
        public async Task<ActionResult> updateSalariesBulk(List<SalaryAllocation> salaryAllocations)
        {
            // for each salary allocation find exact allocation in salaryallotion gegorian or hijri and update it as well its generation

            try
            {
                foreach (var salaryAllocation in salaryAllocations)
                {
                    if (salaryAllocation.isHijri)
                    {
                        salary_allocation_hijri salary_Allocation_Hijri = _context.salary_allocation_hijri.Where(x => x.id == salaryAllocation.id).Include(x => x.salary_generation_hijri).FirstOrDefault();
                        if (salary_Allocation_Hijri != null)
                        {
                            salary_Allocation_Hijri.salary = salaryAllocation.salary;
                            salary_Allocation_Hijri.rentAllowance = salaryAllocation.rentAllowance;
                            salary_Allocation_Hijri.fmbAllowance = salaryAllocation.fmbAllowance;
                            salary_Allocation_Hijri.marriageAllowance = salaryAllocation.marriageAllowance;
                            salary_Allocation_Hijri.convenienceAllowance = salaryAllocation.convenienceAllowance;
                            salary_Allocation_Hijri.arrears = salaryAllocation.arrears;
                            salary_Allocation_Hijri.sabeel = salaryAllocation.sabeel;
                            salary_Allocation_Hijri.marafiqKhairiyah = salaryAllocation.marafiqKhairiyah;
                            salary_Allocation_Hijri.qardanHasanahRefundable = salaryAllocation.qardanHasanahRefundable;
                            salary_Allocation_Hijri.qardanHasanahNonRefundable = salaryAllocation.qardanHasanahNonRefundable;
                            salary_Allocation_Hijri.withHoldings = salaryAllocation.withHoldings;
                            salary_Allocation_Hijri.overtime = salaryAllocation.overtime;
                            salary_Allocation_Hijri.shortfall = salaryAllocation.shortfall;
                            salary_Allocation_Hijri.professionTax = salaryAllocation.professionTax;
                            salary_Allocation_Hijri.incomeTax = salaryAllocation.incomeTax;
                            salary_Allocation_Hijri.qismRemarks = salaryAllocation.qismRemarks;
                            salary_Allocation_Hijri.systemRemarks = salaryAllocation.systemRemarks;
                            salary_Allocation_Hijri.tds = salaryAllocation.tds;

                            salary_Allocation_Hijri.ctc = _salaryService.caculateCTC(salary_Allocation_Hijri);


                            int requiredAmountRedistribution = salary_Allocation_Hijri.salary - salary_Allocation_Hijri.ctc;

                            foreach (var salaryGeneration in salary_Allocation_Hijri.salary_generation_hijri)
                            {
                                salaryGeneration.netSalary += (requiredAmountRedistribution / salary_Allocation_Hijri.salary) * salaryGeneration.netSalary;
                            }

                            salary_Allocation_Hijri.netEarnings = _salaryService.netSalary(salary_Allocation_Hijri);


                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        salary_allocation_gegorian salary_Allocation_Hijri = _context.salary_allocation_gegorian.Where(x => x.id == salaryAllocation.id).Include(x => x.salary_generation_gegorgian).FirstOrDefault();
                        if (salary_Allocation_Hijri != null)
                        {
                            salary_Allocation_Hijri.salary = salaryAllocation.salary;
                            salary_Allocation_Hijri.rentAllowance = salaryAllocation.rentAllowance;
                            salary_Allocation_Hijri.fmbAllowance = salaryAllocation.fmbAllowance;
                            salary_Allocation_Hijri.marriageAllowance = salaryAllocation.marriageAllowance;
                            salary_Allocation_Hijri.convenienceAllowance = salaryAllocation.convenienceAllowance;
                            salary_Allocation_Hijri.arrears = salaryAllocation.arrears;
                            salary_Allocation_Hijri.sabeel = salaryAllocation.sabeel;
                            salary_Allocation_Hijri.marafiqKhairiyah = salaryAllocation.marafiqKhairiyah;
                            salary_Allocation_Hijri.qardanHasanahRefundable = salaryAllocation.qardanHasanahRefundable;
                            salary_Allocation_Hijri.qardanHasanahNonRefundable = salaryAllocation.qardanHasanahNonRefundable;
                            salary_Allocation_Hijri.withHoldings = salaryAllocation.withHoldings;
                            salary_Allocation_Hijri.shortfall = salaryAllocation.shortfall;
                            salary_Allocation_Hijri.overtime = salaryAllocation.overtime;
                            salary_Allocation_Hijri.professionTax = salaryAllocation.professionTax;
                            salary_Allocation_Hijri.incomeTax = salaryAllocation.incomeTax;
                            salary_Allocation_Hijri.qismRemarks = salaryAllocation.qismRemarks;
                            salary_Allocation_Hijri.systemRemarks = salaryAllocation.systemRemarks;
                            salary_Allocation_Hijri.tds = salaryAllocation.tds;

                            salary_Allocation_Hijri.ctc = _salaryService.caculateCTC(salary_Allocation_Hijri);

                            int requiredAmountRedistribution = salary_Allocation_Hijri.salary - salary_Allocation_Hijri.ctc;

                            foreach (var salaryGeneration in salary_Allocation_Hijri.salary_generation_gegorgian)
                            {
                                salaryGeneration.netSalary += (requiredAmountRedistribution / salary_Allocation_Hijri.salary) * salaryGeneration.netSalary;
                            }

                            salary_Allocation_Hijri.netEarnings = _salaryService.netSalary(salary_Allocation_Hijri);

                            _context.SaveChanges();
                        }
                    }
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            return Ok();

        }
    }

    public class PayrollProcessing
    {
        public bool isHijri { get; set; }
        public int? hMonth { get; set; }
        public int? hYear { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public string paymentFrom { get; set; }
        public string qismIds { get; set; }
        public List<int>? qismIdList { get; set; }
    }
    public class SalarySpecificHeadReportFormat
    {
        public int srNo { get; set; }
        public string name { get; set; }
        public int itsId { get; set; }
        public string panCard { get; set; }
        public int? amount { get; set; }
        public string hijriMonth { get; set; }
        public int packageId { get; set; }
    }

    public class JameaTahfeezReportModel
    {
        public int srNo { get; set; }
        public int itsId { get; set; }
        public string name { get; set; }
        public string hijri_Month_Year { get; set; }

        public int periods { get; set; }
        public string pan { get; set; }
        public string account_Number { get; set; }
        public string bank_AccountName { get; set; }
        public int salary { get; set; }
    }
}
