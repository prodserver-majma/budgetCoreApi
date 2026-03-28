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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeeController : ControllerBase
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

        public FeeController(mzdbContext context, IMapper mapper, TokenService tokenService)
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

        [Route("getStudentLedger/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getStudentLedger(int itsId)
        {
            string api = "getStudentLedger/{itsId}";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (itsId == 500)
            {
                itsId = authUser.ItsId;
            }
            List<FeeTransactionModel> models = new List<FeeTransactionModel>();
            List<FeePaymentModel> references = new List<FeePaymentModel>();
            //mz_student s = new mz_student();

            var venueIds = (from srr in _context.registrationform_dropdown_set
                            join rdss in _context.student_registration_rights on srr.id equals rdss.programSetId
                            where rdss.itsId == authUser.ItsId
                            select srr.venueId).Distinct().ToList();

            List<registrationform_dropdown_set> rfds = _context.registrationform_dropdown_set.Where(x => venueIds.Contains(x.venueId.Value)).ToList();
            List<int> psetsAssigned = rfds.Select(x => x.id).ToList();

            var students = _context.mz_student.Where(x => psetsAssigned.Contains(x.psetId.Value)).ToList();

            //List<student_registration_rights> srrss = _context.student_registration_rights.Where(x => x.itsId == authUser.ItsId).ToList();

            //var students = (from s in _context.mz_student
            //                join r in _context.student_registration_rights
            //                    on s.psetId equals r.programSetId
            //                where r.itsId == authUser.ItsId
            //                select s)
            //            .ToList();

            List<dropdown_dataset_header> modes = new List<dropdown_dataset_header>();
            try
            {
                var s = students.Where(x => x.itsID == itsId).FirstOrDefault();
                if (s == null)
                {
                    if (itsId != 500)
                    {
                        s = _context.mz_student.Where(x => x.mz_id == itsId).FirstOrDefault();

                        if (s == null)
                        {
                            return BadRequest(new { message = "Student not found" });
                        }
                    }
                    else
                    {
                        return BadRequest(new { message = "Student not found" });
                    }
                }

                registrationform_dropdown_set pset1 = _context
                    .registrationform_dropdown_set.Where(x => x.id == s.psetId)
                    .FirstOrDefault();
                registrationform_programs p1 = _context
                    .registrationform_programs.Where(x => x.id == pset1.programId)
                    .FirstOrDefault();
                registrationform_subprograms sp1 = _context
                    .registrationform_subprograms.Where(x => x.id == pset1.subprogramId)
                    .FirstOrDefault();
                venue v1 = _context.venue.Where(x => x.Id == pset1.venueId).FirstOrDefault();

                List<mz_student_fee_allotment> allAllotments =
                    _context.mz_student_fee_allotment.ToList();
                List<hijri_months> allmonths = _context.hijri_months.ToList();

                List<greg_months> gregMonths = _context.greg_months.ToList();
                List<registrationform_dropdown_set> allpsets =
                    _context.registrationform_dropdown_set.ToList();
                List<registrationform_programs> allprograms =
                    _context.registrationform_programs.ToList();
                List<registrationform_subprograms> allsubprograms =
                    _context.registrationform_subprograms.ToList();
                List<venue> allVenues = _context.venue.ToList();

                List<mz_student_fee_transaction> transactions = _context
                    .mz_student_fee_transaction.Where(x => x.studentId == s.mz_id)
                    .ToList();

                foreach (mz_student_fee_transaction i in transactions)
                {
                    List<mz_student_fee_transaction> Allotment_transactions = transactions
                        .Where(x => x.allotmentId == i.allotmentId)
                        .ToList();

                    string remarks = "";
                    string balance = "";
                    int balance2 = 0;
                    string cssClass = "";
                    mz_student_fee_allotment a = allAllotments
                        .Where(x => x.id == i.allotmentId)
                        .FirstOrDefault();
                    //hijri_months m = allmonths.Where(x => x.id == a.monthId).FirstOrDefault();

                    greg_months m = gregMonths.FirstOrDefault(x => x.id == a.monthId);


                    registrationform_dropdown_set pset = allpsets
                        .Where(x => x.id == a.pSetId)
                        .FirstOrDefault();
                    registrationform_programs p = new registrationform_programs();
                    registrationform_subprograms sp = new registrationform_subprograms();
                    venue v = new venue();
                    if (pset != null)
                    {
                        p = allprograms.Where(x => x.id == pset.programId).FirstOrDefault();
                        sp = allsubprograms.Where(x => x.id == pset.subprogramId).FirstOrDefault();
                        v = allVenues.Where(x => x.Id == pset.venueId).FirstOrDefault();
                    }

                    if (i.debit != null)
                    {
                        if (a.hijriYear != null)
                        {
                            remarks = "(" + m?.month_name + " - " + a.hijriYear + ")";
                        }
                    }

                    List<mz_student_fee_transaction> tList = transactions
                        .Where(x => x.id <= i.id)
                        .ToList();

                    balance = (tList.Sum(x => x.credit) - tList.Sum(x => x.debit)).ToString();
                    balance2 = (
                        Allotment_transactions.Sum(x => x.debit ?? 0)
                        - Allotment_transactions.Sum(x => x.credit ?? 0)
                    );

                    if (balance2 == 0)
                    {
                        cssClass = "complete";
                    }
                    if (m == null)
                    {
                        models.Add(
                            new FeeTransactionModel
                            {
                                id = i.id,
                                debitNo = i.debit ?? 0,
                                creditNo = i.credit ?? 0,
                                credit = i.credit?.ToString(),
                                debit = i.debit?.ToString(),
                                paymentType = i.paymentMode,
                                dateTime = i.createdOn ?? DateTime.Today,
                                note = remarks + " :: " + i.remarks + " :: " + a.remarks,
                                createdBy = i.createdBy,
                                balance = balance,
                                reference =
                                    sp?.name
                                    + "-"
                                    + v?.displayName
                                    + " - "
                                    + "(  Miscellaneous :: "
                                    + a.remarks
                                    + ")",
                                cssClass = cssClass,
                            }
                        );
                    }
                    else
                    {
                        models.Add(
                            new FeeTransactionModel
                            {
                                id = i.id,
                                debitNo = i.debit ?? 0,
                                creditNo = i.credit ?? 0,
                                credit = i.credit?.ToString(),
                                debit = i.debit?.ToString(),
                                paymentType = i.paymentMode,
                                dateTime = i.createdOn ?? DateTime.Today,
                                note = remarks + " :: " + i.remarks + " :: " + a.remarks,
                                createdBy = i.createdBy,
                                balance = balance,
                                reference =
                                    sp?.name
                                    + "-"
                                    + v?.displayName
                                    + " - "
                                    + "( "
                                    + m?.month_name
                                    + " / "
                                    + a.hijriYear
                                    + ")",
                                cssClass = cssClass
                            }
                        );
                    }
                }

                int? D_withoutR = models
                    .Where(x => x.paymentType != "Reverse")
                    .ToList()
                    .Sum(x => x.debitNo);
                int? waived = models
                    .Where(x => x.paymentType == "Waive")
                    .ToList()
                    .Sum(x => x.creditNo);

                int? C_withoutW = models
                    .Where(x => x.paymentType != "Waive")
                    .ToList()
                    .Sum(x => x.creditNo);
                int? reversed = models
                    .Where(x => x.paymentType == "Reverse")
                    .ToList()
                    .Sum(x => x.debitNo);

                List<mz_student_ewallet> wallets = _context
                    .mz_student_ewallet.Where(x => x.studentId == s.mz_id && x.status == true)
                    .ToList();

                int wallet_c = wallets.Sum(x => x.credit) ?? 0;
                int wallet_d = wallets.Sum(x => x.debit) ?? 0;
                int wallet_b = (wallets.Sum(x => x.credit) ?? 0) - (wallets.Sum(x => x.debit) ?? 0);

                string balance1 = (
                    models.Sum(x => x.creditNo) - models.Sum(x => x.debitNo)
                ).ToString();
                List<mz_student_fee_allotment> transactions1 = _context
                    .mz_student_fee_allotment.Where(x => x.studentId == s.mz_id)
                    .ToList();

                foreach (mz_student_fee_allotment i in transactions1)
                {
                    List<mz_student_fee_transaction> transactions_1 = transactions
                        .Where(x => x.studentId == i.studentId && x.allotmentId == i.id)
                        .ToList();
                    registrationform_dropdown_set pset = allpsets
                        .Where(x => x.id == i.pSetId)
                        .FirstOrDefault();
                    registrationform_programs p = new registrationform_programs();
                    registrationform_subprograms sp = new registrationform_subprograms();
                    venue v = new venue();
                    if (pset != null)
                    {
                        p = allprograms.Where(x => x.id == pset.programId).FirstOrDefault();
                        sp = allsubprograms.Where(x => x.id == pset.subprogramId).FirstOrDefault();
                        v = allVenues.Where(x => x.Id == pset.venueId).FirstOrDefault();
                    }

                    int? D_withoutR_1 = transactions_1
                        .Where(x => x.paymentMode != "Reverse")
                        .ToList()
                        .Sum(x => x.debit);
                    int? waived_1 = transactions_1
                        .Where(x => x.paymentMode == "Waive")
                        .ToList()
                        .Sum(x => x.credit);

                    int? C_withoutW_1 = transactions_1
                        .Where(x => x.paymentMode != "Waive")
                        .ToList()
                        .Sum(x => x.credit);
                    int? reversed_1 = transactions_1
                        .Where(x => x.paymentMode == "Reverse")
                        .ToList()
                        .Sum(x => x.debit);

                    int? w = (D_withoutR_1 - waived_1) - (C_withoutW_1 - reversed_1);
                    int? r = (C_withoutW_1 - reversed_1);

                    hijri_months m = allmonths.Where(x => x.id == i.monthId).FirstOrDefault();

                    if (m == null)
                    {
                        references.Add(
                            new FeePaymentModel
                            {
                                allotmentId = i.id,
                                name =
                                    v?.displayName
                                    + " - "
                                    + "(  Miscellaneous :: "
                                    + i.remarks
                                    + ")  W : "
                                    + w
                                    + " R : "
                                    + r
                            }
                        );
                    }
                    else
                    {
                        references.Add(
                            new FeePaymentModel
                            {
                                allotmentId = i.id,
                                name =
                                    v?.displayName
                                    + " - "
                                    + "( "
                                    + m?.hijriMonthName
                                    + " / "
                                    + i.hijriYear
                                    + ")  W : "
                                    + w
                                    + " R : "
                                    + r
                            }
                        );
                    }
                }

                string fcName = "";
                int fcId = 0;
                string amount = "";
                List<mz_student_feecategory> categories = _context.mz_student_feecategory.ToList();

                if (s.fcId != null || s.fcId != 0)
                {
                    mz_student_feecategory fc_Name = _context
                        .mz_student_feecategory.Where(x => x.id == s.fcId)
                        .FirstOrDefault();
                    if (fc_Name != null)
                    {
                        fcName = fc_Name.categoryName;
                        fcId = fc_Name.id;
                    }

                    mz_student_feecategory_pset amnt = _context
                        .mz_student_feecategory_pset.Where(x =>
                            x.fcId == s.fcId && x.psetId == s.psetId
                        )
                        .FirstOrDefault();
                    if (amnt != null)
                    {
                        amount = amnt.amount?.ToString();
                    }
                }

                List<mz_fee_collection_center> cCenters =
                    _context.mz_fee_collection_center.ToList();

                string status = "In-Active";

                if (s.activeStatus == true)
                {
                    status = "Active";
                }

                var pmodes = _context
                    .mz_receipt_payment_mode_rights.Where(x => x.itsId == authUser.ItsId)
                    .ToList();

                foreach (var i in pmodes)
                {
                    var m = _context
                        .mz_receipt_payment_modes.Where(x => x.id == i.paymentModeId)
                        .FirstOrDefault();

                    modes.Add(new dropdown_dataset_header { name = m.name });
                }

                return Ok(
                    new
                    {
                        modes = modes,
                        status = status,
                        cCenters = cCenters,
                        wallet_b = wallet_b,
                        wallet_d = wallet_d,
                        wallet_c = wallet_c,
                        fcId = fcId,
                        amount = amount,
                        categories = categories,
                        fcName = fcName,
                        references = references,
                        models = models,
                        allocated = D_withoutR,
                        paid = C_withoutW,
                        waived = waived,
                        reversed = reversed,
                        balance = balance1,
                        name = s.nameEng
                            + " ( MZ_ID : "
                            + s.mz_id
                            + " )  :: ( Status : "
                            + s.activeStatus
                            + " )",
                        program = p1.name
                            + " _ "
                            + sp1.name
                            + " _ "
                            + v1.displayName
                            + " ( "
                            + pset1.id
                            + " )"
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getPendingTransactionsList")]
        [HttpPost]
        public async Task<ActionResult> getPendingTransactionsList(FeePaymentModel model)
        {
            string api = "getPendingTransactionsList";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<FeeTransactionModel> models = new List<FeeTransactionModel>();
            List<FeePaymentModel> references = new List<FeePaymentModel>();

            List<string> txnList = _helperService.parseMobNo(model.name);

            foreach (var i in txnList)
            {
                mz_student_receipt r = _context
                    .mz_student_receipt.Where(x => x.transactionId == i && x.status == "Active")
                    .FirstOrDefault();

                if (r != null)
                {
                    mz_student s = _context
                        .mz_student.Where(x => x.mz_id == r.studentId)
                        .FirstOrDefault();
                    references.Add(new FeePaymentModel { paymentId = i, name = s.nameEng });
                }
                else
                {
                    references.Add(new FeePaymentModel { paymentId = i, name = "Not Found" });
                }
            }

            return Ok(new { export = references });
        }

        [Route("getwaiveandreverseamount/{itsId}/{allotmentId}/{type}")]
        [HttpGet]
        public async Task<ActionResult> getWaiveAndReverseAmount(
            int itsId,
            int allotmentId,
            string type
        )
        {
            string api = "getwaiveandreverseamount/{itsId}/{allotmentId}/{type}";
            //// Add_ApiLogs(api);
            mz_student s = new mz_student();

            s = _context.mz_student.Where(x => x.itsID == itsId).FirstOrDefault();
            if (s == null)
            {
                if (itsId != 500)
                {
                    s = _context.mz_student.Where(x => x.mz_id == itsId).FirstOrDefault();
                }
                else
                {
                    return BadRequest(new { message = "Student not found" });
                }
            }

            List<mz_student_fee_transaction> transactions = _context
                .mz_student_fee_transaction.Where(x => x.allotmentId == allotmentId)
                .ToList();

            if (type == "Waive")
            {
                int? D_withoutR = transactions
                    .Where(x => x.paymentMode != "Reverse")
                    .ToList()
                    .Sum(x => x.debit);
                int? waived = transactions
                    .Where(x => x.paymentMode == "Waive")
                    .ToList()
                    .Sum(x => x.credit);

                int? C_withoutW = transactions
                    .Where(x => x.paymentMode != "Waive")
                    .ToList()
                    .Sum(x => x.credit);
                int? reversed = transactions
                    .Where(x => x.paymentMode == "Reverse")
                    .ToList()
                    .Sum(x => x.debit);

                int amount =
                    ((D_withoutR ?? 0) - (waived ?? 0)) - ((C_withoutW ?? 0) - (reversed ?? 0));

                return Ok(amount);
            }
            else if (type == "Reverse")
            {
                int? C_withoutW = transactions
                    .Where(x => x.paymentMode != "Waive")
                    .ToList()
                    .Sum(x => x.credit);
                int? reversed = transactions
                    .Where(x => x.paymentMode == "Reverse")
                    .ToList()
                    .Sum(x => x.debit);

                int amount = (C_withoutW ?? 0 - reversed ?? 0);

                return Ok(amount);
            }
            else if (type == "Reverse (Wallet)")
            {
                int? C_withoutW = transactions
                    .Where(x => x.paymentMode != "Waive")
                    .ToList()
                    .Sum(x => x.credit);
                int? reversed = transactions
                    .Where(x => x.paymentMode == "Reverse")
                    .ToList()
                    .Sum(x => x.debit);

                int amount = (C_withoutW ?? 0 - reversed ?? 0);

                return Ok(amount);
            }

            return Ok();
        }

        [Route("submitFcId/{itsId}/{fcId}")]
        [HttpGet]
        public async Task<ActionResult> SubmitFcId(int itsId, int fcId)
        {
            string api = "submitFcId/{itsId}/{fcId}";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (itsId == 500)
            {
                itsId = authUser.ItsId;
            }
            mz_student s = new mz_student();

            s = _context.mz_student.Where(x => x.itsID == itsId).FirstOrDefault();
            if (s == null)
            {
                if (itsId != 500)
                {
                    s = _context.mz_student.Where(x => x.mz_id == itsId).FirstOrDefault();
                }
                else
                {
                    return BadRequest(new { message = "Student not found" });
                }
            }
            s.fcId = fcId;

            _context.SaveChanges();

            return Ok();
        }

        [Route("submitPsetId/{itsId}/{pset}")]
        [HttpGet]
        public async Task<ActionResult> SubmitPsetId(int itsId, int pset)
        {
            string api = "submitPsetId/{itsId}/{fcId}";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (itsId == 500)
            {
                itsId = authUser.ItsId;
            }
            mz_student s = new mz_student();

            s = _context.mz_student.Where(x => x.itsID == itsId).FirstOrDefault();
            if (s == null)
            {
                if (itsId != 500)
                {
                    s = _context.mz_student.Where(x => x.mz_id == itsId).FirstOrDefault();
                }
                else
                {
                    return BadRequest(new { message = "Student not found" });
                }
            }

            if (s.psetId != pset)
            {
                registrationform_dropdown_set set1 = _context
                    .registrationform_dropdown_set.Where(x => x.id == s.psetId)
                    .FirstOrDefault();
                registrationform_programs p1 = _context
                    .registrationform_programs.Where(x => x.id == set1.programId)
                    .FirstOrDefault();
                registrationform_subprograms sp1 = _context
                    .registrationform_subprograms.Where(x => x.id == set1.subprogramId)
                    .FirstOrDefault();
                venue v1 = _context.venue.Where(x => x.Id == set1.venueId).FirstOrDefault();

                registrationform_dropdown_set set2 = _context
                    .registrationform_dropdown_set.Where(x => x.id == pset)
                    .FirstOrDefault();
                registrationform_programs p2 = _context
                    .registrationform_programs.Where(x => x.id == set2.programId)
                    .FirstOrDefault();
                registrationform_subprograms sp2 = _context
                    .registrationform_subprograms.Where(x => x.id == set2.subprogramId)
                    .FirstOrDefault();
                venue v2 = _context.venue.Where(x => x.Id == set2.venueId).FirstOrDefault();

                s.psetId = pset;

                string psetName1 = p1.name + "_" + sp1.name + "_" + v1.displayName;
                string psetName2 = p2.name + "_" + sp2.name + "_" + v2.displayName;

                //mz_student_logs l = new mz_student_logs { description = "From :- " + psetName1 + " --  To :- " + psetName2, createdBy = authUser.Name, createdOn = indianTime, typeId = 3, studentId = s.mz_id, logId = Convert.ToInt32(authUser.logId) };
                //context.mz_student_logs.Add(l);
            }

            _context.SaveChanges();

            return Ok();
        }


        [Route("waiveandreverse")]
        [HttpPost]
        public async Task<ActionResult> waiveandreverse(FeeTransactionModel model)
        {
            string api = "waiveandreverse";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            mz_student s = _context.mz_student.Where(x => x.itsID == model.itsId).FirstOrDefault();

            List<mz_student_fee_transaction> transactions = _context
                .mz_student_fee_transaction.Where(x => x.allotmentId == model.id)
                .ToList();

            if (Convert.ToInt32(model.debit) <= 0)
            {
                return BadRequest(new { message = "Amount cannot be zero or less then zero" });
            }

            if (model.action == "Waive")
            {
                var trns = transactions.Where(x => x.paymentMode == "Waive").FirstOrDefault();
                if (trns != null)
                {
                    return BadRequest(new { message = "The selected fees is already waived." });
                }
                int? D_withoutR = transactions
                    .Where(x => x.paymentMode != "Reverse")
                    .ToList()
                    .Sum(x => x.debit);
                int? waived = transactions
                    .Where(x => x.paymentMode == "Waive")
                    .ToList()
                    .Sum(x => x.credit);

                int? C_withoutW = transactions
                    .Where(x => x.paymentMode != "Waive")
                    .ToList()
                    .Sum(x => x.credit);
                int? reversed = transactions
                    .Where(x => x.paymentMode == "Reverse")
                    .ToList()
                    .Sum(x => x.debit);

                int amount =
                    ((D_withoutR ?? 0) - (waived ?? 0)) - ((C_withoutW ?? 0) - (reversed ?? 0));

                if (Convert.ToInt32(model.debit) > amount)
                {
                    return BadRequest(new { message = "Max limit of waive for this reference is " + amount });
                }
                else
                {
                    _context.mz_student_fee_transaction.Add(
                        new mz_student_fee_transaction
                        {
                            allotmentId = model.id,
                            createdBy = authUser.Name,
                            createdOn = indianTime,
                            currency = "INR",
                            credit = Convert.ToInt32(model.debit),
                            paymentMode = "Waive",
                            studentId = s.mz_id,
                            remarks = model.reason + " :: " + model.note
                        }
                    );
                }
            }
            else if (model.action == "Reverse")
            {
                var trns = transactions.Where(x => x.paymentMode == "Reverse").FirstOrDefault();
                if (trns != null)
                {
                    return BadRequest(new { message = "The selected fees is already reversed." });
                }
                int? C_withoutW = transactions
                    .Where(x => x.paymentMode != "Waive")
                    .ToList()
                    .Sum(x => x.credit);
                int? reversed = transactions
                    .Where(x => x.paymentMode == "Reverse")
                    .ToList()
                    .Sum(x => x.debit);

                int amount = (C_withoutW ?? 0 - reversed ?? 0);

                if (Convert.ToInt32(model.debit) > amount)
                {
                    return BadRequest(new { message = "Max limit of reverse for this reference is " + amount });
                }
                else
                {
                    _context.mz_student_fee_transaction.Add(
                        new mz_student_fee_transaction
                        {
                            allotmentId = model.id,
                            createdBy = authUser.Name,
                            createdOn = indianTime,
                            currency = "INR",
                            debit = Convert.ToInt32(model.debit),
                            paymentMode = "Reverse",
                            studentId = s.mz_id,
                            remarks = model.reason + " :: " + model.note
                        }
                    );
                }
            }
            else if (model.action == "Reverse (Wallet)")
            {
                int? C_withoutW = transactions
                    .Where(x => x.paymentMode != "Waive")
                    .ToList()
                    .Sum(x => x.credit);
                int? reversed = transactions
                    .Where(x => x.paymentMode == "Reverse")
                    .ToList()
                    .Sum(x => x.debit);

                int amount = (C_withoutW ?? 0 - reversed ?? 0);

                if (Convert.ToInt32(model.debit) > amount)
                {
                    return BadRequest(new { message = "Max limit of reverse for this reference is " + amount });
                }
                else
                {
                    _context.mz_student_fee_transaction.Add(
                        new mz_student_fee_transaction
                        {
                            allotmentId = model.id,
                            createdBy = authUser.Name,
                            createdOn = indianTime,
                            currency = "INR",
                            debit = Convert.ToInt32(model.debit),
                            paymentMode = "Reverse",
                            studentId = s.mz_id,
                            remarks = "Transfer to Wallet :: " + model.reason + " :: " + model.note
                        }
                    );

                    //_context.mz_student_ewallet.Add(
                    //    new mz_student_ewallet
                    //    {
                    //        createdBy = authUser.Name,
                    //        createdOn = indianTime,
                    //        credit = Convert.ToInt32(model.debit),
                    //        currency = "INR",
                    //        note = model.reason + " :: " + model.note,
                    //        paymentType = "Transfer",
                    //        status = true,
                    //        studentId = s.mz_id
                    //    }
                    //);
                }
            }
            _context.SaveChanges();

            return Ok();
        }

        [Route("getFeeAllocationForPayment/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getFeeAllocationForPayment(int itsId)
        {
            string api = "api/studentfee/getFeeAllocationForPayment/{itsId}";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<FeePaymentModel> fees = new List<FeePaymentModel>();
            mz_student s = new mz_student();

            s = _context.mz_student.Where(x => x.itsID == itsId).FirstOrDefault();
            if (s == null)
            {
                if (itsId != 500)
                {
                    s = _context.mz_student.Where(x => x.mz_id == itsId).FirstOrDefault();
                }
                else
                {
                    return BadRequest(new { message = "Student not found" });
                }
            }
            List<mz_student_fee_allotment> allotments = _context
                .mz_student_fee_allotment.Where(x => x.studentId == s.mz_id)
                .ToList();
            allotments = allotments.OrderBy(x => x.hijriYear).ThenBy(x => x.monthId).ToList();
            int c = 1;
            foreach (var i in allotments)
            {
                List<mz_student_fee_transaction> transactions = _context
                    .mz_student_fee_transaction.Where(x => x.allotmentId == i.id)
                    .ToList();

                int? D_withoutR = transactions
                    .Where(x => x.paymentMode != "Reverse")
                    .ToList()
                    .Sum(x => x.debit);
                int? waived = transactions
                    .Where(x => x.paymentMode == "Waive")
                    .ToList()
                    .Sum(x => x.credit);

                int? C_withoutW = transactions
                    .Where(x => x.paymentMode != "Waive")
                    .ToList()
                    .Sum(x => x.credit);
                int? reversed = transactions
                    .Where(x => x.paymentMode == "Reverse")
                    .ToList()
                    .Sum(x => x.debit);

                int amount =
                    ((D_withoutR ?? 0) - (waived ?? 0)) - ((C_withoutW ?? 0) - (reversed ?? 0));

                registrationform_dropdown_set pset = _context
                    .registrationform_dropdown_set.Where(x => x.id == i.pSetId)
                    .FirstOrDefault();

                venue v = _context.venue.Where(x => x.Id == pset.venueId).FirstOrDefault();

                if (amount > 0)
                {
                    fees.Add(
                        new FeePaymentModel
                        {
                            amount = amount,
                            allotmentId = i.id,
                            id = c,
                            name =
                                v?.displayName
                                + " - "
                                + "( "
                                + i.monthId
                                + " / "
                                + i.hijriYear
                                + " H ) - "
                                + amount
                                + ".00 "
                                + i.currency
                        }
                    );

                    c = c + 1;
                }
            }

            return Ok(fees);
        }

        [Route("createReciept")]
        [HttpPost]
        public async Task<ActionResult> CreatePaymentReciept(List<FeePaymentModel> models)
        {
            string api = "createReciept";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //string transactionId = models.FirstOrDefault().paymentId;
            FeeTransactionModel reciept = models.FirstOrDefault().reciept;
            mz_student s = new mz_student();

            s = _context.mz_student.Where(x => x.itsID == reciept.itsId).FirstOrDefault();
            if (s == null)
            {
                if (reciept.itsId != 500)
                {
                    s = _context.mz_student.Where(x => x.mz_id == reciept.itsId).FirstOrDefault();
                }
                else
                {
                    return BadRequest(new { message = "Student not found" });
                }
            }
            int recieptNumber = 1;

            var rrList = _context
                .mz_student_receipt.Where(x =>
                    x.paymentMode == reciept.paymentType
                    && x.collectionCenter == reciept.collectioncenterId
                )
                .ToList();

            if (rrList.Count != 0)
            {
                recieptNumber =
                    (
                        rrList
                            .OrderByDescending(x => x.recieptNumber)
                            .FirstOrDefault()
                            ?.recieptNumber ?? 0
                    ) + 1;
            }

            foreach (var i in models)
            {
                int amount = _helperService.getMaxWaiveAmount(i.allotmentId ?? 0);

                if (i.amount > amount)
                {
                    return BadRequest(new { message = "Selected references has no due amount, please refresh." });
                }
            }

            var r = new mz_student_receipt
            {
                chequeDate = reciept.chequeDate,
                note = reciept.note,
                amount = models.Sum(x => x.amount),
                createdBy = authUser.Name,
                createdOn = indianTime,
                paymentMode = reciept.paymentType,
                currency = "INR",
                recieptDate = DateOnly.FromDateTime(indianTime),
                status = "Active",
                studentId = s.mz_id,
                transactionId = reciept.transactionId ?? "",
                collectionCenter = reciept.collectioncenterId,
                recieptNumber = recieptNumber,
                account = "DAWAT-E-HADIYAH",
                bankName = reciept.bankName
            };
            _context.mz_student_receipt.Add(r);
            _context.SaveChanges();

            foreach (var i in models)
            {
                _context.mz_student_fee_transaction.Add(
                    new mz_student_fee_transaction
                    {
                        allotmentId = i.allotmentId,
                        createdBy = authUser.Name,
                        createdOn = indianTime,
                        currency = "INR",
                        credit = i.amount,
                        paymentMode = reciept.paymentType,
                        studentId = s.mz_id,
                        transactionId = reciept.transactionId ?? "",
                        collection_center_no = reciept.collectioncenterId,
                        recieptId = r.id
                    }
                );
            }

            _context.SaveChanges();

            string strAmount = r.amount?.ToString();
            CurrencyAmount currencyConverter = new CurrencyAmount();

            string AmountInWords = currencyConverter.ConvertNumbertoWords((long)r.amount) + " ONLY";
            DateTime rDate;
            if (r.recieptDate?.Day >= 22)
            {
                int month2 = r.recieptDate?.AddMonths(1).Month ?? 1;
                int year2 = r.recieptDate?.AddMonths(1).Year ?? 1;
                rDate = new DateTime(year2, month2, 1);
            }
            else
            {
                int month2 = r.recieptDate?.AddMonths(-1).Month ?? 1;
                int year2 = r.recieptDate?.AddMonths(-1).Year ?? 1;

                DateTime todate = new DateTime(
                    r.recieptDate.Value.Year,
                    r.recieptDate.Value.Month,
                    7
                );
                DateTime fromdate = new DateTime(year2, month2, 22);

                if (
                    r.recieptDate >= DateOnly.FromDateTime(fromdate)
                    && r.recieptDate <= DateOnly.FromDateTime(todate)
                )
                {
                    rDate = new DateTime(r.recieptDate.Value.Year, r.recieptDate.Value.Month, 1);
                }
                else
                {
                    rDate = new DateTime(r.recieptDate.Value.Year, r.recieptDate.Value.Month, 15);
                }
            }

            var f = new FeesPaidModel
            {
                receiptNo = r.recieptNumber.ToString(),
                ItsId = s.itsID ?? 0,
                printDate = rDate,
                ChequeDate = r.chequeDate,
                ChequeNo = r.transactionId,
                BankName = r.bankName,
                CreatedBy = r.createdBy,
                FeePaidAmount = r.amount ?? 0,
                name = s.nameEng,
                PaymentMode = r.paymentMode,
                ReceiptId = r.recieptNumber ?? 0,
                amountInWord = AmountInWords,
            };

            return Ok(f);
        }

        [Route("useEwallet")]
        [HttpPost]
        public async Task<ActionResult> useEwallet(List<FeePaymentModel> models)
        {
            string api = "useEwallet";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            string transactionId = models.FirstOrDefault().paymentId;
            FeeTransactionModel reciept = models.FirstOrDefault().reciept;
            mz_student s = new mz_student();

            s = _context.mz_student.Where(x => x.itsID == reciept.itsId).FirstOrDefault();
            if (s == null)
            {
                if (reciept.itsId != 500)
                {
                    s = _context.mz_student.Where(x => x.mz_id == reciept.itsId).FirstOrDefault();
                }
                else
                {
                    return BadRequest(new { message = "Student not found" });
                }
            }

            foreach (var i in models)
            {
                int amount = _helperService.getMaxWaiveAmount(i.allotmentId ?? 0);

                if (i.amount > amount)
                {
                    return BadRequest(new { message = "Selected references has no due amount, please refresh." });
                }
            }

            foreach (var i in models)
            {
                List<mz_student_ewallet> wallets = _context
                    .mz_student_ewallet.Where(x => x.studentId == s.mz_id && x.status == true)
                    .ToList();

                if (wallets.Count > 0)
                {
                    int sum = (wallets.Sum(x => x.credit) ?? 0) - (wallets.Sum(x => x.debit) ?? 0);

                    if (sum > 0)
                    {
                        if (sum <= i.amount)
                        {
                            var w = new mz_student_ewallet
                            {
                                createdBy = authUser.Name,
                                createdOn = indianTime,
                                debit = sum,
                                currency = "INR",
                                note =
                                    reciept.note + " :: Mz_Student_Allotment_Id - " + i.allotmentId,
                                paymentType = "Used",
                                status = true,
                                studentId = s.mz_id
                            };
                            _context.mz_student_ewallet.Add(w);

                            _context.mz_student_fee_transaction.Add(
                                new mz_student_fee_transaction
                                {
                                    allotmentId = i.allotmentId,
                                    createdBy = authUser.Name,
                                    createdOn = indianTime,
                                    currency = "INR",
                                    credit = sum,
                                    paymentMode = "EWallet",
                                    transactionId = s.mz_id.ToString(),
                                    studentId = s.mz_id,
                                    remarks = " :: Mz_Student_EWallet_Id - " + w.id
                                }
                            );
                        }
                        else if (sum > i.amount)
                        {
                            var w = new mz_student_ewallet
                            {
                                createdBy = authUser.Name,
                                createdOn = indianTime,
                                debit = i.amount,
                                currency = "INR",
                                note =
                                    reciept.note + " :: Mz_Student_Allotment_Id - " + i.allotmentId,
                                paymentType = "Used",
                                status = true,
                                studentId = s.mz_id
                            };
                            _context.mz_student_ewallet.Add(w);

                            _context.mz_student_fee_transaction.Add(
                                new mz_student_fee_transaction
                                {
                                    allotmentId = i.allotmentId,
                                    createdBy = authUser.Name,
                                    createdOn = indianTime,
                                    currency = "INR",
                                    credit = i.amount,
                                    paymentMode = "EWallet",
                                    transactionId = s.mz_id.ToString(),
                                    studentId = s.mz_id,
                                    remarks = " :: Mz_Student_EWallet_Id - " + w.id
                                }
                            );
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            _context.SaveChanges();

            return Ok();
        }

        [Route("getReciepts")]
        [HttpPost]
        public async Task<ActionResult> getallfeealloted(SearchRecieptModel model)
        {
            string api = "getRecipets";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                DateOnly minDate = DateOnly.FromDateTime(DateTime.MinValue);

                var venueIds = (from srr in _context.registrationform_dropdown_set
                                join rdss in _context.student_registration_rights on srr.id equals rdss.programSetId
                                where rdss.itsId == authUser.ItsId
                                select srr.venueId).Distinct().ToList();

                List<registrationform_dropdown_set> rfds = _context.registrationform_dropdown_set.Where(x => venueIds.Contains(x.venueId.Value)).ToList();
                List<int> psetsAssigned = rfds.Select(x => x.id).ToList();

                var students = _context.mz_student.Where(x => psetsAssigned.Contains(x.psetId.Value)).ToList();

                var studentsId = students.Select(x => x.mz_id).ToList();

                bool isDateFiltered = model.fromDate != minDate && model.toDate != minDate && model.fromDate != null && model.toDate != null;

                var itsIds = string.IsNullOrEmpty(model.itsCsv) ? null : _helperService.parseItsId(model.itsCsv);

                var query = _context.mz_student_receipt.AsQueryable();
                if (itsIds != null)
                {
                    List<int> studentIdList = students.Where(s => itsIds.Contains(s.itsID ?? 0)).Select(s => s.mz_id).ToList();
                    query = query.Where(r => studentIdList.Contains(r.studentId ?? 0));
                }
                if (isDateFiltered)
                {
                    query = query.Where(r => r.recieptDate >= model.fromDate && r.recieptDate <= model.toDate && studentsId.Contains(r.studentId.Value));
                }

                List<mz_student_receipt> receipts = await query.ToListAsync();
                List<int> studentIds = receipts.Select(r => r.studentId ?? 0).Distinct().ToList();
                //List<mz_student> students = await _context.mz_student.Where(s => studentIds.Contains(s.mz_id)).ToListAsync();
                List<int> centerIds = receipts.Select(r => r.collectionCenter ?? 0).ToList();
                List<mz_fee_collection_center> centers = await _context.mz_fee_collection_center.Where(c => centerIds.Contains(c.id)).ToListAsync();


                List<RecieptModel> modelnew = receipts.Select(r =>
                {
                    DateTime rDate = CalculateReceiptDate(r.recieptDate?.ToDateTime(new TimeOnly(0, 0)) ?? DateTime.MinValue);
                    mz_student student = students.Where(s => s.mz_id == r.studentId).FirstOrDefault();
                    string centerName = centers.Where(x => x.id == r.collectionCenter).Select(x => x.name).FirstOrDefault();
                    string amountInWords = _helperService.ChangeToWords(r.amount?.ToString());

                    return new RecieptModel
                    {
                        chequeNo = r.transactionId,
                        createdBy = r.createdBy,
                        paymentMode = r.paymentMode,
                        receiptId = r.id.ToString(),
                        feePaidAmount = r.amount.ToString(),
                        itsId = student.itsID ?? 0,
                        name = student.nameEng,
                        account = r.account,
                        recieptDate = r.recieptDate ?? today,
                        recieptDate_print = rDate,
                        note = r.note,
                        collectionCenter = centerName,
                        status = r.status,
                        receiptNo = r.recieptNumber?.ToString(),
                        bankName = r.bankName,
                        studentId = student.mz_id,
                        printDate = rDate,
                        amountInWord = amountInWords,
                        chequeDate = r.chequeDate,
                        id = r.id,
                    };
                }).ToList();

                List<dropdown_dataset_options> paymentModeDD = modelnew
                    .GroupBy(x => x.paymentMode)
                    .Select(x => new dropdown_dataset_options { name = x.Key }).ToList();
                List<dropdown_dataset_options> statusDD = modelnew
                    .GroupBy(x => x.status)
                    .Select(x => new dropdown_dataset_options { name = x.Key }).ToList();
                var exportCategory = await _context.export_category.Where(x => x.categoryId == 15).ToListAsync();

                return Ok(new feeAllotedResultModel { model = modelnew, exportCategory = exportCategory, paymentModeDD = paymentModeDD, statusDD = statusDD });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        private DateTime CalculateReceiptDate(DateTime receiptDate)
        {
            // Check if the day part of receiptDate is greater than 21
            if (receiptDate.Day > 21)
            {
                // Add one month to the current receiptDate and set the day to the 1st of the next month
                return new DateTime(receiptDate.Year, receiptDate.Month, 1).AddMonths(1);
            }
            else
            {
                // Return the original receiptDate since the day is 21 or less
                return receiptDate;
            }
        }


        [Route("getActualIncomeDashboard")]
        [HttpPost]
        public async Task<ActionResult> getActualIncomeDashboard(SearchRecieptModel model)
        {
            string api = "getActualIncomeDashboard";
            //// Add_ApiLogs(api);


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<IncomeDashBoardModel> modelnew = new List<IncomeDashBoardModel>();
                DateRange dateRange = new DateRange();
                DateTime dt = DateTime.MinValue;

                List<mz_student_fee_transaction> transactions = _context
                    .mz_student_fee_transaction.Where(x => !x.paymentMode.Equals("Waive"))
                    .ToList();

                List<int> psets = new List<int>();
                List<string> psetNames = new List<string>();
                List<int> psetIncomes = new List<int>();
                MahadIncome_Branch Income = new MahadIncome_Branch();

                ElearningService eqservice = new ElearningService();

                if (model.fromDate != DateOnly.MinValue && model.toDate != DateOnly.MinValue)
                {
                    //dateRange.FromDate = model.fromDate;
                    //dateRange.ToDate = model.toDate.AddDays(1);
                    transactions = transactions
                        .Where(x =>
                            DateOnly.FromDateTime(x.createdOn ?? dt) >= model.fromDate
                            && DateOnly.FromDateTime(x.createdOn ?? dt) <= model.toDate?.AddDays(1)
                        )
                        .ToList();

                    List<FeeCategoryModel> branchIds = new List<FeeCategoryModel>();
                    List<string> currencys = new List<string>();

                    branchIds.Add(new FeeCategoryModel { id = 1, psetName = "Elearning Surat" });
                    branchIds.Add(new FeeCategoryModel { id = 10, psetName = "Atfaal Surat" });
                    branchIds.Add(new FeeCategoryModel { id = 25, psetName = "Talim al-Quran" });
                    branchIds.Add(new FeeCategoryModel { id = 98, psetName = "Tahfeez Raza" });

                    currencys.Add("INR");
                    currencys.Add("PKR");
                    currencys.Add("USD");

                    foreach (var i in transactions)
                    {
                        mz_student_fee_allotment a = _context
                            .mz_student_fee_allotment.Where(x => x.id == i.allotmentId)
                            .FirstOrDefault();
                        if (a != null)
                        {
                            psets.Add(a.pSetId ?? 0);
                            i.psetId = a.pSetId ?? 0;
                        }
                    }

                    psets = psets.Distinct().ToList();
                    int credit1 = transactions
                        .Where(x => x.paymentMode != "Waive")
                        .ToList()
                        .Sum(x => x.credit ?? 0);
                    int debit1 = transactions
                        .Where(x => x.paymentMode == "Reverse")
                        .ToList()
                        .Sum(x => x.debit ?? 0);

                    int gTotal = credit1 - debit1;

                    foreach (var i in branchIds)
                    {
                        Income.Elearning = eqservice.GetElearning_Income_InDateRange_2_BranchWise(
                            dateRange.FromDate.Value.ToString("dd-MM-yyyy"),
                            model.toDate?.ToString("dd-MM-yyyy"),
                            i.id,
                            i.psetName
                        );
                        gTotal =
                            gTotal
                            + Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == "INR")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                    }

                    foreach (var i in psets)
                    {
                        int credit = transactions
                            .Where(x => x.psetId == i && x.paymentMode != "Waive")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int debit = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Reverse")
                            .ToList()
                            .Sum(x => x.debit ?? 0);

                        string venue = "";
                        string program = "";
                        string subProgram = "";

                        registrationform_dropdown_set rset = _context
                            .registrationform_dropdown_set.Where(x => x.id == i)
                            .FirstOrDefault();
                        if (rset != null)
                        {
                            registrationform_programs p = _context
                                .registrationform_programs.Where(x => x.id == rset.programId)
                                .FirstOrDefault();
                            registrationform_subprograms sp = _context
                                .registrationform_subprograms.Where(x => x.id == rset.subprogramId)
                                .FirstOrDefault();
                            venue v = _context
                                .venue.Where(x => x.Id == rset.venueId)
                                .FirstOrDefault();

                            venue = v?.displayName;
                            program = p.name;
                            subProgram = sp.name;
                        }

                        List<ChartLabel> data = new List<ChartLabel>();
                        List<int> amounts = new List<int>();

                        int totalIncome = credit - debit;

                        int cash = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Cash")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int cheque = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Cheque")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int online = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Online")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int ewallet = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "EWallet")
                            .ToList()
                            .Sum(x => x.credit ?? 0);

                        amounts.Add(cash);
                        amounts.Add(cheque);
                        amounts.Add(online);
                        amounts.Add(ewallet);

                        data.Add(new ChartLabel { data = amounts, label = "Online" });
                        float incomePercentage = (totalIncome * 100) / gTotal;

                        psetNames.Add(program + "_" + subProgram + "_" + venue);
                        psetIncomes.Add(totalIncome);

                        modelnew.Add(
                            new IncomeDashBoardModel
                            {
                                currency = "INR",
                                gTotalIncome = gTotal,
                                incomePercentage = incomePercentage,
                                data = data,
                                cashAmount = cash,
                                chequeAmount = cheque,
                                totalIncome = totalIncome,
                                onlineAmount = online,
                                walletAmount = ewallet,
                                psetName = program + "_" + subProgram + "_" + venue
                            }
                        );
                    }

                    foreach (var i in branchIds)
                    {
                        foreach (var j in currencys)
                        {
                            Income.Elearning =
                                eqservice.GetElearning_Income_InDateRange_2_BranchWise(
                                    dateRange.FromDate.Value.ToString("dd-MM-yyyy"),
                                    model.toDate?.ToString("dd-MM-yyyy"),
                                    i.id,
                                    i.psetName + " - " + j
                                );

                            List<ChartLabel> data = new List<ChartLabel>();
                            List<int> amounts = new List<int>();

                            string venue = i.psetName;

                            int totalIncome = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j)
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );

                            int cash = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j && x.Mode == "Cash")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                            int cheque = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j && x.Mode == "Cheque")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                            int online = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j && x.Mode == "Online")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                            int ewallet = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x =>
                                        x.Currency == j && x.Mode == "e-Wallet used"
                                    )
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );

                            amounts.Add(cash);
                            amounts.Add(cheque);
                            amounts.Add(online);
                            amounts.Add(ewallet);

                            data.Add(new ChartLabel { data = amounts, label = "Online" });
                            float incomePercentage = (totalIncome * 100) / gTotal;

                            psetNames.Add(venue + " - " + j);
                            psetIncomes.Add(totalIncome);

                            modelnew.Add(
                                new IncomeDashBoardModel
                                {
                                    currency = j,
                                    gTotalIncome = gTotal,
                                    incomePercentage = incomePercentage,
                                    data = data,
                                    cashAmount = cash,
                                    chequeAmount = cheque,
                                    totalIncome = totalIncome,
                                    onlineAmount = online,
                                    walletAmount = ewallet,
                                    psetName = venue
                                }
                            );
                        }
                    }
                }
                else
                {
                    dateRange.FromDate = DateTime.Now.AddDays(-30).Date;
                    dateRange.ToDate = DateTime.Now.Date;
                    transactions = transactions
                        .Where(x =>
                            x.createdOn >= DateTime.Today.AddDays(-30).Date
                            && x.createdOn <= DateTime.Today.Date
                        )
                        .ToList();

                    foreach (var i in transactions)
                    {
                        mz_student_fee_allotment a = _context
                            .mz_student_fee_allotment.Where(x => x.id == i.allotmentId)
                            .FirstOrDefault();
                        if (a != null)
                        {
                            psets.Add(a.pSetId ?? 0);
                            i.psetId = a.pSetId ?? 0;
                        }
                    }

                    psets = psets.Distinct().ToList();
                    int credit1 = transactions
                        .Where(x => x.paymentMode != "Waive")
                        .ToList()
                        .Sum(x => x.credit ?? 0);
                    int debit1 = transactions
                        .Where(x => x.paymentMode == "Reverse")
                        .ToList()
                        .Sum(x => x.debit ?? 0);

                    int gTotal = credit1 - debit1;

                    List<FeeCategoryModel> branchIds = new List<FeeCategoryModel>();
                    List<string> currencys = new List<string>();

                    branchIds.Add(new FeeCategoryModel { id = 1, psetName = "Elearning Surat" });
                    branchIds.Add(new FeeCategoryModel { id = 10, psetName = "Atfaal Surat" });
                    branchIds.Add(new FeeCategoryModel { id = 25, psetName = "Talim al-Quran" });
                    branchIds.Add(new FeeCategoryModel { id = 98, psetName = "Tahfeez Raza" });

                    currencys.Add("INR");
                    currencys.Add("PKR");
                    currencys.Add("USD");

                    foreach (var i in branchIds)
                    {
                        Income.Elearning = eqservice.GetElearning_Income_InDateRange_2_BranchWise(
                            DateTime.Today.AddDays(-30).Date.ToString("dd-MM-yyyy"),
                            DateTime.Today.Date.ToString("dd-MM-yyyy"),
                            i.id,
                            i.psetName
                        );
                        gTotal =
                            gTotal
                            + Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == "INR")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                    }

                    foreach (var i in psets)
                    {
                        int credit = transactions
                            .Where(x => x.psetId == i && x.paymentMode != "Waive")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int debit = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Reverse")
                            .ToList()
                            .Sum(x => x.debit ?? 0);

                        string venue = "";
                        string program = "";
                        string subProgram = "";

                        registrationform_dropdown_set rset = _context
                            .registrationform_dropdown_set.Where(x => x.id == i)
                            .FirstOrDefault();
                        if (rset != null)
                        {
                            registrationform_programs p = _context
                                .registrationform_programs.Where(x => x.id == rset.programId)
                                .FirstOrDefault();
                            registrationform_subprograms sp = _context
                                .registrationform_subprograms.Where(x => x.id == rset.subprogramId)
                                .FirstOrDefault();
                            venue v = _context
                                .venue.Where(x => x.Id == rset.venueId)
                                .FirstOrDefault();

                            venue = v?.displayName;
                            program = p.name;
                            subProgram = sp.name;
                        }

                        List<ChartLabel> data = new List<ChartLabel>();
                        List<int> amounts = new List<int>();

                        int totalIncome = credit - debit;

                        int cash = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Cash")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int cheque = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Cheque")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int online = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Online")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int ewallet = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "EWallet")
                            .ToList()
                            .Sum(x => x.credit ?? 0);

                        amounts.Add(cash);
                        amounts.Add(cheque);
                        amounts.Add(online);
                        amounts.Add(ewallet);

                        data.Add(new ChartLabel { data = amounts, label = "Online" });
                        float incomePercentage = (totalIncome * 100) / gTotal;

                        psetNames.Add(program + "_" + subProgram + "_" + venue);
                        psetIncomes.Add(totalIncome);

                        modelnew.Add(
                            new IncomeDashBoardModel
                            {
                                currency = "INR",
                                gTotalIncome = gTotal,
                                incomePercentage = incomePercentage,
                                data = data,
                                cashAmount = cash,
                                chequeAmount = cheque,
                                totalIncome = totalIncome,
                                onlineAmount = online,
                                walletAmount = ewallet,
                                psetName = program + "_" + subProgram + "_" + venue
                            }
                        );
                    }

                    foreach (var i in branchIds)
                    {
                        foreach (var j in currencys)
                        {
                            Income.Elearning =
                                eqservice.GetElearning_Income_InDateRange_2_BranchWise(
                                    DateTime.Today.AddDays(-30).Date.ToString("dd-MM-yyyy"),
                                    DateTime.Today.Date.ToString("dd-MM-yyyy"),
                                    i.id,
                                    i.psetName + " - " + j
                                );

                            List<ChartLabel> data = new List<ChartLabel>();
                            List<int> amounts = new List<int>();

                            string venue = i.psetName + " - " + j;

                            int totalIncome = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j)
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );

                            int cash = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j && x.Mode == "Cash")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                            int cheque = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j && x.Mode == "Cheque")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                            int online = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j && x.Mode == "Online")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                            int ewallet = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x =>
                                        x.Currency == j && x.Mode == "e-Wallet used"
                                    )
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );

                            amounts.Add(cash);
                            amounts.Add(cheque);
                            amounts.Add(online);
                            amounts.Add(ewallet);

                            data.Add(new ChartLabel { data = amounts, label = "Online" });
                            float incomePercentage = (totalIncome * 100) / gTotal;

                            psetNames.Add(venue);
                            psetIncomes.Add(totalIncome);

                            modelnew.Add(
                                new IncomeDashBoardModel
                                {
                                    currency = j,
                                    gTotalIncome = gTotal,
                                    incomePercentage = incomePercentage,
                                    data = data,
                                    cashAmount = cash,
                                    chequeAmount = cheque,
                                    totalIncome = totalIncome,
                                    onlineAmount = online,
                                    walletAmount = ewallet,
                                    psetName = venue
                                }
                            );
                        }
                    }
                }
                return Ok(
                    new
                    {
                        model = modelnew,
                        psetIncomes = psetIncomes,
                        psetNames = psetNames
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getDepartmentWiseIncomeDashboard")]
        [HttpPost]
        public async Task<ActionResult> getDepartmentWiseIncomeDashboard(SearchRecieptModel model)
        {
            string api = "getDepartmentWiseIncomeDashboard";
            //// Add_ApiLogs(api);
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<IncomeExpenseDashBoardModel> modelnew = new List<IncomeExpenseDashBoardModel>();
                DateRange dateRange = new DateRange();

                List<mz_student_fee_transaction> transactions = _context
                    .mz_student_fee_transaction.Where(x => !x.paymentMode.Equals("Waive"))
                    .ToList();

                List<int> psets = new List<int>();
                List<string> psetNames = new List<string>();
                List<int> psetIncomes = new List<int>();
                List<int> deptExpenses = new List<int>();
                ElearningService eqservice = new ElearningService();
                MahadIncome_Branch Income = new MahadIncome_Branch();
                DateTime dt = DateTime.MinValue;
                List<dept_venue> dv = _context.dept_venue.ToList();
                List<dept_venue> considerDepartments = new List<dept_venue>();

                if (model.fromDate != DateOnly.MinValue && model.toDate != DateOnly.MinValue)
                {
                    //dateRange.FromDate = model.fromDate;
                    //dateRange.ToDate = model.toDate.AddDays(1);
                    transactions = transactions
                        .Where(x =>
                            x.createdOn >= model.fromDate?.ToDateTime(new TimeOnly(0, 0))
                            && x.createdOn <= model.toDate?.AddDays(1).ToDateTime(new TimeOnly(23, 59))
                        )
                        .ToList();

                    List<FeeCategoryModel> branchIds = new List<FeeCategoryModel>();
                    List<string> currencys = new List<string>();

                    branchIds.Add(
                        new FeeCategoryModel
                        {
                            id = 1,
                            psetName = "ELEARNING Surat",
                            psetId = 3
                        }
                    );
                    branchIds.Add(
                        new FeeCategoryModel
                        {
                            id = 10,
                            psetName = "ATFAAL Surat",
                            psetId = 21
                        }
                    );
                    branchIds.Add(
                        new FeeCategoryModel
                        {
                            id = 25,
                            psetName = "TALIM-AL-QURAN Surat",
                            psetId = 35
                        }
                    );
                    branchIds.Add(
                        new FeeCategoryModel
                        {
                            id = 98,
                            psetName = "Tahfeez Raza",
                            psetId = 0
                        }
                    );

                    currencys.Add("INR");
                    currencys.Add("PKR");
                    currencys.Add("USD");

                    foreach (var i in transactions)
                    {
                        mz_student_fee_allotment a = _context
                            .mz_student_fee_allotment.Where(x => x.id == i.allotmentId)
                            .FirstOrDefault();
                        if (a != null)
                        {
                            psets.Add(a.pSetId ?? 0);
                            i.psetId = a.pSetId ?? 0;
                        }
                    }

                    psets = psets.Distinct().ToList();
                    int credit1 = transactions
                        .Where(x => x.paymentMode != "Waive")
                        .ToList()
                        .Sum(x => x.credit ?? 0);
                    int debit1 = transactions
                        .Where(x => x.paymentMode == "Reverse")
                        .ToList()
                        .Sum(x => x.debit ?? 0);

                    int gTotal = credit1 - debit1;

                    foreach (var i in branchIds)
                    {
                        Income.Elearning = eqservice.GetElearning_Income_InDateRange_2_BranchWise(
                            dateRange.FromDate.Value.ToString("dd-MM-yyyy"),
                            model.toDate?.ToString("dd-MM-yyyy"),
                            i.id,
                            i.psetName
                        );
                        gTotal =
                            gTotal
                            + Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == "INR")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                    }

                    foreach (var i in psets)
                    {
                        int credit = transactions
                            .Where(x => x.psetId == i && x.paymentMode != "Waive")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int debit = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Reverse")
                            .ToList()
                            .Sum(x => x.debit ?? 0);

                        string venue = "";
                        string program = "";
                        string subProgram = "";

                        registrationform_dropdown_set rset = _context
                            .registrationform_dropdown_set.Where(x => x.id == i)
                            .FirstOrDefault();
                        if (rset != null)
                        {
                            registrationform_programs p = _context
                                .registrationform_programs.Where(x => x.id == rset.programId)
                                .FirstOrDefault();
                            registrationform_subprograms sp = _context
                                .registrationform_subprograms.Where(x => x.id == rset.subprogramId)
                                .FirstOrDefault();
                            venue v = _context
                                .venue.Where(x => x.Id == rset.venueId)
                                .FirstOrDefault();

                            venue = v?.displayName;
                            program = p.name;
                            subProgram = sp.name;
                        }

                        List<ChartLabel> data = new List<ChartLabel>();
                        List<int> amounts = new List<int>();

                        int totalIncome = credit - debit;

                        int cash = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Cash")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int cheque = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Cheque")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int online = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Online")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int ewallet = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "EWallet")
                            .ToList()
                            .Sum(x => x.credit ?? 0);

                        amounts.Add(cash);
                        amounts.Add(cheque);
                        amounts.Add(online);
                        amounts.Add(ewallet);

                        data.Add(new ChartLabel { data = amounts, label = "Online" });
                        float incomePercentage = (totalIncome * 100) / gTotal;

                        string d = dv.Where(x => x.id == rset.deptVenueId)
                            .FirstOrDefault()
                            .deptName;

                        if (psetNames.Contains(d + "_" + venue))
                        {
                            int index = psetNames.IndexOf(d + "_" + venue);
                            psetIncomes[index] += totalIncome;
                            IncomeExpenseDashBoardModel idm = modelnew[index];
                            idm.totalIncome += totalIncome;
                            idm.cashAmount += cash;
                            idm.chequeAmount += cheque;
                            idm.onlineAmount += online;
                            idm.walletAmount += ewallet;
                        }
                        else
                        {
                            considerDepartments.Add(
                                dv.Where(x => x.id == rset.deptVenueId).FirstOrDefault()
                            );
                            modelnew.Add(
                                new IncomeExpenseDashBoardModel
                                {
                                    currency = "INR",
                                    gTotalIncome = gTotal,
                                    data = data,
                                    cashAmount = cash,
                                    chequeAmount = cheque,
                                    totalIncome = totalIncome,
                                    onlineAmount = online,
                                    walletAmount = ewallet,
                                    psetName = d + "_" + venue
                                }
                            );
                            psetNames.Add(d + "_" + venue);
                            psetIncomes.Add(totalIncome);
                            deptExpenses.Add(0);
                        }
                    }

                    foreach (var i in branchIds)
                    {
                        foreach (var j in currencys)
                        {
                            Income.Elearning =
                                eqservice.GetElearning_Income_InDateRange_2_BranchWise(
                                    dateRange.FromDate.Value.ToString("dd-MM-yyyy"),
                                    model.toDate?.ToString("dd-MM-yyyy"),
                                    i.id,
                                    i.psetName + " - " + j
                                );

                            List<ChartLabel> data = new List<ChartLabel>();
                            List<int> amounts = new List<int>();

                            string venue = i.psetName;

                            int totalIncome = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j)
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );

                            int cash = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j && x.Mode == "Cash")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                            int cheque = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j && x.Mode == "Cheque")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                            int online = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j && x.Mode == "Online")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                            int ewallet = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x =>
                                        x.Currency == j && x.Mode == "e-Wallet used"
                                    )
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );

                            amounts.Add(cash);
                            amounts.Add(cheque);
                            amounts.Add(online);
                            amounts.Add(ewallet);

                            data.Add(new ChartLabel { data = amounts, label = "Online" });
                            float incomePercentage = (totalIncome * 100) / gTotal;

                            psetNames.Add(venue + " - " + j);
                            psetIncomes.Add(totalIncome);
                            deptExpenses.Add(0);
                            considerDepartments.Add(
                                dv.Where(x => x.id == i.psetId).FirstOrDefault()
                            );

                            modelnew.Add(
                                new IncomeExpenseDashBoardModel
                                {
                                    currency = j,
                                    gTotalIncome = gTotal,
                                    incomePercentage = incomePercentage,
                                    data = data,
                                    cashAmount = cash,
                                    chequeAmount = cheque,
                                    totalIncome = totalIncome,
                                    onlineAmount = online,
                                    walletAmount = ewallet,
                                    psetName = venue + " - " + j
                                }
                            );
                        }
                    }
                }
                else
                {
                    dateRange.FromDate = DateTime.Now.AddDays(-30).Date;
                    dateRange.ToDate = DateTime.Now.Date;
                    transactions = transactions
                        .Where(x =>
                            x.createdOn >= DateTime.Today.AddDays(-30).Date
                            && x.createdOn <= DateTime.Today.Date
                        )
                        .ToList();

                    foreach (var i in transactions)
                    {
                        mz_student_fee_allotment a = _context
                            .mz_student_fee_allotment.Where(x => x.id == i.allotmentId)
                            .FirstOrDefault();
                        if (a != null)
                        {
                            psets.Add(a.pSetId ?? 0);
                            i.psetId = a.pSetId ?? 0;
                        }
                    }

                    psets = psets.Distinct().ToList();
                    int credit1 = transactions
                        .Where(x => x.paymentMode != "Waive")
                        .ToList()
                        .Sum(x => x.credit ?? 0);
                    int debit1 = transactions
                        .Where(x => x.paymentMode == "Reverse")
                        .ToList()
                        .Sum(x => x.debit ?? 0);

                    int gTotal = credit1 - debit1;

                    List<FeeCategoryModel> branchIds = new List<FeeCategoryModel>();
                    List<string> currencys = new List<string>();

                    branchIds.Add(
                        new FeeCategoryModel
                        {
                            id = 1,
                            psetName = "ELEARNING Surat",
                            psetId = 3
                        }
                    );
                    branchIds.Add(
                        new FeeCategoryModel
                        {
                            id = 10,
                            psetName = "ATFAAL Surat",
                            psetId = 21
                        }
                    );
                    branchIds.Add(
                        new FeeCategoryModel
                        {
                            id = 25,
                            psetName = "TALIM-AL-QURAN Surat",
                            psetId = 35
                        }
                    );
                    branchIds.Add(
                        new FeeCategoryModel
                        {
                            id = 98,
                            psetName = "Tahfeez Raza",
                            psetId = 0
                        }
                    );

                    currencys.Add("INR");
                    currencys.Add("PKR");
                    currencys.Add("USD");

                    foreach (var i in branchIds)
                    {
                        Income.Elearning = eqservice.GetElearning_Income_InDateRange_2_BranchWise(
                            DateTime.Today.AddDays(-30).Date.ToString("dd-MM-yyyy"),
                            DateTime.Today.Date.ToString("dd-MM-yyyy"),
                            i.id,
                            i.psetName
                        );
                        gTotal =
                            gTotal
                            + Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == "INR")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                    }

                    foreach (var i in psets)
                    {
                        int credit = transactions
                            .Where(x => x.psetId == i && x.paymentMode != "Waive")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int debit = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Reverse")
                            .ToList()
                            .Sum(x => x.debit ?? 0);

                        string venue = "";
                        string program = "";
                        string subProgram = "";

                        registrationform_dropdown_set rset = _context
                            .registrationform_dropdown_set.Where(x => x.id == i)
                            .FirstOrDefault();
                        if (rset != null)
                        {
                            registrationform_programs p = _context
                                .registrationform_programs.Where(x => x.id == rset.programId)
                                .FirstOrDefault();
                            registrationform_subprograms sp = _context
                                .registrationform_subprograms.Where(x => x.id == rset.subprogramId)
                                .FirstOrDefault();
                            venue v = _context
                                .venue.Where(x => x.Id == rset.venueId)
                                .FirstOrDefault();

                            venue = v?.displayName;
                            program = p.name;
                            subProgram = sp.name;
                        }

                        List<ChartLabel> data = new List<ChartLabel>();
                        List<int> amounts = new List<int>();

                        int totalIncome = credit - debit;

                        int cash = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Cash")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int cheque = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Cheque")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int online = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "Online")
                            .ToList()
                            .Sum(x => x.credit ?? 0);
                        int ewallet = transactions
                            .Where(x => x.psetId == i && x.paymentMode == "EWallet")
                            .ToList()
                            .Sum(x => x.credit ?? 0);

                        amounts.Add(cash);
                        amounts.Add(cheque);
                        amounts.Add(online);
                        amounts.Add(ewallet);

                        data.Add(new ChartLabel { data = amounts, label = "Online" });
                        float incomePercentage = (totalIncome * 100) / gTotal;

                        string d = dv.Where(x => x.id == rset.deptVenueId)
                            .FirstOrDefault()
                            .deptName;

                        if (psetNames.Contains(d + "_" + venue))
                        {
                            int index = psetNames.IndexOf(d + "_" + venue);
                            psetIncomes[index] += totalIncome;
                            IncomeExpenseDashBoardModel idm = modelnew[index];
                            idm.totalIncome += totalIncome;
                            idm.cashAmount += cash;
                            idm.chequeAmount += cheque;
                            idm.onlineAmount += online;
                            idm.walletAmount += ewallet;
                        }
                        else
                        {
                            considerDepartments.Add(
                                dv.Where(x => x.id == rset.deptVenueId).FirstOrDefault()
                            );
                            modelnew.Add(
                                new IncomeExpenseDashBoardModel
                                {
                                    currency = "INR",
                                    gTotalIncome = gTotal,
                                    data = data,
                                    cashAmount = cash,
                                    chequeAmount = cheque,
                                    totalIncome = totalIncome,
                                    onlineAmount = online,
                                    walletAmount = ewallet,
                                    psetName = d + "_" + venue
                                }
                            );
                            psetNames.Add(d + "_" + venue);
                            psetIncomes.Add(totalIncome);
                            deptExpenses.Add(0);
                        }
                    }

                    foreach (var i in branchIds)
                    {
                        foreach (var j in currencys)
                        {
                            Income.Elearning =
                                eqservice.GetElearning_Income_InDateRange_2_BranchWise(
                                    DateTime.Today.AddDays(-30).Date.ToString("dd-MM-yyyy"),
                                    DateTime.Today.Date.ToString("dd-MM-yyyy"),
                                    i.id,
                                    i.psetName + " - " + j
                                );

                            List<ChartLabel> data = new List<ChartLabel>();
                            List<int> amounts = new List<int>();

                            string venue = i.psetName + " - " + j;

                            int totalIncome = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j)
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );

                            int cash = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j && x.Mode == "Cash")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                            int cheque = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j && x.Mode == "Cheque")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                            int online = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x => x.Currency == j && x.Mode == "Online")
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );
                            int ewallet = Convert.ToInt32(
                                Income
                                    .Elearning.Where(x =>
                                        x.Currency == j && x.Mode == "e-Wallet used"
                                    )
                                    .Sum(x => Convert.ToInt32(x.amount))
                            );

                            amounts.Add(cash);
                            amounts.Add(cheque);
                            amounts.Add(online);
                            amounts.Add(ewallet);

                            data.Add(new ChartLabel { data = amounts, label = "Online" });
                            float incomePercentage = (totalIncome * 100) / gTotal;

                            psetNames.Add(venue);
                            psetIncomes.Add(totalIncome);
                            deptExpenses.Add(0);
                            considerDepartments.Add(
                                dv.Where(x => x.id == i.psetId).FirstOrDefault()
                            );

                            modelnew.Add(
                                new IncomeExpenseDashBoardModel
                                {
                                    currency = j,
                                    gTotalIncome = gTotal,
                                    incomePercentage = incomePercentage,
                                    data = data,
                                    cashAmount = cash,
                                    chequeAmount = cheque,
                                    totalIncome = totalIncome,
                                    onlineAmount = online,
                                    walletAmount = ewallet,
                                    psetName = venue
                                }
                            );
                        }
                    }
                }

                foreach (dept_venue d in considerDepartments)
                {
                    if (d == null)
                    {
                        continue;
                    }
                    venue v = _context.venue.Where(x => x.Id == d.venueId).FirstOrDefault();

                    string venue = v?.displayName;

                    int index = psetNames.IndexOf(d.deptName + "_" + venue);
                    int index2 = psetNames.IndexOf(d.deptName + " " + d.venueName + " - INR");
                    if (index == -1 && index2 == -1)
                    {
                        continue;
                    }
                    List<dept_venue_baseitem> baseitemsList = _context
                        .dept_venue_baseitem.Where(x => x.deptVenueId == d.id)
                        .ToList();

                    int ai = index;
                    if (ai == -1)
                    {
                        ai = index2;
                    }
                    int financialYear2 = _helperService.GetFinancialYear(dt);
                    foreach (dept_venue_baseitem i in baseitemsList)
                    {
                        switch (i.baseItemId)
                        {
                            case 1:
                                deptExpenses[ai] += await _salaryService.getSalaryExpenseAmount(
                                    d.id,
                                    dateRange,
                                    "Khidmatguzaar"
                                );
                                break;
                            case 51:
                                deptExpenses[ai] += await _salaryService.getSalaryExpenseAmount(
                                    d.id,
                                    dateRange,
                                    "Visiting Faculty"
                                );
                                break;
                            case 50:
                                deptExpenses[ai] += await _salaryService.getSalaryExpenseAmount(
                                    d.id,
                                    dateRange,
                                    "Staff"
                                );
                                break;
                            default:
                                deptExpenses[ai] += Convert.ToInt32(
                                    _budgetArazService
                                        .getBudgetConsumedAmount(
                                            d.id,
                                            0, //psetId to b    e updated here
                                            i.baseItemId ?? 0,
                                            financialYear2
                                        )
                                        .Sum(x => x.amount)
                                );
                                break;
                        }
                    }
                    modelnew[ai].totalExpense = deptExpenses[ai];
                }

                return Ok(
                    new
                    {
                        model = modelnew,
                        departmentIncomes = psetIncomes,
                        departmentNames = psetNames,
                        departmentExpenses = deptExpenses
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getAcrualIncomeDashboard")]
        [HttpPost]
        public async Task<ActionResult> getAcrualIncomeDashboard(SearchRecieptModel model)
        {
            string api = "getAcrualIncomeDashboard";
            //// Add_ApiLogs(api);


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<AcrualIncomeDashBoardModel> modelnew = new List<AcrualIncomeDashBoardModel>();

                List<mz_student_fee_allotment> allotments = _context
                    .mz_student_fee_allotment.Where(x =>
                        x.hijriYear == model.hijriYear && x.monthId == model.monthId
                    )
                    .ToList();
                List<mz_student_fee_allotment> allotments2 = new List<mz_student_fee_allotment>();
                List<mz_student_fee_allotment> allotments3 = new List<mz_student_fee_allotment>();
                List<mz_student_fee_allotment> allotments4 = new List<mz_student_fee_allotment>();
                List<int> psetIncomes = new List<int>();
                List<string> psetNames = new List<string>();

                foreach (var i in allotments)
                {
                    List<mz_student_fee_transaction> t = _context
                        .mz_student_fee_transaction.Where(x =>
                            x.allotmentId == i.id && x.paymentMode == "Waive"
                        )
                        .ToList();

                    int amt = (i.feeAlloted) - (t.Sum(x => x.credit ?? 0)) ?? 0;
                    if (amt > 0)
                    {
                        allotments2.Add(
                            new mz_student_fee_allotment
                            {
                                currency = i.currency,
                                fcId = i.fcId,
                                hijriYear = i.hijriYear,
                                monthId = i.monthId,
                                pSetId = i.pSetId,
                                feeAlloted = amt
                            }
                        );
                    }
                }

                allotments3 = allotments2
                    .GroupBy(x => new
                    {
                        x.fcId,
                        x.feeAlloted,
                        x.pSetId
                    })
                    .Select(x => x.FirstOrDefault())
                    .ToList();

                allotments4 = allotments3
                    .GroupBy(x => new { x.pSetId })
                    .Select(x => x.FirstOrDefault())
                    .ToList();

                foreach (var i in allotments4)
                {
                    string venue = "";
                    string program = "";
                    string subProgram = "";

                    registrationform_dropdown_set rset = _context
                        .registrationform_dropdown_set.Where(x => x.id == i.pSetId)
                        .FirstOrDefault();
                    if (rset != null)
                    {
                        registrationform_programs p = _context
                            .registrationform_programs.Where(x => x.id == rset.programId)
                            .FirstOrDefault();
                        registrationform_subprograms sp = _context
                            .registrationform_subprograms.Where(x => x.id == rset.subprogramId)
                            .FirstOrDefault();
                        venue v = _context.venue.Where(x => x.Id == rset.venueId).FirstOrDefault();

                        venue = v?.displayName;
                        program = p.name;
                        subProgram = sp.name;
                    }

                    int totalIncome = allotments2
                        .Where(x => x.pSetId == i.pSetId)
                        .Sum(x => x.feeAlloted ?? 0);
                    List<FeesAllotmentModel> data = new List<FeesAllotmentModel>();

                    foreach (var j in allotments3.Where(x => x.pSetId == i.pSetId).ToList())
                    {
                        int total = allotments2
                            .Where(x =>
                                x.pSetId == i.pSetId
                                && x.fcId == i.fcId
                                && x.feeAlloted == i.feeAlloted
                            )
                            .ToList()
                            .Count;

                        mz_student_feecategory fc = _context
                            .mz_student_feecategory.Where(x => x.id == j.fcId)
                            .FirstOrDefault();
                        data.Add(
                            new FeesAllotmentModel
                            {
                                fc_Name = fc.categoryName,
                                reason = total.ToString(),
                                feeAlloted = i.feeAlloted,
                                remarks = (total * i.feeAlloted).ToString()
                            }
                        );
                    }

                    psetIncomes.Add(totalIncome);
                    psetNames.Add(program + "_" + subProgram + "_" + venue);

                    modelnew.Add(
                        new AcrualIncomeDashBoardModel
                        {
                            data = data,
                            psetName = program + "_" + subProgram + "_" + venue,
                            totalIncome = totalIncome
                        }
                    );
                }

                return Ok(
                    new
                    {
                        model = modelnew,
                        psetIncomes = psetIncomes,
                        psetNames = psetNames
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("createOnlineManualReciept")]
        [HttpPost]
        public async Task<ActionResult> CreateOnlinePaymentManualReciept(
            List<FeePaymentModel> models
        )
        {
            string api = "createOnlineManualReciept";
            // Add_ApiLogs(api);
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            // IPaymentService pService = ServiceFactory.GetPaymentService();

            string? transactionId = models.FirstOrDefault()?.paymentId;
            FeeTransactionModel reciept = models.FirstOrDefault()?.reciept ?? new FeeTransactionModel();

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            RazorpayClient client = new RazorpayClient(
                "rzp_live_HC7TNKqDciQDfc",
                "axG9X38plXEYNInn4Yoe5Jcd"
            );
            Payment payment = new Payment();
            try
            {
                payment = client.Payment.Fetch(transactionId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Transaction ID is incorrect" });
            }

            if (payment.Attributes.description != reciept.itsId)
            {
                return BadRequest(new { message = "The Transaction ID does not belong to the entered ITS." });
            }
            if (payment.Attributes.amount != (models.Sum(x => x.amount) * 100))
            {
                return BadRequest(new { message = "Amount is incorrect for the entered transaction ID!" });
            }
            if (payment.Attributes.status == "failed")
            {
                return BadRequest(new { message = "Payment is failed  for the entered transaction ID!" });
            }
            int ii = payment.Attributes.created_at;
            DateTime fd = new DateTime(1970, 1, 1);

            DateTime dateTime1 = fd.AddSeconds((double)ii);

            DateTime pay_Date = TimeZoneInfo.ConvertTimeFromUtc(dateTime1, INDIAN_ZONE);

            if (
                reciept.chequeDate?.Day != pay_Date.Day
                || reciept.chequeDate?.Month != pay_Date.Month
                || reciept.chequeDate?.Year != pay_Date.Year
            )
            {
                return BadRequest(new { message = "Payment date is incorrect for the entered transaction ID!" });
            }

            mz_student? s = new mz_student();

            s = _context.mz_student.Where(x => x.itsID == reciept.itsId).FirstOrDefault();
            if (s == null)
            {
                if (reciept.itsId != 500)
                {
                    s = _context.mz_student.Where(x => x.mz_id == reciept.itsId).FirstOrDefault();
                }
                else
                {
                    return BadRequest(new { message = "Student not found" });
                }
            }
            int recieptNumber = 1;
            mz_student_receipt? r1 = _context
                .mz_student_receipt.Where(x =>
                    x.transactionId == transactionId && x.status == "Active"
                )
                .FirstOrDefault();

            if (r1 != null)
            {
                return BadRequest(new { message = "Transaction Id already exist" });
            }
            var rrList = _context
                .mz_student_receipt.Where(x =>
                    x.paymentMode == reciept.paymentType
                    && x.collectionCenter == reciept.collectioncenterId
                )
                .ToList();

            foreach (var i in models)
            {
                int amount = _helperService.getMaxWaiveAmount(i.allotmentId ?? 0);

                if (i.amount > amount)
                {
                    return BadRequest(new { message = "Selected references has no due amount, please refresh." });
                }
            }

            if (rrList.Count != 0)
            {
                recieptNumber =
                    (
                        rrList
                            .OrderByDescending(x => x.recieptNumber)
                            .FirstOrDefault()
                            ?.recieptNumber ?? 0
                    ) + 1;
            }

            mz_student_receipt r = new mz_student_receipt
            {
                note = reciept.note,
                amount = models.Sum(x => x.amount),
                createdBy = "SELF (" + authUser.Name + ")",
                createdOn = indianTime,
                paymentMode = reciept.paymentType ?? "",
                currency = "INR",
                recieptDate = reciept.chequeDate,
                status = "Active",
                studentId = s.mz_id,
                transactionId = transactionId ?? "",
                collectionCenter = reciept.collectioncenterId,
                recieptNumber = recieptNumber,
                account = "DAWAT-E-HADIYAH",
                bankName = "RazorPay PG"
            };
            _context.mz_student_receipt.Add(r);

            _context.SaveChanges(); //mz_student_receipt Id required

            foreach (var i in models)
            {
                _context.mz_student_fee_transaction.Add(
                    new mz_student_fee_transaction
                    {
                        allotmentId = i.allotmentId,
                        createdBy = "SELF (" + authUser.Name + ")",
                        createdOn = indianTime,
                        currency = "INR",
                        credit = i.amount,
                        paymentMode = reciept.paymentType ?? "N/A",
                        studentId = s.mz_id,
                        transactionId = transactionId ?? "N/A",
                        collection_center_no = reciept.collectioncenterId,
                        recieptId = r.id
                    }
                );
            }

            string strAmount = r.amount?.ToString() ?? "0";
            string AmountInWords = _helperService.ChangeToWords(strAmount);
            DateOnly DO = DateOnly.FromDateTime(DateTime.Now);
            DateTime rDate = r.recieptDate?.ToDateTime(new TimeOnly(0, 0)) ?? DO.ToDateTime(new TimeOnly(0, 0));
            if (r.recieptDate?.Day >= 22)
            {
                int month2 = rDate.AddMonths(1).Month;
                int year2 = rDate.AddMonths(1).Year;
                rDate = new DateTime(year2, month2, 1);
            }
            else
            {
                int month2 = rDate.AddMonths(-1).Month;
                int year2 = rDate.AddMonths(-1).Year;

                DateTime todate = new DateTime(
                    r.recieptDate.Value.Year,
                    r.recieptDate.Value.Month,
                    7
                );
                DateTime fromdate = new DateTime(year2, month2, 22);

                if (rDate >= fromdate && rDate <= todate)
                {
                    rDate = new DateTime(r.recieptDate.Value.Year, r.recieptDate.Value.Month, 1);
                }
                else
                {
                    rDate = new DateTime(r.recieptDate.Value.Year, r.recieptDate.Value.Month, 15);
                }
            }
            _context.SaveChanges();

            var f = new FeesPaidModel
            {
                ItsId = s.itsID ?? 0,
                printDate = rDate,
                ChequeDate = r.chequeDate,
                ChequeNo = r.transactionId,
                BankName = r.bankName,
                CreatedBy = r.createdBy,
                FeePaidAmount = r.amount ?? 0,
                name = s.nameEng,
                PaymentMode = r.paymentMode,
                ReceiptId = r.recieptNumber ?? 0,
                amountInWord = AmountInWords,
            };

            return Ok(f);
        }

        [Route("ReverseReceipt")]
        [HttpPost]
        public async Task<ActionResult> ReverseReceipt_new(ReverseReceipt_Model model)
        {
            string api = "ReverseReceipt";
            // Add_ApiLogs(api);
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            mz_student_receipt r = _context
                .mz_student_receipt.Where(x => x.id == model.rId)
                .FirstOrDefault();

            if (r != null)
            {
                if (r.status == "InActive")
                {
                    return BadRequest(new { message = "receipt is already In-Active" });
                }

                List<mz_student_fee_transaction> transactions1 = _context
                    .mz_student_fee_transaction.Where(x => x.recieptId == r.id)
                    .ToList();

                foreach (var i in transactions1)
                {
                    int amount = _helperService.getMaxReverseAmount(i.allotmentId ?? 0);

                    if (amount < i.credit)
                    {
                        return BadRequest(
                            "Some or full amount has already been reversed for this receipt."
                        );
                    }
                }
                int amnt = 0;
                foreach (var i in transactions1)
                {
                    int amount = _helperService.getMaxReverseAmount(i.allotmentId ?? 0);

                    if (amount >= i.credit)
                    {
                        mz_student s = _context
                            .mz_student.Where(x => x.mz_id == i.studentId)
                            .FirstOrDefault();
                        _context.mz_student_fee_transaction.Add(
                            new mz_student_fee_transaction
                            {
                                recieptId = r.id,
                                allotmentId = i.allotmentId,
                                createdBy = authUser.Name,
                                createdOn = indianTime,
                                currency = "INR",
                                debit = Convert.ToInt32(i.credit),
                                paymentMode = "Reverse",
                                studentId = s.mz_id,
                                remarks = model.reason
                            }
                        );

                        amnt = amnt + i.credit ?? 0;
                    }
                }

                r.status = "InActive";
                r.note =
                    r.note
                    + " :: "
                    + model.reason
                    + " :: "
                    + model.note
                    + " :: Reverse Amount : "
                    + amnt
                    + " :: DateTime : "
                    + indianTime;

                if (model.isgaramat == "Yes")
                {
                    var stud = _context
                        .mz_student.Where(x => x.mz_id == r.studentId)
                        .FirstOrDefault();

                    if (stud != null)
                    {
                        var a = new mz_student_fee_allotment
                        {
                            currency = "INR",
                            createdBy = authUser.Name,
                            createdOn = DateTime.Now,
                            feeAlloted = model.amount1,
                            pSetId = stud.psetId,
                            studentId = stud.mz_id,
                            remarks =
                                "Miscellaneous - "
                                + "Cheque Bounce Garamat :: Receipt_Id : "
                                + r.id,
                            waiveStatus = false
                        };
                        _context.mz_student_fee_allotment.Add(a);

                        _context.SaveChanges();

                        var b = new mz_student_fee_transaction
                        {
                            currency = "INR",
                            debit = model.amount1,
                            createdOn = DateTime.Now,
                            allotmentId = a.id,
                            createdBy = authUser.Name,
                            studentId = stud.mz_id
                        };
                        _context.mz_student_fee_transaction.Add(b);

                        a.txn_Id = b.id;
                        _context.SaveChanges();
                    }
                }
            }

            _context.SaveChanges();

            return Ok();
        }
    }

    public class StudentLogsModel
    {
        public string type { get; set; }

        public string description { get; set; }

        public DateTime createdOn { get; set; }

        public string createdBy { get; set; }
    }

    public class ReverseReceipt_Model
    {
        public int rId { get; set; }
        public string reason { get; set; }

        public string note { get; set; }
        public string isgaramat { get; set; }

        public int amount1 { get; set; }
    }
}
