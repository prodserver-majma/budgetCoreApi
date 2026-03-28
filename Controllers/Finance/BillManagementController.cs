using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using mahadalzahrawebapi.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RazorLight;
using System.Diagnostics;
using System.Net;

namespace mahadalzahrawebapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BillManagementController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;
        private readonly BudgetArazService _budgetArazService;
        private readonly DateTime indianTime;

        private readonly IConverter _converter;
        private readonly IWebHostEnvironment _env;

        public BillManagementController(
            mzdbContext context,
            IMapper mapper,
            TokenService tokenService,
            IConverter converter,
            IWebHostEnvironment env
        )
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _salaryService = new SalaryService(context);
            _helperService = new HelperService(context);
            _itsService = new ItsServiceRemote();
            _jhsService = new IJHSServiceRemote();
            _globalConstants = new globalConstants();
            _budgetArazService = new BudgetArazService(context);
            _converter = converter;
            indianTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            _env = env;
        }

        public class ResponseModel
        {
            public string Message { get; set; }
            public decimal Amount { get; set; }
            public int Quantity { get; set; }
            public int Id { get; set; }
        }

        public class itemDetailsMode
        {
            public int id { get; set; }
            public decimal amount { get; set; }
            public int quantity { get; set; }
        }

        public struct constanlDictionary
        {
            public string key;
            public string value;
        }

        [Route("globalconstants")]
        [HttpGet]
        public async Task<IActionResult> getBillFundReqInfo()
        {
            try
            {
                List<global_constant> constants = _context.global_constant.ToList();
                return Ok(constants);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("globalconstants")]
        [HttpPost]
        public async Task<IActionResult> setBillFundReqInfo(constanlDictionary constant)
        {
            try
            {
                List<global_constant> constants = _context.global_constant.ToList();
                constants.Where(x => x.key == constant.key).FirstOrDefault().value = constant.value;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        public int switchMonth(int month)
        {
            int monthNum = 0;

            switch (month)
            {
                case 04:
                    monthNum = 1;
                    break;
                case 05:
                    monthNum = 2;
                    break;
                case 06:
                    monthNum = 3;
                    break;
                case 07:
                    monthNum = 4;
                    break;
                case 08:
                    monthNum = 5;
                    break;
                case 09:
                    monthNum = 6;
                    break;
                case 10:
                    monthNum = 7;
                    break;
                case 11:
                    monthNum = 8;
                    break;
                case 12:
                    monthNum = 9;
                    break;
                case 01:
                    monthNum = 10;
                    break;
                case 02:
                    monthNum = 11;
                    break;
                case 03:
                    monthNum = 12;
                    break;
            }
            return monthNum;
        }

        [Route("GetBankMaster")]
        [HttpGet]
        public async Task<IActionResult> GetBankMaster()
        {
            string api = "GetBankMaster";
            // Add_ApiLogs(api);

            var bankList = _context.bankmaster.ToList();
            return Ok(bankList);
        }

        [Route("addBill")]
        [HttpPost]
        public async Task<IActionResult> addBill(List<BillManagementModel> Bills)
        {
            string api = "addBill";
            //// Add_ApiLogs(api);

            BillManagementModel Bill = Bills.FirstOrDefault();
            int amount = Bill.totalAmount ?? 0;

            Bill.totalAmount = amount;

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string userName = authUser.Name;

            DateTime dt = DateTime.Now;
            DateTime cutofftime = new DateTime(2026, 3, 25);
            if (dt >= cutofftime && authUser.ItsId != 50423552)
            {
                return BadRequest(new { message = "Bill entry will resume from 2nd April" });
            }

            int BudgetFinancialYear = _helperService.GetFinancialYear(dt);
            if (BudgetFinancialYear > Bill.billDate?.Year)
            {
                return BadRequest(
                    "Can't enter bills with Bill Date before 1st April " + Bill.billDate?.Year
                );
            }
            //return BadRequest( new { message = "Bill Entry Temporarily Not available");
            string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string directory = System.IO.Path.GetDirectoryName(assemblyLocation);
            Debug.WriteLine($"The application is looking for DLLs in: {directory}");
            try
            {
                DateOnly dc = Bill.billDate ?? DateOnly.FromDateTime(DateTime.Today);

                var duplicate = _context
                    .mz_expense_bill_master.Where(x =>
                        x.vendorId == Bill.vendorId
                        && x.billAmount == Bill.totalAmount
                        && x.billDate == dc
                        && x.billNo == Bill.billNumber
                        && x.isWaived == false
                    )
                    .FirstOrDefault();
                if (duplicate != null)
                {
                    return BadRequest(new { message = "Duplicate Bill Entry!" });
                }

                dept_venue_baseitem dvbi = _context
                    .dept_venue_baseitem.Where(x =>
                        x.baseItemId == Bill.baseItemId && x.deptVenueId == Bill.deptVenueId && x.psetId == Bill.psetId
                    )
                    .FirstOrDefault();
                bool itemlock = dvbi.hasItemBlock;
                float totalExpense = 0;

                //totalExpense = _budgetArazService.getBudgetExpenseAmountAll_New_BillEntry(dvbi.deptVenueId ?? 0, dvbi.baseItemId ?? 0, BudgetFinancialYear);
                totalExpense = _budgetArazService
                    .getBudgetConsumedAmount(
                        dvbi.deptVenueId ?? 0,
                        dvbi.psetId ?? 0,
                        dvbi.baseItemId ?? 0,
                        BudgetFinancialYear
                    )
                    .Sum(x => x.amount);

                List<mz_expense_budget_araz> budgetalottment = _context
                    .mz_expense_budget_araz.Where(x =>
                        x.deptVenueId == dvbi.deptVenueId
                        && x.psetId == dvbi.psetId
                        && x.baseItemId == dvbi.baseItemId
                        && x.financialYear == BudgetFinancialYear
                        && x.stage == "Sanctioned"
                    )
                    .ToList();



                if (budgetalottment.Count == 0)
                {
                    return BadRequest(
                        "No Budget Found for the selected Dept_Venue & Expense Head."
                    );
                }

                int billMonth = 0;
                if(Bill.billDate != null)
                {
                    billMonth = Bill.billDate.Value.Month;

                }

                var monthCheck = billMonth % 3;

                    var start = 0;
                    var end = 0;

                    if (monthCheck == 0)
                    {
                        start = billMonth - 2;
                        end = billMonth;
                    }
                    else if (monthCheck == 2)
                    {
                        start = billMonth - 1;
                        end = billMonth + 1;
                    }
                    else
                    {
                        start = billMonth;
                        end = billMonth + 2;
                    }


                    start = switchMonth(start);
                    end = switchMonth(end);

                int rembudget = 0;
                foreach (var budgetAraz in budgetalottment)
                {

                    rembudget += budgetAraz.mz_expense_budget_araz_monthly.Where(x => int.Parse(x.month_num.Replace("Month ", "")) >= 1 && int.Parse(x.month_num.Replace("Month ", "")) <= end).Sum(x =>
                    ((int)(x.quantity  * x.amount))
                    + (x.transferredAmount)
                    - ((int)x.consumedAmount)
                );
                    //foreach (var budgetArazMonth in budgetAraz.mz_expense_budget_araz_monthly)
                    //{
                        
                    //}
                }
                
                if (rembudget < Bill.totalAmount)
                {
                    return BadRequest(
                        "Budget Amount Exceeded or No budget left, Contact Administration"
                    );
                }

                //cache.DeleteItem("getBillin-" + Bill.deptVenueId.ToString() + "-" + Bill.baseItemId.ToString() + "-" + BudgetFinancialYear);


                mz_expense_bill_master newbill = new mz_expense_bill_master
                {
                    baseItemId = Bill.baseItemId,
                    billAmount = Bill.totalAmount,
                    billDate = Bill.billDate,
                    createdBy = authUser.ItsId.ToString(),
                    createdOn = indianTime,
                    deptVenueId = Bill.deptVenueId,
                    psetId = Bill.psetId ?? 0,
                    financialYear = BudgetFinancialYear,
                    vendorId = Bill.vendorId,
                    billNo = Bill.billNumber,
                    isWaived = false,
                    status = "Pending",
                    paymentMode_User = Bill.paymentMode_User,
                    paymentTo_AccName = Bill.paymentTo_AccName,
                    paymentTo_AccNum = Bill.paymentTo_AccNum,
                    paymentTo_BankName = Bill.paymentTo_BankName,
                    paymentTo_ifsc = Bill.paymentTo_ifsc,
                    gstAmount = Bill.gstAmount,
                    conveyanceAmount = Bill.conveyanceAmount,
                    gstPercentage = Bill.gstPercentage,
                    tdsAmount = Bill.tdsAmount,
                    tdsPercentage = Bill.tdsPercentage,
                    tdsApplicableAmount = Bill.tdsApplicableAmount,
                };
                _context.mz_expense_bill_master.Add(newbill);
                _context.SaveChanges();

                mz_expense_deptvenue_cash_wallet c = new mz_expense_deptvenue_cash_wallet
                {
                    createdBy = authUser.Name,
                    createdOn = indianTime,
                    debit = Bill.totalAmount,
                    currency = "INR",
                    deptVenueId = Bill.deptVenueId,
                    paymentType = "Used",
                    note = "Unique Id: - " + newbill.id,
                    status = true,
                };
                if (Bill.paymentMode_User == "Cash")
                {
                    _context.mz_expense_deptvenue_cash_wallet.Add(c);
                }
                mz_expense_bill_logs billLog = new mz_expense_bill_logs
                {
                    billId = newbill.id,
                    createdBy = authUser.Name,
                    createdOn = indianTime,
                    status = "Bill Entered",
                    description = "Bill Status:- Pending"
                };
                _context.mz_expense_bill_logs.Add(billLog);

                List<mz_expense_budget_araz> budgetList = _context
                    .mz_expense_budget_araz.Where(x =>
                        x.deptVenueId == Bill.deptVenueId
                        && x.baseItemId == Bill.baseItemId
                        && x.financialYear == BudgetFinancialYear
                    )
                    .ToList();
                int success = 0;
                decimal dedAmount = 0;
                int billQuantity = 0;
                var details = new List<itemDetailsMode>();
                foreach (var item in Bills)
                {
                    try
                    {
                        String res = _budgetArazService.consumeBudgetResourse(
                            authUser,
                            item,
                            BudgetFinancialYear,
                            true
                        );
                        Debug.WriteLine(res);
                        var resp = System.Text.Json.JsonSerializer.Deserialize<ResponseModel>(res);
                        if (resp.Message == "Success")
                        {
                            details.Add(new itemDetailsMode
                            {
                                id = resp.Id,
                                amount = resp.Amount,
                                quantity = resp.Quantity
                            });
                            //success++;
                            //dedAmount = resp.Amount;
                            //billQuantity = resp.Quantity;
                        }
                    }
                    catch (Exception e)
                    {
                         //Debug.WriteLine(details);

                        var suc = success;
                        int mnthNm = switchMonth(billMonth);

                        foreach(var k in details)
                        {
                            var budgetMonth = _context.mz_expense_budget_araz_monthly.Where(x => x.id == k.id).FirstOrDefault();
                            budgetMonth.consumedAmount -= (float)k.amount;
                            budgetMonth.consumedQuantity -= k.quantity;

                            var budgetInd = budgetalottment.Where(x => x.id == budgetMonth.budget_araz_id).FirstOrDefault();

                            budgetInd.consumedAmount -= (float)k.amount;
                            budgetInd.consumedQty -= k.quantity;
                            //Debug.WriteLine(budgetMonth);
                        }

                        //

                        //var monthBudget = budgetMonth.mz_expense_budget_araz_monthly.Where(x => int.Parse(x.month_num.Replace("Month ", "")) == mnthNm).FirstOrDefault();

                        //monthBudget.consumedAmount -= (float)dedAmount;
                        //monthBudget.consumedQuantity -= (float)billQuantity;

                        //for(int k = 0; k <= success; k++)
                        //{

                        //    var budgetMonth = budgetalottment.Where(x => x.deptVenueId == Bills[k].deptVenueId && x.psetId == Bills[k].psetId && x.itemId == Bills[k].itemId).FirstOrDefault();

                        //    var monthBudget = budgetMonth.mz_expense_budget_araz_monthly.Where(x => int.Parse(x.month_num.Replace("Month ", "")) == mnthNm).FirstOrDefault();

                        //    monthBudget.consumedAmount -= (float)dedAmount;
                        //    monthBudget.consumedQuantity -= (float)billQuantity;
                        //    //foreach(var monthlies in monthBudget)
                        //    //{

                        //    //}
                        //    //budgetItem.consumedAmount -= (int)Math.Round((double)(item.quantity * item.amountPerPc));
                        //    //budgetItem.consumedQty -= (int)item.quantity;
                        //      Debug.WriteLine(monthBudget);

                        //}



                        foreach (var revertItem in Bills)
                        {
                            if (revertItem.id == item.id)
                            {
                                break;
                            }
                            String res = _budgetArazService.consumeBudgetResourse(
                                authUser,
                                revertItem,
                                BudgetFinancialYear,
                                false
                            );
                        }

                        if (Bill.paymentMode_User == "Cash")
                        {
                            _context.mz_expense_deptvenue_cash_wallet.Remove(c);
                        }
                        _context.mz_expense_bill_logs.Remove(billLog);
                        _context.mz_expense_bill_master.Remove(newbill);
                        //cache.DeleteItem("getBillin-" + Bill.deptVenueId.ToString() + "-" + Bill.baseItemId.ToString() + "-" + BudgetFinancialYear);
                        _context.SaveChanges();
                             return BadRequest(new { message = e.Message} );
                    }

                    mz_expense_bill_item i = new mz_expense_bill_item
                    {
                        billId = newbill.id,
                        amountPerPc = item.amountPerUom,
                        itemId = item.itemId,
                        quantity = item.quantity,
                        remarks = item.remarks,
                        gstAmount = item.gstAmount,
                        gstPercentage = item.itemGstPercentage
                    };
                    _context.mz_expense_bill_item.Add(i);

                    //mz_expense_budget_araz budgetItem = budgetList.Where(x => x.itemId == item.itemId).FirstOrDefault();
                    //if (budgetItem != null)
                    //{
                    //    budgetItem.consumedAmount += (int)Math.Round(item.quantity * item.amountPerUom);
                    //    budgetItem.consumedQty += (int)item.quantity;
                    //}
                }

                mz_expense_vendor_transaction v = new mz_expense_vendor_transaction
                {
                    billId = newbill.id,
                    credit = newbill.billAmount,
                    currency = "INR",
                    vendorId = newbill.vendorId,
                    createdBy = authUser.Name,
                    createdOn = indianTime,
                    remarks = "Bill Entry",
                };
                _context.mz_expense_vendor_transaction.Add(v);
                _context.SaveChanges();

                newbill.txn_Id = v.id;

                try
                {
                    dept_venue dv = _context
                        .dept_venue.Where(x => x.id == newbill.deptVenueId)
                        .FirstOrDefault();
                    mz_expense_procurement_baseitem bi = _context
                        .mz_expense_procurement_baseitem.Where(x => x.id == newbill.baseItemId)
                        .FirstOrDefault();

                    mz_expense_vendor_master vendor = _context
                        .mz_expense_vendor_master.Where(x => x.id == newbill.vendorId)
                        .FirstOrDefault();

                    registrationform_dropdown_set rdp = _context.registrationform_dropdown_set.Where(x => x.id == newbill.psetId).FirstOrDefault();
                    registrationform_subprograms sbp = _context.registrationform_subprograms.Where(x => x.id == rdp.subprogramId).FirstOrDefault();

                    if (!string.IsNullOrEmpty(Bill.billAttachment))
                    {
                        byte[] binaryData = Convert.FromBase64String(Bill.billAttachment ?? "");

                        MemoryStream stream2 = new MemoryStream(binaryData);

                        ExpenseBillBrief billBrief = new ExpenseBillBrief
                        {
                            billid = newbill.id,
                            billNumber = newbill.billNo,
                            vendorId = newbill.vendorId ?? 0,
                            nameOfVendor = vendor.name ?? "-",
                            //vendorName = vendor.name ?? "",
                            vendorAccName = newbill.paymentTo_AccName,
                            vendorAccountNo = newbill.paymentTo_AccNum,
                            vendorBankName = newbill.paymentTo_BankName,
                            vendorIfsc = newbill.paymentTo_ifsc,
                            billDate = newbill.billDate ?? DateOnly.FromDateTime(DateTime.Today),
                            entryDate = newbill.createdOn ?? DateTime.Today,
                            billAmount = newbill.billAmount ?? 0,
                            paymentMode = newbill.paymentMode_User,
                            deptName = dv.deptName ?? "-",
                            //venueName = dv.venueName ?? "-",
                            nameOfVenue = dv.venueName ?? "-",
                            baseItemName = bi?.name,
                            className = sbp.name
                        };

                        // Define the template path
                        string templatePath = Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory,
                            "Templates",
                            "expenseBillBrief.cshtml"
                        );

                        // Check if the file exists
                        if (!System.IO.File.Exists(templatePath))
                        {
                            throw new FileNotFoundException(
                                $"The template file was not found at path: {templatePath}"
                            );
                        }

                        // Read the template content
                        string templateContent = await System.IO.File.ReadAllTextAsync(
                            templatePath
                        );

                        // Configure RazorLight to use a file system project
                        var engine = new RazorLightEngineBuilder()
                            .UseFileSystemProject(Path.GetDirectoryName(templatePath)) // Set the base directory for templates
                            .UseMemoryCachingProvider()
                            .Build();

                        // Correctly call CompileRenderAsync with the model and template content
                        string renderedHtml = await engine.CompileRenderAsync<ExpenseBillBrief>(
                            Path.GetFileName(templatePath),
                            billBrief
                        );

                        var doc = new HtmlToPdfDocument()
                        {
                            GlobalSettings =
                            {
                                ColorMode = ColorMode.Color,
                                Orientation = Orientation.Portrait,
                                PaperSize = PaperKind.A4,
                                Margins = new MarginSettings()
                                {
                                    Top = 5,
                                    Bottom = 10,
                                    Left = 5,
                                    Right = 7
                                },
                            },
                            Objects =
                            {
                                new ObjectSettings()
                                {
                                    HtmlContent = renderedHtml,
                                    WebSettings =
                                    {
                                        DefaultEncoding = "utf-8",
                                        LoadImages = true,
                                        EnableIntelligentShrinking = false, // Prevents automatic scaling
                                    },
                                    // FooterSettings =
                                    // {
                                    //     FontName = "Arial",
                                    //     FontSize = 9,
                                    //     Right = "Page [page] of [toPage]",
                                    // }
                                }
                            },
                        };

                        // byte[] pdf = _converter.Convert(doc);
                        byte[] systemPdfBytes = _converter.Convert(doc);
                        var fileStr = Bill.billAttachmentFileName;

                        // Save PDF to a MemoryStream and upload to S3
                        MemoryStream systemPdfStream = new MemoryStream(systemPdfBytes); // This is your system generated PDF

                        MemoryStream combinedPdfStream = _helperService.CombinePdfStreams(stream2, systemPdfStream);

                        Debug.WriteLine(combinedPdfStream.GetType());
                        string billAttachmentFileName = newbill.id + "_" + Bill.billAttachmentFileName;

                        try
                        {
                            //Debug.WriteLine($"This is upload debug: {$"UserName: {Environment.UserName}"}");
                            var uploadFolder = Path.Combine(_env.WebRootPath, "vendorBills");

                            Directory.CreateDirectory(uploadFolder);

                            byte[] fileBytes = Convert.FromBase64String(Bill.billAttachment);

                            var fileName = Path.Combine(uploadFolder, billAttachmentFileName);

                            System.IO.File.WriteAllBytes(fileName, combinedPdfStream.ToArray());

                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine($"This is upload debug: {e.Message}");
                        }

                        newbill.billAttachment = billAttachmentFileName;
                    }

                    //if (!string.IsNullOrEmpty(Bill.billAttachment))
                    //{
                    //    byte[] binaryData = Convert.FromBase64String(Bill.billAttachment ?? "");

                    //    MemoryStream stream2 = new MemoryStream(binaryData);

                    //    ExpenseBillBrief billBrief = new ExpenseBillBrief
                    //    {
                    //        billid = newbill.id,
                    //        billNumber = newbill.billNo,
                    //        vendorId = newbill.vendorId ?? 0,
                    //        vendorName = vendor?.name,
                    //        vendorAccName = newbill.paymentTo_AccName,
                    //        vendorAccountNo = newbill.paymentTo_AccNum,
                    //        vendorBankName = newbill.paymentTo_BankName,
                    //        vendorIfsc = newbill.paymentTo_ifsc,
                    //        billDate = newbill.billDate ?? DateOnly.FromDateTime(DateTime.Today),
                    //        entryDate = newbill.createdOn ?? DateTime.Today,
                    //        billAmount = newbill.billAmount ?? 0,
                    //        paymentMode = newbill.paymentMode_User,
                    //        deptName = dv?.deptName,
                    //        venueName = dv?.venueName,
                    //        baseItemName = bi?.name,
                    //    };

                    //    // Define the template path
                    //    string templatePath = Path.Combine(
                    //        AppDomain.CurrentDomain.BaseDirectory,
                    //        "Templates",
                    //        "expenseBillBrief.cshtml"
                    //    );

                    //    // Check if the file exists
                    //    if (!System.IO.File.Exists(templatePath))
                    //    {
                    //        throw new FileNotFoundException(
                    //            $"The template file was not found at path: {templatePath}"
                    //        );
                    //    }

                    //    // Read the template content
                    //    string templateContent = await System.IO.File.ReadAllTextAsync(
                    //        templatePath
                    //    );

                    //    // Configure RazorLight to use a file system project
                    //    var engine = new RazorLightEngineBuilder()
                    //        .UseFileSystemProject(Path.GetDirectoryName(templatePath)) // Set the base directory for templates
                    //        .UseMemoryCachingProvider()
                    //        .Build();

                    //    // Correctly call CompileRenderAsync with the model and template content
                    //    string renderedHtml = await engine.CompileRenderAsync<ExpenseBillBrief>(
                    //        Path.GetFileName(templatePath),
                    //        billBrief
                    //    );

                    //    var doc = new HtmlToPdfDocument()
                    //    {
                    //        GlobalSettings =
                    //        {
                    //            ColorMode = ColorMode.Color,
                    //            Orientation = Orientation.Portrait,
                    //            PaperSize = PaperKind.A4,
                    //            Margins = new MarginSettings()
                    //            {
                    //                Top = 5,
                    //                Bottom = 10,
                    //                Left = 5,
                    //                Right = 7
                    //            },
                    //        },
                    //        Objects =
                    //        {
                    //            new ObjectSettings()
                    //            {
                    //                HtmlContent = renderedHtml,
                    //                WebSettings =
                    //                {
                    //                    DefaultEncoding = "utf-8",
                    //                    LoadImages = true,
                    //                    EnableIntelligentShrinking = false, // Prevents automatic scaling
                    //                },
                    //                // FooterSettings =
                    //                // {
                    //                //     FontName = "Arial",
                    //                //     FontSize = 9,
                    //                //     Right = "Page [page] of [toPage]",
                    //                // }
                    //            }
                    //        },
                    //    };




                    //    // byte[] pdf = _converter.Convert(doc);
                    //    byte[] systemPdfBytes = _converter.Convert(doc);
                    //    var fileStr = Bill.billAttachmentFileName;

                    //    // Save PDF to a MemoryStream and upload to S3
                    //    MemoryStream systemPdfStream = new MemoryStream(systemPdfBytes); // This is your system generated PDF

                    //    MemoryStream combinedPdfStream = _helperService.CombinePdfStreams(stream2, systemPdfStream);

                    //    string billAttachmentFileName = newbill.id + "_" + Bill.billAttachmentFileName;

                    //    try
                    //    {
                    //        //Debug.WriteLine($"This is upload debug: {$"UserName: {Environment.UserName}"}");
                    //        var uploadFolder = Path.Combine(_env.WebRootPath, "vendorBills");

                    //        Directory.CreateDirectory(uploadFolder);

                    //        byte[] fileBytes = Convert.FromBase64String(Bill.billAttachment);

                    //        var fileName = Path.Combine(uploadFolder, billAttachmentFileName);

                    //        System.IO.File.WriteAllBytes(fileName, fileBytes);

                    //    }
                    //    catch (Exception e)
                    //    {
                    //        Debug.WriteLine($"This is upload debug: {e.Message}");
                    //    }

                    //    // Upload combined PDF to S3
                    //    //string pdfUrl = await _helperService.UploadFileToS3(
                    //    //    combinedPdfStream,
                    //    //    "CombinedBillBrief.pdf",
                    //    //    "BillBriefs"
                    //    //);

                    //    newbill.billAttachment = billAttachmentFileName;
                    //}
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }

                _context.SaveChanges();

                return Ok(newbill.id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("gettodaysEnteredBills")]
        [HttpGet]
        public async Task<IActionResult> gettodaysEnteredBills()
        {
            string api = "gettodaysEnteredBills";
            //// Add_ApiLogs(api);

            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<BillManagementModel> model = new List<BillManagementModel>();

                string itsid = authUser.ItsId.ToString();
                DateTime to = DateTime.Today.AddDays(1).Date;
                List<mz_expense_bill_master> bills = _context
                    .mz_expense_bill_master.Where(x =>
                        x.createdOn >= DateTime.Today.Date
                        && x.createdOn < to
                        && x.createdBy == itsid
                    )
                    .ToList();

                foreach (var i in bills)
                {
                    mz_expense_procurement_baseitem item = _context
                        .mz_expense_procurement_baseitem.Where(x => x.id == i.baseItemId)
                        .FirstOrDefault();
                    dept_venue dv = _context
                        .dept_venue.Where(x => x.id == i.deptVenueId)
                        .FirstOrDefault();
                    mz_expense_vendor_master vendor = _context
                        .mz_expense_vendor_master.Where(x => x.id == i.vendorId)
                        .FirstOrDefault();

                    model.Add(
                        new BillManagementModel
                        {
                            isFundRequestedString =
                                (i.isFundRequested ?? false) ? "Requested" : "Not Requested",
                            isFundRequested = i.isFundRequested ?? false,
                            id = i.id,
                            baseItemId = i.baseItemId ?? 0,
                            billDate = i.billDate ?? DateOnly.FromDateTime(DateTime.Today),
                            billNumber = i.billNo,
                            deptVenueId = i.deptVenueId ?? 0,
                            totalAmount = i.billAmount ?? 0,
                            vendorId = i.vendorId ?? 0,
                            baseItemName = item.name,
                            deptVenueName = dv.deptName + "_" + dv.venueName,
                            deptName = dv.deptName,
                            venueName = dv.venueName,
                            vendorName = vendor.name,
                            paymentTo_AccName = i.paymentTo_AccName,
                            paymentTo_AccNum = i.paymentTo_AccNum,
                            paymentTo_BankName = i.paymentTo_BankName,
                            paymentTo_ifsc = i.paymentTo_ifsc,
                            isWaived = i.isWaived,
                            paymentMode_User = i.paymentMode_User,
                            status = i.status,
                            paymentMode_Admin = i.paymentMode_Admin,
                            gstAmount = i.gstAmount,
                            gstPercentage = i.gstPercentage,
                            isReconciled = i.isReconciled,
                            tdsAmount = i.tdsAmount ?? 0,
                            tdsApplicableAmount = i.tdsApplicableAmount ?? 0,
                            tdsPercentage = i.tdsPercentage,
                            conveyanceAmount = i.conveyanceAmount,
                            billAttachment = i.billAttachment
                        }
                    );
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getbillsofpaymentId/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbillsofpaymentId(int id)
        {
            string api = "getbillsofpaymentId/{id}";
            //// Add_ApiLogs(api);


            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<BillManagementModel> model = new List<BillManagementModel>();

                string itsid = authUser.ItsId.ToString();
                DateTime to = DateTime.Today.AddDays(1).Date;
                List<mz_expense_vendor_transaction> transactions = _context
                    .mz_expense_vendor_transaction.Where(x => x.paymentId == id)
                    .ToList();

                foreach (var j in transactions)
                {
                    mz_expense_bill_master i = _context
                        .mz_expense_bill_master.Where(x => x.id == j.billId)
                        .FirstOrDefault();
                    mz_expense_procurement_baseitem item = _context
                        .mz_expense_procurement_baseitem.Where(x => x.id == i.baseItemId)
                        .FirstOrDefault();
                    dept_venue dv = _context
                        .dept_venue.Where(x => x.id == i.deptVenueId)
                        .FirstOrDefault();
                    mz_expense_vendor_master vendor = _context
                        .mz_expense_vendor_master.Where(x => x.id == i.vendorId)
                        .FirstOrDefault();

                    int itsid1 = Convert.ToInt32(i.createdBy);
                    user u1 = _context.user.Where(x => x.ItsId == itsid1).FirstOrDefault();
                    model.Add(
                        new BillManagementModel
                        {
                            createdBy = u1.Username,
                            createdOn = i.createdOn ?? DateTime.Today,
                            id = i.id,
                            baseItemId = i.baseItemId ?? 0,
                            billDate = i.billDate ?? DateOnly.FromDateTime(DateTime.Today),
                            billNumber = i.billNo,
                            deptVenueId = i.deptVenueId ?? 0,
                            totalAmount = i.billAmount ?? 0,
                            vendorId = i.vendorId ?? 0,
                            baseItemName = item.name,
                            deptVenueName = dv.deptName + "_" + dv.venueName,
                            deptName = dv.deptName,
                            venueName = dv.venueName,
                            vendorName = vendor.name,
                            paymentTo_AccName = i.paymentTo_AccName,
                            paymentTo_AccNum = i.paymentTo_AccNum,
                            paymentTo_BankName = i.paymentTo_BankName,
                            paymentTo_ifsc = i.paymentTo_ifsc,
                            isWaived = i.isWaived,
                            paymentMode_User = i.paymentMode_User,
                            status = i.status,
                            paymentMode_Admin = i.paymentMode_Admin,
                            gstAmount = i.gstAmount,
                            gstPercentage = i.gstPercentage,
                            isReconciled = i.isReconciled,
                            tdsAmount = i.tdsAmount ?? 0,
                            tdsApplicableAmount = i.tdsApplicableAmount ?? 0,
                            tdsPercentage = i.tdsPercentage,
                            conveyanceAmount = i.conveyanceAmount
                        }
                    );
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("billstep1approvedorrejected/{billId}/{status}/{reason}")]
        [HttpGet]
        public async Task<IActionResult> Bill_Step1_approvedorrejected(
            int billId,
            string status,
            string? reason
        )
        {
            string api = "billstep1approvedorrejected/{billId}/{status}/{reason}";
            //// Add_ApiLogs(api);

            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                BillManagementModel model = new BillManagementModel();

                int financialYear = _helperService.GetFinancialYear(DateTime.Now);

                mz_expense_bill_master bill = _context
                    .mz_expense_bill_master.Where(x => x.id == billId)
                    .FirstOrDefault();

                var numMonth = bill.billDate.Value.Month;

                numMonth = switchMonth(numMonth);

                //cache.DeleteItem("getBillin-" + bill.deptVenueId.ToString() + "-" + bill.baseItemId.ToString() + "-" + financialYear);

                if (status == "Approved")
                {
                    bill.status = "Approved";

                    _context.mz_expense_bill_logs.Add(
                        new mz_expense_bill_logs
                        {
                            status = "Approved",
                            billId = bill.id,
                            createdOn = indianTime,
                            createdBy = authUser.Name,
                        }
                    );
                }
                else if (status == "Rejected")
                {
                    List<mz_expense_vendor_transaction> transactions = _context
                        .mz_expense_vendor_transaction.Where(x => x.billId == bill.id)
                        .ToList();

                    int? C_withoutR = transactions
                        .Where(x => x.paymentMode != "Reverse")
                        .ToList()
                        .Sum(x => x.credit);
                    int? waived = transactions
                        .Where(x => x.paymentMode == "Waive")
                        .ToList()
                        .Sum(x => x.debit);

                    int? D_withoutW = transactions
                        .Where(x => x.paymentMode != "Waive")
                        .ToList()
                        .Sum(x => x.debit);
                    int? reversed = transactions
                        .Where(x => x.paymentMode == "Reverse")
                        .ToList()
                        .Sum(x => x.credit);

                    int amount =
                        ((C_withoutR ?? 0) - (waived ?? 0)) - ((D_withoutW ?? 0) - (reversed ?? 0));

                    if (amount == bill.billAmount)
                    {
                        bill.status = "Rejected";

                        mz_expense_vendor_transaction t = new mz_expense_vendor_transaction
                        {
                            billId = bill.id,
                            debit = bill.billAmount,
                            paymentMode = "Waive",
                            vendorId = bill.vendorId,
                            createdBy = authUser.Name,
                            createdOn = indianTime,
                            remarks = "Rejected Bill",
                        };
                        _context.mz_expense_vendor_transaction.Add(t);
                        bill.isWaived = true;

                        _context.mz_expense_bill_logs.Add(
                            new mz_expense_bill_logs
                            {
                                status = "Rejected & Waived",
                                billId = bill.id,
                                createdOn = indianTime,
                                createdBy = authUser.Name,
                                description = "Bill status = Rejected :: Reason:- " + reason
                            }
                        );
                    }

                    if (bill.paymentMode_User == "Cash")
                    {
                        bill.status = "Rejected";

                        mz_expense_deptvenue_cash_wallet c = new mz_expense_deptvenue_cash_wallet
                        {
                            createdBy = authUser.Name,
                            createdOn = indianTime,
                            credit = bill.billAmount,
                            currency = "INR",
                            deptVenueId = bill.deptVenueId,
                            paymentType = "Reverse",
                            note = "Rejected Bill :: Unique Id: - " + bill.id,
                            status = true
                        };
                        _context.mz_expense_deptvenue_cash_wallet.Add(c);
                    }

                    List<mz_expense_budget_araz> budgetList = _context
                        .mz_expense_budget_araz.Where(x =>
                            x.deptVenueId == bill.deptVenueId
                            && x.psetId == bill.psetId
                            && x.baseItemId == bill.baseItemId
                            && x.financialYear == bill.financialYear
                        )
                        .ToList();
                    List<mz_expense_bill_item> itemList = _context
                        .mz_expense_bill_item.Where(x => x.billId == bill.id)
                        .ToList();
                    List<mz_expense_budget_araz_monthly> budgetMonth = _context.mz_expense_budget_araz_monthly.ToList();
                    foreach (mz_expense_bill_item item in itemList)
                    {
                        mz_expense_budget_araz budgetItem = budgetList.Where(x => x.itemId == item.itemId).FirstOrDefault();
                        if (budgetItem != null)
                        {
                            mz_expense_budget_araz_monthly monthBudget = budgetMonth.Where(x => x.budget_araz_id == budgetItem.id && int.Parse(x.month_num.Replace("Month ", "")) == numMonth).FirstOrDefault();

                            float amountWithoutGst = (float)(
                bill.billAmount - bill.gstAmount - bill.conveyanceAmount
            );
                            float gstItemAmount =
                                ((bill.gstAmount ?? 0) / amountWithoutGst) * (item.quantity * item.amountPerPc)
                                ?? 0.0f;
                            float conveyanceItemAmount =
                                ((bill.conveyanceAmount ?? 0) / amountWithoutGst)
                                    * item.quantity
                                    * item.amountPerPc
                                ?? 0.0f;

                            float totalDedactable = (float)
                                Math.Round(
                                    (item.quantity * item.amountPerPc) + gstItemAmount + conveyanceItemAmount
                                        ?? 0.0f
                                );

                            //var totDed = ((item.quantity * item.amountPerPc) + item.gstAmount + bill.conveyanceAmount);
                            monthBudget.consumedAmount -= (float)totalDedactable;
                            monthBudget.consumedQuantity -= (float) item.quantity;
                            //foreach(var monthlies in monthBudget)
                            //{

                            //}
                            budgetItem.consumedAmount -= (int)totalDedactable;
                            budgetItem.consumedQty -= (int)item.quantity;
                        }
                        model.amountPerUom = item.amountPerPc ?? 0;
                        model.quantity = item.quantity ?? 0;
                        model.deptVenueId = bill.deptVenueId ?? 0;
                        model.baseItemId = bill.baseItemId ?? 0;
                        model.itemId = item.itemId ?? 0;
                        model.totalAmount = bill.billAmount ?? 0;
                        model.gstAmount = bill.gstAmount ?? 0;
                        model.conveyanceAmount = bill.conveyanceAmount ?? 0;
                        model.createdBy = authUser.Name;

                        String res = _budgetArazService.consumeBudgetResourse(
                            authUser,
                            model,
                            bill.financialYear ?? 2024,
                            false
                        );
                    }
                }
                _context.SaveChanges();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("billstep1getBillsForapprovedorrejected/{type}")]
        [HttpGet]
        public async Task<IActionResult> Bill_Step1_getBillsForapprovedorrejected(string type)
        {
            string api = "billstep1getBillsForapprovedorrejected";
            //// Add_ApiLogs(api);


            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<BillManagementModel> model = new List<BillManagementModel>();

                List<BillManagementModel> billsModel = new List<BillManagementModel>();

                List<user_dept_venue_baseitem> udvbi = _context
                    .user_dept_venue_baseitem.Where(x => x.itsId == authUser.ItsId)
                    .ToList();

                List<int> dvIds = udvbi.Select(x => x.dept_venueId ?? 0).ToList();
                List<int> biIds = udvbi.Select(x => x.baseItemId ?? 0).ToList();

                int financialYear = _helperService.GetFinancialYear(DateTime.UtcNow);

                List<mz_expense_bill_master> allBills = _context
                    .mz_expense_bill_master.Where(x =>
                        dvIds.Contains(x.deptVenueId ?? 0)
                        && biIds.Contains(x.baseItemId ?? 0)
                        && x.status == type
                        && x.isWaived == false
                        && x.financialYear == financialYear
                    )
                    .ToList();

                List<mz_expense_procurement_baseitem> allbaseItems = _context
                    .mz_expense_procurement_baseitem.Where(x => biIds.Contains(x.id))
                    .ToList();

                List<dept_venue> alldv = _context
                    .dept_venue.Include(x => x.registrationform_dropdown_set)
                    .Where(x => dvIds.Contains(x.id))
                    .ToList();

                List<int> vendorIds = allBills.Select(x => x.vendorId ?? 0).ToList();

                List<int> userIds = allBills.Select(x => Convert.ToInt32(x.createdBy)).ToList();

                List<mz_expense_vendor_master> vendors = _context
                    .mz_expense_vendor_master.Where(x => vendorIds.Contains(x.id))
                    .ToList();

                List<user> allusers = _context.user.Where(x => userIds.Contains(x.ItsId)).ToList();

                //List<registrationform_dropdown_set> rds = _context.registrationform_dropdown_set.ToList();

                List<registrationform_subprograms> sbs = _context.registrationform_subprograms.ToList();

                foreach (var bill in allBills)
                {
                    mz_expense_procurement_baseitem item = allbaseItems
                            .Where(x => x.id == bill.baseItemId)
                            .FirstOrDefault();
                    dept_venue dv = alldv.Where(x => x.id == bill.deptVenueId).FirstOrDefault();
                    mz_expense_vendor_master vendor = vendors
                        .Where(x => x.id == bill.vendorId)
                        .FirstOrDefault();
                    user u = allusers.Where(x => x.ItsId == authUser.ItsId).FirstOrDefault();

                    //var rd = rds.Where(x => x.deptVenueId == dv.id).ToList();

                    foreach(var rd in dv.registrationform_dropdown_set.Where(x => x.id == bill.psetId))
                    {
                        var sb = sbs.Where(x => x.id == rd.subprogramId).FirstOrDefault();

                        billsModel.Add(
                            new BillManagementModel
                            {
                                id = bill.id,
                                baseItemId = bill.baseItemId ?? 0,
                                billDate = bill.billDate ?? DateOnly.FromDateTime(DateTime.Today),
                                billNumber = bill.billNo,
                                deptVenueId = bill.deptVenueId ?? 0,
                                totalAmount = bill.billAmount ?? 0,
                                vendorId = bill.vendorId ?? 0,
                                baseItemName = item.name,
                                deptVenueName = dv.deptName + "_" + dv.venueName + "_" + sb.name,
                                deptName = dv.deptName,
                                venueName = dv.venueName,
                                vendorName = vendor.name,
                                createdOn = bill.createdOn ?? DateTime.Now,
                                createdBy = u?.Username,
                                paymentTo_AccName = bill.paymentTo_AccName,
                                paymentTo_AccNum = bill.paymentTo_AccNum,
                                paymentTo_BankName = bill.paymentTo_BankName,
                                paymentTo_ifsc = bill.paymentTo_ifsc,
                                isWaived = bill.isWaived,
                                paymentMode_User = bill.paymentMode_User,
                                status = bill.status,
                                paymentMode_Admin = bill.paymentMode_Admin,
                                gstAmount = bill.gstAmount,
                                gstPercentage = bill.gstPercentage,
                                isReconciled = bill.isReconciled,
                                tdsAmount = bill.tdsAmount ?? 0,
                                tdsApplicableAmount = bill.tdsApplicableAmount ?? 0,
                                tdsPercentage = bill.tdsPercentage,
                                conveyanceAmount = bill.conveyanceAmount,
                                billAttachment = bill.billAttachment
                            }
                        );


                    }                        
                }

                //foreach (var j in udvbi)
                //{
                //    List<mz_expense_bill_master> bills = allBills
                //        .Where(x => x.deptVenueId == j.dept_venueId && x.baseItemId == j.baseItemId)
                //        .ToList();

                //    foreach (var i in bills)
                //    {
                //        mz_expense_procurement_baseitem item = allbaseItems
                //            .Where(x => x.id == i.baseItemId)
                //            .FirstOrDefault();
                //        dept_venue dv = alldv.Where(x => x.id == i.deptVenueId).FirstOrDefault();
                //        mz_expense_vendor_master vendor = vendors
                //            .Where(x => x.id == i.vendorId)
                //            .FirstOrDefault();
                //        int itsid = Convert.ToInt32(i.createdBy);
                //        user u = allusers.Where(x => x.ItsId == itsid).FirstOrDefault();
                //        billsModel.Add(
                //            new BillManagementModel
                //            {
                //                id = i.id,
                //                baseItemId = i.baseItemId ?? 0,
                //                billDate = i.billDate ?? DateOnly.FromDateTime(DateTime.Today),
                //                billNumber = i.billNo,
                //                deptVenueId = i.deptVenueId ?? 0,
                //                totalAmount = i.billAmount ?? 0,
                //                vendorId = i.vendorId ?? 0,
                //                baseItemName = item.name,
                //                deptVenueName = dv.deptName + "_" + dv.venueName,
                //                deptName = dv.deptName,
                //                venueName = dv.venueName,
                //                vendorName = vendor.name,
                //                createdOn = i.createdOn ?? DateTime.Now,
                //                createdBy = u?.Username,
                //                paymentTo_AccName = i.paymentTo_AccName,
                //                paymentTo_AccNum = i.paymentTo_AccNum,
                //                paymentTo_BankName = i.paymentTo_BankName,
                //                paymentTo_ifsc = i.paymentTo_ifsc,
                //                isWaived = i.isWaived,
                //                paymentMode_User = i.paymentMode_User,
                //                status = i.status,
                //                paymentMode_Admin = i.paymentMode_Admin,
                //                gstAmount = i.gstAmount,
                //                gstPercentage = i.gstPercentage,
                //                isReconciled = i.isReconciled,
                //                tdsAmount = i.tdsAmount ?? 0,
                //                tdsApplicableAmount = i.tdsApplicableAmount ?? 0,
                //                tdsPercentage = i.tdsPercentage,
                //                conveyanceAmount = i.conveyanceAmount,
                //                billAttachment = i.billAttachment
                //            }
                //        );
                //    }
                //}

                billsModel = billsModel.OrderBy(x => x.id).ToList();

                return Ok(billsModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("billstep2getApprovedBills")]
        [HttpGet]
        public async Task<IActionResult> Bill_Step2_billstep2getApprovedBills()
        {
            string api = "billstep2getApprovedBills";
            //// Add_ApiLogs(api);


            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<BillManagementModel> model = new List<BillManagementModel>();

                List<BillManagementModel> billsModel = new List<BillManagementModel>();

                List<user_dept_venue_baseitem> udvbi = _context
                    .user_dept_venue_baseitem.Where(x => x.itsId == authUser.ItsId)
                    .ToList();

                List<int> dvIds = udvbi.Select(x => x.dept_venueId ?? 0).ToList();
                List<int> biIds = udvbi.Select(x => x.baseItemId ?? 0).ToList();

                int financialYear = _helperService.GetFinancialYear(DateTime.UtcNow);

                List<mz_expense_bill_master> allBills = _context
                    .mz_expense_bill_master.Where(x =>
                        dvIds.Contains(x.deptVenueId ?? 0)
                        && biIds.Contains(x.baseItemId ?? 0)
                        && x.status == "Approved"
                        && x.isWaived == false
                        && x.financialYear == financialYear
                    )
                    .ToList();

                List<mz_expense_procurement_baseitem> allbaseItems = _context
                    .mz_expense_procurement_baseitem.Where(x => biIds.Contains(x.id))
                    .ToList();
                List<dept_venue> alldv = _context
                    .dept_venue.Where(x => dvIds.Contains(x.id))
                    .ToList();

                List<int> vendorIds = allBills.Select(x => x.vendorId ?? 0).ToList();
                List<int> userIds = allBills.Select(x => Convert.ToInt32(x.createdBy)).ToList();

                List<mz_expense_vendor_master> vendors = _context
                    .mz_expense_vendor_master.Where(x => vendorIds.Contains(x.id))
                    .ToList();
                List<user> allusers = _context.user.Where(x => userIds.Contains(x.ItsId)).ToList();

                foreach (var j in udvbi)
                {
                    List<mz_expense_bill_master> bills = allBills
                        .Where(x =>
                            x.deptVenueId == j.dept_venueId
                            && x.baseItemId == j.baseItemId
                            && x.status == "Approved"
                            && x.isWaived == false
                        )
                        .ToList();

                    foreach (var i in bills)
                    {
                        mz_expense_procurement_baseitem item = allbaseItems
                            .Where(x => x.id == i.baseItemId)
                            .FirstOrDefault();
                        dept_venue dv = alldv.Where(x => x.id == i.deptVenueId).FirstOrDefault();
                        mz_expense_vendor_master vendor = vendors
                            .Where(x => x.id == i.vendorId)
                            .FirstOrDefault();
                        int itsid = Convert.ToInt32(i.createdBy);
                        user u = allusers.Where(x => x.ItsId == itsid).FirstOrDefault();
                        billsModel.Add(
                            new BillManagementModel
                            {
                                id = i.id,
                                baseItemId = i.baseItemId ?? 0,
                                billDate = i.billDate ?? DateOnly.FromDateTime(DateTime.Today),
                                billNumber = i.billNo,
                                deptVenueId = i.deptVenueId ?? 0,
                                totalAmount = i.billAmount ?? 0,
                                vendorId = i.vendorId ?? 0,
                                baseItemName = item.name,
                                deptVenueName = dv.deptName + "_" + dv.venueName,
                                deptName = dv.deptName,
                                venueName = dv.venueName,
                                vendorName = vendor.name,
                                createdOn = i.createdOn ?? DateTime.Now,
                                createdBy = u?.Username,
                                paymentTo_AccName = i.paymentTo_AccName,
                                paymentTo_AccNum = i.paymentTo_AccNum,
                                paymentTo_BankName = i.paymentTo_BankName,
                                paymentTo_ifsc = i.paymentTo_ifsc,
                                isWaived = i.isWaived,
                                paymentMode_User = i.paymentMode_User,
                                status = i.status,
                                paymentMode_Admin = i.paymentMode_Admin,
                                gstAmount = i.gstAmount,
                                gstPercentage = i.gstPercentage,
                                isReconciled = i.isReconciled,
                                tdsAmount = i.tdsAmount,
                                tdsApplicableAmount = i.tdsApplicableAmount,
                                tdsPercentage = i.tdsPercentage,
                                conveyanceAmount = i.conveyanceAmount,
                                billAttachment = i.billAttachment
                            }
                        );
                    }
                }

                billsModel = billsModel.OrderBy(x => x.id).ToList();

                return Ok(billsModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("billstep2getToPaidBills")]
        [HttpGet]
        public async Task<IActionResult> Bill_Step2_billstep3getToBePaidBills()
        {
            string api = "billstep2getToPaidBills";
            //// Add_ApiLogs(api);


            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<PackagePaymentModel> model = new List<PackagePaymentModel>();

                int financialYear = _helperService.GetFinancialYear(DateTime.UtcNow);
                List<mz_expense_bill_master> bills = _context
                    .mz_expense_bill_master.Where(x =>
                        x.status == "ToBePaid"
                        && x.isWaived == false
                        && x.financialYear == financialYear
                    )
                    .ToList();

                List<int> dvIds = bills.Select(x => x.deptVenueId ?? 0).ToList();
                List<int> biIds = bills.Select(x => x.baseItemId ?? 0).ToList();

                List<mz_expense_procurement_baseitem> allbaseItems = _context
                    .mz_expense_procurement_baseitem.Where(x => biIds.Contains(x.id))
                    .ToList();
                List<dept_venue> alldv = _context
                    .dept_venue.Where(x => dvIds.Contains(x.id))
                    .ToList();

                List<int> vendorIds = bills.Select(x => x.vendorId ?? 0).ToList();
                List<int> userIds = bills.Select(x => Convert.ToInt32(x.createdBy)).ToList();

                List<mz_expense_vendor_master> vendors = _context
                    .mz_expense_vendor_master.Where(x => vendorIds.Contains(x.id))
                    .ToList();
                List<user> allusers = _context.user.Where(x => userIds.Contains(x.ItsId)).ToList();

                List<int> packages = new List<int>();

                foreach (var i in bills)
                {
                    packages.Add(i.packageId ?? 0);
                }

                packages = packages.Distinct().ToList();

                List<mz_expense_bills_package> allPacages = _context
                    .mz_expense_bills_package.Where(x => packages.Contains(x.id))
                    .ToList();

                foreach (var j in allPacages)
                {
                    try
                    {
                        List<mz_expense_bill_master> bill = bills
                            .Where(x => x.packageId == j.id)
                            .ToList();
                        List<BillManagementModel> billsModel = new List<BillManagementModel>();

                        foreach (var i in bill)
                        {
                            mz_expense_procurement_baseitem item = allbaseItems
                                .Where(x => x.id == i.baseItemId)
                                .FirstOrDefault();
                            dept_venue dv = alldv
                                .Where(x => x.id == i.deptVenueId)
                                .FirstOrDefault();
                            mz_expense_vendor_master vendor = vendors
                                .Where(x => x.id == i.vendorId)
                                .FirstOrDefault();
                            int itsid = Convert.ToInt32(i.createdBy);
                            user u = allusers.Where(x => x.ItsId == itsid).FirstOrDefault();
                            billsModel.Add(
                                new BillManagementModel
                                {
                                    packageId = i.packageId,
                                    id = i.id,
                                    baseItemId = i.baseItemId ?? 0,
                                    billDate = i.billDate ?? DateOnly.FromDateTime(DateTime.Today),
                                    billNumber = i.billNo,
                                    deptVenueId = i.deptVenueId ?? 0,
                                    totalAmount = i.billAmount ?? 0,
                                    vendorId = i.vendorId ?? 0,
                                    baseItemName = item.name,
                                    deptVenueName = dv.deptName + "_" + dv.venueName,
                                    deptName = dv.deptName,
                                    venueName = dv.venueName,
                                    vendorName = vendor.name,
                                    createdOn = i.createdOn ?? DateTime.Now,
                                    createdBy = u?.Username,
                                    paymentTo_AccName = i.paymentTo_AccName,
                                    paymentTo_AccNum = i.paymentTo_AccNum,
                                    paymentTo_BankName = i.paymentTo_BankName,
                                    paymentTo_ifsc = i.paymentTo_ifsc,
                                    isWaived = i.isWaived,
                                    paymentMode_User = i.paymentMode_User,
                                    status = i.status,
                                    paymentMode_Admin = i.paymentMode_Admin,
                                    gstAmount = i.gstAmount,
                                    gstPercentage = i.gstPercentage,
                                    isReconciled = i.isReconciled,
                                    tdsAmount = i.tdsAmount,
                                    tdsApplicableAmount = i.tdsApplicableAmount,
                                    tdsPercentage = i.tdsPercentage,
                                    conveyanceAmount = i.conveyanceAmount,
                                    paymentFrom_BankName = i.paymentFrom_BankName,
                                    billAttachment = i.billAttachment
                                }
                            );
                        }

                        model.Add(
                            new PackagePaymentModel
                            {
                                id = j.id,
                                name = j.name,
                                bills = billsModel
                            }
                        );
                    }
                    catch (Exception ex) { }
                }

                model = model.OrderBy(x => x.id).ToList();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getallbills")]
        [HttpGet]
        public async Task<IActionResult> getallbills()
        {
            string api = "getallbills";
            if (Request != null)
            {
                //// Add_ApiLogs(api);
            }

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<BillManagementModel> model = new List<BillManagementModel>();

                List<user_dept_venue_baseitem> udvbi = _context
                    .user_dept_venue_baseitem.Where(x => x.itsId == authUser.ItsId)
                    .ToList();
                List<mz_expense_bill_master> allBills = _context.mz_expense_bill_master.ToList();
                List<dept_venue> allDV = _context.dept_venue.ToList();
                List<mz_expense_vendor_master> allVendors =
                    _context.mz_expense_vendor_master.ToList();
                List<mz_expense_procurement_baseitem> allBaseItems =
                    _context.mz_expense_procurement_baseitem.ToList();
                List<BillItemListModel> allItems = (
                    from bi in _context.mz_expense_bill_item
                    join pi in _context.mz_expense_procurement_item on bi.itemId equals pi.id
                    select new BillItemListModel
                    {
                        itemName = pi.name,
                        remark = bi.remarks,
                        billId = bi.billId
                    }
                ).ToList();
                List<mz_expense_vendor_transaction> vendorTransactions =
                    _context.mz_expense_vendor_transaction.ToList();
                List<mz_expense_vendor_payment> vendorPayments =
                    _context.mz_expense_vendor_payment.ToList();

                List<user> allusers = _context.user.ToList();

                foreach (var j in udvbi)
                {
                    List<BillManagementModel> savedBills = new List<BillManagementModel>();

                    //savedBills = cache.GetItem<List<BillManagementModel>>("getBillin-" + j.dept_venueId.ToString() + "-" + j.baseItemId.ToString());

                    if (savedBills.Count > 0)
                    {
                        savedBills.ForEach(x => model.Add(x));
                        continue;
                    }

                    List<mz_expense_bill_master> bills = allBills
                        .Where(x => x.deptVenueId == j.dept_venueId && x.psetId == j.psetId && x.baseItemId == j.baseItemId)
                        .ToList();
                    List<BillManagementModel> tobeCached = new List<BillManagementModel>();

                    foreach (mz_expense_bill_master i in bills)
                    {
                        mz_expense_procurement_baseitem item = allBaseItems
                            .Where(x => x.id == i.baseItemId)
                            .FirstOrDefault();
                        dept_venue dv = allDV.Where(x => x.id == i.deptVenueId).FirstOrDefault();
                        mz_expense_vendor_master vendor = allVendors
                            .Where(x => x.id == i.vendorId)
                            .FirstOrDefault();
                        int itsid = Convert.ToInt32(i.createdBy);
                        user u = allusers.Where(x => x.ItsId == itsid).FirstOrDefault();

                        var items = allItems.Where(x => x.billId == i.id).ToList();

                        string item1 = "";
                        string remarks1 = "";

                        foreach (var k in items)
                        {
                            item1 = item1 + " || " + k.itemName;
                            remarks1 = remarks1 + " || " + k.remark;
                        }

                        mz_expense_vendor_transaction payment = vendorTransactions
                            .Where(x => x.billId == i.id && x.debit != null && x.paymentId != null)
                            .FirstOrDefault();

                        BillManagementModel m = new BillManagementModel();

                        if (payment != null)
                        {
                            mz_expense_vendor_payment actualPayment = vendorPayments
                                .Where(x => x.id == payment.paymentId)
                                .FirstOrDefault();
                            m.paymentDate = actualPayment.paymentDate ?? dateOnly;
                            m.paymentDateString = (actualPayment.paymentDate ?? dateOnly).ToString(
                                "dd-MM-yyyy"
                            );
                            m.txnId = payment.transactionId;
                        }

                        m.billDateString = (i.billDate ?? dateOnly).ToString("dd-MM-yyyy");
                        m.entryDateString = (i.createdOn ?? DateTime.Today).ToString("dd-MM-yyyy");
                        m.remarks = remarks1;
                        m.itemName = item1;
                        m.isFundRequestedString =
                            (i.isFundRequested ?? false) ? "Requested" : "Not Requested";
                        m.isFundRequested = i.isFundRequested ?? false;
                        m.id = i.id;
                        m.baseItemId = i.baseItemId ?? 0;
                        m.billDate = i.billDate ?? dateOnly;
                        m.billNumber = i.billNo;
                        m.deptVenueId = i.deptVenueId ?? 0;
                        m.totalAmount = i.billAmount ?? 0;
                        m.vendorId = i.vendorId ?? 0;
                        m.baseItemName = item.name;
                        m.deptVenueName = dv.deptName + "_" + dv.venueName;
                        m.deptName = dv.deptName;
                        m.venueName = dv.venueName;
                        m.vendorName = vendor.name;
                        m.createdOn = i.createdOn ?? DateTime.Now;
                        m.createdBy = u?.Username;
                        m.paymentTo_AccName = i.paymentTo_AccName;
                        m.paymentTo_AccNum = i.paymentTo_AccNum;
                        m.paymentTo_BankName = i.paymentTo_BankName;
                        m.paymentTo_ifsc = i.paymentTo_ifsc;
                        m.isWaived = i.isWaived ?? false;
                        m.paymentMode_User = i.paymentMode_User;
                        m.status = i.status;
                        m.paymentMode_Admin = i.paymentMode_Admin;
                        m.gstAmount = i?.gstAmount;
                        m.gstPercentage = i.gstPercentage ?? 0;
                        m.isReconciled = i.isReconciled ?? false;
                        m.tdsAmount = i?.tdsAmount;
                        m.tdsApplicableAmount = i?.tdsApplicableAmount;
                        m.tdsPercentage = i?.tdsPercentage;
                        m.conveyanceAmount = i?.conveyanceAmount;
                        m.billAttachment = i.billAttachment;
                        model.Add(m);
                        tobeCached.Add(m);
                    }
                    //cache.AddItem("getBillin-" + j.dept_venueId.ToString() + "-" + j.baseItemId.ToString(), tobeCached, DateTime.Now.AddDays(30));
                }

                model = model.OrderBy(x => x.id).ToList();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getbillsofyear/{financialYear}")]
        [HttpGet]
        public async Task<IActionResult> getBillsOfYear(int financialYear)
        {
            string api = "getallbills";

            string currentBillId = "";
            string step = "";

            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                List<BillManagementModel> model = new List<BillManagementModel>();

                List<user_dept_venue_baseitem> udvbi = _context
                    .user_dept_venue_baseitem.Where(x => x.itsId == authUser.ItsId)
                    .ToList();

                List<mz_expense_bill_master> allBills = _context
                    .mz_expense_bill_master.Where(x => x.financialYear == financialYear)
                    .ToList();

                List<dept_venue> allDV = _context.dept_venue.ToList();

                List<mz_expense_vendor_master> allVendors =
                    _context.mz_expense_vendor_master.ToList();

                List<mz_expense_procurement_baseitem> allBaseItems =
                    _context.mz_expense_procurement_baseitem.ToList();

                List<BillItemListModel> allItems = _context
                    .mz_expense_bill_item.Include(x => x.item)
                    .Select(x => new BillItemListModel
                    {
                        itemName = x.item.name,
                        remark = x.remarks,
                        billId = x.billId
                    })
                    .ToList();

                List<registrationform_dropdown_set> rds = _context.registrationform_dropdown_set.ToList();

                List<registrationform_subprograms> rs = _context.registrationform_subprograms.ToList();

                List<mz_expense_vendor_transaction> vendorTransactions =
                    _context.mz_expense_vendor_transaction.ToList();

                List<mz_expense_vendor_payment> vendorPayments =
                    _context.mz_expense_vendor_payment.ToList();

                List<user> allusers = _context.user.ToList();

                step = "1";
                foreach (var j in udvbi)
                {
                    // List<BillManagementModel> savedBills = new List<BillManagementModel>();
                    List<BillManagementModel> savedBills = null;

                    if (
                        savedBills != null
                        && financialYear == _helperService.GetFinancialYear(DateTime.Now)
                    )
                    {
                        savedBills.ForEach(x => model.Add(x));
                        continue;
                    }

                    List<mz_expense_bill_master> bills = allBills
                        .Where(x => x.deptVenueId == j.dept_venueId && x.psetId == j.psetId && x.baseItemId == j.baseItemId)
                        .ToList();
                    List<BillManagementModel> tobeCached = new List<BillManagementModel>();

                    foreach (mz_expense_bill_master i in bills)
                    {
                        currentBillId = i.id.ToString();
                        mz_expense_procurement_baseitem item = allBaseItems
                            .Where(x => x.id == i.baseItemId)
                            .FirstOrDefault();
                        dept_venue dv = allDV.Where(x => x.id == i.deptVenueId).FirstOrDefault();
                        mz_expense_vendor_master vendor = allVendors
                            .Where(x => x.id == i.vendorId)
                            .FirstOrDefault();
                        int itsid = Convert.ToInt32(i.createdBy);
                        user u = allusers.Where(x => x.ItsId == itsid).FirstOrDefault();

                        registrationform_dropdown_set rd = rds.Where(x => x.id == i.psetId).FirstOrDefault();

                        registrationform_subprograms r = rs.Where(x => x.id == rd.subprogramId).FirstOrDefault();

                        var items = allItems.Where(x => x.billId == i.id).ToList();

                        string item1 = "";
                        string remarks1 = "";
                        step = "2";

                        foreach (var k in items)
                        {
                            item1 = item1 + " || " + k.itemName;
                            remarks1 = remarks1 + " || " + k.remark;
                        }

                        step = "3";

                        mz_expense_vendor_transaction payment = vendorTransactions
                            .Where(x => x.billId == i.id && x.debit != null && x.paymentId != null)
                            .FirstOrDefault();

                        BillManagementModel m = new BillManagementModel();

                        if (payment != null)
                        {
                            mz_expense_vendor_payment actualPayment = vendorPayments
                                .Where(x => x.id == payment.paymentId)
                                .FirstOrDefault();
                            m.paymentDate = actualPayment.paymentDate ?? dateOnly;
                            m.paymentDateString = (actualPayment.paymentDate ?? dateOnly).ToString(
                                "dd-MM-yyyy"
                            );
                            m.txnId = payment.transactionId;
                        }
                        step = "4";

                        m.billDateString = (i.billDate ?? dateOnly).ToString("dd-MM-yyyy");
                        m.entryDateString = (i.createdOn ?? DateTime.Today).ToString("dd-MM-yyyy");
                        m.remarks = remarks1;
                        m.itemName = item1;
                        m.isFundRequestedString =
                            (i.isFundRequested ?? false) ? "Requested" : "Not Requested";
                        m.isFundRequested = i.isFundRequested ?? false;
                        m.id = i.id;
                        m.baseItemId = i.baseItemId ?? 0;
                        m.billDate = i.billDate ?? dateOnly;
                        m.billNumber = i.billNo;
                        m.deptVenueId = i.deptVenueId ?? 0;
                        m.totalAmount = i.billAmount ?? 0;
                        m.vendorId = i.vendorId ?? 0;
                        m.baseItemName = item.name;
                        m.deptVenueName = dv.deptName + "_" + dv.venueName;
                        m.deptName = dv.deptName;
                        m.venueName = dv.venueName;
                        m.vendorName = vendor.name;
                        m.className = r.name;
                        m.createdOn = i.createdOn ?? DateTime.Now;
                        step = "5";
                        m.createdBy = u?.Username;
                        m.paymentTo_AccName = i.paymentTo_AccName;
                        m.paymentTo_AccNum = i.paymentTo_AccNum;
                        m.paymentTo_BankName = i.paymentTo_BankName;
                        m.paymentTo_ifsc = i.paymentTo_ifsc;
                        m.isWaived = i.isWaived ?? false;
                        m.paymentMode_User = i.paymentMode_User;
                        m.status = i.status;
                        m.paymentMode_Admin = i.paymentMode_Admin;
                        m.gstAmount = i?.gstAmount;
                        m.gstPercentage = i.gstPercentage ?? 0;
                        m.isReconciled = i.isReconciled ?? false;
                        m.tdsAmount = i?.tdsAmount;
                        m.tdsApplicableAmount = i?.tdsApplicableAmount;
                        m.tdsPercentage = i?.tdsPercentage;
                        m.conveyanceAmount = i?.conveyanceAmount;
                        m.billAttachment = i.billAttachment;
                        model.Add(m);
                        tobeCached.Add(m);
                    }
                }

                model = model.OrderBy(x => x.id).ToList();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    ex.ToString() + "last bill: " + currentBillId + "failed after step: " + step
                );
            }
        }

        [Route("getbillsbystatus/{status}")]
        [HttpGet]
        public async Task<IActionResult> getallbills(string status)
        {
            string api = "getbillsbystatus/{status}";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
                List<BillManagementModel> model = new List<BillManagementModel>();

                status = char.ToUpper(status[0]) + status.Substring(1);

                if (
                    !(
                        status == "Approved"
                        || status == "Pending"
                        || status == "Paid"
                        || status == "Rejected"
                    )
                )
                {
                    return BadRequest(new { message = " Status not registered in the application " });
                }

                List<user_dept_venue_baseitem> udvbi = _context
                    .user_dept_venue_baseitem.Where(x => x.itsId == authUser.ItsId)
                    .ToList();

                foreach (var j in udvbi)
                {
                    List<mz_expense_bill_master> bills = _context
                        .mz_expense_bill_master.Where(x =>
                            x.deptVenueId == j.dept_venueId
                            && x.baseItemId == j.baseItemId
                            && x.status == status
                        )
                        .ToList();

                    foreach (var i in bills)
                    {
                        mz_expense_procurement_baseitem item = _context
                            .mz_expense_procurement_baseitem.Where(x => x.id == i.baseItemId)
                            .FirstOrDefault();
                        dept_venue dv = _context
                            .dept_venue.Where(x => x.id == i.deptVenueId)
                            .FirstOrDefault();
                        mz_expense_vendor_master vendor = _context
                            .mz_expense_vendor_master.Where(x => x.id == i.vendorId)
                            .FirstOrDefault();
                        int itsid = Convert.ToInt32(i.createdBy);
                        user u = _context.user.Where(x => x.ItsId == itsid).FirstOrDefault();
                        var items = _context
                            .mz_expense_bill_item.Where(x => x.billId == i.id)
                            .ToList();

                        string item1 = "";
                        string remarks1 = "";

                        foreach (var k in items)
                        {
                            var ii = _context
                                .mz_expense_procurement_item.Where(x => x.id == k.itemId)
                                .FirstOrDefault();

                            item1 = item1 + " || " + ii.name;
                            remarks1 = remarks1 + " || " + k.remarks;
                        }
                        mz_expense_vendor_transaction payment = _context
                            .mz_expense_vendor_transaction.Where(x =>
                                x.billId == i.id && x.debit != null && x.paymentId != null
                            )
                            .FirstOrDefault();

                        if (payment != null)
                        {
                            model.Add(
                                new BillManagementModel
                                {
                                    billDateString = (i.billDate ?? dateOnly).ToString(
                                        "dd-MM-yyyy"
                                    ),
                                    entryDateString = (i.createdOn ?? DateTime.Today).ToString(
                                        "dd-MM-yyyy"
                                    ),
                                    paymentDate = DateOnly.FromDateTime(
                                        payment.createdOn ?? DateTime.MinValue
                                    ),
                                    paymentDateString = (
                                        payment.createdOn ?? DateTime.Today
                                    ).ToString("dd-MM-yyyy"),
                                    remarks = remarks1,
                                    itemName = item1,
                                    isFundRequestedString =
                                        (i.isFundRequested ?? false)
                                            ? "Requested"
                                            : "Not Requested",
                                    isFundRequested = i.isFundRequested ?? false,
                                    id = i.id,
                                    baseItemId = i.baseItemId ?? 0,
                                    billDate = i.billDate ?? dateOnly,
                                    billNumber = i.billNo,
                                    deptVenueId = i.deptVenueId ?? 0,
                                    totalAmount = i.billAmount ?? 0,
                                    vendorId = i.vendorId ?? 0,
                                    baseItemName = item.name,
                                    deptVenueName = dv.deptName + "_" + dv.venueName,
                                    deptName = dv.deptName,
                                    venueName = dv.venueName,
                                    vendorName = vendor.name,
                                    createdOn = i.createdOn ?? DateTime.Now,
                                    createdBy = u?.Username,
                                    paymentTo_AccName = i.paymentTo_AccName,
                                    paymentTo_AccNum = i.paymentTo_AccNum,
                                    paymentTo_BankName = i.paymentTo_BankName,
                                    paymentTo_ifsc = i.paymentTo_ifsc,
                                    isWaived = i.isWaived,
                                    paymentMode_User = i.paymentMode_User,
                                    status = i.status,
                                    paymentMode_Admin = i.paymentMode_Admin,
                                    gstAmount = i.gstAmount,
                                    gstPercentage = i.gstPercentage,
                                    isReconciled = i.isReconciled,
                                    tdsAmount = i.tdsAmount ?? 0,
                                    tdsApplicableAmount = i.tdsApplicableAmount ?? 0,
                                    tdsPercentage = i.tdsPercentage,
                                    conveyanceAmount = i.conveyanceAmount
                                }
                            );
                        }
                        else
                        {
                            model.Add(
                                new BillManagementModel
                                {
                                    billDateString = (i.billDate ?? dateOnly).ToString(
                                        "dd-MM-yyyy"
                                    ),
                                    entryDateString = (i.createdOn ?? DateTime.Today).ToString(
                                        "dd-MM-yyyy"
                                    ),
                                    remarks = remarks1,
                                    itemName = item1,
                                    isFundRequestedString =
                                        (i.isFundRequested ?? false)
                                            ? "Requested"
                                            : "Not Requested",
                                    isFundRequested = i.isFundRequested ?? false,
                                    id = i.id,
                                    baseItemId = i.baseItemId ?? 0,
                                    billDate = i.billDate ?? dateOnly,
                                    billNumber = i.billNo,
                                    deptVenueId = i.deptVenueId ?? 0,
                                    totalAmount = i.billAmount ?? 0,
                                    vendorId = i.vendorId ?? 0,
                                    baseItemName = item.name,
                                    deptVenueName = dv.deptName + "_" + dv.venueName,
                                    deptName = dv.deptName,
                                    venueName = dv.venueName,
                                    vendorName = vendor.name,
                                    createdOn = i.createdOn ?? DateTime.Now,
                                    createdBy = u?.Username,
                                    paymentTo_AccName = i.paymentTo_AccName,
                                    paymentTo_AccNum = i.paymentTo_AccNum,
                                    paymentTo_BankName = i.paymentTo_BankName,
                                    paymentTo_ifsc = i.paymentTo_ifsc,
                                    isWaived = i.isWaived,
                                    paymentMode_User = i.paymentMode_User,
                                    status = i.status,
                                    paymentMode_Admin = i.paymentMode_Admin,
                                    gstAmount = i.gstAmount,
                                    gstPercentage = i.gstPercentage,
                                    isReconciled = i.isReconciled,
                                    tdsAmount = i.tdsAmount ?? 0,
                                    tdsApplicableAmount = i.tdsApplicableAmount ?? 0,
                                    tdsPercentage = i.tdsPercentage,
                                    conveyanceAmount = i.conveyanceAmount,
                                    billAttachment = i.billAttachment
                                }
                            );
                        }
                    }
                }

                model = model.OrderBy(x => x.id).ToList();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getallbillsbydate")]
        [HttpGet]
        public async Task<IActionResult> getallbillsbydate()
        {
            string api = "getallbillsbydate";

            DateRange dateRange = new DateRange
            {
                FromDate = DateTime.Parse("01/06/2022"),
                ToDate = DateTime.Today
            };
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
                List<BillManagementModel> model = new List<BillManagementModel>();

                List<user_dept_venue_baseitem> udvbi = _context
                    .user_dept_venue_baseitem.Where(x => x.itsId == authUser.ItsId)
                    .ToList();

                List<BillManagementModel> l = (
                    from bir in _context.user_dept_venue_baseitem
                    where bir.itsId == authUser.ItsId
                    join bills in _context.mz_expense_bill_master
                        on bir.dept_venueId equals bills.deptVenueId
                    where bir.baseItemId == bills.baseItemId
                    join item in _context.mz_expense_procurement_baseitem
                        on bills.baseItemId equals item.id
                    join dv in _context.dept_venue on bills.deptVenueId equals dv.id
                    join vendor in _context.mz_expense_vendor_master
                        on bills.vendorId equals vendor.id
                    join items in _context.mz_expense_bill_item on bills.id equals items.billId
                    join payment in _context.mz_expense_vendor_transaction
                        on bills.id equals payment.billId
                    where
                        (
                            payment.debit != null
                            && payment.paymentId != null
                            && payment.createdOn >= dateRange.FromDate
                            && payment.createdOn <= dateRange.ToDate
                        )
                    select new BillManagementModel
                    {
                        entryDate = DateOnly.FromDateTime(bills.createdOn ?? DateTime.MinValue),
                        paymentDate = DateOnly.FromDateTime(payment.createdOn ?? DateTime.MinValue),
                        remarks = items.remarks,
                        itemName = "",
                        isFundRequestedString =
                            (bills.isFundRequested ?? false) ? "Requested" : "Not Requested",
                        isFundRequested = bills.isFundRequested ?? false,
                        id = bills.id,
                        baseItemId = bills.baseItemId ?? 0,
                        billDate = bills.billDate ?? dateOnly,
                        billNumber = bills.billNo,
                        deptVenueId = bills.deptVenueId ?? 0,
                        totalAmount = bills.billAmount ?? 0,
                        vendorId = bills.vendorId ?? 0,
                        baseItemName = item.name,
                        deptVenueName = dv.deptName + "_" + dv.venueName,
                        deptName = dv.deptName,
                        venueName = dv.venueName,
                        vendorName = vendor.name,
                        createdOn = bills.createdOn ?? DateTime.Now,
                        paymentTo_AccName = bills.paymentTo_AccName,
                        paymentTo_AccNum = bills.paymentTo_AccNum,
                        paymentTo_BankName = bills.paymentTo_BankName,
                        paymentTo_ifsc = bills.paymentTo_ifsc,
                        isWaived = bills.isWaived,
                        paymentMode_User = bills.paymentMode_User,
                        status = bills.status,
                        paymentMode_Admin = bills.paymentMode_Admin,
                        gstAmount = bills.gstAmount,
                        gstPercentage = bills.gstPercentage,
                        isReconciled = bills.isReconciled,
                        tdsAmount = bills.tdsAmount ?? 0,
                        tdsApplicableAmount = bills.tdsApplicableAmount ?? 0,
                        tdsPercentage = bills.tdsPercentage,
                        conveyanceAmount = bills.conveyanceAmount,
                        itemId = items.id,
                        billAttachment = bills.billAttachment
                    }
                ).ToList();

                foreach (BillManagementModel k in l)
                {
                    var ii = _context
                        .mz_expense_procurement_item.Where(x => x.id == k.itemId)
                        .FirstOrDefault();
                    if (ii != null)
                    {
                        k.itemName = k.itemName + " || " + ii.name;
                    }
                    k.billDateString = k.billDate?.ToString("dd-MM-yyyy");
                    k.entryDateString = k.entryDate?.ToString("dd-MM-yyyy");
                }

                return Ok(l);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getpayments")]
        [HttpPost]
        public async Task<IActionResult> getallfeealloted(SearchRecieptModel model)
        {
            string api = "getpayments";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                var itsIds = string.IsNullOrEmpty(model.itsCsv)
                    ? new List<int>()
                    : _helperService.parseIds(model.itsCsv);
                bool dateFilter =
                    model.fromDate != null
                    && model.toDate != null
                    && model.fromDate != DateOnly.MinValue
                    && model.toDate != DateOnly.MinValue;

                var vendorQuery = _context.mz_expense_vendor_master.AsQueryable();
                if (itsIds.Any())
                {
                    vendorQuery = vendorQuery.Where(v => itsIds.Contains(v.id));
                }
                var vendors = await vendorQuery.ToListAsync();

                var receiptsQuery = _context.mz_expense_vendor_payment.AsQueryable();
                if (dateFilter)
                {
                    receiptsQuery = receiptsQuery.Where(x =>
                        x.paymentDate >= model.fromDate && x.paymentDate <= model.toDate
                    );
                }
                if (itsIds.Any())
                {
                    var vendorIds = vendors.Select(v => v.id).ToList();
                    receiptsQuery = receiptsQuery.Where(x => vendorIds.Contains(x.vendorId ?? 0));
                }

                var receipts = await receiptsQuery.ToListAsync();
                var receiptIds = receipts.Select(r => r.id).ToList();
                var transactions = await _context
                    .mz_expense_vendor_transaction.Where(t => receiptIds.Contains(t.paymentId ?? 0))
                    .ToListAsync();
                var billIds = transactions.Select(t => t.billId).Distinct().ToList();
                var allBills = await _context
                    .mz_expense_bill_master.Where(b => billIds.Contains(b.id))
                    .ToListAsync();

                List<RecieptModel> modelnew = receipts
                    .Select(r => new RecieptModel
                    {
                        chequeNo = r.transactionId,
                        createdBy = r.createdBy,
                        paymentMode = r.paymentMode,
                        receiptId = r.id.ToString(),
                        feePaidAmount = r.debit?.ToString(),
                        itsId = vendors.FirstOrDefault(v => v.id == r.vendorId)?.id ?? 0,
                        name = vendors.FirstOrDefault(v => v.id == r.vendorId)?.name ?? "",
                        recieptDate = r.paymentDate ?? dateOnly,
                        note = r.note,
                        status = r.status,
                        account = allBills
                            .FirstOrDefault(b =>
                                transactions.Any(t => t.paymentId == r.id && t.billId == b.id)
                            )
                            ?.paymentFrom_BankName,
                        tdsAmount =
                            allBills
                                .Where(b =>
                                    transactions.Any(t => t.paymentId == r.id && t.billId == b.id)
                                )
                                .Sum(b => b.tdsAmount) ?? 0,
                        id = r.id,
                    })
                    .ToList();

                var paymentModeDD = modelnew
                    .GroupBy(x => x.paymentMode)
                    .Select(x => new dropdown_dataset_options { name = x.Key })
                    .ToList();
                var statusDD = modelnew
                    .GroupBy(x => x.status)
                    .Select(x => new dropdown_dataset_options { name = x.Key })
                    .ToList();

                var exportCategory = await _context
                    .export_category.Where(x => x.categoryId == 15)
                    .ToListAsync();

                return Ok(
                    new feeAllotedResultModel
                    {
                        model = modelnew,
                        exportCategory = exportCategory,
                        paymentModeDD = paymentModeDD,
                        statusDD = statusDD
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getItemsOfBill/{billId}")]
        [HttpGet]
        public async Task<IActionResult> getItemsOfBill(int billId)
        {
            string api = "getItemsOfBill/{billId}";
            //// Add_ApiLogs(api);


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
                List<BillManagementModel> model = new List<BillManagementModel>();

                List<mz_expense_bill_item> bills = _context
                    .mz_expense_bill_item.Where(x => x.billId == billId)
                    .ToList();

                foreach (var i in bills)
                {
                    mz_expense_procurement_item item = _context
                        .mz_expense_procurement_item.Where(x => x.id == i.itemId)
                        .FirstOrDefault();

                    model.Add(
                        new BillManagementModel
                        {
                            id = i.id,
                            itemName = item.name,
                            itemId = item.id,
                            quantity = i.quantity ?? 0,
                            amountPerUom = i.amountPerPc ?? 0,
                            remarks = i.remarks,
                            itemGstAmount = i.gstAmount,
                            itemGstPercentage = i.gstPercentage
                        }
                    );
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getLogsOfBill/{billId}")]
        [HttpGet]
        public async Task<IActionResult> getLogsOfBill(int billId)
        {
            string api = "getLogsOfBill/{billId}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
                List<BillManagementModel> model = new List<BillManagementModel>();

                List<billLog> bills = _context
                    .mz_expense_bill_logs.Where(x => x.billId == billId)
                    .ToList()
                    .Select(x => new billLog
                    {
                        billId = x.billId,
                        createdBy = x.createdBy,
                        createdOn = x.createdOn,
                        description = x.description,
                        id = x.id,
                        status = x.status,
                    })
                    .ToList();

                List<billLog> bl = bills.Where(x => x.status == "Paid").ToList();
                if (bl.Count > 0)
                {
                    foreach (billLog x in bl)
                    {
                        var bill = _context
                            .mz_expense_vendor_transaction.Where(vt =>
                                vt.billId == billId && vt.paymentId != null
                            )
                            .Join(
                                _context.mz_expense_vendor_payment,
                                vt => vt.paymentId,
                                vp => vp.id,
                                (vt, vp) => new { vt, vp }
                            )
                            .Where(result => result.vp.status == "Active")
                            .Select(result => result.vp)
                            .FirstOrDefault();

                        if (bill != null)
                        {
                            billLog b = bills.Where(y => y.id == x.id).FirstOrDefault();
                            bills.Remove(b);
                            b.paymentDate = bill.paymentDate?.ToString("dd/MM/yyyy");
                            b.transactionId = bill.transactionId;
                            bills.Add(b);
                        }
                    }
                }

                return Ok(bills);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("AddtobepaidtoBills")]
        [HttpPost]
        public async Task<IActionResult> tobepaidBills(List<BillManagementModel> Bills)
        {
            string api = "AddtobepaidtoBills";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                if (Bills.FirstOrDefault().paymentMode_Admin == "Cheque")
                {
                    List<int?> venderids = new List<int?>();

                    foreach (var i in Bills.Where(x => x.select == true).ToList())
                    {
                        venderids.Add(i.vendorId);
                    }

                    venderids = venderids.Distinct().ToList();

                    foreach (var j in venderids)
                    {
                        mz_expense_vendor_master v = _context
                            .mz_expense_vendor_master.Where(x => x.id == j)
                            .FirstOrDefault();
                        var p = new mz_expense_bills_package
                        {
                            name = Bills.FirstOrDefault().paymentMode_Admin + " - " + v.name
                        };
                        _context.mz_expense_bills_package.Add(p);
                        _context.SaveChanges(); //required due to Id usage

                        foreach (
                            var i in Bills.Where(x => x.select == true && x.vendorId == j).ToList()
                        )
                        {
                            mz_expense_bill_master b = _context
                                .mz_expense_bill_master.Where(x => x.id == i.id)
                                .FirstOrDefault();

                            b.status = "ToBePaid";
                            b.paymentFrom_BankName = i.paymentFrom_BankName;
                            b.tdsAmount = i.tdsAmount;
                            b.tdsApplicableAmount = i.tdsApplicableAmount;
                            b.tdsPercentage = i.tdsPercentage;

                            b.paymentMode_Admin = i.paymentMode_Admin;
                            b.packageId = p.id;

                            _context.mz_expense_bill_logs.Add(
                                new mz_expense_bill_logs
                                {
                                    billId = b.id,
                                    createdBy = authUser.Name,
                                    createdOn = indianTime,
                                    status = "ToBePaid - From Bank:- " + i.paymentFrom_BankName
                                }
                            );
                        }
                    }
                }
                else
                {
                    var p = new mz_expense_bills_package
                    {
                        name = Bills.FirstOrDefault().paymentMode_Admin
                    };
                    _context.mz_expense_bills_package.Add(p);
                    _context.SaveChanges(); //required due to Id usage

                    foreach (var i in Bills.Where(x => x.select == true).ToList())
                    {
                        mz_expense_bill_master b = _context
                            .mz_expense_bill_master.Where(x => x.id == i.id)
                            .FirstOrDefault();

                        b.status = "ToBePaid";
                        b.paymentFrom_BankName = i.paymentFrom_BankName;
                        b.tdsAmount = i.tdsAmount;
                        b.tdsApplicableAmount = i.tdsApplicableAmount;
                        b.tdsPercentage = i.tdsPercentage;

                        b.paymentMode_Admin = i.paymentMode_Admin;
                        b.packageId = p.id;

                        _context.mz_expense_bill_logs.Add(
                            new mz_expense_bill_logs
                            {
                                billId = b.id,
                                createdBy = authUser.Name,
                                createdOn = indianTime,
                                status = "ToBePaid - From Bank:- " + i.paymentFrom_BankName
                            }
                        );
                    }
                }

                _context.SaveChanges();
                return Ok(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [Route("AddtoPaidtoBills")]
        [HttpPost]
        public async Task<IActionResult> toPaidBills(List<BillManagementModel> Bills)
        {
            string api = "AddtoPaidtoBills";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                int financialYear = _helperService.GetFinancialYear(DateTime.Now);

                List<string> vendors = new List<string>();
                foreach (var i in Bills)
                {
                    vendors.Add(i.paymentTo_AccNum);
                    //cache.DeleteItem("getBillin-" + i.deptVenueId.ToString() + "-" + i.baseItemId.ToString() + "-" + financialYear);
                }

                vendors = vendors.Distinct().ToList();

                foreach (var j in vendors)
                {
                    List<BillManagementModel> bs = Bills
                        .Where(x => x.paymentTo_AccNum == j)
                        .ToList();

                    mz_expense_vendor_payment p = new mz_expense_vendor_payment
                    {
                        createdBy = authUser.Name,
                        createdOn = indianTime,
                        paymentMode = bs.FirstOrDefault().paymentMode_Admin,
                        transactionId = bs.FirstOrDefault().txnId,
                        vendorId = bs.FirstOrDefault().vendorId,
                        currency = "INR",
                        debit = bs.Sum(x => x.totalAmount),
                        paymentDate = bs.FirstOrDefault().paymentDate,
                        status = "Active"
                    };

                    _context.mz_expense_vendor_payment.Add(p);
                    _context.SaveChanges(); //require payment ID
                    foreach (var i in bs)
                    {
                        mz_expense_vendor_transaction t = new mz_expense_vendor_transaction
                        {
                            billId = i.id,
                            createdOn = indianTime,
                            createdBy = authUser.Name,
                            currency = "INR",
                            paymentMode = i.paymentMode_Admin,
                            transactionId = i.txnId,
                            vendorId = i.vendorId,
                            debit = i.totalAmount,
                            paymentId = p.id
                        };
                        _context.mz_expense_vendor_transaction.Add(t);
                        _context.SaveChanges(); //require transaction id
                        mz_expense_bill_master b = _context
                            .mz_expense_bill_master.Where(x => x.id == i.id)
                            .FirstOrDefault();

                        b.status = "Paid";
                        b.txn_Id = t.id;

                        _context.mz_expense_bill_logs.Add(
                            new mz_expense_bill_logs
                            {
                                billId = b.id,
                                createdBy = authUser.Name,
                                createdOn = indianTime,
                                status = "Paid",
                                description = "TXN ID : " + p.transactionId
                            }
                        );
                    }
                }

                _context.SaveChanges();
                return Ok(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [Route("gobacktoApprovedStatus")]
        [HttpPost]
        public async Task<IActionResult> gobacktoApprovedStatus(List<BillManagementModel> Bills)
        {
            string api = "gobacktoApprovedStatus";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                int financialYear = _helperService.GetFinancialYear(DateTime.Now);

                foreach (var i in Bills)
                {
                    //cache.DeleteItem("getBillin-" + i.deptVenueId.ToString() + "-" + i.baseItemId.ToString() + "-" + financialYear);
                    mz_expense_bill_master b = _context
                        .mz_expense_bill_master.Where(x => x.id == i.id)
                        .FirstOrDefault();

                    b.paymentMode_Admin = null;
                    b.packageId = null;
                    b.status = "Approved";
                    b.paymentFrom_BankName = null;

                    _context.mz_expense_bill_logs.Add(
                        new mz_expense_bill_logs
                        {
                            billId = b.id,
                            createdBy = authUser.Name,
                            createdOn = indianTime,
                            status = "Back To Approved"
                        }
                    );
                }
                _context.SaveChanges();

                return Ok(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [Route("reversepayment")]
        [HttpPost]
        public async Task<IActionResult> ReversePayment(RecieptModel model)
        {
            string api = "api/feecontroller/ReverseReceipt";
            //// Add_ApiLogs(api);

            try
            {
                int financialYear = _helperService.GetFinancialYear(DateTime.Now);
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
                mz_expense_vendor_payment payment = _context
                    .mz_expense_vendor_payment.Where(x => x.id == model.id)
                    .FirstOrDefault();

                List<mz_expense_vendor_transaction> transaction = _context
                    .mz_expense_vendor_transaction.Where(x => x.paymentId == model.id)
                    .ToList();

                List<mz_expense_bill_master> bills = (
                    from t in transaction
                    join b in _context.mz_expense_bill_master on t.billId equals b.id
                    select b
                ).ToList();
                if (payment.status == "InActive")
                {
                    return BadRequest(new { message = "Payment is InActive" });
                }

                payment.status = "InActive";

                foreach (mz_expense_vendor_transaction vt in transaction)
                {
                    _context.mz_expense_vendor_transaction.Add(
                        new mz_expense_vendor_transaction
                        {
                            billId = vt.billId,
                            createdBy = authUser.Name,
                            createdOn = indianTime,
                            currency = vt.currency,
                            paymentId = vt.paymentId,
                            vendorId = vt.vendorId,
                            credit = vt.debit,
                            paymentMode = "Reverse",
                            remarks = model.note
                        }
                    );
                }

                foreach (mz_expense_bill_master b in bills)
                {
                    //cache.DeleteItem("getBillin-" + b.deptVenueId.ToString() + "-" + b.baseItemId.ToString() + "-" + financialYear);
                    b.status = "ToBePaid";
                    _context.mz_expense_bill_logs.Add(
                        new mz_expense_bill_logs
                        {
                            status = "Reversed",
                            billId = b.id,
                            createdOn = indianTime,
                            createdBy = authUser.Name,
                            description = "Bill status = Revesed :: Reason:- " + model.note
                        }
                    );
                }

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("removebillfrompackage/{billid}")]
        [HttpGet]
        public async Task<IActionResult> removeBillFromPackage(int billid)
        {
            string api = "removebillfrompackage" + billid.ToString();
            //// Add_ApiLogs(api);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

            try
            {
                int financialYear = _helperService.GetFinancialYear(DateTime.Now);

                //using (var context = new mzmanageEntities())
                //{
                mz_expense_bill_master bill = (
                    from b in _context.mz_expense_bill_master
                    where b.id == billid
                    select b
                ).FirstOrDefault();

                //cache.DeleteItem("getBillin-" + bill.deptVenueId.ToString() + "-" + bill.baseItemId.ToString() + "-" + financialYear);
                bill.packageId = null;
                bill.status = "Approved";
                bill.paymentFrom_BankName = null;
                bill.paymentMode_Admin = null;
                _context.mz_expense_bill_logs.Add(
                    new mz_expense_bill_logs
                    {
                        status = "Removed from pkg",
                        billId = bill.id,
                        createdOn = indianTime,
                        createdBy = authUser.Name,
                        description = "Bill status = Removed from pkg"
                    }
                );

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("changeIsFundRequestStatus")]
        [HttpPost]
        public async Task<IActionResult> changeIsFundRequestStatus(FeesAllotmentModel model)
        {
            string api = "changeIsFundRequestStatus";
            //// Add_ApiLogs(api);

            string fieldName = model.remarks;
            string itsIdCSV = model.itsIdCSV;
            string data = model.reason;
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                List<int> itsIds = _helperService.parseIds(itsIdCSV);

                if (itsIds.Count > 50)
                {
                    return BadRequest(new { message = "Students cannot be more than 50" });
                }

                int financialYear = _helperService.GetFinancialYear(DateTime.Now);

                int c = 0;
                foreach (var i in itsIds)
                {
                    mz_expense_bill_master b = _context
                        .mz_expense_bill_master.Where(x => x.id == i)
                        .FirstOrDefault();

                    if (b != null)
                    {
                        //cache.DeleteItem("getBillin-" + b.deptVenueId.ToString() + "-" + b.baseItemId.ToString() + "-" + financialYear);
                        b.isFundRequested = Convert.ToBoolean(model.remarks);

                        mz_expense_bill_logs l = new mz_expense_bill_logs
                        {
                            billId = i,
                            createdOn = DateTime.Now,
                            status = "Fund Requested - " + model.remarks,
                            createdBy = authUser.Name,
                        };

                        _context.mz_expense_bill_logs.Add(l);
                    }
                }
                _context.SaveChanges();

                return Ok(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [Route("secret/billmanagement/clearallcache")]
        [HttpGet]
        public async Task<IActionResult> deleteAllBillCache()
        {
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                DateTime dt = DateTime.Now;
                int financialYear = _helperService.GetFinancialYear(dt);
                List<dept_venue> dv = _context.dept_venue.ToList();
                foreach (dept_venue d in dv)
                {
                    List<mz_expense_procurement_baseitem> bi =
                        _context.mz_expense_procurement_baseitem.ToList();
                    foreach (mz_expense_procurement_baseitem b in bi)
                    {
                        //cache.DeleteItem("getBillin-" + d.id.ToString() + "-" + b.id.ToString() + "-" + financialYear);
                    }
                }
                return Ok("cache cleared");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class billLog
    {
        public int id { get; set; }
        public int? billId { get; set; }
        public DateTime? createdOn { get; set; }
        public string createdBy { get; set; }
        public string status { get; set; }
        public string paymentDate { get; set; }
        public string transactionId { get; set; }
        public string description { get; set; }
    }

    public class feeAllotedResultModel
    {
        public List<RecieptModel> model { get; set; }
        public List<export_category> exportCategory { get; set; }
        public List<dropdown_dataset_options> paymentModeDD { get; set; }
        public List<dropdown_dataset_options> statusDD { get; set; }
    }
}
