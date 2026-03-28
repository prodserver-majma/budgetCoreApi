using Abp.Extensions;
using AutoMapper;
using JetBrains.Annotations;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Mappings.Training;
using mahadalzahrawebapi.Migrations;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Tls;
using System.Diagnostics;
using System.Linq;

namespace mahadalzahrawebapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentFeeAllotmentController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;
        private readonly ElearningService _elearningService;

        public StudentFeeAllotmentController(
            mzdbContext context,
            IMapper mapper,
            TokenService tokenService
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
            _elearningService = new ElearningService();
        }

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById(
            "Asia/Kolkata"
        );
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        //CacheService cache = new CacheService();

        [Route("getallotedfees/{hmonth}/{hyear}")]
        [HttpGet]
        public async Task<ActionResult> getAllFeeCategory(int hmonth, string hyear)
        {
            string api = "getallotedfees/{hmonth}/{hyear}";
            //// Add_ApiLogs(api);


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<FeesAllotmentModel> model = new List<FeesAllotmentModel>();

                List<mz_student_fee_allotment> fa = _context
                    .mz_student_fee_allotment.Where(x =>
                        x.hijriYear == hyear && x.monthId == hmonth
                    )
                    .ToList();

                foreach (var i in fa)
                {
                    mz_student_feecategory fc = _context
                        .mz_student_feecategory.Where(x => x.id == i.fcId)
                        .FirstOrDefault();
                    hijri_months hm = _context
                        .hijri_months.Where(x => x.id == i.monthId)
                        .FirstOrDefault();
                    registrationform_dropdown_set pset = _context
                        .registrationform_dropdown_set.Where(x => x.id == i.pSetId)
                        .FirstOrDefault();
                    registrationform_programs p = _context
                        .registrationform_programs.Where(x => x.id == pset.programId)
                        .FirstOrDefault();
                    registrationform_subprograms sp = _context
                        .registrationform_subprograms.Where(x => x.id == pset.subprogramId)
                        .FirstOrDefault();
                    venue v = _context.venue.Where(x => x.Id == pset.venueId).FirstOrDefault();
                    model.Add(
                        new FeesAllotmentModel
                        {
                            feeAlloted = i.feeAlloted,
                            fcId = i.fcId,
                            currency = i.currency,
                            createdOn = i.createdOn,
                            createdBy = i.createdBy,
                            hijriYear = i.hijriYear,
                            monthId = i.monthId,
                            pSetId = i.pSetId,
                            studentId = i.studentId,
                            txn_Id = i.txn_Id,
                            id = i.id,
                            reason = i.reason,
                            remarks = i.remarks,
                            fc_Name = fc.categoryName,
                            monthName = hm.hijriMonthName,
                            psetName = p.name + "_" + sp.name + "_" + v?.displayName,
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

        [Route("getFeesCategoryClassWise/{psetId}")]
        [HttpGet]
        public async Task<IActionResult> getFeesCategoryClassWise(int psetId)
        {
            var feeCategories = new List<FeeCategories>();
            var feeCategory_Pset = _context.mz_student_feecategory_pset.Where(x => x.psetId == psetId).ToList();
            var feeCategory = _context.mz_student_feecategory.ToList();

            foreach (var fee in feeCategory_Pset)
            {
                var FC = feeCategory.Where(x => x.id == fee.fcId).FirstOrDefault();
                if(FC != null)
                {
                    feeCategories.Add(new FeeCategories
                    {
                        Id = fee.id,
                        Name = FC.categoryName
                    });
                }
                
            }
            return Ok(feeCategories);
        }

        [Route("getAllPsetForFeeAllotment")]
        [HttpGet]
        public async Task<ActionResult> getAllPsetForFeeAllotment()
        {
            string api = "getAllPsetForFeeAllotment";
            //// Add_ApiLogs(api);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            string currentpset = "";
            try
            {
                List<FeeCategories> model = new List<FeeCategories>();

                // Fetch all data from the database first
                List<registrationform_dropdown_set> psets = _context.registrationform_dropdown_set.ToList();
                List<registrationform_programs> programs = _context.registrationform_programs.ToList();
                List<registrationform_subprograms> subprograms = _context.registrationform_subprograms.ToList();

                student_registration_rights srrs = _context.student_registration_rights.FirstOrDefault(x => x.itsId == authUser.ItsId);

                registrationform_dropdown_set rds = _context.registrationform_dropdown_set.FirstOrDefault(x => x.id == srrs.programSetId);



                List<registrationform_dropdown_set> rdsss = _context.registrationform_dropdown_set.Where(x => x.venueId == rds.venueId).ToList();

                var venueIds = (from srr in _context.registrationform_dropdown_set
                                join rdss in _context.student_registration_rights on srr.id equals rdss.programSetId
                                where rdss.itsId == authUser.ItsId
                                select srr.venueId).Distinct().ToList();

               Debug.WriteLine("This is venueIds query");



                List<registrationform_dropdown_set> rfds = _context.registrationform_dropdown_set.Where(x => venueIds.Contains(x.venueId.Value)).ToList();
                List<int> psetsAssigned = rfds.Select(x => x.id).ToList();

                List<venue> venues = _context.venue.ToList();

                // Perform grouping and selection client-side
                List<mz_student_feecategory_pset> fee = _context.mz_student_feecategory_pset
                    .Where(x => psetsAssigned.Contains(x.psetId.Value))
                    .AsEnumerable() // Switch to client-side evaluation
                    .GroupBy(x => x.psetId)
                    .Select(x => x.First())
                    .ToList();

                foreach (mz_student_feecategory_pset f in fee)
                {
                    currentpset = f.psetId?.ToString() ?? "0";
                    registrationform_dropdown_set? pset = psets
                        .Where(x => x.id == f.psetId)
                        .FirstOrDefault();

                    if (pset == null)
                    {
                        continue;
                    }

                    registrationform_programs? p = programs
                        .Where(x => x.id == pset.programId)
                        .FirstOrDefault();
                    registrationform_subprograms? sp = subprograms
                        .Where(x => x.id == pset.subprogramId)
                        .FirstOrDefault();

                    venue? v = venues.Where(x => x.Id == pset.venueId).FirstOrDefault();
                    
                    model.Add(new FeeCategories
                    {
                        Id = f.psetId ?? 0,
                        Name = sp?.name + "_" + v?.displayName,
                        classId = sp?.id,
                        schoolId = v?.Id
                    });
                }
                model = model.OrderBy(x => x.schoolId).ThenBy(x => x.classId).ToList();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString() + " failed at " + currentpset);
            }
        }


        [Route("getstudentsList/{itsIdCSV}")]
        [HttpGet]
        public async Task<ActionResult> getStudentList(string itsIdCSV)
        {
            string api = "getstudentsList/{itsIdCSV}";
            //// Add_ApiLogs(api);


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<CampStudentModel> model = new List<CampStudentModel>();

                //List<student_registration_rights> srrss = _context.student_registration_rights.Where(x => x.itsId == authUser.ItsId).ToList();

                

                var venueIds = (from srr in _context.registrationform_dropdown_set
                                join rdss in _context.student_registration_rights on srr.id equals rdss.programSetId
                                where rdss.itsId == authUser.ItsId
                                select srr.venueId).Distinct().ToList();

                List<registrationform_dropdown_set> rfds = _context.registrationform_dropdown_set.Where(x => venueIds.Contains(x.venueId.Value)).ToList();
                List<int> psetsAssigned = rfds.Select(x => x.id).ToList();

                var students = _context.mz_student.Where(x => psetsAssigned.Contains(x.psetId.Value)).ToList();

                List<int> itsIds = _helperService.parseItsId(itsIdCSV);

                foreach (var itsId in itsIds)
                {
                    mz_student? s = students.Where(x => x.itsID == itsId)
                        .FirstOrDefault();
                    if (s == null)
                    {
                        return BadRequest(new {message = "Student not found for ITS ID: " + itsId });
                    }
                    registrationform_dropdown_set? pset = _context
                        .registrationform_dropdown_set.Where(x => x.id == s.psetId)
                        .FirstOrDefault();
                    registrationform_programs? p = _context
                        .registrationform_programs.Where(x => x.id == pset.programId)
                        .FirstOrDefault();
                    registrationform_subprograms? sp = _context
                        .registrationform_subprograms.Where(x => x.id == pset.subprogramId)
                        .FirstOrDefault();
                    venue? v = _context.venue.Where(x => x.Id == pset.venueId).FirstOrDefault();
                    string fc_name = "_Blank";
                    string amount = "";
                    string currency = "";

                    //if (s.fcId != null)
                    //{
                    //    mz_student_feecategory? fc = _context
                    //        .mz_student_feecategory.Where(x => x.id == s.fcId)
                    //        .FirstOrDefault();
                    //    if (fc == null)
                    //    {
                    //        return BadRequest("Invalid Fee Category Id for ITS ID: " + s.itsID);
                    //    }
                    //    fc_name = fc?.categoryName;
                    //    if (s.psetId != null)
                    //    {
                    //        mz_student_feecategory_pset? fc_pset = _context
                    //            .mz_student_feecategory_pset.Where(x =>
                    //                x.psetId == s.psetId && x.fcId == s.fcId
                    //            )
                    //            .FirstOrDefault();
                    //        if (fc_pset == null)
                    //        {
                    //            return BadRequest("Fee Category amount not found for ITS ID: " + s.itsID + "with FC ID: " + s.fcId + " and PSET ID: " + s.psetId);
                    //        }
                    //        amount = fc_pset?.amount?.ToString();
                    //        currency = fc_pset?.currency;
                    //    }
                    //}

                    model.Add(
                        new CampStudentModel
                        {
                            programSetId = s.psetId,
                            subProgramName = sp.name,
                            fc_name = fc_name,
                            amount = amount,
                            currency = currency,
                            ItsId = s.itsID ?? 0,
                            pset_Name = p?.name + " _ " + sp?.name + " _ " + v?.displayName,
                            StudentName = s.nameEng,
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

        [Route("getstudentsListWaive/{itsIdCSV}")]
        [HttpGet]
        public async Task<ActionResult> getstudentsListWaive(string itsIdCSV)
        {
            string api = "getstudentsList/{itsIdCSV}";
            //// Add_ApiLogs(api);


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<CampStudentModel> model = new List<CampStudentModel>();

                List<int> itsIds = _helperService.parseItsId(itsIdCSV);

                //List<mz_student> students = _context.mz_student.ToList();

                List<mz_student_fee_allotment> allotmentss = _context.mz_student_fee_allotment.ToList();

                List<mz_student_feecategory_pset> fc_psets = _context.mz_student_feecategory_pset.ToList();

                List<mz_student_feecategory> fcs = _context.mz_student_feecategory.ToList();

                List<greg_months> gregMonths = _context.greg_months.ToList();

                var venueIds = (from srr in _context.registrationform_dropdown_set
                                join rdss in _context.student_registration_rights on srr.id equals rdss.programSetId
                                where rdss.itsId == authUser.ItsId
                                select srr.venueId).Distinct().ToList();

                List<registrationform_dropdown_set> rfds = _context.registrationform_dropdown_set.Where(x => venueIds.Contains(x.venueId.Value)).ToList();
                List<int> psetsAssigned = rfds.Select(x => x.id).ToList();

                var students = _context.mz_student.Where(x => psetsAssigned.Contains(x.psetId.Value)).ToList();

                //List<feeCategory> Fc = new List<feeCategory>();

                foreach (var itsId in itsIds)
                {
                    List<feeCategory> Fc = new List<feeCategory>();   // <-- FIX HERE

                    mz_student? s = students.Where(x => x.itsID == itsId).FirstOrDefault();

                    if (s == null)
                    {
                        return BadRequest(new {message = "Student not found for ITS ID: " + itsId });
                    }

                    var allotments = allotmentss.Where(x => x.studentId == s.mz_id).ToList();

                    if (allotments.Count > 0)
                    {
                        foreach (var allot in allotments)
                        {
                            var fc_psetss = fc_psets.FirstOrDefault(x => x.fcId == allot.fcId && x.psetId == allot.pSetId);

                            if (fc_psetss == null)
                                continue;

                            var fc = fcs.FirstOrDefault(x => x.id == fc_psetss.fcId);

                            if (fc == null)
                                continue;

                            var greg_month = gregMonths.FirstOrDefault(x => x.id == allot.monthId);
                            // Create the monthly record
                            monthlyAmount monthwise = new monthlyAmount
                            {
                                month = greg_month.month_name,
                                amount = (double)(allot.feeAlloted ?? 0)
                            };

                            // Check if Fc already contains an entry for this id
                            var existing = Fc.FirstOrDefault(x => x.id == fc_psetss.id);

                            if (existing != null)
                            {
                                existing.month.Add(monthwise);
                            }
                            else
                            {
                                Fc.Add(new feeCategory
                                {
                                    id = fc_psetss.id,
                                    name = fc.categoryName,
                                    month = new List<monthlyAmount> { monthwise }
                                });
                            }
                        }

                        registrationform_dropdown_set? pset = _context
                            .registrationform_dropdown_set.Where(x => x.id == s.psetId)
                            .FirstOrDefault();
                        registrationform_programs? p = _context
                            .registrationform_programs.Where(x => x.id == pset.programId)
                            .FirstOrDefault();
                        registrationform_subprograms? sp = _context
                            .registrationform_subprograms.Where(x => x.id == pset.subprogramId)
                            .FirstOrDefault();
                        venue? v = _context.venue.Where(x => x.Id == pset.venueId).FirstOrDefault();

                        model.Add(
                            new CampStudentModel
                            {
                                programSetId = s.psetId,
                                subProgramName = sp.name,
                                feeCategoriesAlloted = Fc,  // now correct per student
                                amount = "",
                                currency = "",
                                ItsId = s.itsID ?? 0,
                                pset_Name = p?.name + " _ " + sp?.name + " _ " + v?.displayName,
                                StudentName = s.nameEng,
                            }
                        );
                    }
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getAllStudents")]
        [HttpPost]
        public async Task<ActionResult> getAllStudents(FeesAllotmentModel model1)
        {
            string api = "getAllStudents";
            //// Add_ApiLogs(api);


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<CampStudentModel> model = new List<CampStudentModel>();

                List<mz_student> students = new List<mz_student>();

                foreach (var i in model1.monthList)
                {
                    List<mz_student> students2 = _context
                        .mz_student.Where(x => x.psetId == i)
                        .ToList();

                    foreach (var j in students2)
                    {
                        students.Add(j);
                    }
                }

                List<dropdown_dataset_options> programDD = new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> itsIdDD = new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> nameDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> fcNameDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> pendingamountDD =
                    new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> walletamountDD =
                    new List<dropdown_dataset_options>();

                int c = 1;

                foreach (var stud in students)
                {
                    mz_student s = _context
                        .mz_student.Where(x => x.itsID == stud.itsID)
                        .FirstOrDefault();

                    registrationform_dropdown_set pset = _context
                        .registrationform_dropdown_set.Where(x => x.id == s.psetId)
                        .FirstOrDefault();
                    registrationform_programs p = _context
                        .registrationform_programs.Where(x => x.id == pset.programId)
                        .FirstOrDefault();
                    registrationform_subprograms sp = _context
                        .registrationform_subprograms.Where(x => x.id == pset.subprogramId)
                        .FirstOrDefault();
                    venue v = _context.venue.Where(x => x.Id == pset.venueId).FirstOrDefault();

                    mz_student_feecategory fc = _context
                        .mz_student_feecategory.Where(x => x.id == s.fcId)
                        .FirstOrDefault();
                    string a = "0";
                    string aa = "InActive";
                    string feeCategory = "_Blank";
                    if (stud.activeStatus ?? false)
                    {
                        a = "1";
                        aa = "Active";
                    }
                    if (fc != null)
                    {
                        feeCategory = fc.categoryName;
                    }

                    List<mz_student_fee_transaction> transactions_1 = _context
                        .mz_student_fee_transaction.Where(x => x.studentId == stud.mz_id)
                        .ToList();

                    List<mz_student_ewallet> ewallets = _context
                        .mz_student_ewallet.Where(x =>
                            x.studentId == stud.mz_id && x.status == true
                        )
                        .ToList();

                    int? p1 =
                        (transactions_1.ToList().Sum(x => x.debit))
                        - (transactions_1.ToList().Sum(x => x.credit));

                    int? w =
                        (ewallets.ToList().Sum(x => x.credit))
                        - (ewallets.ToList().Sum(x => x.debit));

                    model.Add(
                        new CampStudentModel
                        {
                            pendingAmount = p1 ?? 0,
                            walletAmount = w ?? 0,
                            activeStatusString2 = aa,
                            activeStatusString = a,
                            Id = c,
                            programName = p.name,
                            subProgram = sp.name,
                            venueName = v?.displayName,
                            EmailId = stud.studentEmail,
                            MobileNo = stud.studentMobile,
                            fatherEmail = stud.fatherEmail,
                            fatherMobile = stud.fatherMobile,
                            motherEmail = stud.motherEmail,
                            motherMobile = stud.motherMobile,
                            ItsId = s.itsID ?? 0,
                            pset_Name = p.name + " _ " + sp.name + " _ " + v?.displayName,
                            fc_name = feeCategory,
                            StudentName = s.nameEng,
                            age = stud.age ?? 0,
                            program = p.name,
                            subProgramName = sp.name,
                            venue = v?.displayName,
                        }
                    );
                    c = c + 1;
                }
                programDD = model
                    .OrderBy(x => x.pset_Name)
                    .GroupBy(x => x.pset_Name)
                    .Select(x => new dropdown_dataset_options
                    {
                        name = x.FirstOrDefault().pset_Name?.ToString()
                    })
                    .ToList();
                itsIdDD = model
                    .OrderBy(x => x.ItsId)
                    .GroupBy(x => x.ItsId)
                    .Select(x => new dropdown_dataset_options
                    {
                        name = x.FirstOrDefault().ItsId.ToString()
                    })
                    .ToList();
                nameDD = model
                    .OrderBy(x => x.StudentName)
                    .GroupBy(x => x.StudentName)
                    .Select(x => new dropdown_dataset_options
                    {
                        name = x.FirstOrDefault().StudentName?.ToString()
                    })
                    .ToList();
                fcNameDD = model
                    .OrderBy(x => x.fc_name)
                    .GroupBy(x => x.fc_name)
                    .Select(x => new dropdown_dataset_options
                    {
                        name = x.FirstOrDefault().fc_name?.ToString()
                    })
                    .ToList();
                pendingamountDD = model
                    .OrderBy(x => x.pendingAmount)
                    .GroupBy(x => x.pendingAmount)
                    .Select(x => new dropdown_dataset_options
                    {
                        name = x.FirstOrDefault().pendingAmount.ToString()
                    })
                    .ToList();
                walletamountDD = model
                    .OrderBy(x => x.walletAmount)
                    .GroupBy(x => x.walletAmount)
                    .Select(x => new dropdown_dataset_options
                    {
                        name = x.FirstOrDefault().walletAmount.ToString()
                    })
                    .ToList();

                return Ok(
                    new
                    {
                        model = model,
                        programDD = programDD,
                        itsIdDD = itsIdDD,
                        nameDD = nameDD,
                        fcNameDD = fcNameDD,
                        walletamountDD = walletamountDD,
                        pendingamountDD = pendingamountDD
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getAllActiveStudents")]
        [HttpPost]
        public async Task<ActionResult> getAllActiveStudents(FeesAllotmentModel model1)
        {
            string api = "getAllActiveStudents";

            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                var authUser = _tokenService.GetAuthUserFromToken(token);
                //AuthUser authUser = new AuthUser();
                List<CampStudentModel> model = new List<CampStudentModel>();


                List<mz_student> students = new List<mz_student>();
                List<mz_on_off_modules> module = _context.mz_on_off_modules.ToList();

                if (model1.monthList?.Count == 0)
                {
                    return BadRequest(new { message = "Please select atleast one program" });
                }

                List<mz_pset_elqgrpid_mapping> GroupIdsComplete = _context.mz_pset_elqgrpid_mapping.Where(x => model1.monthList.Any(y => y == (x.pSetId ?? 0))).ToList();
                List<mz_student> studentsAll = _context.mz_student.ToList();

                List<registrationform_dropdown_set> allPset = _context.registrationform_dropdown_set.ToList();
                List<registrationform_programs> allProgram = _context.registrationform_programs.ToList();
                List<registrationform_subprograms> allSubProgram = _context.registrationform_subprograms.ToList();
                List<venue> allVenue = _context.venue.ToList();

                List<mz_student_feecategory> feeCategories = _context.mz_student_feecategory.ToList();

                List<nisaab_classes> nisaabClasses = _context.nisaab_classes.ToList();
                // List<masool_classtype> allMasool = _context.masool_classtype.ToList();
                // List<khidmat_guzaar> kgs = _context.khidmat_guzaar.ToList();

                foreach (var i in model1.monthList)
                {
                    mz_on_off_modules? m = module.Where(x => x.id == 1).FirstOrDefault();

                    List<mz_pset_elqgrpid_mapping> GroupIds = GroupIdsComplete.Where(x => x.pSetId == i).ToList();

                    if (m?.status == true && i != 120 && i != 121)
                    {
                        bool anyAdded = false;
                        try
                        {
                            anyAdded = await UpdateFromElq(GroupIds, i, authUser);
                        }
                        catch (Exception ex)
                        {
                        }

                        if (anyAdded)
                        {
                            studentsAll = await _context.mz_student.ToListAsync();
                        }
                    }

                    List<mz_student> students2 = studentsAll.Where(x => x.psetId == i && x.activeStatus == true).ToList();

                    foreach (var j in students2)
                    {
                        students.Add(j);
                    }
                }

                List<int> studentIds = students.Select(x => x.mz_id).ToList();

                List<mz_student_fee_transaction> allTransaction = _context.mz_student_fee_transaction.Where(x => studentIds.Contains(x.studentId ?? 0)).ToList();
                List<mz_student_ewallet> allEwallets = _context.mz_student_ewallet.Where(x => studentIds.Contains(x.studentId ?? 0) && x.status == true).ToList();

                List<dropdown_dataset_options> programDD = new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> itsIdDD = new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> nameDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> fcNameDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> pendingamountDD = new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> walletamountDD = new List<dropdown_dataset_options>();

                int c = 1;

                foreach (var stud in students)
                {

                    mz_student? s = studentsAll.Where(x => x.itsID == stud.itsID).FirstOrDefault();
                    if (s?.psetId == null)
                    {
                        return BadRequest(new { message = "program not found for ITS ID :" + s?.itsID + " " + s?.nameEng });
                    }
                    registrationform_dropdown_set? pset = allPset.Where(x => x.id == s.psetId).FirstOrDefault();

                    if (pset == null)
                    {
                        return BadRequest(new { message = "program not found for ITS ID :" + s.itsID + " " + s.nameEng });
                    }

                    registrationform_programs p = allProgram.Where(x => x.id == pset.programId).FirstOrDefault() ?? new registrationform_programs();
                    registrationform_subprograms sp = allSubProgram.Where(x => x.id == pset.subprogramId).FirstOrDefault() ?? new registrationform_subprograms();
                    venue v = allVenue.Where(x => x.Id == pset.venueId).FirstOrDefault() ?? new venue();

                    if (pset == null || p == null || sp == null || v == null)
                    {
                        return BadRequest(new { message = "program not found for ITS ID :" + s.itsID + " " + s.nameEng });
                    }

                    mz_student_feecategory? fc = feeCategories.Where(x => x.id == s.fcId).FirstOrDefault();
                    string a = "1";
                    string aa = "Active";
                    string feeCategory = "_Blank";

                    if (fc != null)
                    {
                        feeCategory = fc.categoryName;
                    }

                    List<mz_student_fee_transaction> transactions_1 = allTransaction.Where(x => x.studentId == stud.mz_id).ToList();

                    List<mz_student_ewallet> ewallets = allEwallets.Where(x => x.studentId == stud.mz_id && x.status == true).ToList();

                    int? p1 = (transactions_1.ToList().Sum(x => x.debit)) - (transactions_1.ToList().Sum(x => x.credit));

                    int? w = ewallets.ToList().Sum(x => x.credit) - (ewallets.ToList().Sum(x => x.debit));
                    string className = "";
                    string masoolName = "Not Found";

                    if (s.classId != null)
                    {
                        nisaab_classes? nc = nisaabClasses.Where(x => x.id == s.classId).FirstOrDefault();
                        if (nc != null)
                        {
                            className = nc.className;
                        }
                    }

                    model.Add(new CampStudentModel
                    {
                        Jamiat = s.jamiat,
                        blood_grp = s.bloodGroup,
                        DOB_string = s.dobGregorian,
                        DOB_hijri = s.dobHijri,
                        nationality = s.nationality,
                        address = s.address,
                        dq_fasal = s.dq_fasal ?? 0,
                        hafizYear = s.hifzSanadYear ?? 0,
                        std = className,
                        masoolName = masoolName,
                        trno = s.trNo ?? 0,
                        watan = s.vatan,
                        maqaam = s.maqaam,
                        Jamaat = s.jamaat,
                        mzId = s.mz_id,
                        programSetId = s.psetId,
                        pendingAmount = p1 ?? 0,
                        walletAmount = w ?? 0,
                        activeStatusString2 = aa,
                        activeStatusString = a,
                        Id = c,
                        programName = p.name,
                        subProgram = sp.name,
                        venueName = v.displayName,
                        EmailId = stud.studentEmail,
                        MobileNo = stud.studentMobile,
                        fatherEmail = stud.fatherEmail,
                        fatherMobile = stud.fatherMobile,
                        motherEmail = stud.motherEmail,
                        motherMobile = stud.motherMobile,
                        ItsId = s.itsID ?? 0,
                        pset_Name = p.name + " _ " + sp.name + " _ " + v.displayName,
                        fc_name = feeCategory,
                        StudentName = s.nameEng,
                        age = stud.age ?? 0,
                        program = p.name,
                        subProgramName = sp.name,
                        venue = v.displayName,
                    });
                    c = c + 1;
                }

                programDD = model.OrderBy(x => x.pset_Name).GroupBy(x => x.pset_Name).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.pset_Name?.ToString() }).ToList();
                itsIdDD = model.OrderBy(x => x.ItsId).GroupBy(x => x.ItsId).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.ItsId?.ToString() }).ToList();
                nameDD = model.OrderBy(x => x.StudentName).GroupBy(x => x.StudentName).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.StudentName?.ToString() }).ToList();
                fcNameDD = model.OrderBy(x => x.fc_name).GroupBy(x => x.fc_name).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.fc_name?.ToString() }).ToList();
                pendingamountDD = model.OrderBy(x => x.pendingAmount).GroupBy(x => x.pendingAmount).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.pendingAmount?.ToString() }).ToList();
                walletamountDD = model.OrderBy(x => x.walletAmount).GroupBy(x => x.walletAmount).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.walletAmount?.ToString() }).ToList();

                return Ok(new ActiveStudentsReturnModel
                {
                    model = model,
                    programDD = programDD,
                    itsIdDD = itsIdDD,
                    nameDD = nameDD,
                    fcNameDD = fcNameDD,
                    walletamountDD = walletamountDD,
                    pendingamountDD = pendingamountDD
                });


            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }




        [Route("getAllInActiveStudents")]
        [HttpPost]
        public async Task<ActionResult> getAllInactiveStudents(FeesAllotmentModel model1)
        {
            string api = "api/studentfeeallotment/getAllInActiveStudents";

            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                var authUser = _tokenService.GetAuthUserFromToken(token);

                List<CampStudentModel> model = new List<CampStudentModel>();

                IQueryable<mz_student> studentsAll = _context.mz_student;

                List<registrationform_dropdown_set> allPset = _context.registrationform_dropdown_set.ToList();
                List<registrationform_programs> allProgram = _context.registrationform_programs.ToList();
                List<registrationform_subprograms> allSubProgram = _context.registrationform_subprograms.ToList();
                List<venue> allVenue = _context.venue.ToList();

                List<mz_student_feecategory> feeCategories = _context.mz_student_feecategory.ToList();
                List<mz_student_fee_transaction> allTransaction = _context.mz_student_fee_transaction.ToList();
                List<mz_student_ewallet> allEwallets = _context.mz_student_ewallet.ToList();

                List<mz_student> students = new List<mz_student>();

                if (model1.monthList.Count == 0)
                {
                    return BadRequest(new { message = "Please select atleast one program" });
                }

                foreach (var i in model1.monthList)
                {
                    List<mz_student> students2 = studentsAll.Where(x => x.psetId == i && x.activeStatus == false).ToList();

                    foreach (var j in students2)
                    {
                        students.Add(j);
                    }

                }

                List<dropdown_dataset_options> programDD = new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> itsIdDD = new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> nameDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> fcNameDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> pendingamountDD = new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> walletamountDD = new List<dropdown_dataset_options>();

                int c = model1.id;

                foreach (var stud in students)
                {

                    mz_student s = studentsAll.Where(x => x.itsID == stud.itsID).FirstOrDefault();

                    registrationform_dropdown_set pset = allPset.Where(x => x.id == s.psetId).FirstOrDefault();
                    registrationform_programs p = allProgram.Where(x => x.id == pset.programId).FirstOrDefault();
                    registrationform_subprograms sp = allSubProgram.Where(x => x.id == pset.subprogramId).FirstOrDefault();
                    venue v = allVenue.Where(x => x.Id == pset.venueId).FirstOrDefault();

                    mz_student_feecategory fc = feeCategories.Where(x => x.id == s.fcId).FirstOrDefault();
                    string a = "0";
                    string aa = "InActive";
                    string feeCategory = "_Blank";
                    if (fc != null)
                    {
                        feeCategory = fc.categoryName;
                    }

                    List<mz_student_fee_transaction> transactions_1 = allTransaction.Where(x => x.studentId == stud.mz_id).ToList();

                    List<mz_student_ewallet> ewallets = allEwallets.Where(x => x.studentId == stud.mz_id && x.status == true).ToList();

                    int? p1 = (transactions_1.ToList().Sum(x => x.debit)) - (transactions_1.ToList().Sum(x => x.credit));

                    int? w = (ewallets.ToList().Sum(x => x.credit)) - (ewallets.ToList().Sum(x => x.debit));


                    model.Add(new CampStudentModel
                    {
                        pendingAmount = p1 ?? 0,
                        walletAmount = w ?? 0,
                        activeStatusString2 = aa,
                        activeStatusString = a,
                        Id = c,
                        programName = p.name,
                        subProgram = sp.name,
                        venueName = v.displayName,
                        EmailId = stud.studentEmail,
                        MobileNo = stud.studentMobile,
                        fatherEmail = stud.fatherEmail,
                        fatherMobile = stud.fatherMobile,
                        motherEmail = stud.motherEmail,
                        motherMobile = stud.motherMobile,
                        ItsId = s.itsID ?? 0,
                        pset_Name = p.name + " _ " + sp.name + " _ " + v.displayName,
                        fc_name = feeCategory,
                        StudentName = s.nameEng,
                        age = stud.age ?? 0,
                        program = p.name,
                        subProgramName = sp.name,
                        venue = v.displayName,
                        mzId = stud.mz_id
                    });
                    c = c + 1;
                }
                programDD = model.OrderBy(x => x.pset_Name).GroupBy(x => x.pset_Name).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().pset_Name?.ToString() }).ToList();
                itsIdDD = model.OrderBy(x => x.ItsId).GroupBy(x => x.ItsId).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().ItsId.ToString() }).ToList();
                nameDD = model.OrderBy(x => x.StudentName).GroupBy(x => x.StudentName).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().StudentName?.ToString() }).ToList();
                fcNameDD = model.OrderBy(x => x.fc_name).GroupBy(x => x.fc_name).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().fc_name?.ToString() }).ToList();
                pendingamountDD = model.OrderBy(x => x.pendingAmount).GroupBy(x => x.pendingAmount).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().pendingAmount.ToString() }).ToList();
                walletamountDD = model.OrderBy(x => x.walletAmount).GroupBy(x => x.walletAmount).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().walletAmount.ToString() }).ToList();

                return Ok(new ActiveStudentsReturnModel { model = model, programDD = programDD, itsIdDD = itsIdDD, nameDD = nameDD, fcNameDD = fcNameDD, walletamountDD = walletamountDD, pendingamountDD = pendingamountDD });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // [Route("getAllActiveStudents")]
        // [HttpPost]
        // public async Task<ActionResult> getAllActiveStudents(FeesAllotmentModel model1)
        // {
        //     string api = "getAllActiveStudents";
        //     //// Add_ApiLogs(api);


        //     try
        //     {
        //         string token = _tokenService.ExtractTokenFromRequest(HttpContext);
        //         AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

        //         List<CampStudentModel> model = new List<CampStudentModel>();

        //         List<mz_student> students = new List<mz_student>();
        //         List<mz_on_off_modules> module = _context.mz_on_off_modules.ToList();
        //         List<mz_pset_elqgrpid_mapping> GroupIdsComplete = _context
        //             .mz_pset_elqgrpid_mapping.Where(x => model1.monthList.Any(y => y == x.pSetId))
        //             .ToList();
        //         List<mz_student> studentsAll = _context.mz_student.ToList();

        //         List<registrationform_dropdown_set> allPset =
        //             _context.registrationform_dropdown_set.ToList();
        //         List<registrationform_programs> allProgram =
        //             _context.registrationform_programs.ToList();
        //         List<registrationform_subprograms> allSubProgram =
        //             _context.registrationform_subprograms.ToList();
        //         List<venue> allVenue = _context.venue.ToList();

        //         List<mz_student_feecategory> feeCategories =
        //             _context.mz_student_feecategory.ToList();
        //         List<mz_student_fee_transaction> allTransaction =
        //             _context.mz_student_fee_transaction.ToList();
        //         List<mz_student_ewallet> allEwallets = _context.mz_student_ewallet.ToList();
        //         List<nisaab_classes> nisaabClasses = _context.nisaab_classes.ToList();
        //         //List<masool_classtype> allMasool = _context.masool_classtype.ToList();
        //         List<khidmat_guzaar> kgs = _context.khidmat_guzaar.ToList();

        //         foreach (var i in model1.monthList)
        //         {
        //             mz_on_off_modules m = module.Where(x => x.id == 1).FirstOrDefault();

        //             List<mz_pset_elqgrpid_mapping> GroupIds = GroupIdsComplete
        //                 .Where(x => x.pSetId == i)
        //                 .ToList();

        //             if (m.status == true && i != 120 && i != 121)
        //             {
        //                 UpdateFromElq(GroupIds, i, authUser);
        //             }

        //             List<mz_student> students2 = studentsAll
        //                 .Where(x => x.psetId == i && x.activeStatus == true)
        //                 .ToList();

        //             foreach (var j in students2)
        //             {
        //                 students.Add(j);
        //             }
        //         }

        //         List<dropdown_dataset_options> programDD = new List<dropdown_dataset_options>();

        //         List<dropdown_dataset_options> itsIdDD = new List<dropdown_dataset_options>();

        //         List<dropdown_dataset_options> nameDD = new List<dropdown_dataset_options>();
        //         List<dropdown_dataset_options> fcNameDD = new List<dropdown_dataset_options>();
        //         List<dropdown_dataset_options> pendingamountDD =
        //             new List<dropdown_dataset_options>();

        //         List<dropdown_dataset_options> walletamountDD =
        //             new List<dropdown_dataset_options>();

        //         int c = 1;

        //         foreach (var stud in students)
        //         {
        //             mz_student s = studentsAll.Where(x => x.itsID == stud.itsID).FirstOrDefault();
        //             if (s.psetId == null)
        //             {
        //                 return BadRequest(
        //                     "program not found for ITS ID :" + s.itsID + " " + s.nameEng
        //                 );
        //             }
        //             registrationform_dropdown_set pset = allPset
        //                 .Where(x => x.id == s.psetId)
        //                 .FirstOrDefault();
        //             registrationform_programs p = allProgram
        //                 .Where(x => x.id == pset.programId)
        //                 .FirstOrDefault();
        //             registrationform_subprograms sp = allSubProgram
        //                 .Where(x => x.id == pset.subprogramId)
        //                 .FirstOrDefault();
        //             venue v = allVenue.Where(x => x.Id == pset.venueId).FirstOrDefault();

        //             if (pset == null || p == null || sp == null || v == null)
        //             {
        //                 return BadRequest(
        //                     "program not found for ITS ID :" + s.itsID + " " + s.nameEng
        //                 );
        //             }

        //             mz_student_feecategory fc = feeCategories
        //                 .Where(x => x.id == s.fcId)
        //                 .FirstOrDefault();
        //             string a = "1";
        //             string aa = "Active";
        //             string feeCategory = "_Blank";

        //             if (fc != null)
        //             {
        //                 feeCategory = fc.categoryName;
        //             }

        //             List<mz_student_fee_transaction> transactions_1 = allTransaction
        //                 .Where(x => x.studentId == stud.mz_id)
        //                 .ToList();

        //             List<mz_student_ewallet> ewallets = allEwallets
        //                 .Where(x => x.studentId == stud.mz_id && x.status == true)
        //                 .ToList();

        //             int? p1 =
        //                 (transactions_1.ToList().Sum(x => x.debit))
        //                 - (transactions_1.ToList().Sum(x => x.credit));

        //             int? w =
        //                 (ewallets.ToList().Sum(x => x.credit))
        //                 - (ewallets.ToList().Sum(x => x.debit));
        //             string className = "";
        //             string masoolName = "Not Found";

        //             if (s.classId != null)
        //             {
        //                 nisaab_classes nc = nisaabClasses
        //                     .Where(x => x.id == s.classId)
        //                     .FirstOrDefault();
        //                 if (nc != null)
        //                 {
        //                     className = nc.className;
        //                 }
        //             }

        //             model.Add(
        //                 new CampStudentModel
        //                 {
        //                     Jamiat = s.jamiat,
        //                     blood_grp = s.bloodGroup,
        //                     DOB_string = s.dobGregorian,
        //                     DOB_hijri = s.dobHijri,
        //                     nationality = s.nationality,
        //                     address = s.address,
        //                     dq_fasal = s.dq_fasal ?? 0,
        //                     hafizYear = s.hifzSanadYear ?? 0,
        //                     std = className,
        //                     masoolName = masoolName,
        //                     trno = s.trNo ?? 0,
        //                     watan = s.vatan,
        //                     maqaam = s.maqaam,
        //                     Jamaat = s.jamaat,
        //                     mzId = s.mz_id,
        //                     programSetId = s.psetId,
        //                     pendingAmount = p1 ?? 0,
        //                     walletAmount = w ?? 0,
        //                     activeStatusString2 = aa,
        //                     activeStatusString = a,
        //                     Id = c,
        //                     programName = p.name,
        //                     subProgram = sp.name,
        //                     venueName = v?.displayName,
        //                     EmailId = stud.studentEmail,
        //                     MobileNo = stud.studentMobile,
        //                     fatherEmail = stud.fatherEmail,
        //                     fatherMobile = stud.fatherMobile,
        //                     motherEmail = stud.motherEmail,
        //                     motherMobile = stud.motherMobile,
        //                     ItsId = s.itsID ?? 0,
        //                     pset_Name = p.name + " _ " + sp.name + " _ " + v?.displayName,
        //                     fc_name = feeCategory,
        //                     StudentName = s.nameEng,
        //                     age = stud.age ?? 0,
        //                     program = p.name,
        //                     subProgramName = sp.name,
        //                     venue = v?.displayName,
        //                 }
        //             );
        //             c = c + 1;
        //         }

        //         programDD = model
        //             .OrderBy(x => x.pset_Name)
        //             .GroupBy(x => x.pset_Name)
        //             .Select(x => new dropdown_dataset_options
        //             {
        //                 name = x.FirstOrDefault()?.pset_Name?.ToString()
        //             })
        //             .ToList();
        //         itsIdDD = model
        //             .OrderBy(x => x.ItsId)
        //             .GroupBy(x => x.ItsId)
        //             .Select(x => new dropdown_dataset_options
        //             {
        //                 name = x.FirstOrDefault()?.ItsId.ToString()
        //             })
        //             .ToList();
        //         nameDD = model
        //             .OrderBy(x => x.StudentName)
        //             .GroupBy(x => x.StudentName)
        //             .Select(x => new dropdown_dataset_options
        //             {
        //                 name = x.FirstOrDefault()?.StudentName?.ToString()
        //             })
        //             .ToList();
        //         fcNameDD = model
        //             .OrderBy(x => x.fc_name)
        //             .GroupBy(x => x.fc_name)
        //             .Select(x => new dropdown_dataset_options
        //             {
        //                 name = x.FirstOrDefault().fc_name?.ToString()
        //             })
        //             .ToList();
        //         pendingamountDD = model
        //             .OrderBy(x => x.pendingAmount)
        //             .GroupBy(x => x.pendingAmount)
        //             .Select(x => new dropdown_dataset_options
        //             {
        //                 name = x.FirstOrDefault().pendingAmount.ToString()
        //             })
        //             .ToList();
        //         walletamountDD = model
        //             .OrderBy(x => x.walletAmount)
        //             .GroupBy(x => x.walletAmount)
        //             .Select(x => new dropdown_dataset_options
        //             {
        //                 name = x.FirstOrDefault().walletAmount.ToString()
        //             })
        //             .ToList();

        //         return Ok(
        //             new
        //             {
        //                 model = model,
        //                 programDD = programDD,
        //                 itsIdDD = itsIdDD,
        //                 nameDD = nameDD,
        //                 fcNameDD = fcNameDD,
        //                 walletamountDD = walletamountDD,
        //                 pendingamountDD = pendingamountDD
        //             }
        //         );
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.ToString());
        //     }
        // }

        // [Route("getAllInActiveStudents")]
        // [HttpPost]
        // public async Task<ActionResult> getAllInactiveStudents(FeesAllotmentModel model1)
        // {
        //     string api = "getAllInActiveStudents";
        //     //// Add_ApiLogs(api);

        //     try
        //     {
        //         string token = _tokenService.ExtractTokenFromRequest(HttpContext);
        //         AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
        //         List<CampStudentModel> model = new List<CampStudentModel>();

        //         List<mz_student> studentsAll = _context.mz_student.ToList();

        //         List<registrationform_dropdown_set> allPset =
        //             _context.registrationform_dropdown_set.ToList();
        //         List<registrationform_programs> allProgram =
        //             _context.registrationform_programs.ToList();
        //         List<registrationform_subprograms> allSubProgram =
        //             _context.registrationform_subprograms.ToList();
        //         List<venue> allVenue = _context.venue.ToList();

        //         List<mz_student_feecategory> feeCategories =
        //             _context.mz_student_feecategory.ToList();
        //         List<mz_student_fee_transaction> allTransaction =
        //             _context.mz_student_fee_transaction.ToList();
        //         List<mz_student_ewallet> allEwallets = _context.mz_student_ewallet.ToList();

        //         List<mz_student> students = new List<mz_student>();

        //         foreach (var i in model1.monthList)
        //         {
        //             List<mz_student> students2 = studentsAll
        //                 .Where(x => x.psetId == i && x.activeStatus == false)
        //                 .ToList();

        //             foreach (var j in students2)
        //             {
        //                 students.Add(j);
        //             }
        //         }

        //         List<dropdown_dataset_options> programDD = new List<dropdown_dataset_options>();

        //         List<dropdown_dataset_options> itsIdDD = new List<dropdown_dataset_options>();

        //         List<dropdown_dataset_options> nameDD = new List<dropdown_dataset_options>();
        //         List<dropdown_dataset_options> fcNameDD = new List<dropdown_dataset_options>();
        //         List<dropdown_dataset_options> pendingamountDD =
        //             new List<dropdown_dataset_options>();

        //         List<dropdown_dataset_options> walletamountDD =
        //             new List<dropdown_dataset_options>();

        //         int c = model1.id;

        //         foreach (var stud in students)
        //         {
        //             mz_student s = studentsAll.Where(x => x.itsID == stud.itsID).FirstOrDefault();

        //             registrationform_dropdown_set pset = allPset
        //                 .Where(x => x.id == s.psetId)
        //                 .FirstOrDefault();
        //             registrationform_programs p = allProgram
        //                 .Where(x => x.id == pset.programId)
        //                 .FirstOrDefault();
        //             registrationform_subprograms sp = allSubProgram
        //                 .Where(x => x.id == pset.subprogramId)
        //                 .FirstOrDefault();
        //             venue v = allVenue.Where(x => x.Id == pset.venueId).FirstOrDefault();

        //             mz_student_feecategory fc = feeCategories
        //                 .Where(x => x.id == s.fcId)
        //                 .FirstOrDefault();
        //             string a = "0";
        //             string aa = "InActive";
        //             string feeCategory = "_Blank";
        //             if (fc != null)
        //             {
        //                 feeCategory = fc.categoryName;
        //             }

        //             List<mz_student_fee_transaction> transactions_1 = allTransaction
        //                 .Where(x => x.studentId == stud.mz_id)
        //                 .ToList();

        //             List<mz_student_ewallet> ewallets = allEwallets
        //                 .Where(x => x.studentId == stud.mz_id && x.status == true)
        //                 .ToList();

        //             int? p1 =
        //                 (transactions_1.ToList().Sum(x => x.debit))
        //                 - (transactions_1.ToList().Sum(x => x.credit));

        //             int? w =
        //                 (ewallets.ToList().Sum(x => x.credit))
        //                 - (ewallets.ToList().Sum(x => x.debit));

        //             model.Add(
        //                 new CampStudentModel
        //                 {
        //                     pendingAmount = p1 ?? 0,
        //                     walletAmount = w ?? 0,
        //                     activeStatusString2 = aa,
        //                     activeStatusString = a,
        //                     Id = c,
        //                     programName = p.name,
        //                     subProgram = sp.name,
        //                     venueName = v?.displayName,
        //                     EmailId = stud.studentEmail,
        //                     MobileNo = stud.studentMobile,
        //                     fatherEmail = stud.fatherEmail,
        //                     fatherMobile = stud.fatherMobile,
        //                     motherEmail = stud.motherEmail,
        //                     motherMobile = stud.motherMobile,
        //                     ItsId = s.itsID ?? 0,
        //                     pset_Name = p.name + " _ " + sp.name + " _ " + v?.displayName,
        //                     fc_name = feeCategory,
        //                     StudentName = s.nameEng,
        //                     age = stud.age ?? 0,
        //                     program = p.name,
        //                     subProgramName = sp.name,
        //                     venue = v?.displayName,
        //                     mzId = stud.mz_id
        //                 }
        //             );
        //             c = c + 1;
        //         }
        //         programDD = model
        //             .OrderBy(x => x.pset_Name)
        //             .GroupBy(x => x.pset_Name)
        //             .Select(x => new dropdown_dataset_options
        //             {
        //                 name = x.FirstOrDefault().pset_Name?.ToString()
        //             })
        //             .ToList();
        //         itsIdDD = model
        //             .OrderBy(x => x.ItsId)
        //             .GroupBy(x => x.ItsId)
        //             .Select(x => new dropdown_dataset_options
        //             {
        //                 name = x.FirstOrDefault().ItsId.ToString()
        //             })
        //             .ToList();
        //         nameDD = model
        //             .OrderBy(x => x.StudentName)
        //             .GroupBy(x => x.StudentName)
        //             .Select(x => new dropdown_dataset_options
        //             {
        //                 name = x.FirstOrDefault().StudentName?.ToString()
        //             })
        //             .ToList();
        //         fcNameDD = model
        //             .OrderBy(x => x.fc_name)
        //             .GroupBy(x => x.fc_name)
        //             .Select(x => new dropdown_dataset_options
        //             {
        //                 name = x.FirstOrDefault().fc_name?.ToString()
        //             })
        //             .ToList();
        //         pendingamountDD = model
        //             .OrderBy(x => x.pendingAmount)
        //             .GroupBy(x => x.pendingAmount)
        //             .Select(x => new dropdown_dataset_options
        //             {
        //                 name = x.FirstOrDefault().pendingAmount.ToString()
        //             })
        //             .ToList();
        //         walletamountDD = model
        //             .OrderBy(x => x.walletAmount)
        //             .GroupBy(x => x.walletAmount)
        //             .Select(x => new dropdown_dataset_options
        //             {
        //                 name = x.FirstOrDefault().walletAmount.ToString()
        //             })
        //             .ToList();

        //         return Ok(
        //             new ActiveStudentsReturnModel
        //             {
        //                 model = model,
        //                 programDD = programDD,
        //                 itsIdDD = itsIdDD,
        //                 nameDD = nameDD,
        //                 fcNameDD = fcNameDD,
        //                 walletamountDD = walletamountDD,
        //                 pendingamountDD = pendingamountDD
        //             }
        //         );
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.ToString());
        //     }
        // }

        [Route("getallfeealloted")]
        [HttpPost]
        public async Task<ActionResult> getallfeealloted(FeesAllotmentModel model)
        {
            string api = "getallfeealloted";
            //// Add_ApiLogs(api);


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                var allotmentss = _context.mz_student_fee_allotment.AsQueryable();

                if (model.fcId != null)
                {
                    allotmentss = allotmentss.Where(x => x.fcId == model.fcId);
                }

                if (model.pSetId != null)
                {
                    allotmentss = allotmentss.Where(x => x.pSetId == model.pSetId);
                }

                if (model.hijriYear != null)
                {
                    allotmentss = allotmentss.Where(x => x.hijriYear == model.hijriYear);
                }

                List<mz_student_fee_allotment> allotments = allotmentss.ToList();

                List<mz_student> students = _context.mz_student.ToList();

                List<registrationform_dropdown_set> rfds = _context.registrationform_dropdown_set.ToList();

                List<registrationform_programs> rfp = _context.registrationform_programs.ToList();

                List<registrationform_subprograms> rfs = _context.registrationform_subprograms.ToList();

                List<venue> venues = _context.venue.ToList();

                List<mz_student_feecategory> feeCategories = _context.mz_student_feecategory.ToList();

                List<mz_student_feecategory_pset> fc_psets = _context.mz_student_feecategory_pset.ToList();

                List<greg_months> gregMonths = _context.greg_months.ToList();

                List<FeesAllotmentModel> modelnew = new List<FeesAllotmentModel>();

                foreach (var m in model.monthList)
                {
                    List<feeCategory> Fc = new List<feeCategory>();

                    List<mz_student_fee_allotment> allotment = allotments.Where(x =>
                            x.monthId == m && x.hijriYear == model.hijriYear
                        )
                        .ToList();
                    List<dropdown_dataset_options> programDD = new List<dropdown_dataset_options>();

                    List<dropdown_dataset_options> itsIdDD = new List<dropdown_dataset_options>();

                    List<dropdown_dataset_options> nameDD = new List<dropdown_dataset_options>();
                    List<dropdown_dataset_options> fcNameDD = new List<dropdown_dataset_options>();

                    int c = 1;

                    foreach (var stud in allotment)
                    {

                        mz_student s = students.Where(x => x.mz_id == stud.studentId)
                            .FirstOrDefault();

                        registrationform_dropdown_set pset = rfds.Where(x => x.id == stud.pSetId)
                            .FirstOrDefault();
                        registrationform_programs p = rfp.Where(x => x.id == pset.programId)
                            .FirstOrDefault();
                        registrationform_subprograms sp = rfs.Where(x => x.id == pset.subprogramId)
                            .FirstOrDefault();
                        venue v = venues.Where(x => x.Id == pset.venueId).FirstOrDefault();

                        mz_student_feecategory fc = feeCategories.Where(x => x.id == stud.fcId)
                            .FirstOrDefault();
                        greg_months month = gregMonths.Where(x => x.id == stud.monthId)
                            .FirstOrDefault();

                        var existingStud = modelnew.FirstOrDefault(x => x.itsIdCSV == s.itsID.ToString());


                        monthlyAmount monthwise = new monthlyAmount
                        {
                            month = month.month_name,
                            amount = stud.feeAlloted
                        };

                        if (existingStud != null)
                        {
                            var existingCat = existingStud.feeCategoriesAlloted.FirstOrDefault(x => x.id == fc.id);

                            if (existingCat != null)
                            {
                                // Add new month under same category
                                existingCat.month.Add(monthwise);
                            }
                            else
                            {
                                // Create new category for this student
                                existingStud.feeCategoriesAlloted.Add(new feeCategory
                                {
                                    id = fc.id,
                                    name = fc.categoryName,
                                    month = new List<monthlyAmount> { monthwise }
                                });
                            }

                            continue;
                        }

                        modelnew.Add(new FeesAllotmentModel
                        {
                            id = c,
                            itsIdCSV = s.itsID.ToString(),
                            studentName = s.nameEng,
                            psetName = sp.name+"_" + v.CampVenue,
                            hijriYear = stud.hijriYear,
                            feeCategoriesAlloted = new List<feeCategory>{new feeCategory{
                                id = fc.id,
                                name = fc.categoryName,
                                month = new List<monthlyAmount> { monthwise }
                            }
                        }
                        }); c = c + 1;
                    }
                }

                return Ok(new { model = modelnew });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("allocateStudentFeesbulk/{hmonth}/{fcId}/{hyear}/{psetId}/{remarks}")]
        [HttpPost]
        public async Task<ActionResult> AllocateStudentFees_BULK(
            int hmonth,
            int fcId,
            string hyear,
            int psetId,
            string remarks,
            FeesAllotmentModel model
        )
        {
            string api = "api/query/allocateStudentFeesbulk/{hmonth}/{hyear}/{psetId}/{remarks}";
            //// Add_ApiLogs(api);

            int monthId = hmonth;
            string hijriYear = hyear;
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<mz_student> studentList = _context.mz_student.Where(x =>x.activeStatus == true && x.psetId == psetId).ToList();

                mz_student_feecategory_pset feeCategory = _context.mz_student_feecategory_pset.Where(x => x.id == fcId && x.psetId == psetId).FirstOrDefault();

                List<mz_student_feecategory_pset_monthly> feeCategoriesMonthly = _context.mz_student_feecategory_pset_monthly.Where(x => x.student_feecategory_pset_id == feeCategory.id).ToList();

                List<mz_student_fee_allotment> feeAllotment = _context.mz_student_fee_allotment.Where(x => x.pSetId == psetId && x.monthId == monthId && x.hijriYear == hijriYear && x.fcId == feeCategory.fcId).ToList();

                List<greg_months> gregMonths = _context.greg_months.ToList();

                List<mz_student_fee_excluding_list> exs = _context.mz_student_fee_excluding_list.ToList();

                List<mz_student_fee_transaction> transactionss = _context.mz_student_fee_transaction.ToList();

                int c = 0;

                if(studentList.Count() < 1)
                {
                    return BadRequest(new { message = "No Students Fund" });
                }
                foreach (var stud in studentList)
                {
                    var checkAllotment = feeAllotment.FirstOrDefault(x => x.studentId == stud.mz_id && x.fcId == feeCategory.fcId && x.pSetId == psetId && x.hijriYear == hijriYear && x.monthId == monthId);
                    if(checkAllotment == null)
                    {
                        mz_student_fee_excluding_list ex = exs.Where(x => x.studentMzId == stud.mz_id && x.pSetId == psetId && x.monthId == monthId && x.hijriYear == hijriYear).FirstOrDefault();

                        if (ex == null)
                        {
                            var gregMonth = gregMonths.Where(x => x.id == monthId).FirstOrDefault();
                            var feeCatMnth = feeCategoriesMonthly.Where(x => x.month == gregMonth.slug).FirstOrDefault();

                            if (feeCatMnth != null)
                            {
                                var aa = feeAllotment.Where(x => x.studentId == stud.mz_id).ToList();

                                if (aa.Count < 1)
                                {
                                    c = c + 1;
                                    var a = new mz_student_fee_allotment
                                    {
                                        currency = "INR",
                                        createdBy = authUser.Name,
                                        createdOn = DateTime.Now,
                                        fcId = feeCategory.fcId,
                                        feeAlloted = feeCatMnth.fees_per_student,
                                        hijriYear = hyear,
                                        monthId = monthId,
                                        pSetId = stud.psetId,
                                        studentId = stud.mz_id,
                                        remarks = remarks,
                                        waiveStatus = false
                                    };
                                    _context.mz_student_fee_allotment.Add(a);
                                    _context.SaveChanges();

                                    var b = new mz_student_fee_transaction
                                    {
                                        currency = "INR",
                                        debit = feeCatMnth.fees_per_student,
                                        createdOn = DateTime.Now,
                                        allotmentId = a.id,
                                        createdBy = authUser.Name,
                                        studentId = stud.mz_id
                                    };
                                    _context.mz_student_fee_transaction.Add(b);

                                    a.txn_Id = b.id;
                                    _context.SaveChanges();
                                } else {
                                    int count = 1;
                                    //var allotments = feeAllotment.Where(x => x.studentId == stud.mz_id).ToList();

                                    foreach (var a1 in aa)
                                    {
                                        List<mz_student_fee_transaction> transactions = transactionss.Where(x => x.allotmentId == a1.id).ToList();

                                        int? D_withoutR = transactions
                                            .Where(x => x.paymentMode != "Reverse")
                                            .ToList()
                                            .Sum(x => x.debit);
                                        int? waived = transactions
                                            .Where(x => x.paymentMode == "Waive")
                                            .ToList()
                                            .Sum(x => x.credit);

                                        int? amount = D_withoutR - waived;

                                        if (amount == 0)
                                        {
                                            if (count != aa.Count)
                                            {
                                                count = count + 1;

                                                continue;
                                            }
                                            else
                                            {
                                                c = c + 1;
                                                var a = new mz_student_fee_allotment
                                                {
                                                    currency = "INR",
                                                    createdBy = authUser.Name,
                                                    createdOn = DateTime.Now,
                                                    fcId = feeCategory.fcId,
                                                    feeAlloted = feeCatMnth.fees_per_student,
                                                    hijriYear = hyear,
                                                    monthId = monthId,
                                                    pSetId = stud.psetId,
                                                    studentId = stud.mz_id,
                                                    remarks = remarks,
                                                    waiveStatus = false
                                                };
                                                _context.mz_student_fee_allotment.Add(a);
                                                _context.SaveChanges();

                                                var b = new mz_student_fee_transaction
                                                {
                                                    currency = "INR",
                                                    debit = feeCatMnth.fees_per_student,
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
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    count = count + 1;
                                }
                                //_context.SaveChanges();

                            }
                            else
                            {
                                return BadRequest(new { message = "Fees not entered for " + gregMonth.month_name });
                            }

                            //}
                        }
                    }
                    
                }
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        //[Route("waiveStudentFeesBulk")]
        //[HttpPost]
        //public async Task<ActionResult> WaiveStudentFees_Bulk_old(FeesAllotmentModel model)
        //{
        //    string api = "waiveStudentFeesBulk";
        //    //// Add_ApiLogs(api);

        //    string hyear = model.hijriYear;
        //    int monthId = model.monthList[0];
        //    string remarks = model.remarks;
        //    string itsIdCSV = model.itsIdCSV;

        //    string token = _tokenService.ExtractTokenFromRequest(HttpContext);
        //    AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
        //    List<int> itsIds = _helperService.parseItsId(itsIdCSV);
        //    List<mz_student> studentList = _context
        //        .mz_student.Where(x => x.activeStatus == true && x.fcId != null)
        //        .ToList();
        //    studentList = studentList.Where(x => itsIds.Any(y => y == x.itsID)).ToList();
        //    if (model.monthList.Count > 1)
        //    {
        //        return BadRequest(new { message = "selection of only one month is allowed" });
        //    }
        //    List<mz_student_fee_allotment> allotments = _context
        //        .mz_student_fee_allotment.Where(x =>
        //            x.monthId == monthId && x.hijriYear == model.hijriYear
        //        )
        //        .ToList();
        //    allotments = allotments
        //        .Where(x => studentList.Any(y => y.mz_id == x.studentId))
        //        .ToList();
        //    String ResponseMessage = "Success";
        //    int c = 1;
        //    if (allotments.Count == 0)
        //    {
        //        return BadRequest(new { message = "Reqeust failed allocation empty" });
        //    }
        //    foreach (var i in allotments)
        //    {
        //        List<mz_student_fee_transaction> transactions = _context
        //            .mz_student_fee_transaction.Where(x =>
        //                x.studentId == i.studentId && x.allotmentId == i.id
        //            )
        //            .ToList();

        //        if (transactions.Count == 1 && transactions[0].debit == i.feeAlloted)
        //        {
        //            _context.mz_student_fee_transaction.Add(
        //                new mz_student_fee_transaction
        //                {
        //                    allotmentId = i.id,
        //                    createdBy = authUser.Name,
        //                    createdOn = indianTime,
        //                    currency = "INR",
        //                    credit = i.feeAlloted,
        //                    paymentMode = "Waive",
        //                    studentId = i.studentId,
        //                    remarks = "Bulk Waive service :: " + model.remarks
        //                }
        //            );
        //        }
        //        else
        //        {
        //            ResponseMessage = "one or more entries were not waived";
        //        }
        //    }
        //    _context.SaveChanges();

        //    return Ok(ResponseMessage);
        //}

        [Route("waiveStudentFeesBulk")]
        [HttpPost]
        public async Task<ActionResult> WaiveStudentFees_Bulk(List<FeesAllotmentModel> models)
        {
            string api = "waiveStudentFeesBulk";
            //// Add_ApiLogs(api);
            String ResponseMessage = "Success";

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<mz_student> studentsList = _context.mz_student.Where(x => x.activeStatus == true && x.fcId != null).ToList();

            List<mz_student_fee_allotment> allotments = _context.mz_student_fee_allotment.ToList();

            List<greg_months> gregMonths = _context.greg_months.ToList();

            foreach (var model in models)
            {
                string hyear = model.hijriYear;
                //int monthId = model.monthList[0];
                string remarks = model.remarks;
                string itsIdCSV = model.itsIdCSV;

                var student = studentsList.Where(x => x.itsID == model.itsId).FirstOrDefault();

                foreach (var item1 in model.months)
                {
                    var greg_months = gregMonths.Where(x => x.month_name == item1).FirstOrDefault();

                    List<mz_student_fee_allotment> allotment = allotments.Where(x =>
                        x.monthId == greg_months.id && x.hijriYear == model.hijriYear && x.studentId == student.mz_id
                    )
                    .ToList();

                    //allotments = allotments
                    //    .Where(x => studentsList.Any(y => y.mz_id == x.studentId))
                    //    .ToList();

                    int c = 1;
                    if (allotment.Count == 0)
                    {
                        return BadRequest(new { message = "Reqeust failed allocation empty" });
                    }

                    foreach (var i in allotment)
                    {
                        List<mz_student_fee_transaction> transactions = _context
                            .mz_student_fee_transaction.Where(x =>
                                x.studentId == i.studentId && x.allotmentId == i.id
                            )
                            .ToList();

                        if (transactions.Count == 1 && transactions[0].debit == i.feeAlloted)
                        {
                            _context.mz_student_fee_transaction.Add(
                                new mz_student_fee_transaction
                                {
                                    allotmentId = i.id,
                                    createdBy = authUser.Name,
                                    createdOn = indianTime,
                                    currency = "INR",
                                    credit = i.feeAlloted,
                                    paymentMode = "Waive",
                                    studentId = i.studentId,
                                    remarks = "Bulk Waive service :: " + model.remarks
                                }
                            );
                        }
                        else
                        {
                            ResponseMessage = "one or more entries were not waived";
                        }
                    }
                }

                
            }
            
            _context.SaveChanges();

            return Ok(new {message = ResponseMessage });
        }

        [Route("allocateStudentFeesmanual")]
        [HttpPost]
        public async Task<ActionResult> AllocateStudentFees_Manual(List<FeesAllotmentModel> models)
        {
            string api = "api/query/allocateStudentFeesmanual";
            //// Add_ApiLogs(api);
            ///
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<mz_student> studs = _context.mz_student.ToList();

            List<mz_student_feecategory_pset_monthly> fc_pset_months = _context.mz_student_feecategory_pset_monthly.ToList();
            
            List<greg_months> greg_months = _context.greg_months.ToList();

            List<mz_student_fee_excluding_list> exs = _context.mz_student_fee_excluding_list.ToList();

            List<mz_student_fee_allotment> allotmentss = _context.mz_student_fee_allotment.ToList();

            List<mz_student_feecategory_pset> fc_psets = _context.mz_student_feecategory_pset.ToList();

            List<mz_student_fee_transaction> transactionss = _context.mz_student_fee_transaction.ToList();
            try
            {
                foreach (var model in models)
                {
                    string hyear = model.hijriYear;
                    string remarks = model.remarks;
                    string itsIdCSV = model.itsIdCSV;


                    //List<int> itsIds = _helperService.parseItsId(itsIdCSV);
                    //mz_student stud = _context
                    //    .mz_student.Where(x => x.activeStatus == true && x.fcId != null && x.itsID == model.itsId)
                    //    .FirstOrDefault();
                    //studentList = studentList.Where(x => itsIds.Any(y => y == x.itsID)).ToList();
                    //if (itsIds.Count > 50)
                    //{
                    //    return BadRequest(new { message = "Allocation cannot be done for more than 50 students." });
                    //}

                    var stud = studs.Where(x => x.activeStatus == true && x.fcId != null && x.itsID == model.itsId).FirstOrDefault();
                    int c = 0;
                    //foreach (var i in itsIds)
                    //{
                    //var stud = _context
                    //    .mz_student.Where(x => x.itsID == i && x.activeStatus == true)
                    //    .FirstOrDefault();

                    if (stud != null)
                    {
                        foreach (var monthId in model.monthList)
                        {
                            //mz_student_fee_excluding_list ex = _context
                            //    .mz_student_fee_excluding_list.Where(x =>
                            //        x.studentMzId == stud.mz_id
                            //        && x.pSetId == stud.psetId
                            //        && x.monthId == monthId
                            //        && x.hijriYear == hyear
                            //    )
                            //    .FirstOrDefault();

                            var ex = exs.Where(x => x.studentMzId == stud.mz_id && x.pSetId == stud.psetId && x.monthId == monthId && x.hijriYear == hyear).FirstOrDefault();

                            if (ex == null)
                            {
                                //var allotments = _context
                                //    .mz_student_fee_allotment.Where(x =>
                                //        x.hijriYear == hyear
                                //        && x.monthId == monthId
                                //        && x.studentId == stud.mz_id
                                //    )
                                //    .ToList();

                                var allotments = allotmentss.Where(x => x.hijriYear == hyear && x.monthId == monthId && x.studentId == stud.mz_id).ToList();

                                //mz_student_feecategory_pset fc_pset = _context
                                //        .mz_student_feecategory_pset.Where(x =>
                                //            x.id == model.fcId && x.psetId == model.programSetId
                                //        )
                                //        .FirstOrDefault();

                                var fc_pset = fc_psets.Where(x => x.id == model.fcId && x.psetId == model.programSetId).FirstOrDefault();

                                var greg_month = greg_months.Where(x => x.id == monthId).FirstOrDefault();
                                var fc_pset_month = fc_pset_months.Where(x => x.student_feecategory_pset_id == fc_pset.id && x.month == greg_month.slug).FirstOrDefault();

                                if (allotments.Count < 1)
                                {
                                    if (fc_pset != null)
                                    {
                                        c = c + 1;
                                        if (fc_pset_month != null)
                                         {
                                            var a = new mz_student_fee_allotment
                                            {
                                                currency = "INR",
                                                createdBy = authUser.Name,
                                                createdOn = DateTime.Now,
                                                fcId = fc_pset.fcId,
                                                feeAlloted = fc_pset_month.fees_per_student,
                                                hijriYear = hyear,
                                                monthId = monthId,
                                                pSetId = fc_pset.psetId,
                                                studentId = stud.mz_id,
                                                remarks = remarks,
                                                waiveStatus = false
                                            };
                                            _context.mz_student_fee_allotment.Add(a);
                                            _context.SaveChanges();

                                            var b = new mz_student_fee_transaction
                                            {
                                                currency = "INR",
                                                debit = fc_pset_month.fees_per_student,
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
                                else
                                {
                                    int count = 1;
                                    //var allotments = _context
                                    //    .mz_student_fee_allotment.Where(x =>
                                    //        x.hijriYear == hyear
                                    //        && x.monthId == monthId
                                    //        && x.studentId == stud.mz_id
                                    //    )
                                    //    .ToList();

                                    foreach (var a1 in allotments)
                                    {
                                        //List<mz_student_fee_transaction> transactions = _context
                                        //    .mz_student_fee_transaction.Where(x =>
                                        //        x.allotmentId == a1.id
                                        //    )
                                        //    .ToList();
                                        var transactions = transactionss.Where(x => x.allotmentId == a1.id).ToList();

                                        int ? D_withoutR = transactions
                                            .Where(x => x.paymentMode != "Reverse")
                                            .ToList()
                                            .Sum(x => x.debit);
                                        int? waived = transactions
                                            .Where(x => x.paymentMode == "Waive")
                                            .ToList()
                                            .Sum(x => x.credit);

                                        int? amount = D_withoutR - waived;

                                        if (amount == 0)
                                        {
                                            if (count != allotments.Count)
                                            {
                                                count = count + 1;

                                                continue;
                                            }
                                            else
                                            {
                                                //mz_student_feecategory_pset fc_pset = _context
                                                //    .mz_student_feecategory_pset.Where(x =>
                                                //        x.fcId == model.fcId
                                                //        && x.psetId == model.programSetId
                                                //    )
                                                //    .FirstOrDefault();

                                                if (fc_pset != null && fc_pset_month != null)
                                                {
                                                    c = c + 1;
                                                    var a = new mz_student_fee_allotment
                                                    {
                                                        currency = "INR",
                                                        createdBy = authUser.Name,
                                                        createdOn = DateTime.Now,
                                                        fcId = fc_pset.fcId,
                                                        feeAlloted = fc_pset_month.fees_per_student,
                                                        hijriYear = hyear,
                                                        monthId = monthId,
                                                        pSetId = model.programSetId,
                                                        studentId = stud.mz_id,
                                                        remarks = remarks,
                                                        waiveStatus = false
                                                    };
                                                    _context.mz_student_fee_allotment.Add(a);
                                                    _context.SaveChanges();

                                                    var b = new mz_student_fee_transaction
                                                    {
                                                        currency = "INR",
                                                        debit = fc_pset.amount,
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
                                        else
                                        {
                                            break;
                                        }

                                        count = count + 1;
                                    }
                                }
                            }
                        }
                    }
                    //}
                }

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [Route("allocateStudentFeesmiscellaneous")]
        [HttpPost]
        public async Task<ActionResult> AllocateStudentFees_Miscellaneous(FeesAllotmentModel model)
        {
            string api = "api/query/allocateStudentFeesmiscellaneous";
            //// Add_ApiLogs(api);

            int amount = model.feeAlloted ?? 0;
            string remarks = model.remarks;
            string itsIdCSV = model.itsIdCSV;
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<mz_student> studentList = _context
                    .mz_student.Where(x => x.activeStatus == true && x.fcId != null)
                    .ToList();
                List<int> itsIds = _helperService.parseItsId(itsIdCSV);

                foreach (var i in itsIds)
                {
                    var stud = _context.mz_student.Where(x => x.itsID == i).FirstOrDefault();

                    if (stud != null)
                    {
                        var a = new mz_student_fee_allotment
                        {
                            currency = "INR",
                            createdBy = authUser.Name,
                            createdOn = DateTime.Now,
                            feeAlloted = amount,
                            pSetId = stud.psetId,
                            studentId = stud.mz_id,
                            remarks = "Miscellaneous - " + remarks,
                            waiveStatus = false
                        };
                        _context.mz_student_fee_allotment.Add(a);

                        _context.SaveChanges();

                        var b = new mz_student_fee_transaction
                        {
                            currency = "INR",
                            debit = amount,
                            createdOn = DateTime.Now,
                            allotmentId = a.id,
                            createdBy = authUser.Name,
                            studentId = stud.mz_id
                        };
                        _context.mz_student_fee_transaction.Add(b);
                        _context.SaveChanges();

                        a.txn_Id = b.id;
                    }
                }
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [Route("updatePsetMonthly")]
        [HttpPost]   // <-- IMPORTANT FIX
        public async Task<IActionResult> UpdatePsetMonthly()
        {
            try
            {
                // Fetch ONLY what is needed
                var fc_psets = await _context.mz_student_feecategory_pset
                                             .OrderByDescending(x => x.id)
                                             .ToListAsync();

                var fc_psets_monthly = await _context.mz_student_feecategory_pset_monthly
                                                     .ToListAsync();

                var estimates = await _context.mz_expense_estimate_student
                                              .ToListAsync();

                var estimatesMonthly = await _context.mz_expense_estimate_student_monthly
                                                     .ToListAsync();

                // All inserts collected here (1 SaveChanges at end)
                List<mz_student_feecategory_pset_monthly> newRows = new List<mz_student_feecategory_pset_monthly>();


                foreach (var fc in fc_psets)
                {
                    // Skip invalid psetId
                    if (fc.psetId == null)
                        continue;

                    // Check if already exists in monthly table
                    var fcMonthly = fc_psets_monthly
                                     .Where(x => x.student_feecategory_pset_id == fc.id)
                                     .ToList();

                    if (fcMonthly.Count < 1)
                    {
                        // Find matching estimate
                        var es = estimates.FirstOrDefault(x => x.sfcp_id == fc.id);

                        if (es == null)
                            continue;

                        // Get monthly estimate rows
                        var esm = estimatesMonthly
                                    .Where(x => x.estimate_student_id == es.id)
                                    .ToList();

                        foreach (var item1 in esm)
                        {
                            var monthlyPset = new mz_student_feecategory_pset_monthly
                            {
                                student_feecategory_pset_id = fc.id,
                                psetId = fc.psetId ?? 0,
                                month = item1.month,
                                student_count = item1.student_count,
                                fees_per_student = item1.fees_per_student
                            };

                            newRows.Add(monthlyPset);
                        }
                    }
                }

                // Insert all collected rows in a single batch
                if (newRows.Count > 0)
                {
                    _context.mz_student_feecategory_pset_monthly.AddRange(newRows);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { message = "Monthly PSet updated successfully", inserted = newRows.Count });
            }
            catch (Exception ex)
            {
                // Return detailed error for debugging
                return StatusCode(500, new { error = ex.ToString() });
            }
        }

        private async Task<bool> UpdateFromElq(
            List<mz_pset_elqgrpid_mapping> groupIds,
            int psetId,
            AuthUser authUser
        )
        {
            bool newAddition = false;
            try
            {
                List<mz_student> allStudents = await _context.mz_student.ToListAsync();
                List<mz_student> mz_students = allStudents.Where(x => x.psetId == psetId).ToList();

                List<mz_student> elq_students = new List<mz_student>();

                foreach (mz_pset_elqgrpid_mapping j in groupIds)
                {

                    var stages = new List<int>();
                    stages.Add(0);
                    stages.Add(2);
                    stages.Add(7);

                    List<mz_student> students = _elearningService
                        .GetStudents_FromGroup(j.elqId ?? 0);

                    students = students.Where(x => stages.Contains(x.elq_GroupId ?? 0))
                        .ToList();

                    //List<mz_student> students = cache.GetItem<List<mz_student>>("elqStudentsList-" + j.elqId);
                    //if (students == null)
                    //{
                    //    students = elearning.GetStudents_FromGroup(j.elqId ?? 0).Where(x => stages.Contains(x.elq_GroupId ?? 0)).ToList();
                    //    cache.AddItem("elqStudentsList-" + j.elqId, students, DateTime.Now.AddMinutes(30));
                    //}

                    if (psetId == 40 || psetId == 66)
                    {
                        foreach (var k in students)
                        {
                            elq_students.Add(k);
                        }
                    }
                    else
                    {
                        if (!(j.elqId == 7))
                        {
                            foreach (var k in students)
                            {
                                elq_students.Add(k);
                            }
                        }
                    }
                }

                foreach (var i in mz_students)
                {
                    i.activeStatus = false;
                }
                await _context.SaveChangesAsync();
                foreach (var i in elq_students)
                {
                    mz_student? s = allStudents.Where(x => x.itsID == i.itsID).FirstOrDefault();

                    if (s != null)
                    {
                        int p = s.psetId ?? 0;
                        s.activeStatus = true;
                        s.psetId = psetId;
                        s.elq_BranchName = i.elq_BranchName;
                    }
                    else
                    {
                        newAddition = true;
                        mz_student s1 = new mz_student
                        {
                            elq_BranchName = i.elq_BranchName,
                            itsID = i.itsID,
                            psetId = psetId,
                            activeStatus = true
                        };

                        ItsUser? user = await _itsService.GetItsUser(i.itsID ?? 0);
                        if (user == null)
                        {
                            s1.nameEng = "ITS User Not Found";
                            await _context.SaveChangesAsync();

                            continue;
                        }
                        ItsUser? father = await _itsService.GetItsUser(user?.Father_ItsId ?? 0);

                        ItsUser? mother = await _itsService.GetItsUser(user?.Mother_ItsId ?? 0);

                        s1.gender = user.Gender;
                        s1.jamaat = user.Jamaat;
                        s1.jamaat = user.Jamiat;
                        s1.maqaam = user.Maqaam;
                        s1.nameEng = user.Name;
                        s1.nationality = user.Nationality;
                        s1.studentEmail = user.EmailId;
                        s1.studentMobile = user.MobileNo;
                        s1.vatan = user.Vatan;
                        s1.address = user.Address;
                        s1.age = user.Age;
                        s1.bloodGroup = user.BloodGroup;
                        s1.dobGregorian = user.Dob.ToString("dd/MM/yyyy hh:mm:ss tt");
                        s1.dobHijri = user.DOB_Hijri;
                        s1.nameArabic = user.Arabic_FullName;
                        s1.fatherEmail = father?.EmailId ?? "N/A";
                        s1.fatherMobile = father?.MobileNo ?? "N/A";
                        s1.motherEmail = mother?.EmailId ?? "N/A";
                        s1.motherMobile = mother?.MobileNo ?? "N/A";

                        await _helperService.SaveITSImage(user.Photo, user.ItsId);
                        await _context.mz_student.AddAsync(s1);
                        await _context.SaveChangesAsync();
                    }
                }
                await _context.SaveChangesAsync();
                return newAddition;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }

    public class ActiveStudentsReturnModel
    {
        public List<CampStudentModel> model { get; set; }
        public List<dropdown_dataset_options> programDD { get; set; }
        public List<dropdown_dataset_options> itsIdDD { get; set; }
        public List<dropdown_dataset_options> nameDD { get; set; }
        public List<dropdown_dataset_options> fcNameDD { get; set; }
        public List<dropdown_dataset_options> walletamountDD { get; set; }
        public List<dropdown_dataset_options> pendingamountDD { get; set; }
    }

    public class FeeCategories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? classId { get; set; }
        public int? schoolId { get; set; }
    }

}
