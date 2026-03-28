using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using System.Net;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
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

        private readonly NotificationService _notificationService;

        private readonly WhatsAppApiService _whatsAppApiService;

        public PaymentController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _salaryService = new SalaryService(context);
            _helperService = new HelperService(context);
            _itsService = new ItsServiceRemote();
            _jhsService = new IJHSServiceRemote();
            _globalConstants = new globalConstants();
            _whatsAppApiService = new WhatsAppApiService(context);
            _notificationService = new NotificationService();
        }

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        [Route("createReciept")]
        [HttpPost]
        public async Task<ActionResult> CreatePaymentReciept(List<FeePaymentModel> models)
        {
            string api = "createReciept";


            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            string transactionId = models.FirstOrDefault().paymentId;

            mz_student s = _context.mz_student.Where(x => x.itsID == authUser.ItsId).FirstOrDefault();
            int recieptNumber = 1;
            // var rr = _context.mz_student_receipt.Where(x => x.paymentMode == "Online" && x.collectionCenter == 1).Last();
            var rrList = _context.mz_student_receipt.Where(x => x.paymentMode == "Online" && x.collectionCenter == 1).ToList();

            var tsnId = _context.mz_student_receipt.Where(x => x.transactionId == transactionId).FirstOrDefault();

            if (tsnId != null)
            {
                return Ok();
            }
            else
            {
                if (rrList.Count != 0)
                {
                    recieptNumber = (rrList.OrderByDescending(x => x.recieptNumber).FirstOrDefault()?.recieptNumber ?? 0) + 1;

                }

                var r = new mz_student_receipt
                {
                    amount = models.Sum(x => x.amount),
                    createdBy = "SELF",
                    createdOn = indianTime,
                    paymentMode = "Online",
                    currency = "INR",
                    recieptDate = DateOnly.FromDateTime(indianTime.Date),
                    status = "Active",
                    studentId = s.mz_id,
                    transactionId = transactionId,
                    collectionCenter = 1,
                    recieptNumber = recieptNumber,
                    account = "DAWAT-E-HADIYAH",
                    bankName = "Razorpay PG"
                };

                _context.mz_student_receipt.Add(r);
                _context.SaveChanges();


                foreach (var i in models)
                {

                    _context.mz_student_fee_transaction.Add(new mz_student_fee_transaction
                    {
                        allotmentId = i.allotmentId,
                        createdBy = "SELF",
                        createdOn = indianTime,
                        currency = "INR",
                        credit = i.amount,
                        paymentMode = "Online",
                        studentId = s.mz_id,
                        transactionId = transactionId,
                        collection_center_no = 1,
                        recieptId = r.id
                    });

                    _context.SaveChanges();
                }
                return Ok(r.id);
            }



        }

        [AllowAnonymous]
        [Route("mzhubraqam/createReciept/{transactionId}/{recieptId}")]
        [HttpGet]
        public async Task<ActionResult> CreateHubRaqamReciept(string transactionId, int recieptId)
        {
            string api = "api/mzhubraqam/createReciept/" + transactionId + "/" + recieptId.ToString();

            yellowreceipt y = _context.yellowreceipt.Where(x => x.Id == recieptId).FirstOrDefault();

            //RazorpayClient client = new RazorpayClient("rzp_live_HC7TNKqDciQDfc", "axG9X38plXEYNInn4Yoe5Jcd");

            //// Fetch the order using the transactionId
            //Order order = client.Order.Fetch(transactionId);

            //// Fetch the payment details associated with the order
            //List<Payment> payments = order.Payments();

            //// Check if any payment has been captured
            //var capturedPayment = payments.FirstOrDefault(p => p.Attributes["status"] == "captured");

            //if (capturedPayment == null)
            //{
            //    return BadRequest( new { message = "Payment not captured");
            //}

            if (y == null)
            {
                return BadRequest(new { message = "Reciept not found" });
            }

            ItsUser p = await _itsService.GetItsUser(y.ItsId);
            current_counter recieptNumber = _context.current_counter.Where(x => x.name == "onlineYellowReciept").FirstOrDefault();

            y.EntryId = recieptNumber.count;
            recieptNumber.count++;
            y.status = "Paid";
            y.ChequeNo = transactionId;
            y.paymentDate = DateOnly.FromDateTime(DateTime.Today);
            _context.SaveChanges();
            return Ok(y.Id);
        }

        [Route("getFeeAllocationForPayment")]
        [HttpGet]
        public async Task<ActionResult> getFeeAllocationForPayment()
        {
            string api = "getFeeAllocationForPayment";

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<FeePaymentModel> fees = new List<FeePaymentModel>();

            mz_student s = _context.mz_student.Where(x => x.itsID == authUser.ItsId).FirstOrDefault();
            List<mz_student_fee_allotment> allotments = _context.mz_student_fee_allotment.Where(x => x.studentId == s.mz_id).ToList();
            List<int> allotids = allotments.Select(x => x.id).ToList();
            List<mz_student_fee_transaction> allTransactions = _context.mz_student_fee_transaction.Where(x => allotids.Contains(x.allotmentId ?? 0)).ToList();
            List<registrationform_dropdown_set> allPset = _context.registrationform_dropdown_set.Include(x => x.venue).ToList();

            allotments = allotments.OrderBy(x => x.hijriYear).ThenBy(x => x.monthId).ToList();
            int c = 1;
            foreach (var i in allotments)
            {
                List<mz_student_fee_transaction> transactions = allTransactions.Where(x => x.allotmentId == i.id).ToList();

                int? D_withoutR = transactions.Where(x => x.paymentMode != "Reverse").ToList().Sum(x => x.debit);
                int? waived = transactions.Where(x => x.paymentMode == "Waive").ToList().Sum(x => x.credit);

                int? C_withoutW = transactions.Where(x => x.paymentMode != "Waive").ToList().Sum(x => x.credit);
                int? reversed = transactions.Where(x => x.paymentMode == "Reverse").ToList().Sum(x => x.debit);


                int amount = ((D_withoutR ?? 0) - (waived ?? 0)) - ((C_withoutW ?? 0) - (reversed ?? 0));

                registrationform_dropdown_set pset = allPset.Where(x => x.id == i.pSetId).FirstOrDefault();
                venue v = pset.venue;


                if (amount > 0)
                {
                    fees.Add(new FeePaymentModel
                    {
                        amount = amount,
                        allotmentId = i.id,
                        id = c,
                        name = v.displayName + " - " + "( " + i.monthId + " / " + i.hijriYear + " H ) - " + amount + ".00 " + i.currency
                    });

                    c = c + 1;
                }
            }

            return Ok(fees);

        }

        [Route("getRazorPayOrderId/{amount}")]
        [HttpGet]
        public async Task<ActionResult> getRazorPayOrderId(int amount)
        {
            string api = "getRazorPayOrderId/{amount}";

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                RazorpayClient client = new RazorpayClient("rzp_live_HC7TNKqDciQDfc", "axG9X38plXEYNInn4Yoe5Jcd");

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", amount * 100); // amount in the smallest currency unit

                options.Add("currency", "INR");
                Order order = client.Order.Create(options);

                var orderAttributes = order.Attributes.ToObject<Dictionary<string, object>>();

                return Ok(orderAttributes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private struct orderPaymentModel
        {
            public Order order;
            public dynamic payer;
        }

        [AllowAnonymous]
        [Route("mzhubraqam/getRazorPayOrderId/{recieptId}")]
        [HttpGet]
        public async Task<ActionResult> getHubRaqamReciept(int recieptId)
        {
            string api = "api/mzhubraqam/getRazorPayOrderId/" + recieptId.ToString();
            //Add_ApiLogs(api);

            // var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            // AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                RazorpayClient client = new RazorpayClient("rzp_live_HC7TNKqDciQDfc", "axG9X38plXEYNInn4Yoe5Jcd");

                Dictionary<string, object> options = new Dictionary<string, object>();

                yellowreceipt? receipts = _context.yellowreceipt.Where(x => x.PaymentMode == "Online" && x.Id == recieptId).FirstOrDefault();

                if (receipts == null)
                {
                    return BadRequest(new { message = "Receipt not found" });
                }

                if (receipts.status == "Paid")
                {
                    return Ok();
                }

                miscRazorPayModel res = new miscRazorPayModel();

                int amount = receipts.Amount;
                options.Add("amount", amount * 100); // amount in the smallest currency unit

                options.Add("currency", "INR");
                Order order = client.Order.Create(options);

                ItsUser payee = await _itsService.GetItsUser(receipts.ItsId);

                var orderAttributes = order.Attributes.ToObject<Dictionary<string, object>>();

                res.order = orderAttributes;
                res.payer = new miscRazorPayPayerModel
                {
                    name = payee.Name,
                    itsId = payee.ItsId,
                    emailAddress = receipts.email,
                    whatsappNo = receipts.whatsappNo
                };
                res.payee = payee;

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [Route("getStudentLedger/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getStudentLedger(int itsId)
        {
            string api = "getStudentLedger/{itsId}";

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (itsId == 500)
            {
                itsId = authUser.ItsId;
            }
            List<FeeTransactionModel> models = new List<FeeTransactionModel>();
            List<FeePaymentModel> references = new List<FeePaymentModel>();

            mz_student s = _context.mz_student.Where(x => x.itsID == itsId).FirstOrDefault();
            registrationform_dropdown_set pset = _context.registrationform_dropdown_set.Where(x => x.id == s.psetId).FirstOrDefault();
            registrationform_programs p = _context.registrationform_programs.Where(x => x.id == pset.programId).FirstOrDefault();
            registrationform_subprograms sp = _context.registrationform_subprograms.Where(x => x.id == pset.subprogramId).FirstOrDefault();
            venue v = _context.venue.Where(x => x.Id == pset.venueId).FirstOrDefault();

            List<mz_student_fee_transaction> transactions = _context.mz_student_fee_transaction.Where(x => x.studentId == s.mz_id).ToList();

            foreach (var i in transactions)
            {
                string remarks = "";
                string balance = "";
                mz_student_fee_allotment a = _context.mz_student_fee_allotment.Where(x => x.id == i.allotmentId).FirstOrDefault();
                hijri_months m = _context.hijri_months.Where(x => x.id == a.monthId).FirstOrDefault();

                if (i.debit != null)
                {
                    if (a.hijriYear != null)
                    {
                        remarks = "(" + m?.hijriMonthName + " - " + a.hijriYear + "H )";

                    }
                }

                List<mz_student_fee_transaction> tList = _context.mz_student_fee_transaction.Where(x => x.studentId == s.mz_id && x.id <= i.id).ToList();

                balance = (tList.Sum(x => x.credit) - tList.Sum(x => x.debit)).ToString();

                if (m == null)
                {
                    models.Add(new FeeTransactionModel { id = i.id, debitNo = i.debit ?? 0, creditNo = i.credit ?? 0, credit = i.credit.ToString(), debit = i.debit.ToString(), paymentType = i.paymentMode, dateTime = i.createdOn ?? DateTime.Today, note = remarks + " :: " + i.remarks + " :: " + a.remarks, createdBy = i.createdBy, balance = balance, reference = v.displayName + " - " + "(  Miscellaneous :: " + a.remarks + ")" });

                }
                else
                {
                    models.Add(new FeeTransactionModel { id = i.id, debitNo = i.debit ?? 0, creditNo = i.credit ?? 0, credit = i.credit.ToString(), debit = i.debit.ToString(), paymentType = i.paymentMode, dateTime = i.createdOn ?? DateTime.Today, note = remarks + " :: " + i.remarks + " :: " + a.remarks, createdBy = i.createdBy, balance = balance, reference = v.displayName + " - " + "( " + m?.hijriMonthName + " / " + a.hijriYear + " H )" });
                    // models.Add(new FeeTransactionModel { id = i.id, debitNo = i.debit ?? 0, creditNo = i.credit ?? 0 ,credit = i.credit.ToString(), debit = i.debit.ToString(), paymentType = i.paymentMode, dateTime = i.createdOn ?? DateTime.Today, note = remarks + " :: " + i.remarks + " :: " + a.remarks, createdBy = i.createdBy, balance = balance, reference = v.displayName + " - " + "( " + m?.hijriMonthName + " / " + a.hijriYear + " H )" });

                }
            }

            int? D_withoutR = models.Where(x => x.paymentType != "Reverse").ToList().Sum(x => x.debitNo);
            int? waived = models.Where(x => x.paymentType == "Waive").ToList().Sum(x => x.creditNo);

            int? C_withoutW = models.Where(x => x.paymentType != "Waive").ToList().Sum(x => x.creditNo);
            int? reversed = models.Where(x => x.paymentType == "Reverse").ToList().Sum(x => x.debitNo);

            List<mz_student_ewallet> wallets = _context.mz_student_ewallet.Where(x => x.studentId == s.mz_id && x.status == true).ToList();

            int wallet_c = wallets.Sum(x => x.credit) ?? 0;
            int wallet_d = wallets.Sum(x => x.debit) ?? 0;
            int wallet_b = (wallets.Sum(x => x.credit) ?? 0) - (wallets.Sum(x => x.debit) ?? 0);



            string balance1 = (models.Sum(x => x.creditNo) - models.Sum(x => x.debitNo)).ToString();
            List<mz_student_fee_allotment> transactions1 = _context.mz_student_fee_allotment.Where(x => x.studentId == s.mz_id).ToList();

            foreach (var i in transactions1)
            {
                List<mz_student_fee_transaction> transactions_1 = _context.mz_student_fee_transaction.Where(x => x.studentId == i.studentId && x.allotmentId == i.id).ToList();

                int? D_withoutR_1 = transactions_1.Where(x => x.paymentMode != "Reverse").ToList().Sum(x => x.debit);
                int? waived_1 = transactions_1.Where(x => x.paymentMode == "Waive").ToList().Sum(x => x.credit);

                int? C_withoutW_1 = transactions_1.Where(x => x.paymentMode != "Waive").ToList().Sum(x => x.credit);
                int? reversed_1 = transactions_1.Where(x => x.paymentMode == "Reverse").ToList().Sum(x => x.debit);



                int? w = (D_withoutR_1 - waived_1) - (C_withoutW_1 - reversed_1);
                int? r = (C_withoutW_1 - reversed_1);



                hijri_months m = _context.hijri_months.Where(x => x.id == i.monthId).FirstOrDefault();

                if (m == null)
                {
                    references.Add(new FeePaymentModel { allotmentId = i.id, name = v.displayName + " - " + "(  Miscellaneous :: " + i.remarks + ")  W : " + w + " R : " + r });

                }
                else
                {
                    references.Add(new FeePaymentModel { allotmentId = i.id, name = v.displayName + " - " + "( " + m?.hijriMonthName + " / " + i.hijriYear + " H )  W : " + w + " R : " + r });

                }


            }
            string fcName = "";
            int fcId = 0;
            string amount = "";
            if (s.fcId != null)
            {
                mz_student_feecategory fc_Name = _context.mz_student_feecategory.Where(x => x.id == s.fcId).FirstOrDefault();
                fcName = fc_Name.categoryName;
                fcId = fc_Name.id;
            }
            List<mz_student_feecategory> categories = _context.mz_student_feecategory.ToList();

            mz_student_feecategory_pset amnt = _context.mz_student_feecategory_pset.Where(x => x.fcId == s.fcId && x.psetId == s.psetId).FirstOrDefault();
            if (amnt != null)
            {
                amount = amnt.amount.ToString();
            }

            models.Reverse();

            return Ok(new { wallet_b = wallet_b, wallet_d = wallet_d, wallet_c = wallet_c, fcId = fcId, amount = amount, categories = categories, fcName = fcName, references = references, models = models, allocated = D_withoutR, paid = C_withoutW, waived = waived, reversed = reversed, balance = balance1, name = s.nameEng, program = p.name + " _ " + sp.name + " _ " + v.displayName });

        }

        [Route("miscellaneous/receipt/payment")]
        [HttpPost]
        public async Task<ActionResult> AddMiscellaneousReceipt_New(FeeTransactionModel model1)
        {
            string api = "api/user/miscellaneous/receipt/payment/";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                YellowReceiptModel model = new YellowReceiptModel
                {
                    Name = model1.bankName,
                    Amount = model1.debitNo ?? 0,
                    CreatedBy = authUser.Name,
                    ItsId = model1.itsId ?? 0,
                    PaymentMode = model1.paymentType,
                    CreatedOn = DateTime.Now,
                    Remarks = model1.note,
                    CancelDate = model1.cancelDate,
                    Purpose = model1.purpose,
                    Status = model1.paymentType == "Cash" ? "Paid" : "Pending",
                    whatsappNo = model1.whatsappNo,
                    email = model1.email
                };
                if (model1.paymentType == "Cash")
                {
                    current_counter c = _context.current_counter.Where(x => x.name == "cashchequeYellowRecipt").FirstOrDefault();
                    model.EntryId = c.count;
                    c.count++;
                }
                else
                {
                    if (model.whatsappNo.Length < 9)
                    {
                        return BadRequest(new { message = "whatsapp Number not correct" });
                    }
                    current_counter c = _context.current_counter.Where(x => x.name == "onlineYellowReciept").FirstOrDefault();
                    model.EntryId = c.count;
                    c.count++;

                }

                yellowreceipt receipt = GenerateYellowReceipt(model, authUser);

                string strAmount = receipt.Amount.ToString();
                string AmountInWords = _helperService.ChangeToWords(strAmount);

                _context.SaveChanges();
                if (receipt == null)
                {
                    return BadRequest(new { message = "receipt not found" });
                }

                else
                {

                    var f = new FeesPaidModel
                    {
                        receiptNo = receipt.EntryId.ToString(),
                        ItsId = receipt.ItsId,
                        printDate = receipt.CreatedOn,
                        CreatedBy = receipt.CreatedBy,
                        FeePaidAmount = receipt.Amount,
                        name = receipt.Name,
                        PaymentMode = receipt.PaymentMode,
                        amountInWord = AmountInWords
                    };


                    return Ok(f);
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("yellow/cancelYellowRecipets/{recieptId}")]
        [HttpGet]
        public async Task<ActionResult> cancelYellowReceipts(int recieptId)
        {
            string api = "api/yellow/getYellowRecipets";

            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<RecieptModel> modelnew = new List<RecieptModel>();

                yellowreceipt reciept = _context.yellowreceipt.Where(x => x.Id == recieptId).FirstOrDefault();
                reciept.cancelDate = DateOnly.FromDateTime(DateTime.Today);
                _context.SaveChanges();
                return Ok("Reciept successfully canceled");

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            };
        }

        [Route("yellow/getYellowReciepts")]
        [HttpPost]
        public async Task<ActionResult> getYellowReceipts(SearchRecieptModel model)
        {
            string api = "api/yellow/getYellowReciepts";
            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                var itsIds = string.IsNullOrEmpty(model.itsCsv) ? new List<int>() : _helperService.parseItsId(model.itsCsv);
                bool dateFilter = model.fromDate != DateOnly.MinValue && model.toDate != DateOnly.MinValue && model.fromDate != null && model.toDate != null;

                var receiptsQuery = _context.yellowreceipt.AsQueryable();

                if (itsIds.Any())
                {
                    receiptsQuery = receiptsQuery.Where(x => itsIds.Contains(x.ItsId));
                }

                if (dateFilter)
                {
                    var fromDateTime = model.fromDate?.ToDateTime(new TimeOnly(0, 0));
                    var toDateTime = model.toDate?.ToDateTime(new TimeOnly(23, 59));
                    receiptsQuery = receiptsQuery.Where(x => x.CreatedOn >= fromDateTime && x.CreatedOn <= toDateTime || x.cancelDate >= model.fromDate && x.cancelDate <= model.toDate);
                }

                var receipts = await receiptsQuery.ToListAsync();

                List<RecieptModel> modelnew = receipts.Select(i => new RecieptModel
                {
                    createdBy = i.CreatedBy,
                    paymentMode = i.PaymentMode,
                    receiptNo = i.EntryId?.ToString(),
                    feePaidAmount = i.Amount.ToString(),
                    itsId = i.ItsId,
                    name = i.Name,
                    recieptDate = DateOnly.FromDateTime(i.CreatedOn),
                    note = i.Remarks,
                    id = Convert.ToInt32(i.Id),
                    collectionCenter = i.PaidAt,
                    purpose = i.purpose,
                    cancelDate = i.cancelDate?.ToDateTime(new TimeOnly(0, 0)),
                    amountInWord = _helperService.ChangeToWords(i.Amount.ToString()),
                    printDate = DateTime.Today,
                    recieptDate_print = i.CreatedOn,
                    chequeDate = i.paymentDate
                }).ToList();

                var paymentModeDD = modelnew.GroupBy(x => x.paymentMode).Select(x => new dropdown_dataset_options { name = x.Key }).ToList();
                var statusDD = modelnew.GroupBy(x => x.status).Select(x => new dropdown_dataset_options { name = x.Key }).ToList();
                var exportCategory = await _context.export_category.Where(x => x.categoryId == 15).ToListAsync();

                return Ok(new feeAllotedResultModel { model = modelnew, exportCategory = exportCategory, paymentModeDD = paymentModeDD, statusDD = statusDD });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        private yellowreceipt GenerateYellowReceipt(YellowReceiptModel model, AuthUser authUser)
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

            string createdAt = "Surat";
            int? entryId;
            yellowreceipt entity = new yellowreceipt();
            try
            {
                int financialYear = _helperService.GetFinancialYear(indianTime);
                DateTime today = DateTime.Today;
                DateOnly dateOnlyToday = DateOnly.FromDateTime(today);
                entity = new yellowreceipt
                {
                    ItsId = model.ItsId ?? 0,
                    Name = model.Name,
                    Amount = model.Amount ?? 0,
                    CreatedBy = authUser.Name,
                    Remarks = model.Remarks,
                    PaymentMode = model.PaymentMode,
                    ChequeNo = model.ChequeNo,
                    BankName = model.BankName,
                    EntryId = model.EntryId,
                    PaidAt = createdAt,
                    CreatedOn = indianTime,
                    FinancialYear = financialYear,
                    purpose = model.Purpose,
                    status = model.Status,
                    paymentDate = dateOnlyToday,
                    whatsappNo = model.whatsappNo,
                    email = model.email
                };
                _context.yellowreceipt.Add(entity);
                _context.SaveChanges();

                if (entity.PaymentMode == "Online")
                {
                    String msg = "Salam jameel,\n*" + model.Name + "* \nAs per your request, \nHub(Voluntary Contribution) of INR " + model.Amount + " has been allocated.\n\nPlease, Follow the below given link to complete the payment online.\n\n https://students.mahadalzahra.org/student_dashboard/voluntary-contribution/" + entity.Id + "\n\nFor further details contact you masul. \n\nShukran,\nWa al-Salaam  \n\n\n*Do Not Reply To this message";
                    //String msg = "testing";
                    string emailbody = @"<b>" + model.Name + @"</b>
                  <br />
                  <br />
                  Hub (Voluntary Contribution) of <b>INR " + entity.Amount + @" has been allocated</b>.
                  <br />
                  <br />
                  Please<a href=""https://students.mahadalzahra.org/student_dashboard/voluntary-contribution/" + entity.Id + @"""
                      target=""_blank""> click
                      here</a> to complete the payment online.";
                    _notificationService.SendStandardHTMLEmail("Mahad al-Zahra Voluntary Contribution", emailbody, model.email, "accounts");
                    List<string> num = new List<string> { model.whatsappNo };
                    _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg, "Generate Recipt");

                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

    }

    public class miscRazorPayModel
    {
        public Dictionary<string, object> order { get; set; }

        public miscRazorPayPayerModel payer { get; set; }

        public ItsUser payee { get; set; }
    }

    public class miscRazorPayPayerModel
    {
        public string name { get; set; }
        public int itsId { get; set; }
        public string emailAddress { get; set; }
        public string whatsappNo { get; set; }
    }
}
