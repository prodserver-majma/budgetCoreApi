using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using mahadalzahrawedapi.Mappings.Finance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mahadalzahrawebapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        public VendorController(mzdbContext context, IMapper mapper, TokenService tokenService)
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

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById(
            "Asia/Kolkata"
        );
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        [Route("getVendorLedger/{vendorId}")]
        [HttpGet]
        public async Task<ActionResult> getStudentLedger(int vendorId)
        {
            string api = "getVendorLedger/{vendorId}";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<FeeTransactionModel> models = new List<FeeTransactionModel>();
            List<FeePaymentModel> references = new List<FeePaymentModel>();
            mz_expense_vendor_master s = new mz_expense_vendor_master();
            List<dropdown_dataset_header> modes = new List<dropdown_dataset_header>();

            try
            {
                s = _context.mz_expense_vendor_master.Where(x => x.id == vendorId).FirstOrDefault();
                if (s == null)
                {
                    return BadRequest(new { message = "Vendor not found" });
                }
                List<mz_expense_vendor_transaction> transactions = _context
                    .mz_expense_vendor_transaction.Where(x => x.vendorId == s.id)
                    .ToList();

                List<mz_expense_bill_master> vendorBills = _context
                    .mz_expense_bill_master.Where(x => x.vendorId == s.id)
                    .ToList();

                foreach (var i in transactions)
                {
                    List<mz_expense_vendor_transaction> Allotment_transactions = transactions
                        .Where(x => x.billId == i.billId)
                        .ToList();

                    string remarks = "";
                    string balance = "";
                    int balance2 = 0;
                    string cssClass = "";
                    mz_expense_bill_master a = vendorBills.Where(x => x.id == i.billId)
                        .FirstOrDefault();
                    List<mz_expense_vendor_transaction> tList = transactions
                        .Where(x => x.id <= i.id)
                        .ToList();

                    balance = (tList.Sum(x => x.debit) - tList.Sum(x => x.credit)).ToString();
                    balance2 =
                        Allotment_transactions.Sum(x => x.credit ?? 0)
                        - Allotment_transactions.Sum(x => x.debit ?? 0);

                    DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                    if (balance2 == 0)
                    {
                        cssClass = "complete";
                    }

                    if (!(i.debit == 0 || i.debit == null))
                    {
                        if (!(a?.tdsAmount == 0 || a?.tdsAmount == null))
                        {
                            balance = (
                                tList.Sum(x => x.debit) - (a?.tdsAmount ?? 0) - tList.Sum(x => x.credit) ?? 0
                            ).ToString();

                            models.Add(
                                new FeeTransactionModel
                                {
                                    billNumber = a?.billNo ?? "",
                                    billDate = a?.billDate ?? today,
                                    tdsAmount = a?.tdsAmount ?? 0,
                                    tdsApplicableAmount = a?.tdsApplicableAmount,
                                    tdsPercentage = a?.tdsPercentage,
                                    id = i.id,
                                    debitNo = (i.debit - a?.tdsAmount) ?? 0,
                                    creditNo = i.credit ?? 0,
                                    credit = (i.credit ?? 0).ToString(),
                                    debit = (i.debit - a?.tdsAmount ?? 0).ToString(),
                                    paymentType = i.paymentMode,
                                    dateTime = i.createdOn ?? DateTime.Today,
                                    note = remarks + " :: " + i.remarks,
                                    createdBy = i.createdBy,
                                    balance = balance,
                                    reference = (i.billId ?? 0).ToString(),
                                    cssClass = cssClass
                                }
                            );

                            balance = (
                                tList.Sum(x => x.debit) - tList.Sum(x => x.credit)
                            ).ToString();

                            models.Add(
                                new FeeTransactionModel
                                {
                                    billNumber = a?.billNo ?? "",
                                    billDate = a?.billDate ?? today,
                                    tdsAmount = a?.tdsAmount ?? 0,
                                    tdsApplicableAmount = a?.tdsApplicableAmount,
                                    tdsPercentage = a?.tdsPercentage,
                                    id = i.id,
                                    debitNo = a?.tdsAmount ?? 0,
                                    creditNo = i.credit ?? 0,
                                    credit = (i.credit ?? 0).ToString(),
                                    debit = (a?.tdsAmount ?? 0).ToString(),
                                    paymentType = "TDS",
                                    dateTime = i.createdOn ?? DateTime.Today,
                                    note =
                                        "TDS % : "
                                        + a?.tdsPercentage
                                        + " :: "
                                        + remarks
                                        + " :: "
                                        + i.remarks,
                                    createdBy = i.createdBy,
                                    balance = balance,
                                    reference = (i?.billId ?? 0).ToString(),
                                    cssClass = cssClass
                                }
                            );
                        }
                        else
                        {
                            models.Add(
                                new FeeTransactionModel
                                {
                                    billNumber = a?.billNo ?? "",
                                    billDate = a?.billDate ?? today,
                                    tdsAmount = a?.tdsAmount ?? 0,
                                    tdsApplicableAmount = a?.tdsApplicableAmount,
                                    tdsPercentage = a?.tdsPercentage,
                                    id = i.id,
                                    debitNo = i.debit ?? 0,
                                    creditNo = i.credit ?? 0,
                                    credit = (i.credit ?? 0).ToString(),
                                    debit = (i.debit ?? 0).ToString(),
                                    paymentType = i.paymentMode,
                                    dateTime = i.createdOn ?? DateTime.Today,
                                    note = remarks + " :: " + i.remarks,
                                    createdBy = i.createdBy,
                                    balance = balance,
                                    reference = (i.billId ?? 0).ToString(),
                                    cssClass = cssClass
                                }
                            );
                        }
                    }
                    else
                    {
                        models.Add(
                            new FeeTransactionModel
                            {
                                billNumber = a?.billNo ?? "",
                                billDate = a?.billDate ?? today,
                                tdsAmount = a?.tdsAmount ?? 0,
                                tdsApplicableAmount = a?.tdsApplicableAmount,
                                tdsPercentage = a?.tdsPercentage,
                                id = i.id,
                                debitNo = i.debit ?? 0,
                                creditNo = i.credit ?? 0,
                                credit = (i.credit ?? 0).ToString(),
                                debit = (i.debit ?? 0).ToString(),
                                paymentType = i.paymentMode,
                                dateTime = i.createdOn ?? DateTime.Today,
                                note = remarks + " :: " + i.remarks,
                                createdBy = i.createdBy,
                                balance = balance,
                                reference = (i.billId ?? 0).ToString(),
                                cssClass = cssClass
                            }
                        );
                    }
                }

                int? C_withoutR = models
                    .Where(x => x.paymentType != "Reverse")
                    .ToList()
                    .Sum(x => x.creditNo);
                int? waived = models
                    .Where(x => x.paymentType == "Waive")
                    .ToList()
                    .Sum(x => x.debitNo);

                int? D_withoutW = models
                    .Where(x => x.paymentType != "Waive")
                    .ToList()
                    .Sum(x => x.debitNo);
                int? reversed = models
                    .Where(x => x.paymentType == "Reverse")
                    .ToList()
                    .Sum(x => x.creditNo);

                string balance1 = (
                    models.Sum(x => x.debitNo) - models.Sum(x => x.creditNo)
                ).ToString();
                List<mz_expense_bill_master> transactions1 = vendorBills.ToList();

                foreach (var i in transactions1)
                {
                    List<mz_expense_vendor_transaction> transactions_1 = transactions.Where(x =>
                            x.vendorId == i.vendorId && x.billId == i.id
                        )
                        .ToList();

                    int? C_withoutR_1 = transactions_1
                        .Where(x => x.paymentMode != "Reverse")
                        .ToList()
                        .Sum(x => x.credit);
                    int? waived_1 = transactions_1
                        .Where(x => x.paymentMode == "Waive")
                        .ToList()
                        .Sum(x => x.debit);

                    int? D_withoutW_1 = transactions_1
                        .Where(x => x.paymentMode != "Waive")
                        .ToList()
                        .Sum(x => x.debit);
                    int? reversed_1 = transactions_1
                        .Where(x => x.paymentMode == "Reverse")
                        .ToList()
                        .Sum(x => x.credit);

                    int? w = (C_withoutR_1 - waived_1) - (D_withoutW_1 - reversed_1);
                    int? r = (D_withoutW_1 - reversed_1);

                    references.Add(
                        new FeePaymentModel
                        {
                            allotmentId = i.id,
                            name = "Unique Id:- " + i.id + " W : " + w + " R : " + r
                        }
                    );
                }

                return Ok(
                    new
                    {
                        modes = modes,
                        references = references,
                        models = models,
                        allocated = C_withoutR,
                        paid = D_withoutW,
                        waived = waived,
                        reversed = reversed,
                        balance = balance1,
                        name = s.name + " ( VENDOR_ID : " + s.id + " )"
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getVendorLedgerExport/{vendorId}")]
        [HttpGet]
        public async Task<ActionResult> getVendorLedgerExport(int vendorId)
        {
            string api = "getVendorLedgerExport/{vendorId}";
            //// Add_ApiLogs(api);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<Export_VendorLedgerModel> models = new List<Export_VendorLedgerModel>();
            List<FeePaymentModel> references = new List<FeePaymentModel>();
            mz_expense_vendor_master s = new mz_expense_vendor_master();
            List<dropdown_dataset_header> modes = new List<dropdown_dataset_header>();

            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            try
            {
                s = _context.mz_expense_vendor_master.Where(x => x.id == vendorId).FirstOrDefault();
                if (s == null)
                {
                    return BadRequest(new { message = "Vendor not found" });
                }
                List<mz_expense_vendor_transaction> transactions = _context
                    .mz_expense_vendor_transaction.Where(x => x.vendorId == s.id)
                    .ToList();

                foreach (var i in transactions)
                {
                    List<mz_expense_vendor_transaction> Allotment_transactions = _context
                        .mz_expense_vendor_transaction.Where(x => x.billId == i.billId)
                        .ToList();

                    string remarks = "";
                    string balance = "";
                    int balance2 = 0;
                    string cssClass = "";
                    mz_expense_bill_master a = _context
                        .mz_expense_bill_master.Where(x => x.id == i.billId)
                        .FirstOrDefault();
                    List<mz_expense_vendor_transaction> tList = _context
                        .mz_expense_vendor_transaction.Where(x =>
                            x.vendorId == s.id && x.id <= i.id
                        )
                        .ToList();

                    if (!(i.debit == 0 || i.debit == null))
                    {
                        if (!(a.tdsAmount == 0 || a.tdsAmount == null))
                        {
                            balance = (
                                (tList.Sum(x => x.debit) - a.tdsAmount) - tList.Sum(x => x.credit)
                            ).ToString();

                            models.Add(
                                new Export_VendorLedgerModel
                                {
                                    billNumber = a.billNo,
                                    vendorId = vendorId.ToString(),
                                    vendorName = s.name,
                                    billDate = (a.billDate ?? today).ToString("MM/dd/yyyy"),
                                    id = i.id,
                                    credit = i.credit?.ToString(),
                                    debit = (i.debit - a.tdsAmount).ToString(),
                                    paymentType = i.paymentMode,
                                    txnDate = (i.createdOn ?? DateTime.Today).ToString(
                                        "MM/dd/yyyy"
                                    ),
                                    note = remarks + " :: " + i.remarks,
                                    createdBy = i.createdBy,
                                    balance = balance,
                                    billId = i.billId ?? 0
                                }
                            );

                            balance = (
                                (tList.Sum(x => x.debit)) - tList.Sum(x => x.credit)
                            ).ToString();
                            models.Add(
                                new Export_VendorLedgerModel
                                {
                                    billNumber = a.billNo,
                                    vendorId = vendorId.ToString(),
                                    vendorName = s.name,
                                    billDate = (a.billDate ?? today).ToString("MM/dd/yyyy"),
                                    id = i.id,
                                    credit = i.credit?.ToString(),
                                    debit = a.tdsAmount.ToString(),
                                    paymentType = "TDS",
                                    txnDate = (i.createdOn ?? DateTime.Today).ToString(
                                        "MM/dd/yyyy"
                                    ),
                                    note =
                                        "TDS % : "
                                        + a.tdsPercentage
                                        + " :: "
                                        + remarks
                                        + " :: "
                                        + i.remarks,
                                    createdBy = i.createdBy,
                                    balance = balance,
                                    billId = i.billId ?? 0
                                }
                            );
                        }
                        else
                        {
                            models.Add(
                                new Export_VendorLedgerModel
                                {
                                    billNumber = a.billNo,
                                    vendorId = vendorId.ToString(),
                                    vendorName = s.name,
                                    billDate = (a.billDate ?? today).ToString("MM/dd/yyyy"),
                                    id = i.id,
                                    credit = i.credit?.ToString(),
                                    debit = i.debit?.ToString(),
                                    paymentType = i.paymentMode,
                                    txnDate = (i.createdOn ?? DateTime.Today).ToString(
                                        "MM/dd/yyyy"
                                    ),
                                    note = remarks + " :: " + i.remarks,
                                    createdBy = i.createdBy,
                                    balance = balance,
                                    billId = i.billId ?? 0
                                }
                            );
                        }
                    }
                    else
                    {
                        models.Add(
                            new Export_VendorLedgerModel
                            {
                                billNumber = a.billNo,
                                vendorId = vendorId.ToString(),
                                vendorName = s.name,
                                billDate = (a.billDate ?? today).ToString("MM/dd/yyyy"),
                                id = i.id,
                                credit = i.credit?.ToString(),
                                debit = i.debit?.ToString(),
                                paymentType = i.paymentMode,
                                txnDate = (i.createdOn ?? DateTime.Today).ToString("MM/dd/yyyy"),
                                note = remarks + " :: " + i.remarks,
                                createdBy = i.createdBy,
                                balance = balance,
                                billId = i.billId ?? 0
                            }
                        );
                    }
                }

                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getOnlinePaymentsUser")]
        [HttpGet]
        public async Task<ActionResult> getOnlinePaymentsUser()
        {
            string api = "getOnlinePaymentsUser";
            //// Add_ApiLogs(api);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<user_deptvenue> udvs = _context.user_deptvenue.ToList();

            var depts = _context.dept_venue.ToList();

            var rds = _context.registrationform_dropdown_set.ToList();

            var sps = _context.registrationform_subprograms.ToList();

            try
            {
                var entries = new List<Entry>();
                

                List<mz_expense_online_payment_users> users =
                    _context.mz_expense_online_payment_users.ToList();

                foreach (var i in users)
                {
                    var ud = udvs.Where(x => x.itsId == authUser.ItsId).ToList();
                    foreach (var udv in ud)
                    {
                        //if (udv.psetId == i.psetId && udv.deptVenueId == i.deptVenueId)
                        //{
                            bool check = entries.Any(x => x.id == i.id);
                            if (!check)
                            {
                                var dept = depts.Where(x => x.id == udv.deptVenueId).FirstOrDefault();

                                if (dept != null)
                                {
                                    //var rd = rds.Where(x => x.id == udv.psetId).FirstOrDefault();

                                    //if (rd != null)
                                    //{
                                    //    var sp = sps.Where(x => x.id == rd.subprogramId).FirstOrDefault();
                                        //VendorMasterModel model = _mapper.Map<VendorMasterModel>(i);
                                            
                                        //model.schoolClassName = dept.venueName + "_" + dept.deptName + "_" + sp.name;

                                        entries.Add(
                                            new Entry
                                            {
                                                id = i.id,
                                                accountName = i.accName,
                                                accountNo = i.accNum,
                                                bankName = i.bankName,
                                                ifscCode = i.ifsc,
                                                name = i.name,
                                                schoolClassName = dept.venueName
                                            }
                                        );
                                    //}
                                }
                            }
                        //}
                    }
                }

                return Ok(entries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("AddOnlinePaymentsUser")]
        [HttpPost]
        public async Task<ActionResult> AddOnlinePaymentsUser(VendorMasterModel vendorModel)
        {
            string api = "AddOnlinePaymentsUser";
            //// Add_ApiLogs(api);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //mz_expense_vendor_master user = _mapper.Map<mz_expense_vendor_master>(vendorModel);
            var udv = _context.user_deptvenue.ToList();
            try
            {
                List<mz_expense_vendor_master> entries = new List<mz_expense_vendor_master>();
                
                List<string> msgs = new List<string>();
                mz_expense_online_payment_users a;

                foreach (var ps in vendorModel.schoolId)
                {
                    a = _context.mz_expense_online_payment_users.Where(x => x.accNum == vendorModel.accountNo && x.schoolId == ps).FirstOrDefault();

                    if(a == null)
                    {
                        //var deptId = udv.Where(x => x.psetId == ps).FirstOrDefault();
                        mz_expense_online_payment_users u = new mz_expense_online_payment_users
                        {
                            id = vendorModel.id,
                            accName = vendorModel.accountName,
                            accNum = vendorModel.accountNo,
                            bankName = vendorModel.bankName,
                            ifsc = vendorModel.ifscCode,
                            name = vendorModel.name,
                            schoolId = ps,
                            createdBy = authUser.ItsId
                        };
                        _context.mz_expense_online_payment_users.Add(u);
                    }
                    else
                    {
                        msgs.Add("Vendor already exists for the selected class(es)");
                    }
                }
                _context.SaveChanges();

                if (vendorModel.schoolId.Count() == msgs.Count())
                {
                    return BadRequest(new { message = msgs[0] });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("editOnlinePaymentsUser")]
        [HttpPost]
        public async Task<ActionResult> editOnlinePaymentsUser(mz_expense_vendor_master user)
        {
            string api = "AddOnlinePaymentsUser";
            //// Add_ApiLogs(api);

            try
            {
                List<mz_expense_vendor_master> entries = new List<mz_expense_vendor_master>();

                mz_expense_online_payment_users u = _context
                    .mz_expense_online_payment_users.Where(x => x.id == user.id)
                    .FirstOrDefault();
                if (u == null)
                {
                    return BadRequest(new { message = "Online payment user not found" });
                }

                u.accName = user.accountName;
                u.accNum = user.accountNo;
                u.bankName = user.bankName;
                u.ifsc = user.ifscCode;

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getvendorWalletLedger/{vendorId}")]
        [HttpGet]
        public async Task<ActionResult> getvendorWalletLedger(int vendorId)
        {
            string api = "getvendorWalletLedger/{itsId}";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<FeeTransactionModel> models = new List<FeeTransactionModel>();
            List<FeePaymentModel> references = new List<FeePaymentModel>();

            mz_expense_vendor_master s = _context
                .mz_expense_vendor_master.Where(x => x.id == vendorId)
                .FirstOrDefault();

            List<mz_expense_vendor_wallet> transactions = _context
                .mz_expense_vendor_wallet.Where(x => x.vendorId == s.id)
                .ToList();

            foreach (var i in transactions)
            {
                string status = "Active";
                string balance = "";
                if (!i.status ?? false)
                {
                    status = "InActive";
                }

                List<mz_expense_vendor_wallet> tList = _context
                    .mz_expense_vendor_wallet.Where(x =>
                        x.vendorId == s.id && x.status == true && x.id <= i.id
                    )
                    .ToList();

                balance = (tList.Sum(x => x.credit) - tList.Sum(x => x.debit)).ToString();

                models.Add(
                    new FeeTransactionModel
                    {
                        balance = balance,
                        id = i.id,
                        debitNo = i.debit ?? 0,
                        creditNo = i.credit ?? 0,
                        credit = i.credit.ToString(),
                        debit = i.debit.ToString(),
                        paymentType = i.paymentType,
                        dateTime = i.createdOn ?? DateTime.Today,
                        note = i.note,
                        createdBy = i.createdBy,
                        status = status
                    }
                );
            }

            int? debit = models.Sum(x => x.debitNo);
            int? credit = models.Sum(x => x.creditNo);

            string balance1 = (models.Sum(x => x.creditNo) - models.Sum(x => x.debitNo)).ToString();

            return Ok(
                new
                {
                    models = models,
                    debit = debit,
                    credit = credit,
                    balance = balance1,
                    name = s.name,
                }
            );
        }

        [Route("deleteOnlinePaymentsUser/{vendorId}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteOnlinePaymentsUser(long vendorId)
        {
            string api = "api/deletevendor/{vendorId}";
            //// Add_ApiLogs(api);

            mz_expense_online_payment_users vendor =
                _context.mz_expense_online_payment_users.FirstOrDefault(x => x.id == vendorId);
            if (vendor == null)
                throw new Exception("No such user Found");
            else
            {
                _context.mz_expense_online_payment_users.Remove(vendor);
                _context.SaveChanges();
            }
            return Ok();
        }
    }

    public class Entry
    {
        public int id { get; set; }   // defaults to 0 if not initialized
        public string accountName { get; set; }
        public string accountNo { get; set; }
        public string bankName { get; set; }
        public string ifscCode { get; set; }
        public string name { get; set; }
        public string schoolClassName { get; set; }
    }

}
