using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly ElearningService _elearningService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        public StudentController(mzdbContext context, IMapper mapper, TokenService tokenService)
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
        // log4net.ILog log = // log4net.LogManager.GetLogger(typeof(StudentController));
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        [Route("refreshDataFromIts")]
        [HttpGet]
        public async Task<ActionResult> refreshDataFromIts([FromQuery] int? itsId)
        {
            string api = "refreshDataFromIts/{itsId}";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (itsId == null)
            {
                itsId = authUser.ItsId;
            }

            mz_student s = _context.mz_student.Where(x => x.itsID == itsId).FirstOrDefault();

            try
            {
                ItsUser user = await _itsService.GetItsUser(s.itsID ?? 0);

                ItsUser father = await _itsService.GetItsUser(user.Father_ItsId);

                ItsUser mother = await _itsService.GetItsUser(user.Mother_ItsId);


                s.gender = user.Gender;
                s.jamaat = user.Jamaat;
                s.jamiat = user.Jamiat;
                s.maqaam = user.Maqaam;
                s.nameEng = user.Name;
                s.nationality = user.Nationality;
                s.studentEmail = user.EmailId;
                s.studentMobile = user.MobileNo;
                s.vatan = user.Vatan;
                s.address = user.Address;
                s.age = user.Age;
                s.bloodGroup = user.BloodGroup;
                s.dobGregorian = user.Dob.ToString();
                s.dobHijri = user.DOB_Hijri;
                s.nameArabic = user.Arabic_FullName;
                s.idara = user.Idara;

                if (father != null)
                {
                    s.fatherEmail = father.EmailId;
                    s.fatherMobile = father.MobileNo;
                }
                if (mother != null)
                {
                    s.motherEmail = mother.EmailId;
                    s.motherMobile = mother.MobileNo;
                }
                s.hifzStatus = user.hifzStatus;
                try
                {
                    s.hifzSanadYear = Int32.Parse(user.hifzYear);
                }
                catch (Exception e)
                {

                }

                //SaveITSImage(user.Photo, user.ItsId);

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

            return Ok();


        }



        [Route("refreshDataFromItsBulk")]
        [HttpGet]
        public async Task<ActionResult> refreshDataFromItsBulk()
        {

            string api = "refreshDataFromItsBulk";
            //// Add_ApiLogs(api);

            //List<mz_student> ss = _context.mz_student.ToList();
            List<mz_student> ss = _context.mz_student.Where(x => x.nameEng == null).ToList();

            foreach (var s1 in ss)
            {
                try
                {
                    ItsUser user = await _itsService.GetItsUser(s1.itsID ?? 0);

                    ItsUser father = await _itsService.GetItsUser(user.Father_ItsId);

                    ItsUser mother = await _itsService.GetItsUser(user.Mother_ItsId);


                    s1.gender = user.Gender;
                    s1.jamaat = user.Jamaat;
                    s1.jamiat = user.Jamiat;
                    s1.maqaam = user.Maqaam;
                    s1.nameEng = user.Name;

                    s1.nationality = user.Nationality;
                    s1.studentEmail = user.EmailId;
                    s1.studentMobile = user.MobileNo;
                    s1.vatan = user.Vatan;
                    s1.address = user.Address;
                    s1.age = user.Age;
                    s1.bloodGroup = user.BloodGroup;
                    s1.dobGregorian = user.Dob.ToString();
                    s1.dobHijri = user.DOB_Hijri;
                    s1.nameArabic = user.Arabic_FullName;
                    s1.hifzStatus = user.hifzStatus;
                    s1.idara = user.Idara;

                    if (father != null)
                    {
                        s1.fatherEmail = father.EmailId;
                        s1.fatherMobile = father.MobileNo;
                    }
                    if (mother != null)
                    {
                        s1.motherEmail = mother.EmailId;
                        s1.motherMobile = mother.MobileNo;
                    }
                    try
                    {
                        if (user.hifzYear != "")
                        {
                            s1.hifzSanadYear = Int32.Parse(user.hifzYear);
                        }

                    }
                    catch (Exception e)
                    {
                        BadRequest(e.Message);
                    }

                    await _helperService.SaveITSImage(user.Photo, user.ItsId);

                    _context.SaveChanges();
                }
                catch (Exception ex)
                {

                }


            }

            return Ok();


        }



        [Route("bulkupdateStudentProfileData")]
        [HttpPost]
        public async Task<ActionResult> AllocateStudentFees_Manual(FeesAllotmentModel model)
        {
            string api = "bulkupdateStudentProfileData";
            //// Add_ApiLogs(api);

            string fieldName = model.remarks;
            string itsIdCSV = model.itsIdCSV;
            string data = model.reason;
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


                List<int> itsIds = _helperService.parseItsId(itsIdCSV);

                if (itsIds.Count > 50)
                {
                    return BadRequest(new { message = "Students cannot be more than 50" });
                }

                int c = 0;
                foreach (var i in itsIds)
                {
                    var stud = _context.mz_student.Where(x => x.itsID == i).FirstOrDefault();

                    if (stud != null)
                    {

                        if (fieldName == "classId")
                        {
                            stud.classId = Convert.ToInt32(data);

                        }
                        else if (fieldName == "TRNO")
                        {
                            stud.trNo = Convert.ToInt32(data);

                        }
                        else if (fieldName == "fasal")
                        {
                            stud.dq_fasal = Convert.ToInt32(data);

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


        [Route("getall/nisabMuntasebeen")]
        [HttpGet]
        public async Task<ActionResult> getAllisabMuntasebeen()
        {
            string api = "getall/nisabMuntasebeen";

            try
            {
                //AuthUser authUser = ServiceFactory.GetAuthService().GetAuthUser(HttpContext.Current.User);


                List<nisaab_student_logs> nsl = _context.nisaab_student_logs.ToList();

                List<dynamic> d = new List<dynamic>();

                nsl.ForEach(x => d.Add(new
                {
                    its_id = x.itsId.ToString(),
                    course_name = x.courseName,
                    course_duration = x.courseDuration.ToString(),
                    institute_country = x.instituteCountry,
                    institute_city = x.instituteCity,
                    institute_name = x.instituteName,
                    course_start_date = x.courseStartDate.ToString("yyyy-MM-dd"),
                    course_end_date = x.courseEndDate.ToString("yyyy-MM-dd"),
                }));

                return Ok(d);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        [Route("student/hifz/report")]
        [HttpGet]
        public async Task<ActionResult> GetHifzReport()
        {
            string api = "api/studentController/student/hifz/report";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            HifzData Hifz = new HifzData();
            try
            {
                Hifz = _elearningService.GetCurrentHifzStatus(authUser.ItsId);
                // log.Debug("Successfully fetched Hifz Data with juz : " + Hifz.Juz);
            }
            catch (Exception ex)
            {
                // log.Debug("failed services: " + ex.ToString() + " Hifz:" + (Hifz != null));
            }
            return Ok(Hifz);
        }


        public static readonly string New = "New";
        public static readonly string InProgress = "In-Progress";
        public static readonly string Resolved = "Resolved";
        public static readonly string ReOpened = "Re-Opened";
        public static readonly string Closed = "Closed";


        [Route("fees/setExcludingList/{mzId}/{psetId}/{monthId}/{yearId}")]
        [HttpGet]
        public async Task<ActionResult> setExcludingList(int mzId, int psetId, int monthId, string yearId)
        {
            string api = "api/studentController/fees/setExcludingList/{mzId}/{psetId}/{monthId}/{yearId}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


                _context.mz_student_fee_excluding_list.Add(new mz_student_fee_excluding_list
                {
                    monthId = monthId,
                    hijriYear = yearId,
                    pSetId = psetId,
                    studentMzId = mzId,
                    createdBy = authUser.Name,
                    createdOn = indianTime,
                });
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("fees/getExcludingList")]
        [HttpPost]
        public async Task<ActionResult> getExcludingList(FeesAllotmentModel model1)
        {
            string api = "api/studentController/fees/getExcludingList";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<CampStudentModel> model = new List<CampStudentModel>();


                List<mz_student_fee_excluding_list> students = new List<mz_student_fee_excluding_list>();

                foreach (var i in model1.monthList)
                {


                    List<mz_student_fee_excluding_list> students2 = _context.mz_student_fee_excluding_list.Where(x => x.pSetId == i).ToList();

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

                int c = 1;

                foreach (var stud in students)
                {

                    mz_student s = _context.mz_student.Where(x => x.mz_id == stud.studentMzId).FirstOrDefault();
                    mz_student_fee_excluding_list Es = _context.mz_student_fee_excluding_list.Where(x => x.studentMzId == s.mz_id).FirstOrDefault();

                    registrationform_dropdown_set pset = _context.registrationform_dropdown_set.Where(x => x.id == s.psetId).FirstOrDefault();
                    registrationform_programs p = _context.registrationform_programs.Where(x => x.id == pset.programId).FirstOrDefault();
                    registrationform_subprograms sp = _context.registrationform_subprograms.Where(x => x.id == pset.subprogramId).FirstOrDefault();
                    venue v = _context.venue.Where(x => x.Id == pset.venueId).FirstOrDefault();

                    mz_student_feecategory fc = _context.mz_student_feecategory.Where(x => x.id == s.fcId).FirstOrDefault();
                    string a = "1";
                    string aa = "Active";
                    string feeCategory = "_Blank";
                    //if (stud.activeStatus ?? false)
                    //{
                    //    a = "1";
                    //    aa = "Active";
                    //}
                    if (fc != null)
                    {
                        feeCategory = fc.categoryName;
                    }


                    hijri_months month = _context.hijri_months.Where(x => x.id == stud.monthId).FirstOrDefault();


                    model.Add(new CampStudentModel
                    {
                        hijriyear = stud.hijriYear,
                        hijriMonthName = month.hijriMonthName,
                        mzId = s.mz_id,
                        programSetId = s.psetId,
                        activeStatusString2 = aa,
                        activeStatusString = a,
                        Id = stud.id,
                        programName = p.name,
                        subProgram = sp.name,
                        venueName = v?.displayName,
                        ItsId = s.itsID ?? 0,
                        pset_Name = p.name + " _ " + sp.name + " _ " + v?.displayName,
                        StudentName = s.nameEng,
                        program = p.name,
                        subProgramName = sp.name,
                        venue = v?.displayName,
                    });
                    c = c + 1;

                }
                programDD = model.OrderBy(x => x.pset_Name).GroupBy(x => x.pset_Name).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().pset_Name?.ToString() }).ToList();
                itsIdDD = model.OrderBy(x => x.ItsId).GroupBy(x => x.ItsId).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().ItsId.ToString() }).ToList();
                nameDD = model.OrderBy(x => x.StudentName).GroupBy(x => x.StudentName).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().StudentName?.ToString() }).ToList();
                fcNameDD = model.OrderBy(x => x.fc_name).GroupBy(x => x.fc_name).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().fc_name?.ToString() }).ToList();


                return Ok(new { model = model, programDD = programDD, itsIdDD = itsIdDD, nameDD = nameDD, fcNameDD = fcNameDD, walletamountDD = walletamountDD, pendingamountDD = pendingamountDD });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("deleteExcludedstudent/{id}")]
        [HttpDelete]
        public async Task<ActionResult> deleteExcludedstudent(int id)
        {
            string api = "deleteExcludedstudent/{id}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


                mz_on_off_modules module = _context.mz_on_off_modules.Where(x => x.id == 1).FirstOrDefault();

                if (module.status == false)
                {
                    return BadRequest(new { message = "E-learning data fetching is currently OFF" });
                }

                mz_student_fee_excluding_list m = _context.mz_student_fee_excluding_list.Where(x => x.id == id).FirstOrDefault();

                _context.mz_student_fee_excluding_list.Remove(m);
                _context.SaveChanges();


                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getStudentProfileData")]
        [HttpGet]
        public async Task<ActionResult> getStudentProfileData()
        {
            string api = "api/student/getStudentProfileData";

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            mz_student s = _context.mz_student.Where(x => x.itsID == authUser.ItsId).FirstOrDefault();

            nisaab_classes c = _context.nisaab_classes.Where(x => x.id == s.classId).FirstOrDefault();
            string psetName = "";
            if (s.psetId != null)
            {
                registrationform_dropdown_set pset = _context.registrationform_dropdown_set.Where(x => x.id == s.psetId).FirstOrDefault();
                registrationform_programs p = _context.registrationform_programs.Where(x => x.id == pset.programId).FirstOrDefault();
                registrationform_subprograms sp = _context.registrationform_subprograms.Where(x => x.id == pset.subprogramId).FirstOrDefault();
                venue v = _context.venue.Where(x => x.Id == pset.venueId).FirstOrDefault();

                psetName = p.name + "_" + sp.name + "_" + v.displayName;

            }


            string className = "";
            if (c != null)
            {
                className = c.className;
            }

            var sModel = new StudentModel
            {
                age = s.age.ToString(),
                address = s.address,
                bloodgroup = s.bloodGroup,
                dob_Eng = s.dobGregorian,
                dob_Hijri = s.dobHijri,
                fatherEmailAddress = s.fatherEmail,
                fatherMobileNo = s.fatherMobile,
                fatherwhatsappNo = s.fatherWhatsapp,
                gender = s.gender,
                itsId = s.itsID ?? 0,
                name = s.nameEng,
                jamaat = s.jamaat,
                jamiat = s.jamiat,
                motherEmailAddress = s.motherEmail,
                motherMobileNo = s.motherMobile,
                motherwhatsappNo = s.motherWhatsapp,
                nationality = s.nationality,
                studentEmailAddress = s.studentEmail,
                mz_id = s.mz_id,
                name_arabic = s.nameArabic,
                studentMobileNo = s.studentMobile,
                trNo = s.trNo?.ToString(),
                className = className,
                watan = s.vatan,
                programName = psetName,
                psetId = s.psetId ?? 0,


            };
            return Ok(sModel);

        }


        [Route("get/nisaabalumni")]
        [HttpGet]
        public async Task<ActionResult> getNisaabAlumni()
        {
            string api = "api/get/nisaabalumni";
            // Add_ApiLogs(api);
            List<NisaabStudentAlumniModel> alumniList = new List<NisaabStudentAlumniModel>();

            int sr = 1;
            List<nisaab_alumni> alumniStudents = _context.nisaab_alumni.ToList();
            alumniStudents = alumniStudents.OrderBy(x => x.batchId).ThenByDescending(x => x.jamea).ToList();
            foreach (nisaab_alumni alum in alumniStudents)
            {
                NisaabStudentAlumniModel alumni = new NisaabStudentAlumniModel();
                mz_student? stu = _context.mz_student.Where(x => x.itsID == alum.itsId).FirstOrDefault();
                List<nisaab_student_logs> stulogs = _context.nisaab_student_logs.Where(x => x.itsId == alum.itsId).ToList();
                stulogs = stulogs.OrderBy(x => x.academicYear).ToList();
                if (alum == null || stu == null)
                {
                    continue;
                }
                alumni.itsId = alum.itsId;
                alumni.isJameaStudent = alum.jamea == true ? "Yes" : "No";
                alumni.hifzStatus = stu?.hifzStatus ?? "N/A";
                alumni.farigDarajah = alum.farigDarajah ?? 0;
                alumni.farigYear = alum.farigYear ?? 0;
                alumni.admissionYear = stulogs.FirstOrDefault()?.academicYear ?? 0;
                alumni.admissionDarajah = stulogs.FirstOrDefault()?.courseName ?? "N/A";
                alumni.farigSanad = alum.degree;
                alumni.sanadNum = alum.degreeNum;
                alumni.name = stu?.nameEng ?? "N/A";
                alumni.idara = stu?.idara ?? "N/A";
                alumni.mobileno = stu?.studentMobile ?? "N/A";
                alumni.whatsappno = stu?.studentWhatsapp ?? "N/A";
                alumni.email = stu?.studentEmail ?? "N/A";
                alumni.jamaat = stu?.jamaat ?? "N/A";
                alumni.jamiat = stu?.jamiat ?? "N/A";
                alumni.nisabId = stu?.trNo ?? 0;
                alumni.age = stu?.age ?? 0;
                alumni.batchId = alum.batchId ?? 0;
                alumni.hifzYear = stu?.hifzSanadYear ?? 0;
                alumni.hafizAtFarig = alum.hafizAtFarig;
                if (alum.its != null)
                {
                    alumni.mzcategory = alum.its.idara;
                }
                else
                {
                    alumni.mzcategory = "Not Applicable";
                }
                alumniList.Add(alumni);
            }

            return Ok(alumniList);
        }

    }

    public class alumniShahadatExport
    {
        public int itsId { get; set; }
        public string degree { get; set; }
        public string branch { get; set; }
        public string institution { get; set; }
        public int? farigYear { get; set; }
        public int? farigDarajah { get; set; }

    }
    public class StudentExtensionModel
    {
        public long id { get; set; }
        public int ItsId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }

    public class NisaabStudentAlumniModel
    {
        private readonly mzdbContext _context;

        public NisaabStudentAlumniModel()
        {
        }
        public NisaabStudentAlumniModel(mzdbContext context)
        {
            _context = context;
        }
        public int sr { get; set; }
        public int itsId { get; set; }
        public string name { get; set; }
        public int admissionYear { get; set; }
        public string admissionDarajah { get; set; }
        public int farigYear { get; set; }
        public int age { get; set; }
        public int farigDarajah { get; set; }
        public int nisabId { get; set; }
        public int batchId { get; set; }
        public string hifzStatus { get; set; }
        public int hifzYear { get; set; }
        public string isJameaStudent { get; set; }
        public string farigSanad { get; set; }
        public string sanadNum { get; set; }
        public string idara { get; set; }
        public string mobileno { get; set; }
        public string jamaat { get; set; }
        public string jamiat { get; set; }
        public string whatsappno { get; set; }
        public string email { get; set; }
        public bool hafizAtFarig { get; set; }
        public string mzcategory { get; set; }

        public NisaabStudentAlumniModel toModel(int itsId)
        {
            NisaabStudentAlumniModel alumni = new NisaabStudentAlumniModel();

            nisaab_alumni alum = _context.nisaab_alumni.Where(x => x.itsId == itsId).FirstOrDefault();
            mz_student stu = _context.mz_student.Where(x => x.itsID == itsId).FirstOrDefault();
            List<nisaab_student_logs> stulogs = _context.nisaab_student_logs.Where(x => x.itsId == itsId).ToList();
            stulogs = stulogs.OrderBy(x => x.academicYear).ToList();
            if (alum == null || stu == null)
            {
                return null;
            }
            alumni.itsId = alum.itsId;
            alumni.isJameaStudent = alum.jamea == true ? "Yes" : "No";
            alumni.hifzStatus = stu.hifzStatus ?? "N/A";
            alumni.farigDarajah = alum.farigDarajah ?? 0;
            alumni.farigYear = alum.farigYear ?? 0;
            alumni.admissionYear = stulogs.FirstOrDefault()?.academicYear ?? 0;
            alumni.admissionDarajah = stulogs.FirstOrDefault()?.courseName ?? "N/A";
            alumni.farigSanad = alum.degree;
            alumni.sanadNum = alum.degreeNum;
            alumni.name = stu.nameEng;
            alumni.idara = stu.idara;
            alumni.mobileno = stu.studentMobile;
            alumni.whatsappno = stu.studentWhatsapp;
            alumni.email = stu.studentEmail;
            alumni.jamaat = stu.jamaat;
            alumni.jamiat = stu.jamiat;
            alumni.nisabId = stu.trNo ?? 0;
            alumni.age = stu.age ?? 0;
            alumni.batchId = alum.batchId ?? 0;
            alumni.hifzYear = stu.hifzSanadYear ?? 0;
            alumni.hafizAtFarig = alum.hafizAtFarig;
            if (alum.its != null)
            {
                alumni.mzcategory = alum.its.idara;
            }
            else
            {
                alumni.mzcategory = "Not Applicable";
            }

            return alumni;

        }
    }

}
