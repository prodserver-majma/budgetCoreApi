using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mahadalzahrawebapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        public WalletController(mzdbContext context, IMapper mapper, TokenService tokenService)
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

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        [Route("getStudentWalletLedger/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getStudentWalletLedger(int itsId)
        {
            string api = "getStudentWalletLedger/{itsId}";
            //// Add_ApiLogs(api);

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


            List<mz_student_ewallet> transactions = _context.mz_student_ewallet.Where(x => x.studentId == s.mz_id).ToList();

            foreach (var i in transactions)
            {

                string status = "Active";
                string balance = "";
                if (!i.status ?? false)
                {
                    status = "InActive";
                }

                List<mz_student_ewallet> tList = _context.mz_student_ewallet.Where(x => x.studentId == s.mz_id && x.status == true && x.id <= i.id).ToList();

                balance = (tList.Sum(x => x.credit) - tList.Sum(x => x.debit)).ToString();


                models.Add(new FeeTransactionModel { balance = balance, id = i.id, debitNo = i.debit ?? 0, creditNo = i.credit ?? 0, credit = i.credit.ToString(), debit = i.debit.ToString(), paymentType = i.paymentType, dateTime = i.createdOn ?? DateTime.Today, note = i.note, createdBy = i.createdBy, status = status });

            }

            int? debit = models.Sum(x => x.debitNo);
            int? credit = models.Sum(x => x.creditNo);





            string balance1 = (models.Sum(x => x.creditNo) - models.Sum(x => x.debitNo)).ToString();


            string fcName = "";
            int fcId = 0;

            if (s.fcId != null)
            {
                mz_student_feecategory fc_Name = _context.mz_student_feecategory.Where(x => x.id == s.fcId).FirstOrDefault();
                fcName = fc_Name.categoryName;
                fcId = fc_Name.id;
            }
            List<mz_student_feecategory> categories = _context.mz_student_feecategory.ToList();



            return Ok(new { fcId = fcId, categories = categories, fcName = fcName, models = models, debit = debit, credit = credit, balance = balance1, name = s.nameEng, program = p.name + " _ " + sp.name + " _ " + v?.displayName });




        }



        [Route("transferwallettowallet")]
        [HttpPost]
        public async Task<ActionResult> TransferWalletToWallet(FeeTransactionModel model)
        {
            string api = "transferwallettowallet";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            mz_student s_from = _context.mz_student.Where(x => x.itsID == model.itsId_from).FirstOrDefault();
            mz_student s_to = _context.mz_student.Where(x => x.itsID == model.itsId_to).FirstOrDefault();

            if (s_to == null)
            {
                return BadRequest(new { message = "student not found" });
            }

            List<mz_student_ewallet> ewallets = _context.mz_student_ewallet.Where(x => x.studentId == s_from.mz_id && x.status == true).ToList();

            int? balance = (ewallets.Sum(x => x.credit)) - (ewallets.Sum(x => x.debit));

            if ((balance ?? 0) >= model.debitNo)
            {

                _context.mz_student_ewallet.Add(new mz_student_ewallet { createdBy = authUser.Name, studentId = s_from.mz_id, createdOn = indianTime, paymentType = "WTW", debit = model.debitNo, currency = "INR", status = true, note = model.note + " ::  Transfer amount to Student_Id " + s_to.mz_id });

                _context.mz_student_ewallet.Add(new mz_student_ewallet { createdBy = authUser.Name, studentId = s_to.mz_id, createdOn = indianTime, paymentType = "WTW", credit = model.debitNo, currency = "INR", status = true, note = model.note + " ::  Transfer amount from Student_Id " + s_from.mz_id });
                _context.SaveChanges();

            }

            return Ok();

        }

    }
}
