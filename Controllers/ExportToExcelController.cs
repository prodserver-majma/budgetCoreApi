using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using mahadalzahrawedapi.Mappings.Export;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

namespace mahadalzahrawebapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExportToExcelController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        private readonly static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        readonly DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        public ExportToExcelController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _salaryService = new SalaryService(context);
            _helperService = new HelperService(context);
            _itsService = new ItsServiceRemote();
            _jhsService = new IJHSServiceRemote();
            _globalConstants = new globalConstants();
        }


        [Route("getExportTypeHeaders/{id}")]
        [HttpGet]
        public async Task<IActionResult> getExportTypeHeaders([FromRoute] int id)
        {
            string api = "getExportTypeHeaders/{id}";

            try
            {
                List<export_type_displayheader> options = _context.export_type_displayheader.Where(x => x.typeId == id).ToList();
                return Ok(options);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("getExportTypeFilename/{id}")]
        [HttpGet]
        public async Task<IActionResult> getExportTypeFileName([FromRoute] int id)
        {
            string api = "getExportTypeFilename/{id}";

            try
            {
                export_type options = _context.export_type.Where(x => x.id == id).FirstOrDefault();
                return Ok(new {message = options.fileName ?? "" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("wafd_ul_huffaz")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel(ExportToExcel_WafdulhufazModel w)
        {
            string api = "wafd_ul_huffaz";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


                List<ExportToExcelModel> toRemove2 = w.toRemove.Where(x => x.status == false).ToList();

                List<Wafd_ul_huffaz_ExportModel> model3 = new List<Wafd_ul_huffaz_ExportModel>();


                HashSet<string> propertiesToExclude = new HashSet<string>(toRemove2.Select(x => x.propertyName));

                List<dynamic> transformedModels = model3.Select(m => _helperService.TransformModelForExport(m, propertiesToExclude)).ToList();

                return Ok(new { export = transformedModels });
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("wafd_ul_huffaznew")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel_New(ExportToExcel_WafdulhufazModel w)
        {
            string api = "wafd_ul_huffaznew";


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<int> model2 = w.model;

                List<Wafd_ul_huffaz_ExportModel> model3 = new List<Wafd_ul_huffaz_ExportModel>();
                int c = 1;

                foreach (var i in model2)
                {

                    khidmat_guzaar k = _context.khidmat_guzaar.Where(x => x.itsId == i && x.employeeType == "Khidmatguzaar").FirstOrDefault();

                    string email = k.emailAddress;
                    if (k.officialEmailAddress != null)
                    {
                        email = k.officialEmailAddress;
                    }

                    model3.Add(_mapper.Map<Wafd_ul_huffaz_ExportModel>(k));
                    c = c + 1;
                }


                List<ExportToExcelModel> toRemove2 = w.toRemove.Where(x => x.status == false).ToList();


                HashSet<string> propertiesToExclude = new HashSet<string>(toRemove2.Select(x => x.propertyName));

                List<dynamic> transformedModels = model3.Select(m => _helperService.TransformModelForExport(m, propertiesToExclude)).ToList();

                return Ok(new { export = transformedModels });
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("wajebaatmodel")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel_wajebaatmodel(List<WajebaatModel> w)
        {
            string api = "wajebaatmodel";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<Wajebaat_ExportModel> model3 = new List<Wajebaat_ExportModel>();


                int c = 1;
                foreach (var i in w)
                {
                    khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == i.itsId).FirstOrDefault();

                    model3.Add(new Wajebaat_ExportModel
                    {
                        lastYearWajebaatCurrency = i.lastYear_currency,
                        niyyatAmountCurrency = i.thisYear_currency,
                        lastYearWajebaatINR = (i.lastYearWajebaat * i.lastYear_currencyrate).ToString("F0"),
                        niyyatAmountINR = ((i.niyyatAmount ?? 0) * (i.thisYear_currencyrate)).ToString("F0"),
                        photo2 = kg.photoBase64,
                        age = i.age,
                        itsId = i.itsId,
                        khidmatMoze = i.khidmatMoze,
                        lastYearWajebaat = i.lastYearWajebaat.ToString(),
                        mzIdara = i.mzIdara,
                        name = i.name,
                        niyyatAmount = i.niyyatAmount,
                        srNo = c,
                        wajebaatType = i.wajebaatType,
                        takhmeenAmount = Math.Round(i.takhmeenAmount).ToString()
                    });
                    c = c + 1;
                }

                HashSet<string> propertiesToExclude = new HashSet<string>();

                List<dynamic> transformedModels = model3.Select(m => _helperService.TransformModelForExport(m, propertiesToExclude)).ToList();

                return Ok(new { export = transformedModels });
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("verifiedwajebaatmodel")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel_verifiedwajebaatmodel(DateRange dates)
        {
            string api = "verifiedwajebaatmodel";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                dates.FromDate = dates.FromDate?.AddDays(-1);
                dates.ToDate = dates.ToDate?.AddDays(11);
                List<mz_kg_wajebaat_araz> w = _context.mz_kg_wajebaat_araz.Where(x => x.stage == "Varified" && x.verifiedOn > dates.FromDate && x.verifiedOn < dates.ToDate).ToList();
                List<wajebaatDraftExport> model3 = new List<wajebaatDraftExport>();

                int c = 1;
                foreach (var i in w)
                {
                    khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == i.itsId).FirstOrDefault();

                    model3.Add(new wajebaatDraftExport
                    {
                        srno = c,
                        bankName = i.bankName,
                        dated = i.draftDate?.ToString("dd/MM/yyyy"),
                        draftNo = i.draftNo,
                        its = kg.itsId,
                        name = kg.fullName,
                        wajebaatAmount = i.paidAmount?.ToString("#,#.##", CultureInfo.CreateSpecificCulture("hi-IN"))
                    });
                    c = c + 1;
                }

                return Ok(new { export = model3 });
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("receiptstatementdata")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel_ReceiptStatement(List<RecieptModel> w)
        {
            string api = "receiptstatementdata";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<RecieptModel> model2 = w;
                List<ExportToExcelModel> toRemove = new List<ExportToExcelModel>();
                List<ReceiptStatement_ExportModel> model3 = new List<ReceiptStatement_ExportModel>();
                int c = 1;
                foreach (var i in model2)
                {
                    model3.Add(new ReceiptStatement_ExportModel
                    {
                        srNo = c,
                        mz_Id = i.studentId ?? 0,
                        account = i.account,
                        note = i.note,
                        bankName = i.bankName,
                        transactionId = i.chequeNo,
                        collectionCenter = i.collectionCenter,
                        createdBy = i.createdBy,
                        feePaidAmount = i.feePaidAmount,
                        itsId = i.itsId,
                        name = i.name,
                        paymentMode = i.paymentMode,
                        receiptNo = i.receiptNo,
                        recieptDate = i.recieptDate?.ToString("dd/MM/yyyy"),
                        recieptDate_print = i.recieptDate_print.ToString("dd/MM/yyyy"),
                        status = i.status,
                        receiptId = i.receiptId,
                        chequeDate = i.chequeDate?.ToString("dd/MM/yyyy")
                    });
                    c = c + 1;
                }

                HashSet<string> propertiesToExclude = new HashSet<string>(toRemove.Select(x => x.propertyName));

                List<dynamic> transformedModels = model3.Select(m => _helperService.TransformModelForExport(m, propertiesToExclude)).ToList();

                return Ok(new { export = transformedModels });
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("Yellowreceiptstatementdata")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel_YellowReceiptStatement(List<RecieptModel> w)
        {
            string api = "Yellowreceiptstatementdata";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<RecieptModel> model2 = w;
                List<ExportToExcelModel> toRemove = new List<ExportToExcelModel>();
                List<ReceiptStatement_ExportModel> model3 = new List<ReceiptStatement_ExportModel>();
                int c = 1;
                foreach (var i in model2)
                {
                    model3.Add(new ReceiptStatement_ExportModel
                    {
                        srNo = c,
                        note = i.note,
                        collectionCenter = i.collectionCenter,
                        createdBy = i.createdBy,
                        feePaidAmount = i.feePaidAmount,
                        itsId = i.itsId,
                        name = i.name,
                        paymentMode = i.paymentMode,
                        receiptNo = i.receiptNo,
                        recieptDate = i.recieptDate?.ToString("dd/MM/yyyy"),
                        recieptDate_print = i.recieptDate_print.ToString("dd/MM/yyyy"),
                        receiptId = i.receiptId,
                        chequeDate = i.chequeDate?.ToString("dd/MM/yyyy")
                    }); ;
                    c = c + 1;
                }




                HashSet<string> propertiesToExclude = new HashSet<string>(toRemove.Select(x => x.propertyName));

                List<dynamic> transformedModels = model3.Select(m => _helperService.TransformModelForExport(m, propertiesToExclude)).ToList();

                return Ok(new { export = transformedModels });
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("studentDetails")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel_StudentDetails(List<CampStudentModel> w)
        {
            string api = "studentDetails";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<CampStudentModel> model2 = w;
                List<ExportToExcelModel> toRemove = new List<ExportToExcelModel>();
                List<ExportToExcel_StudentDetails> model3 = new List<ExportToExcel_StudentDetails>();
                int c = 1;
                foreach (var i in model2)
                {
                    model3.Add(new ExportToExcel_StudentDetails
                    {
                        jamiat = i.Jamiat,
                        blood_grp = i.blood_grp,
                        dob_gregorian = i.DOB_string,
                        dob_hijri = i.DOB_hijri,
                        nationality = i.nationality,
                        address = i.address,
                        maqaam = i.maqaam,
                        hafizYear = i.hafizYear ?? 0,
                        jamaat = i.Jamaat,
                        masoolName = i.masoolName,
                        watan = i.watan,
                        std = i.std,
                        trno = i.trno ?? 0,
                        itsId = i.ItsId.ToString(),
                        feeCategory = i.fc_name,
                        fc_amount = i.amount,
                        age = i.age.ToString(),
                        fatherEmail = i.fatherEmail,
                        fatherMobile = i.fatherMobile,
                        motherEmail = i.motherEmail,
                        motherMobile = i.motherMobile,
                        pendingAmount = i.pendingAmount.ToString(),
                        name = i.StudentName,
                        subprogram = i.subProgram,
                        program = i.program,
                        venue = i.venue,
                        studentEmail = i.EmailId,
                        studentMobile = i.MobileNo,
                        walletAmount = i.walletAmount.ToString(),
                        srNo = c.ToString(),
                        activeInactiveStatus = i.activeStatusString
                    });
                    c = c + 1;
                }

                HashSet<string> propertiesToExclude = new HashSet<string>(toRemove.Select(x => x.propertyName));

                List<dynamic> transformedModels = model3.Select(m => _helperService.TransformModelForExport(m, propertiesToExclude)).ToList();

                return Ok(new { export = transformedModels });
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("studentremainingfees")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel_RemainingStudentFees(List<CampStudentModel> students)
        {
            const string api = "remainingstudentfees";

            if (students == null || !students.Any())
            {
                return BadRequest(new { message = "Invalid input." });
            }

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                var mzIds = students.Where(s => s.pendingAmount > 0).Select(s => s.mzId).ToList();
                if (!mzIds.Any())
                {
                    return Ok(new { export = new List<RemainingStudentFeesModel>() });
                }

                var allotments = _context.mz_student_fee_allotment
                    .Where(feeAlot => mzIds.Contains(feeAlot.studentId ?? 0))
                    .Join(_context.hijri_months,
                          feeAlot => feeAlot.monthId,
                          hMonth => hMonth.id,
                          (feeAlot, hMonth) => new { feeAlot, hMonth })
                    .Join(_context.mz_student,
                        combined => combined.feeAlot.studentId,
                        student => student.mz_id,
                        (combined, student) => new { combined.feeAlot, combined.hMonth, student })
                    .Join(_context.registrationform_dropdown_set,
                          combined => combined.feeAlot.pSetId,
                          pset => pset.id,
                          (combined, pset) => new { combined.feeAlot, combined.hMonth, combined.student, prog = pset.program.name, subprog = pset.subprogram.name, venue = pset.venue.displayName })
                    .ToList() // Fetch the data into memory here
                    .SelectMany(
                        combined => _context.mz_student_fee_transaction.Where(ft => ft.allotmentId == combined.feeAlot.id)
                            .DefaultIfEmpty() // This won't work as intended since context isn't tracked after ToList(), adjust accordingly
                            .ToList() // Assuming you adjust for DefaultIfEmpty(), you'd fetch transactions here
                            .GroupBy(ft => ft.allotmentId)
                            .Select(g => new
                            {
                                combined.feeAlot,
                                combined.student,
                                combined.hMonth,
                                combined.prog,
                                combined.subprog,
                                combined.venue,
                                accCredit = g.Sum(ft => (int?)ft.credit) ?? 0,
                                accDebit = g.Sum(ft => (int?)ft.debit) ?? 0
                            }),
                        (combined, transaction) => new { combined.feeAlot, combined.hMonth, combined.student, combined.prog, combined.subprog, combined.venue, transaction.accCredit, transaction.accDebit }
                    )
                    .Where(combined => combined.accCredit - combined.accDebit != 0)
                    .Select(combined => new RemainingStudentFeesModel
                    {
                        // Now safe to use String.Join as we're in memory
                        program = string.Join("_", new[] { combined.prog, combined.subprog, combined.venue }.Where(s => !string.IsNullOrEmpty(s))),
                        hijriMonth = combined.hMonth.hijriMonthName,
                        hijriYear = combined.feeAlot.hijriYear,
                        allotedamount = combined.feeAlot.feeAlloted ?? 0,
                        amountRemaing = -1 * (combined.accCredit - combined.accDebit),
                        its = combined.student.itsID ?? 0,
                        name = combined.student.nameEng
                    })
                    .ToList();


                // Add SrNo
                for (int i = 0; i < allotments.Count; i++)
                {
                    allotments[i].srNo = i + 1;
                    // Populate ITS and StudentName here if possible
                }

                return Ok(new { export = allotments });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("wafd_ul_huffazSectionwizeExport")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel_Wafd_SectionWize(ExportToExcel_WafdulhufazModel w)
        {
            string api = "wafd_ul_huffazSectionwizeExport";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                var huffazModeljson2 = getSectionExportData(w.categoryId ?? 0, w.model);

                List<JObject> exportObject = new List<JObject>();
                dynamic jsonObj = JsonConvert.DeserializeObject(huffazModeljson2);

                foreach (dynamic obj in jsonObj)
                {
                    string s = Convert.ToString(obj);
                    var j = JObject.Parse(s);
                    exportObject.Add(j);
                }

                return Ok(new { export = exportObject });
                //return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("kotak")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel_Kotak(List<BillManagementModel> bills)
        {
            string api = "kotak";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<ExportToExcel_KotakCRM> model3 = new List<ExportToExcel_KotakCRM>();

                List<string> accounts = new List<string>();

                foreach (var i in bills)
                {

                    accounts.Add(i.paymentTo_AccNum);

                }
                accounts = accounts.Distinct().ToList();

                foreach (var i in accounts)
                {
                    List<BillManagementModel> bs = bills.Where(x => x.paymentTo_AccNum == i).ToList();
                    BillManagementModel b = bs.FirstOrDefault();
                    mz_expense_vendor_master vendor = _context.mz_expense_vendor_master.Where(x => x.id == b.vendorId).FirstOrDefault();

                    int amount = (bs.Sum(x => x.totalAmount) ?? 0) - (bs.Sum(x => x.tdsAmount) ?? 0);

                    string paymentMode = "";

                    if (amount > 200000)
                    {
                        paymentMode = "RTGS";
                    }
                    else if (b?.paymentTo_BankName?.ToLower().Contains("kotak") ?? false)
                    {
                        paymentMode = "IFT";
                    }
                    else
                    {
                        paymentMode = "NEFT";
                    }

                    Regex reg = new Regex("[*'\",_&#^@]");

                    var r = new ExportToExcel_KotakCRM
                    {
                        Amount = amount,
                        Bank_Code_Indicator = "M",
                        Beneficiary_Acc_No = i?.ToUpper(),
                        Beneficiary_Email = vendor?.email,
                        Beneficiary_Mobile = vendor?.mobileNo,
                        Beneficiary_Name = reg.Replace(b?.paymentTo_AccName?.ToUpper() ?? "", string.Empty),
                        Client_Code = "",
                        Credit_Narration = "",
                        Debit_Narration = reg.Replace(vendor?.name?.ToUpper() ?? "", string.Empty),
                        Dr_Ac_No = "",
                        Beneficiary_Branch_IFSC_Code = b?.paymentTo_ifsc.ToUpper(),
                        Payment_Type = paymentMode,
                        Product_Code = "RPAY",
                        Enrichment_1 = "",
                        Enrichment_2 = "",
                        Payment_Date = "",
                        Beneficiary_Bank = reg.Replace(vendor?.bankName?.ToUpper() ?? "", string.Empty),
                        Location = vendor?.city?.ToUpper(),
                    };
                    model3.Add(r);
                }



                return Ok(model3);
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

         [Route("bobtoother")]
         [HttpPost]
         public async Task<IActionResult> ExportToExcel_BOBTOOTHER(List<BillManagementModel> bills)
         {
             string api = "api/exporttoexcelnew/bobtoother";
             // Add_ApiLogs(api);

             try
             {
                 string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                 AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                     // List<BOBtoOtherNEFT> model2 = w.FirstOrDefault().model.Where(x => x.select == true).ToList();
                     List<ExportToExcelModel> toRemove3 = new List<ExportToExcelModel>();
                     List<BOBtoOtherNEFT> model3 = new List<BOBtoOtherNEFT>();

                     int c = 1;

                     List<string> accounts = new List<string>();

                     foreach (var i in bills)
                     {

                         accounts.Add(i.paymentTo_AccNum);

                     }
                     accounts = accounts.Distinct().ToList();

                     foreach (var i in accounts)
                     {
                         List<BillManagementModel> bs = bills.Where(x => x.paymentTo_AccNum == i).ToList();

                         model3.Add(new BOBtoOtherNEFT { amount = ((bs.Sum(x => x.totalAmount)) - (bs.Sum(x => x.tdsAmount) ?? 0)), bankAccountName = bs.First().paymentTo_AccName, bankAccountNumber = i, srNo = c, ifsc = bs.First().paymentTo_ifsc, text = "From Mahad Al Zahra, Surat" });
                         c = c + 1;
                     }




                     // var huffazModeljson = JsonConverterService.Serialize(w.model);
                     var huffazModeljson2 = JsonConvert.SerializeObject(model3);

                     List<JObject> exportObject = new List<JObject>();
                     dynamic jsonObj = JsonConvert.DeserializeObject(huffazModeljson2);

                // JavaScriptSerializer js = new JavaScriptSerializer();
                //dynamic jsonObj1 = js.Deserialize<dynamic>(huffazModeljson2);

//commented the part where the data was serialized and then deserialized and then returned instead returned the model3 directly.
                //foreach (dynamic obj in jsonObj)
                //{
                //    string s = Convert.ToString(obj);
                //    var j = JObject.Parse(s);
                //    foreach (var k in toRemove3)
                //    {
                //        try
                //        {
                //            j.Property(k.propertyName).Remove();

                //        }
                //        catch (Exception ex)
                //        {
                //            continue;
                //        }
                //    }

                //    exportObject.Add(j);
                //}

                return Ok(new { export = model3 });
                     //return Ok();

             }
             catch (Exception ex)
             {
                 return BadRequest(ex.ToString());
             }
         }

        [Route("bobtoBOB")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel_BOBTOBOB(List<BillManagementModel> bills)
        {
            string api = "api/exporttoexcelnew/bobtoBOB";
            // Add_ApiLogs(api);

            try
            {

                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


                // List<BOBtoOtherNEFT> model2 = w.FirstOrDefault().model.Where(x => x.select == true).ToList();
                List<ExportToExcelModel> toRemove3 = new List<ExportToExcelModel>();
                List<BOBToBOBNEFT> model3 = new List<BOBToBOBNEFT>();
                int c = 1;

                List<string> accounts = new List<string>();

                foreach (var i in bills)
                {

                    accounts.Add(i.paymentTo_AccNum);

                }
                accounts = accounts.Distinct().ToList();

                foreach (var i in accounts)
                {
                    List<BillManagementModel> bs = bills.Where(x => x.paymentTo_AccNum == i).ToList();

                    string str = "";

                    int len = bs.First().paymentTo_AccNum.Count();

                    int r = 14 - len;

                    str = bs.First().paymentTo_AccNum.ToString().PadLeft(r, '0');



                    model3.Add(new BOBToBOBNEFT { accountNumber2 = str, amount = ((bs.Sum(x => x.totalAmount) ?? 0) - (bs.Sum(x => x.tdsAmount) ?? 0)), bankAccountName = bs.First().paymentTo_AccName, ifsc = bs.First().paymentTo_ifsc, accountNumber1 = i, text = "From Mahad Al Zahra, Surat", sol = str.Substring(0, 4) });
                    c = c + 1;
                }




                //// var huffazModeljson = JsonConverterService.Serialize(w.model);
                //var huffazModeljson2 = JsonConvert.SerializeObject(model3);

                //List<JObject> exportObject = new List<JObject>();
                //dynamic jsonObj = JsonConvert.DeserializeObject(huffazModeljson2);

                //// JavaScriptSerializer js = new JavaScriptSerializer();
                ////dynamic jsonObj1 = js.Deserialize<dynamic>(huffazModeljson2);


                //foreach (dynamic obj in jsonObj)
                //{
                //    string s = Convert.ToString(obj);
                //    var j = JObject.Parse(s);
                //    foreach (var k in toRemove3)
                //    {
                //        try
                //        {
                //            j.Property(k.propertyName).Remove();

                //        }
                //        catch (Exception ex)
                //        {
                //            continue;
                //        }
                //    }


                //    exportObject.Add(j);

                //}



                return Ok(model3);
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("nisaabtalabat")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel(ExportToExcel_NisaabTalabatModel w)
        {
            string api = "nisaabtalabat";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<NisaabTalabatModel> model2 = w.model.Where(x => x.select == true).ToList();

                List<ExportToExcelModel> toRemove = w.toRemove.Where(x => x.status == false).ToList();
                // var huffazModeljson = JsonConverterService.Serialize(w.model);

                HashSet<string> propertiesToExclude = new HashSet<string>(toRemove.Select(x => x.propertyName));

                List<dynamic> transformedModels = model2.Select(m => _helperService.TransformModelForExport(m, propertiesToExclude)).ToList();

                return Ok(new { export = transformedModels });
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private string getSectionExportData(int id, List<int> models)
        {
            string data = "";

            IQueryable<khidmat_guzaar> khidmat_guzaars = _context.khidmat_guzaar
                .Include(x => x.employee_academic_details)
                .Where(x => models.Contains(x.itsId));
            List<khidmat_guzaar> models1 = khidmat_guzaars.ToList();

            if (id == 3)
            {

                List<ExportToExcel_FaimalyDetails> datas = new List<ExportToExcel_FaimalyDetails>();
                int c = 1;
                foreach (var i in models1)
                {
                    List<kg_faimalydetails_its> users = _context.kg_faimalydetails_its.Where(x => x.hofItsId == i.itsId).OrderByDescending(x => x.age).ToList();

                    foreach (var j in users)
                    {
                        datas.Add(new ExportToExcel_FaimalyDetails
                        {
                            r_age = j.age,
                            r_hifzStatus = j.hifzStatus,
                            r_idara = j.idara,
                            r_itsId = j.itsId?.ToString(),
                            r_jamaat = j.jamaat,
                            r_name = j.name,
                            r_nationality = j.nationality,
                            r_occupation = j.occupation,
                            r_relation = j.relation,
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString(),
                            dob = j.dob.ToString(),
                            bloodGrp = j.bloodGroup,
                        });

                    }

                    if (users.Count != 0)
                    {
                        c = c + 1;
                    }

                }

                data = JsonConvert.SerializeObject(datas);
            }

            else if (id == 4)
            {
                List<ExportToExcel_Qualification> datas = new List<ExportToExcel_Qualification>();
                int c = 1;
                foreach (var i in models1)
                {
                    List<wafdprofile_qualification_new> d = _context.wafdprofile_qualification_new.Where(x => x.itsid == i.itsId).ToList().OrderByDescending(x => x.year).ToList();

                    foreach (var j in d)
                    {
                        datas.Add(new ExportToExcel_Qualification
                        {
                            country = j.country,
                            degree = j.degree,
                            institutionName = j.institutionName,
                            mediumOfEducation = j.mediumOfEducation,
                            stage = j.stage,
                            status = j.status,
                            year = j.year,
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });
                    }

                    if (d.Count != 0)
                    {
                        c = c + 1;
                    }
                }
                data = JsonConvert.SerializeObject(datas);
            }

            else if (id == 5)
            {
                List<ExportToExcel_CoursesWorkshop> datas = new List<ExportToExcel_CoursesWorkshop>();
                int c = 1;
                foreach (var i in models1)
                {

                    List<wafdprofile_workshop_data> d = _context.wafdprofile_workshop_data.Where(x => x.itsId == i.itsId).ToList().OrderByDescending(x => x.year).ToList();

                    foreach (var j in d)
                    {
                        datas.Add(new ExportToExcel_CoursesWorkshop
                        {
                            category = j.category,
                            certificatecridentials = j.cetificateCredentials,
                            keyPoints = j.keypoints,
                            mode = j.mode,
                            subCategory = j.subCategory,
                            topic = j.courseName,
                            type = j.type,
                            year = j.completionDate?.ToString("yyyy"),
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });

                    }
                    if (d.Count != 0)
                    {
                        c = c + 1;
                    }
                }
                data = JsonConvert.SerializeObject(datas);
            }

            else if (id == 6)
            {
                List<ExportToExcel_LanguageProficiency> datas = new List<ExportToExcel_LanguageProficiency>();
                int c = 1;
                foreach (var i in models1)
                {
                    List<wafd_languageproficiency> datas2 = _context.wafd_languageproficiency.Where(x => x.itsId == i.itsId).ToList();
                    List<wafd_languageproficiency> datas1 = new List<wafd_languageproficiency>();
                    foreach (var i1 in datas2)
                    {
                        int a = (Convert.ToInt32(i1.listening) + Convert.ToInt32(i1.reading) + Convert.ToInt32(i1.speaking) + Convert.ToInt32(i1.writing)) / 4;

                        i1.selfRanking = a;
                        datas1.Add(i1);
                    }
                    datas1 = datas1.OrderByDescending(x => x.selfRanking).ToList();

                    foreach (var j in datas1)
                    {
                        datas.Add(new ExportToExcel_LanguageProficiency
                        {
                            average = j.selfRanking?.ToString(),
                            language = j.language,
                            listening = j.listening,
                            reading = j.reading,
                            speaking = j.speaking,
                            writing = j.writing,
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });


                    }

                    if (datas1.Count != 0)
                    {
                        c = c + 1;
                    }

                }
                data = JsonConvert.SerializeObject(datas);
            }

            else if (id == 7)
            {
                List<ExportToExcel_FieldOfInterest> datas = new List<ExportToExcel_FieldOfInterest>();
                int c = 1;
                foreach (var i in models1)
                {

                    List<wafd_fieldofinterest> d = _context.wafd_fieldofinterest.Where(x => x.itsId == i.itsId).OrderByDescending(x => x.selfRanking).ToList();

                    foreach (var j in d)
                    {

                        datas.Add(new ExportToExcel_FieldOfInterest
                        {
                            interest = j.fieldofInterest,
                            ranking = j.selfRanking,
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });
                    }

                    if (d.Count != 0)
                    {
                        c = c + 1;
                    }
                }
                data = JsonConvert.SerializeObject(datas);
            }
            else if (id == 8)
            {
                List<ExportToExcel_PhysicalFitness> datas = new List<ExportToExcel_PhysicalFitness>();
                int c = 1;
                foreach (var i in models1)
                {

                    List<wafd_physicalfitness> d = _context.wafd_physicalfitness.Where(x => x.itsId == i.itsId).OrderByDescending(x => x.selfRanking).ToList();

                    foreach (var j in d)
                    {
                        datas.Add(new ExportToExcel_PhysicalFitness
                        {
                            physicalFitness = j.sports,
                            ranking = j.selfRanking?.ToString(),
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });


                    }

                    if (d.Count != 0)
                    {
                        c = c + 1;
                    }


                }
                data = JsonConvert.SerializeObject(datas);
            }
            else if (id == 9)
            {
                List<ExportToExcel_mahadpastmawaze> datas = new List<ExportToExcel_mahadpastmawaze>();
                int c = 1;
                foreach (var i in models1)
                {

                    List<wafd_mahad_past_mawaze> masterModel = new List<wafd_mahad_past_mawaze>();
                    List<wafd_mahad_past_mawaze> model1 = _context.wafd_mahad_past_mawaze.Where(x => x.itsIs == i.itsId).ToList();


                    var model2 = model1
                        .Select(m => new { m.fromYear, m.toYear })
                        .Distinct()
                        .ToList();


                    foreach (var m in model2)
                    {
                        List<wafd_mahad_past_mawaze> model3 = _context.wafd_mahad_past_mawaze.Where(x => x.itsIs == i.itsId && x.fromYear == m.fromYear && x.toYear == m.toYear).ToList();

                        string programs = "";

                        foreach (var i1 in model3)
                        {
                            if (programs == "")
                            {
                                programs = i1.program;
                            }
                            else
                            {
                                programs = programs + " | " + i1.program;

                            }
                        }

                        wafd_mahad_past_mawaze mmm = new wafd_mahad_past_mawaze()
                        {
                            id = model3.FirstOrDefault().id,
                            fromYear = m.fromYear,
                            toYear = m.toYear,
                            mauze = model3.FirstOrDefault().mauze,
                            program = programs,

                        };

                        masterModel.Add(mmm);
                    }

                    foreach (var j in masterModel)
                    {
                        datas.Add(new ExportToExcel_mahadpastmawaze
                        {
                            mauze = j.mauze,
                            program = j.program,
                            year = j.fromYear + " - " + j.toYear,
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });


                    }

                    if (masterModel.Count != 0)
                    {
                        c = c + 1;
                    }
                }
                data = JsonConvert.SerializeObject(datas);
            }
            else if (id == 10)
            {
                List<ExportToExcel_OtherIdaraMawaze> datas = new List<ExportToExcel_OtherIdaraMawaze>();
                int c = 1;
                foreach (var i in models1)
                {

                    List<wafd_otheridara_mawaze> d = _context.wafd_otheridara_mawaze.Where(x => x.itsId == i.itsId).ToList();

                    foreach (var j in d)
                    {
                        datas.Add(new ExportToExcel_OtherIdaraMawaze
                        {
                            khidmatNature = j.khidmatNature,
                            mauze = j.mauze,
                            year = j.fromYear + " - " + j.toYear,
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });
                    }

                    if (d.Count != 0)
                    {
                        c = c + 1;
                    }

                }
                data = JsonConvert.SerializeObject(datas);
            }
            //else if (id == 11)
            //{
            //    List<ExportToExcel_MawazeHistory> datas = new List<ExportToExcel_MawazeHistory>();
            //    int c = 1;
            //    foreach (var i in models1)
            //    {
            //        List<wafdulhuffaz_khidmat_mawaze> d = _context.wafdulhuffaz_khidmat_mawaze.Where(x => x.itsId == i.itsId).OrderByDescending(x => x.hijriYear).ThenBy(x => x.khidmatMainType).ToList();


            //        foreach (var j in d)
            //        {
            //            datas.Add(new ExportToExcel_MawazeHistory
            //            {
            //                khidmatNature = j.khidmatMainType,
            //                mauze = j.mozeName,
            //                year = j.hijriYear?.ToString(),
            //                age = i.age?.ToString(),
            //                farigYear = i.fariqYear?.ToString(),
            //                batchId = i.batchId.ToString(),
            //                farigDarajah = i.farigDarajah?.ToString(),
            //                itsId = i.itsId.ToString(),
            //                name = i.fullName,
            //                srNo = c.ToString()
            //            });


            //        }

            //        if (d.Count != 0)
            //        {
            //            c = c + 1;
            //        }

            //    }
            //    data = JsonConvert.SerializeObject(datas);
            //}
            else if (id == 12)
            {
                List<ExportToExcel_IdentityCard> datas = new List<ExportToExcel_IdentityCard>();
                int c = 1;
                foreach (var i in models1)
                {

                    List<kg_identitycards> users = _context.kg_identitycards.Where(x => x.itsId == i.itsId).ToList();

                    foreach (var j in users)
                    {
                        datas.Add(new ExportToExcel_IdentityCard
                        {
                            cardNo = j.cardNumber,
                            cardType = j.cardType,
                            country = j.country,
                            nameOnCard = j.nameOnCard,
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });



                    }
                    if (users.Count != 0)
                    {
                        c = c + 1;
                    }

                }
                data = JsonConvert.SerializeObject(datas);
            }
            else if (id == 33)
            {
                List<ExportToExcel_StrengthWeakness> datas = new List<ExportToExcel_StrengthWeakness>();
                int c = 1;
                foreach (var i in models1)
                {

                    kg_self_assessment j = _context.kg_self_assessment.Where(x => x.itsId == i.itsId).FirstOrDefault();
                    if (j == null)
                    {
                        datas.Add(new ExportToExcel_StrengthWeakness
                        {
                            strength = "Not Filled",
                            weakness = "Not Filled",
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });
                    }
                    else
                    {
                        datas.Add(new ExportToExcel_StrengthWeakness
                        {
                            strength = j.strength,
                            weakness = j.weakness,
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });
                    }

                    c = c + 1;

                }
                data = JsonConvert.SerializeObject(datas);
            }
            else if (id == 34)
            {
                List<ExportToExcel_Aspirations> datas = new List<ExportToExcel_Aspirations>();
                int c = 1;
                foreach (var i in models1)
                {

                    kg_self_assessment j = _context.kg_self_assessment.Where(x => x.itsId == i.itsId).FirstOrDefault();

                    if (j == null)
                    {
                        datas.Add(new ExportToExcel_Aspirations
                        {
                            roleModel = "Not Filled",
                            alternativeCareerPath = "Not Filled",
                            changeAboutYourself = "Not Filled",
                            longTermGoal = "Not Filled",
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigDarajah?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });

                    }
                    else
                    {
                        datas.Add(new ExportToExcel_Aspirations
                        {
                            roleModel = j.roleModel,
                            alternativeCareerPath = j.alternativeCareerPath,
                            changeAboutYourself = j.changeAboutYourself,
                            longTermGoal = j.longTermGoal,
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });

                    }

                    c = c + 1;
                }

                data = JsonConvert.SerializeObject(datas);
            }
            else if (id == 59)
            {
                List<ExportToExcel_Aspirations> datas = new List<ExportToExcel_Aspirations>();
                int c = 1;
                foreach (var i in models1)
                {

                    kg_self_assessment j = _context.kg_self_assessment.Where(x => x.itsId == i.itsId).FirstOrDefault();

                    if (j == null)
                    {
                        datas.Add(new ExportToExcel_Aspirations
                        {
                            roleModel = "Not Filled",
                            alternativeCareerPath = "Not Filled",
                            changeAboutYourself = "Not Filled",
                            longTermGoal = "Not Filled",
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });

                    }
                    else
                    {
                        datas.Add(new ExportToExcel_Aspirations
                        {
                            roleModel = j.roleModel,
                            alternativeCareerPath = j.alternativeCareerPath,
                            changeAboutYourself = j.changeAboutYourself,
                            longTermGoal = j.longTermGoal,
                            age = i.age?.ToString(),
                            farigYear = i.employee_academic_details.farigYear?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString()
                        });

                    }

                    c = c + 1;


                }


                data = JsonConvert.SerializeObject(datas);
            }
            else if (id == 62)
            {
                List<ExportToExcel_SalaryDetails> datas = new List<ExportToExcel_SalaryDetails>();
                int c = 1;
                khidmat_guzaars = khidmat_guzaars.Include(x => x.mauzeNavigation);
                models1 = khidmat_guzaars.ToList();
                foreach (var i in models1)
                {
                    employee_salary? user = _context.employee_salary.Where(x => x.itsId == i.itsId).FirstOrDefault();
                    int netSalary = _salaryService.netSalary(user);
                    int ctc = _salaryService.caculateCTC(user);
                    //foreach (var j in users)
                    //{
                    datas.Add(new ExportToExcel_SalaryDetails
                    {
                        age = i.age?.ToString(),
                        farigYear = i.employee_academic_details.farigYear?.ToString(),
                        batchId = i.batchId.ToString(),
                        farigDarajah = i.employee_academic_details.farigDarajah?.ToString(),
                        itsId = i.itsId.ToString(),
                        name = i.fullName,
                        srNo = c.ToString(),
                        bqhs = user.bqhs,
                        conveyanceAllowance = user.conveyanceAllowance,
                        extraAllowance = user.extraAllowance,
                        fmbAllowance = user.fmbAllowance,
                        fmbDeduction = user.fmbDeduction,
                        isMahadSalary = user.isMahadSalary == true ? "Yes" : "No",
                        marafiqKhairiyah = user.marafiqKhairiyah,
                        marriageAllowance = user.marriageAllowance,
                        mauze = i.mauzeNavigation.displayName,
                        mumbaiAllowance = user.mumbaiAllowance,
                        professionTax = user.professionTax,
                        qardanHasanah = user.qardanHasanah,
                        rentAllowance = user.rentAllowance,
                        sabeel = user.sabeel,
                        tds = user.tds,
                        grossSalary = user.grossSalary,
                        netSalary = netSalary,
                        ctc = ctc
                    });

                    c = c + 1;

                }

                data = JsonConvert.SerializeObject(datas);
            }


            return data;
        }

    }

    public class wajebaatDraftExport
    {
        public int srno { get; set; }
        public int its { get; set; }
        public string name { get; set; }
        public string wajebaatAmount { get; set; }
        public string draftNo { get; set; }
        public string bankName { get; set; }
        public string dated { get; set; }
    }
}
