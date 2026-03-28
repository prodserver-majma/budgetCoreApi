using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace mahadalzahrawebapi.Controllers
{
    public class searchStruct
    {
        public string itsIdCsv { get; set; }
    }

    public class studentsToBeTagged
    {
        public List<int> studentsToTag { get; set; }
        public int teacherIts { get; set; }
    }

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MaqaraatController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly HelperService _helperService;
        private readonly HijriCalenderService _hijriCalenderService;
        private NotificationService _notificationService;
        private readonly WhatsAppApiService _whatsAppApiService;
        private readonly string adminEmails = "admin@mahadalzahra.com, juzerdiwan@jameasaifiyah.edu";
        private globalConstants _globalConstants;

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        public MaqaraatController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _helperService = new HelperService(context);
            _notificationService = new NotificationService();
            _globalConstants = new globalConstants();
            _whatsAppApiService = new WhatsAppApiService(context);
            _hijriCalenderService = new HijriCalenderService(context);
        }

        //private static readonly string WafdAlHuffaz = "WafdAlHuffaz";
        //private static readonly string MahadAlZahra_KHDGZ = "MAHADALZAHRA KHDGZ";

        [Route("getmaqaraatsessionstudents/{id}")]
        [HttpGet]
        public async Task<IActionResult> getMaqaraatSessionStudents(int id)
        {
            string api = "getmaqaraatsessionstudents/{id}";
            //Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                //InshaController ctrl = new InshaController();
                List<MaqaraatModel> model = new List<MaqaraatModel>();

                List<wafdprofile_maqaraat_data> students = _context.wafdprofile_maqaraat_data.Where(x => x.sessionId == id).Include(x => x.session).ToList();
                List<int> stuIds = students.Select(x => x.studentItsId).ToList();

                if (students.Count == 0)
                {
                    return BadRequest();
                }
                List<khidmat_guzaar> kgs = _context.khidmat_guzaar.Where(x => stuIds.Contains(x.itsId))
                    .Include(x => x.employee_academic_details)
                    .Include(x => x.mauzeNavigation).ToList();
                List<nisaab_classes> nisaab_Classes = _context.nisaab_classes.ToList();
                int c = 0;
                foreach (var i in students)
                {
                    khidmat_guzaar kg = kgs.Where(x => x.itsId == i.studentItsId).FirstOrDefault();
                    //KhidmatGuzarModel h = Translater.khtoModel(kg);
                    int darajah = 0;
                    try
                    {
                        // darajah = ctrl.getWafdCurrentClass(h.FariqYear, h.Daras_darajah);
                        if (kg.employee_academic_details.wafdClassId != null)
                        {
                            darajah = nisaab_Classes.Where(x => x.id == kg.employee_academic_details.wafdClassId).FirstOrDefault().std ?? 0;

                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    venue v = kg?.mauzeNavigation;

                    c = c + 1;
                    MaqaraatModel m = new MaqaraatModel
                    {
                        srNo = c,
                        id = i.id,
                        marks = i.marks,
                        name = kg.fullName,
                        itsId = kg.itsId,
                        whatsappNumber = kg.c_codeWhatsapp + " " + kg.whatsappNo,
                        darajah = darajah.ToString(),
                        moze = v?.displayName ?? "N/A",
                        isPresent = (i.session?.isEvaluated ?? false) ? (i.isPresent ?? true) : true,
                        absentReason = i.absentReason,
                        batchId = kg.employee_academic_details.batchId ?? 0,
                    };

                    model.Add(m);
                }
                return Ok(model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getstudentsofteacher/{masoolItsId}")]
        [HttpGet]
        public async Task<IActionResult> getStudentsOfMasool(int masoolItsId)
        {
            string api = "getstudentsofteacher/{masoolItsId}";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            // ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {

                wafdprofile_maqaraat_teacher m = _context.wafdprofile_maqaraat_teacher.Where(x => x.itsId == masoolItsId).FirstOrDefault();

                if (m == null)
                {
                    throw new Exception("No Masool Found");
                }

                List<khidmat_guzaar> s = _context.khidmat_guzaar.Where(x => x.employee_academic_details.maqaraatTeacherIts == masoolItsId).Include(x => x.mauzeNavigation).Include(x => x.employee_academic_details).ToList();

                List<Wafd_Training_StudentModel> studentsModel = new List<Wafd_Training_StudentModel>();


                foreach (var i in s)
                {
                    string dptvenueName = "";
                    if (i.mauzeNavigation != null)
                    {
                        dptvenueName = i.mauzeNavigation.displayName;
                    }
                    Wafd_Training_StudentModel stud = new Wafd_Training_StudentModel
                    {
                        itsId = i.itsId,
                        name = i.fullName,
                        deptVenueName = dptvenueName,
                        isMasoolStudent = true,
                        masoolItsId = masoolItsId,
                        email = i.officialEmailAddress != null ? i.officialEmailAddress : i.emailAddress,
                        mobile = i.mobileNo,
                        category = i.mz_idara,
                        batchId = i.employee_academic_details.batchId ?? 0,
                        hifzYear = i.employee_academic_details.hifzSanadYear ?? 0
                    };

                    studentsModel.Add(stud);
                }

                // studentsModel.First().mushrifItsId = s.First().wafd_trainingMushrif_ItsId ?? 0;

                studentsModel = studentsModel.OrderByDescending(x => x.batchId).ToList();
                return Ok(studentsModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("getallteacherdata/{qismId}")]
        [HttpGet]
        public async Task<IActionResult> getAllMaqaraatTeacherData(int qismId)
        {
            string api = "getallteacherdata";
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                int academicYear = _helperService.getAcedemicYear(DateTime.Now).acedemicYear;

                List<khidmat_guzaar> masools = new List<khidmat_guzaar>();
                List<MaqaraatTeacher> masoolsModel_active = new List<MaqaraatTeacher>();

                List<khidmat_guzaar> allKg = _context.khidmat_guzaar.Include(x => x.employee_academic_details).ToList();

                //List<dept_venue> deptVenues = _context.dept_venue.Where(x => x.qismId == qismId).ToList();
                List<venue> venues = _context.venue.Where(x => x.qismId == qismId)
                    .Include(x => x.khidmat_guzaar).ThenInclude(x => x.employee_academic_details)
                    .Include(x => x.khidmat_guzaar).ThenInclude(x => x.wafdprofile_maqaraat_teacher)
                    .Include(x => x.khidmat_guzaar).ThenInclude(x => x.wafdprofile_maqaraat_session.Where(y => y.acedemicYear == academicYear)).ThenInclude(x => x.wafdprofile_maqaraat_data)
                    .ToList();

                List<employee_academic_details> s = new List<employee_academic_details>();

                venues.ForEach(x =>
                {
                    x.khidmat_guzaar.ToList().ForEach(y =>
                    {
                        khidmat_guzaar masool = x.khidmat_guzaar.Where(z => z.itsId == (y.wafdprofile_maqaraat_teacher.FirstOrDefault()?.itsId ?? 0)).FirstOrDefault();
                        if (masool != null)
                        {
                            if (!masools.Any(z => z == masool))
                            {
                                MaqaraatTeacher teacher = new MaqaraatTeacher()
                                {
                                    itsId = masool.itsId,
                                    name = masool.fullName,
                                    moze = x.displayName,
                                    category = masool.mz_idara,
                                    batchId = masool.batchId ?? 0
                                };
                                teacher.teacherSession = new List<MaqaraatSessions>();

                                List<wafdprofile_maqaraat_session> sessions = y.wafdprofile_maqaraat_session.Where(m => m.acedemicYear == academicYear).ToList();
                                List<MaqaraatSessions> sess = new List<MaqaraatSessions>();

                                sessions.ForEach(m =>
                                {
                                    MaqaraatSessions ses = new MaqaraatSessions
                                    {
                                        acdemicYear = m.acedemicYear ?? 0,
                                        createdBy = m.createdBy,
                                        date = m.sessionDate,
                                        day = m.sessionDate.DayOfWeek.ToString(),
                                        id = m.id,
                                        isEvaluated2 = m.isEvaluated?.ToString(),
                                        isEvaluated = (m.isEvaluated ?? true),
                                        juz = (m.juz ?? 0),
                                        pages = (m.pages ?? 0),
                                        reason = m.reason
                                    };
                                    ses.Students = new List<MaqaraatStudentMarks>();

                                    m.wafdprofile_maqaraat_data.ToList().ForEach(n =>
                                    {
                                        MaqaraatStudentMarks Ms = new MaqaraatStudentMarks
                                        {
                                            itsId = n.studentItsId,
                                            isPresent = n.isPresent ?? false,
                                            marks = n.marks ?? 0,
                                            name = n.studentIts.fullName,
                                            absentReason = n.absentReason,
                                            category = n.studentIts.mz_idara,
                                            batchId = n.studentIts.batchId ?? 0,
                                            hifzYear = n.studentIts.employee_academic_details.hifzSanadYear ?? 0,
                                        };
                                        ses.Students.Add(Ms);
                                    });
                                    ses.Students = ses.Students.OrderByDescending(n => n.batchId).ToList();
                                    teacher.teacherSession.Add(ses);
                                });

                                teacher.numOfSessions = sessions.Count();
                                List<khidmat_guzaar> students = allKg.Where(m => m.employee_academic_details?.maqaraatTeacherIts == masool.itsId).ToList();
                                teacher.numOfStudents = students.Count();
                                masools.Add(masool);
                                masoolsModel_active.Add(teacher);
                            }
                        }

                    });
                });
                masoolsModel_active = masoolsModel_active.OrderByDescending(x => x.batchId).ToList();
                return Ok(masoolsModel_active);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("addteacher")]
        [HttpPost]
        public async Task<IActionResult> AddMasool(Wafd_Training_MasoolModel masool)
        {
            string api = "addteacher";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            // ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);
            try
            {


                wafdprofile_maqaraat_teacher m = _context.wafdprofile_maqaraat_teacher.Where(x => x.itsId == masool.itsId).FirstOrDefault();

                if (m != null)
                {
                    return BadRequest(new { message = "Teacher already exists" });
                }
                else
                {
                    khidmat_guzaar w = _context.khidmat_guzaar.Where(x => x.itsId == masool.itsId && x.activeStatus == true).Include(x => x.mauzeNavigation).FirstOrDefault();

                    if (w == null)
                    {
                        return BadRequest(new { message = "Employee deos not exist" });
                    }

                    wafdprofile_maqaraat_teacher m1 = new wafdprofile_maqaraat_teacher
                    {
                        itsId = masool.itsId,
                        createdBy = authUser.Name,
                        createdOn = indianTime
                    };

                    _context.wafdprofile_maqaraat_teacher.Add(m1);
                    _context.SaveChanges();

                    try
                    {
                        string mauze = w.mauzeNavigation?.displayName;
                        String msg2 = "Salam jameel,\n" + w.fullName + "\n" + w.itsId + "\n" + mauze + "\n\nPlease be informed that you have been tagged as a *maqraat group teacher* as on " + DateTime.Now.ToString("U") + " (IST)\nFor more details, kindly visit www.mahadalzahra.org > HR login > Side menu bar > Academics > *Maqraat > Tab Add / Edit Session*\nTo add/edit a session, instructions are given on the module.\n\nIf you are unable to view your group participants, may be they are yet te be assigned by your branch masul.\nKindly contact him if no participants are assigned in some days.\n\nFor any technical assistance, contact itsupport@mahadalzahra.com \nFor any query, contact admin@mahadalzahra.com\n\nShukran,\nWassalaam.";
                        //String msg = "testing";
                        string emailbody = @"<b>" + w.fullName + @"</b>
                                    <br/> <b > " + w.itsId + @" </b>
                                    <br/><b > " + mauze + @" </b>
                                    <br/>
                                    <br/>
                                    Please be informed that you have been tagged as a <b>maqraat group teacher</b> as on " + DateTime.Now.ToString("U") + @"(IST)<br/>
                                    For more details, kindly visit www.mahadalzahra.org > HR login > Side menu bar > Academics > <b>Maqraat > Tab Add / Edit Session</b><br/>
                                    To add/edit a session, instructions are given on the module.<br/>
                                    <br/>If you are unable to view your group participants, may be they are yet te be assigned by your branch masul.<br/>
                                    Kindly contact him if no participants are assigned in some days<br/>
                                    <br/>For any technical assistance, contact itsupport@mahadalzahra.com <br/>For any query, contact admin@mahadalzahra.com";
                        string emailto = w.officialEmailAddress != null ? w.officialEmailAddress : w.emailAddress;
                        _notificationService.SendStandardHTMLEmail("Maqraat group assigned", emailbody, emailto, "no-reply");

                        List<string> num = new List<string> { w.c_codeWhatsapp + w.whatsappNo };
                        if (w.whatsappNo != null)
                        {
                            _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2, "Maqaraat Module Notification");
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex);
                    }
                }



                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("getstudentstobetagged")]
        [HttpPost]
        public async Task<IActionResult> getEmployeeFromIts(searchStruct search)
        {
            string api = "getstudentstobetagged";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            // ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            string itsIdCSV = search.itsIdCsv;
            List<int> itsIds = _helperService.parseItsId(itsIdCSV);
            try
            {

                List<khidmat_guzaar> kgs = _context.khidmat_guzaar.Where(x => itsIds.Any(y => y == x.itsId))
                            .Include(its => its.employee_academic_details)
                            .Include(its => its.mauzeNavigation)
                            .Include(its => its.employee_bank_details)
                            .Include(its => its.employee_dept_salary)
                            .ThenInclude(x => x.salaryType)
                            .Include(its => its.employee_dept_salary)
                            .ThenInclude(x => x.deptVenue)
                            .Include(its => its.employee_e_attendence)
                            .Include(its => its.employee_khidmat_details)
                            .Include(its => its.employee_passport_details)
                            .Include(its => its.employee_salary)
                            .ToList();


                List<EmployeeModel> kg = new List<EmployeeModel>();

                if (kgs.Count > 0)
                {
                    foreach (khidmat_guzaar khimatguraz in kgs)
                    {
                        if (!(khimatguraz.mauzeNavigation.qismId == authUser.qismId) || khimatguraz.activeStatus == false)
                        {
                            continue;
                        }
                        EmployeeModel kgmodel = Translator.khtoModel(khimatguraz);
                        kgmodel.deptSalaries.ForEach(x =>
                        {
                            x.dept_venue = new dept_venue_dto
                            {
                                deptName = x.dept_venue.deptName + "_" + x.dept_venue.venueName,
                                venueName = x.dept_venue.venueName
                            };
                            x.salary_Type = new salary_type_dto
                            {
                                id = x.salary_Type.id,
                                Name = x.salary_Type.Name
                            };
                        });

                        kgmodel.select = khimatguraz.employee_academic_details.maqaraatTeacherIts != null;

                        kg.Add(kgmodel);
                    }

                }
                return Ok(kg);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("tagteacherStudent")]
        [HttpPost]
        public async Task<IActionResult> getEmployeeFromIts(studentsToBeTagged model)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string api = "tagteacherStudent";
            // ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            //string itsIdCSV = search.itsIdCsv;
            List<int> itsIds = model.studentsToTag;
            try
            {

                List<khidmat_guzaar> kgs = _context.khidmat_guzaar.Where(x => itsIds.Any(y => y == x.itsId)).Include(x => x.mauzeNavigation).Include(x => x.employee_academic_details).ToList();
                khidmat_guzaar masool = _context.khidmat_guzaar.Where(x => x.itsId == model.teacherIts).FirstOrDefault();
                if (masool == null)
                {
                    return BadRequest(new { message = "Teacher not found" });
                }

                if (kgs.Count > 0)
                {
                    try
                    {

                        foreach (khidmat_guzaar khimatguraz in kgs)
                        {

                            khimatguraz.employee_academic_details.maqaraatTeacherIts = masool.itsId;

                            string mauze = khimatguraz.mauzeNavigation.displayName;


                            String msg2 = "Salam jameel,\n" + khimatguraz.fullName + "\n" + khimatguraz.itsId + "\n" + mauze + "\n\nPlease be informed that you have been tagged with your maqraat teacher " + masool.fullName + " as on " + DateTime.Now.ToString("U") + " (IST)\nFor more details, kindly visit www.mahadalzahra.org > HR login > Side menu bar > Academics > Maqraat.\n\nShukran,\nWassalaam.";
                            //String msg = "testing";
                            string emailbody = @"<b>" + khimatguraz.fullName + @"</b>
                                    <br /> <b> " + khimatguraz.itsId + @" </b>
                                    <br/><b> " + mauze + @" </b>
                                    <br/>
                                    <br/>
                                    Please be informed that you have been tagged with your maqraat teacher " + masool.fullName + @" as on " + DateTime.Now.ToString("U") + @" (IST)<br/>
                                    For more details, kindly visit www.mahadalzahra.org > HR login > Side menu bar > Academics > Maqraat.";
                            string emailto = khimatguraz.officialEmailAddress != null ? khimatguraz.officialEmailAddress : khimatguraz.emailAddress;
                            _notificationService.SendStandardHTMLEmail("Maqraat", emailbody, emailto, "no-reply");
                            List<string> num = new List<string> { khimatguraz.c_codeWhatsapp + khimatguraz.whatsappNo };
                            if (khimatguraz.whatsappNo != null)
                            {
                                _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2, "Maqaraat Module Notification");
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }

                    _context.SaveChanges();

                }
                return Ok();


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("addstudentsinteacher")]
        [HttpPost]
        public async Task<IActionResult> AddStudentsInMasool(List<Wafd_Training_StudentModel> students)
        {
            string api = "addstudentsinteacher";

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            // ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);
            try
            {
                int masoolItsId = students.First().masoolItsId;

                List<khidmat_guzaar> wtoDelete = (from ead in _context.employee_academic_details
                                                  where ead.maqaraatTeacherIts == masoolItsId
                                                  join kh in _context.khidmat_guzaar on ead.itsId equals kh.itsId
                                                  select kh).ToList();

                foreach (var i in wtoDelete)
                {
                    i.employee_academic_details.maqaraatTeacherIts = null;
                }


                List<Wafd_Training_StudentModel> students2 = students.Where(x => x.isMasoolStudent == true).ToList();

                foreach (var i in students2)
                {
                    //khidmat_guzaar w = _context.khidmat_guzaar.Where(x => x.itsId == i.itsId && x.employeeType == "Khidmatguzaar").FirstOrDefault();
                    EmployeeModel w = Translator.khtoModel((from kh in _context.khidmat_guzaar
                                                            where kh.itsId == i.itsId && kh.employeeType == "Khidmatguzaar"
                                                            select kh).FirstOrDefault());
                    //int darajah = inshaController.getWafdCurrentClass(w.FariqYear, w.Daras_darajah);

                    int darajah = 0;
                    if (w.academicDetails.wafdClassId != null)
                    {
                        darajah = _context.nisaab_classes.Where(x => x.id == w.academicDetails.wafdClassId).FirstOrDefault().std ?? 0;

                    }

                    if (darajah > 11)
                    {
                        return BadRequest(new { message = "darajah above 11 is not allowed" });
                    }

                }

                foreach (var i in students2)
                {


                    employee_academic_details w = _context.employee_academic_details.Where(x => x.itsId == i.itsId).FirstOrDefault();

                    w.maqaraatTeacherIts = masoolItsId;

                }
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "error" });
            }

        }

        [Route("deletemaqaraatteacher/{id}")]
        [HttpGet]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            string api = "deletmaqaaratTeacher";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            // ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);
            try
            {
                wafdprofile_maqaraat_teacher m = _context.wafdprofile_maqaraat_teacher.Where(x => x.itsId == id).FirstOrDefault();
                List<employee_academic_details> emp_acd = _context.employee_academic_details.Where(x => x.maqaraatTeacherIts == m.itsId).ToList();

                emp_acd.ForEach(x => x.maqaraatTeacherIts = null);

                _context.wafdprofile_maqaraat_teacher.Remove(m);
                _context.SaveChanges();


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "error" });
            }

        }

        [Route("untagmaqaraatstudent/{itsId}")]
        [HttpGet]
        public async Task<IActionResult> unTagMaqaraatStudent(int itsId)
        {
            string api = "unTagMaqaraatStudent";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            // ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);
            try
            {
                employee_academic_details m = _context.employee_academic_details.Where(x => x.itsId == itsId).FirstOrDefault();
                if (m != null)
                {
                    m.maqaraatTeacherIts = null;
                    _context.SaveChanges();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "error" });
            }

        }

        //[Route("getstudentsofteacher/{masoolItsId}")]
        //[HttpGet]
        //public async Task<IActionResult> getStudentsOfMasool(int masoolItsId)
        //{
        //    string api = "api/wafdtrainingmasool/getstudentsofmasool/{masoolItsId}";
        //    string token = _tokenService.ExtractTokenFromRequest(HttpContext);
        //    AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
        //    try
        //    {

        //            wafdprofile_maqaraat_teacher m = _context.wafdprofile_maqaraat_teacher.Where(x => x.itsId == masoolItsId).FirstOrDefault();

        //            if (m == null)
        //            {
        //                throw new Exception("No Masool Found");
        //            }

        //            List<khidmat_guzaar> s = (from ead in _context.employee_academic_details
        //                                      where ead.maqaraatTeacherIts == masoolItsId
        //                                      join kh in _context.khidmat_guzaar on ead.itsId equals kh.itsId
        //                                      where kh.activeStatus == true
        //                                      select kh).ToList();
        //            List<Wafd_Training_StudentModel> studentsModel = new List<Wafd_Training_StudentModel>();

        //            foreach (var i in s)
        //            {
        //                string dptvenueName = "";
        //                dept_venue d = _context.employee_dept_salary.Where(x => x.itsId == i.itsId).FirstOrDefault().deptVenue;
        //                if (d != null)
        //                {
        //                    dptvenueName = d.venueName;
        //                }
        //                Wafd_Training_StudentModel stud = new Wafd_Training_StudentModel
        //                {
        //                    itsId = i.itsId,
        //                    name = i.fullName,
        //                    deptVenueName = dptvenueName,
        //                    isMasoolStudent = true,
        //                    masoolItsId = masoolItsId,
        //                    email = i.officialEmailAddress != null ? i.officialEmailAddress : i.emailAddress,
        //                    mobile = i.mobileNo
        //                };

        //                studentsModel.Add(stud);

        //            }



        //            // studentsModel.First().mushrifItsId = s.First().wafd_trainingMushrif_ItsId ?? 0;

        //            return Ok(studentsModel);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.ToString());
        //    }
        //}

        [Route("getallteacherdata")]
        [HttpGet]
        public async Task<IActionResult> getAllMaqaraatTeacherData()
        {
            string api = "getallteacherdata";
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


                int academicYear = _hijriCalenderService.getAcedemicYear(DateTime.Now).acedemicYear;

                List<wafdprofile_maqaraat_teacher> masools = new List<wafdprofile_maqaraat_teacher>();
                List<MaqaraatTeacher> masoolsModel_active = new List<MaqaraatTeacher>();
                //List<MaqaraatTeacher> masoolsModel_active = new List<MaqaraatTeacher>();
                List<khidmat_guzaar> allKg = _context.khidmat_guzaar
                .Include(x => x.employee_academic_details).Where(x => x.employee_academic_details.maqaraatTeacherIts != null).ToList();

                List<dept_venue> deptVenues = _context.dept_venue
                .Include(x => x.venue)
                .Include(x => x.employee_dept_salary)
                .ToList();

                List<wafdprofile_maqaraat_teacher> teachers = _context.wafdprofile_maqaraat_teacher.Include(x => x.its).ToList();

                List<wafdprofile_maqaraat_session> allSessions = _context.wafdprofile_maqaraat_session.Include(x => x.wafdprofile_maqaraat_data).ThenInclude(x => x.studentIts).Where(m => m.acedemicYear == academicYear).ToList();

                List<employee_academic_details> s = new List<employee_academic_details>();

                foreach (dept_venue x in deptVenues)
                {
                    // deptVenues.ForEach(x =>
                    // {
                    if (x.employee_dept_salary.Count == 0)
                    {
                        continue;
                    }
                    foreach (employee_dept_salary y in x.employee_dept_salary)
                    {
                        // x.employee_dept_salary.ToList().ForEach(y =>
                        // {
                        wafdprofile_maqaraat_teacher masool = teachers.Where(z => z.itsId == y.itsId).FirstOrDefault();
                        if (masool != null)
                        {
                            if (!masools.Any(z => z == masool))
                            {
                                MaqaraatTeacher teacher = new MaqaraatTeacher()
                                {
                                    itsId = masool.itsId ?? 0,
                                    name = masool.its.fullName ?? "",
                                    moze = x.venue.displayName,
                                    category = masool.its.mz_idara,
                                    batchId = masool.its.batchId ?? 0
                                };
                                teacher.teacherSession = new List<MaqaraatSessions>();

                                List<wafdprofile_maqaraat_session> sessions = allSessions.Where(m => m.acedemicYear == academicYear && m.teacherItsId == y.itsId).ToList();
                                List<MaqaraatSessions> sess = new List<MaqaraatSessions>();

                                sessions.ForEach(m =>
                                {
                                    MaqaraatSessions ses = new MaqaraatSessions
                                    {
                                        acdemicYear = m.acedemicYear ?? 0,
                                        createdBy = m.createdBy,
                                        date = m.sessionDate,
                                        day = m.sessionDate.DayOfWeek.ToString(),
                                        id = m.id,
                                        isEvaluated2 = m.isEvaluated?.ToString(),
                                        isEvaluated = (m.isEvaluated ?? true),
                                        juz = (m.juz ?? 0),
                                        pages = (m.pages ?? 0),
                                        reason = m.reason
                                    };
                                    ses.Students = new List<MaqaraatStudentMarks>();

                                    m.wafdprofile_maqaraat_data.ToList().ForEach(n =>
                                    {
                                        MaqaraatStudentMarks Ms = new MaqaraatStudentMarks
                                        {
                                            itsId = n.studentItsId,
                                            isPresent = n.isPresent ?? false,
                                            marks = n.marks ?? 0,
                                            name = n.studentIts.fullName,
                                            absentReason = n.absentReason,
                                            category = n.studentIts.mz_idara
                                        };
                                        ses.Students.Add(Ms);
                                    });
                                    teacher.teacherSession.Add(ses);
                                });

                                teacher.numOfSessions = sessions.Count();
                                List<khidmat_guzaar> students = allKg.Where(m => m.employee_academic_details.maqaraatTeacherIts == masool.itsId).ToList();

                                teacher.numOfStudents = students.Count();
                                masools.Add(masool);
                                masoolsModel_active.Add(teacher);
                            }
                        }

                        // });
                    }
                    // });
                }
                masoolsModel_active = masoolsModel_active.OrderByDescending(x => x.batchId).ToList();
                return Ok(masoolsModel_active);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getmaqaraatsessions")]
        [HttpPost]
        public async Task<IActionResult> getMaqaraatSession(fetchMaqaraatSessionByDate fetchByModel)
        {
            string api = "getmaqaraatsessions";


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                MaqaraatModel model = new MaqaraatModel();
                //wafdprofile_maqaraat_teacher t = _context.wafdprofile_maqaraat_teacher.Where(x => x.itsId == authUser.ItsId).FirstOrDefault();
                khidmat_guzaar t = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId).Include(x => x.wafdprofile_maqaraat_session).FirstOrDefault();
                wafdprofile_maqaraat_session session = t.wafdprofile_maqaraat_session.Where(x => x.sessionDate == fetchByModel.sessionDate).FirstOrDefault();
                if (session == null)
                {
                    MaqaraatModel s = new MaqaraatModel()
                    {
                        date = new DateTime(fetchByModel.sessionDate.Year, fetchByModel.sessionDate.Month, fetchByModel.sessionDate.Day)
                    };
                    return Ok(s);
                }

                DateTime tempDate = DateTime.Today;
                MaqaraatModel m = new MaqaraatModel()
                {
                    id = session.id,
                    date = new DateTime(session.sessionDate.Year, session.sessionDate.Month, session.sessionDate.Day),
                    juz = session.juz ?? 0,
                    isEvaluated = session.isEvaluated.ToString(),
                    reason = session.reason,
                    acdemicYear = session.acedemicYear?.ToString(),
                    day = session.createdOn.Value.DayOfWeek.ToString(),
                    createdBy = session.createdBy,
                    isEvaluated2 = session.isEvaluated ?? false,
                    createdOn = session.createdOn ?? DateTime.Now,
                    pages = session.pages ?? 0
                };

                return Ok(m);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getmaqaraatsessionsData")]
        [HttpGet]
        public async Task<IActionResult> getMaqaraatSessionData()
        {
            string api = "getmaqaraatsessionsData";
            //Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<MaqaraatModel> model = new List<MaqaraatModel>();
                List<wafdprofile_maqaraat_session> sList = _context.wafdprofile_maqaraat_session.Where(x => x.teacherItsId == authUser.ItsId).ToList();
                List<DateTime> dates = new List<DateTime>();

                foreach (var i in sList)
                {
                    model.Add(new MaqaraatModel
                    {
                        isEvaluated2 = i.isEvaluated ?? false,
                        id = i.id,
                        date = new DateTime(i.sessionDate.Year, i.sessionDate.Month, i.sessionDate.Day),
                        juz = i.juz ?? 0,
                        isEvaluated = i.isEvaluated.ToString(),
                        reason = i.reason,
                        acdemicYear = i.acedemicYear?.ToString(),
                        day = i.sessionDate.DayOfWeek.ToString(),
                        createdBy = i.createdBy,
                        pages = i.pages ?? 0,
                    });
                }

                return Ok(model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getmaqaraatstudentData")]
        [HttpGet]
        public async Task<IActionResult> getMaqaraatstudentData()
        {
            string api = "getmaqaraatstudentData";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<MaqaraatModel> model = new List<MaqaraatModel>();
                List<wafdprofile_maqaraat_data> sList = _context.wafdprofile_maqaraat_data.Where(x => x.studentItsId == authUser.ItsId).ToList();
                List<DateTime> dates = new List<DateTime>();
                List<wafdprofile_maqaraat_session> sessions = _context.wafdprofile_maqaraat_session.ToList();

                foreach (var i in sList)
                {
                    wafdprofile_maqaraat_session s = sessions.Where(x => x.id == i.sessionId).FirstOrDefault();
                    model.Add(new MaqaraatModel
                    {
                        marks = i.marks,
                        id = s.id,
                        date = new DateTime(s.sessionDate.Year, s.sessionDate.Month, s.sessionDate.Day),
                        juz = s.juz ?? 0,
                        isEvaluated = s.isEvaluated.ToString(),
                        reason = s.reason,
                        acdemicYear = s.acedemicYear?.ToString(),
                        day = s.sessionDate.DayOfWeek.ToString(),
                        createdBy = s.createdBy,
                        isEvaluated2 = s.isEvaluated ?? false,
                        absentReason = i.absentReason,
                        isPresent = (i.isPresent ?? true),
                        pages = s.pages ?? 0,
                        createdOn = s.createdOn ?? DateTime.Now,
                        teacherItsId = s.teacherItsId
                    });
                }

                return Ok(model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getmaqaraatteacherstudents")]
        [HttpGet]
        public async Task<IActionResult> getMaqaraatStudents()
        {
            string api = "getmaqaraatteacherstudents";

            try
            {
                List<MaqaraatModel> model = new List<MaqaraatModel>();
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                //InshaController ctrl = new InshaController();
                List<khidmat_guzaar> huffaz = (from ead in _context.employee_academic_details
                                               where ead.maqaraatTeacherIts == authUser.ItsId
                                               join kh in _context.khidmat_guzaar on ead.itsId equals kh.itsId
                                               select kh).ToList();

                foreach (khidmat_guzaar i in huffaz)
                {

                    MaqaraatModel m = new MaqaraatModel
                    {
                        photo = i.photo,
                        itsId = i.itsId,
                        name = i.fullName,
                        mobileNumber = i.mobileNo,
                        whatsappNumber = i.whatsappNo,
                        batchId = i.batchId ?? 0,
                        email = i.officialEmailAddress != null ? i.officialEmailAddress : i.emailAddress
                    };

                    model.Add(m);
                }


                return Ok(model);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        //[Route("getmaqaraatsessionstudents/{id}")]
        //[HttpGet]
        //public async Task<IActionResult> getMaqaraatSessionStudents(int id)
        //{
        //    string api = "getmaqaraatsessionstudents/{id}";

        //    try
        //    {
        //        string token = _tokenService.ExtractTokenFromRequest(HttpContext);
        //        AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

        //        //InshaController ctrl = new InshaController();
        //        List<MaqaraatModel> model = new List<MaqaraatModel>();

        //            List<wafdprofile_maqaraat_data> students = _context.wafdprofile_maqaraat_data.Where(x => x.sessionId == id).ToList();

        //            if (students.Count == 0)
        //            {
        //                return BadRequest( new { message = "no students found");
        //            }

        //            int c = 0;
        //            foreach (var i in students)
        //            {
        //                khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == i.studentItsId).FirstOrDefault();
        //                EmployeeModel h = _mapper.Map<EmployeeModel>(kg);
        //                int darajah = 0;
        //                try
        //                {
        //                    // darajah = ctrl.getWafdCurrentClass(h.FariqYear, h.Daras_darajah);
        //                    if (h.academicDetails.wafdClassId != null)
        //                    {
        //                        darajah = _context.nisaab_classes.Where(x => x.id == h.academicDetails.wafdClassId).FirstOrDefault().std ?? 0;

        //                    }
        //                }
        //                catch (Exception ex)
        //                {

        //                }

        //                venue v = kg.mauzeNavigation ?? new venue();

        //                c = c + 1;
        //                MaqaraatModel m = new MaqaraatModel
        //                {
        //                    srNo = c,
        //                    id = i.id,
        //                    marks = i.marks,
        //                    name = h.basicDetails.fullName,
        //                    itsId = h.basicDetails.itsId,
        //                    whatsappNumber = h.basicDetails.whatsappNo,
        //                    darajah = darajah.ToString(),
        //                    moze = v?.displayName,
        //                    isPresent = (i.session.isEvaluated ?? false) ? (i.isPresent ?? true) : true,
        //                    absentReason = i.absentReason
        //                };

        //                model.Add(m);
        //            }
        //            return Ok(model);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.ToString());
        //    }
        //}

        //[Route("addteacher")]
        //[HttpPost]
        //public async Task<IActionResult> AddMasool(Wafd_Training_MasoolModel masool)
        //{
        //    string api = "addteacher";


        //    string token = _tokenService.ExtractTokenFromRequest(HttpContext);
        //    AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
        //    try
        //    {
        //        wafdprofile_maqaraat_teacher m = _context.wafdprofile_maqaraat_teacher.Where(x => x.itsId == masool.itsId).FirstOrDefault();

        //            if (m != null)
        //            {
        //                return BadRequest( new { message = "teacher Already exist");
        //            }
        //            else
        //            {
        //                khidmat_guzaar w = _context.khidmat_guzaar.Where(x => x.itsId == masool.itsId && x.activeStatus == true && x.employeeType == "Khidmatguzaar").FirstOrDefault();

        //                if (w == null)
        //                {
        //                    return BadRequest( new { message = "Kgz deos not exist");
        //                }

        //                wafdprofile_maqaraat_teacher m1 = new wafdprofile_maqaraat_teacher { itsId = masool.itsId, createdBy = authUser.Name, createdOn = indianTime };

        //                _context.wafdprofile_maqaraat_teacher.Add(m1);
        //                _context.SaveChanges();

        //            }



        //        return Ok("succesfully Allocated");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest( new { message = "error");
        //    }

        //}


        //[Route("addstudentsinteacher")]
        //[HttpPost]
        //public async Task<IActionResult> AddStudentsInMasool(List<Wafd_Training_StudentModel> students)
        //{
        //    string api = "addstudentsinteacher";

        //    string token = _tokenService.ExtractTokenFromRequest(HttpContext);
        //    AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
        //    try
        //    {

        //            int masoolItsId = students.First().masoolItsId;

        //            List<khidmat_guzaar> wtoDelete = (from ead in _context.employee_academic_details
        //                                              where ead.maqaraatTeacherIts == masoolItsId
        //                                              join kh in _context.khidmat_guzaar on ead.itsId equals kh.itsId
        //                                              select kh).ToList();

        //            foreach (var i in wtoDelete)
        //            {
        //                i.employee_academic_details.maqaraatTeacherIts = null;
        //            }


        //            List<Wafd_Training_StudentModel> students2 = students.Where(x => x.isMasoolStudent == true).ToList();

        //            foreach (var i in students2)
        //            {
        //                khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == i.itsId && x.employeeType == "Khidmatguzaar").FirstOrDefault();
        //                EmployeeModel w = _mapper.Map<EmployeeModel>(kg);
        //                //int darajah = inshaController.getWafdCurrentClass(w.FariqYear, w.Daras_darajah);

        //                int darajah = 0;
        //                if (w.academicDetails.wafdClassId != null)
        //                {
        //                    darajah = _context.nisaab_classes.Where(x => x.id == w.academicDetails.wafdClassId).FirstOrDefault().std ?? 0;

        //                }

        //                if (darajah > 11)
        //                {
        //                    return BadRequest( new { message = "darajah above 11 is not allowed");
        //                }

        //            }

        //            foreach (var i in students2)
        //            {


        //                employee_academic_details w = _context.employee_academic_details.Where(x => x.itsId == i.itsId).FirstOrDefault();

        //                w.maqaraatTeacherIts = masoolItsId;

        //            }
        //            _context.SaveChanges();

        //        return Ok("succesfully Submitted");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest( new { message = "error");
        //    }

        //}

        [Route("addsessionentry")]
        [HttpPost]
        public async Task<IActionResult> AddSessionEntry(MaqaraatModel model)
        {
            string api = "addsessionentry";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            try
            {

                wafdprofile_maqaraat_session session = _context.wafdprofile_maqaraat_session.Where(x => x.id == model.id).FirstOrDefault();
                int academicYear = _hijriCalenderService.getAcedemicYear(model.date).acedemicYear;

                if (session == null)
                {
                    session = new wafdprofile_maqaraat_session()
                    {

                        sessionDate = DateOnly.FromDateTime(model.date),
                        juz = model.juz,
                        acedemicYear = academicYear,
                        createdBy = authUser.Name,
                        createdOn = DateTime.Now,
                        isEvaluated = false,
                        teacherItsId = model.itsId == 0 ? authUser.ItsId : model.itsId,
                        pages = model.pages
                    };

                    List<employee_academic_details> students = _context.employee_academic_details.Where(x => x.maqaraatTeacherIts == session.teacherItsId).ToList();
                    students.ForEach(x =>
                    {
                        session.wafdprofile_maqaraat_data.Add(new wafdprofile_maqaraat_data
                        {
                            isPresent = true,
                            studentItsId = x.itsId
                        });
                    });

                    _context.wafdprofile_maqaraat_session.Add(session);
                    _context.SaveChanges();
                    return Ok(session.id);
                }

                session.pages = model.pages;
                session.juz = model.juz;
                _context.SaveChanges();
                return Ok(session.id);


            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "error" });
            }

        }

        [Route("submitstudentsmarksinbulk")]
        [HttpPost]
        public async Task<IActionResult> SubmitStudentsMarks_bulk(List<MaqaraatModel> models)
        {
            string api = "submitstudentsmarksinbulk";
            //Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                List<int> ints = models.Select(x => x.id).ToList();
                List<wafdprofile_maqaraat_data> data = _context.wafdprofile_maqaraat_data
                    .Where(x => ints.Contains(x.id))
                    .Include(x => x.studentIts)
                    .Include(x => x.session)
                    .ThenInclude(x => x.teacherIts)
                    .ThenInclude(x => x.mauzeNavigation)
                    .ThenInclude(x => x.qism)
                    .ThenInclude(x => x.its)
                    .ThenInclude(x => x.its)
                    .Include(x => x.studentIts)
                    .ToList();

                foreach (var model in models)
                {

                    if (model.marks > 10)
                    {
                        return BadRequest(new { message = "Kindly enter marks out of 10!" });
                    }
                }

                wafdprofile_maqaraat_session session = data.Where(x => models.Any(y => y.id == x.id)).FirstOrDefault().session;
                session.isEvaluated = true;

                foreach (var model in models)
                {
                    wafdprofile_maqaraat_data d = data.Where(x => x.id == model.id).FirstOrDefault();

                    d.marks = model.isPresent ? model.marks : null;
                    d.isPresent = model.isPresent;
                    d.absentReason = model.isPresent ? null : model.absentReason;

                    _context.SaveChanges();

                    string formatedDate = d.session.sessionDate.ToString("dd/MM/yyyy");
                    String msg2 = "Salam jameel,\n*" + d.studentIts.fullName + "* \n\nPlease be informed that your maqraat teacher - " + d.session.teacherIts.fullName + " has submitted a new session on the module with below details.\n\nDate: " + formatedDate + "\nJuz: *" + d.session.juz + "* \nPages: *" + d.session.pages + "*\nTotal participants: " + models.Count() + "\nPresent participants: " + models.Where(x => x.isPresent).Count() + "\nAbsent participants: " + models.Where(x => !x.isPresent).Count() + "\n\nFor details, kindly visit www.mahadalzahra.org > Faculty login > Side menu bar > Academic > Maqraat > Maqraat history.\n\nShukran,\nWa al-Salaam.";
                    //String msg = "testing";
                    string emailbody = @"<b>" + d.studentIts.fullName + @"</b>
                                    <br />
                                    <br />
                                    Please be informed that your maqraat teacher - <b>" + d.session.teacherIts.fullName + @"</b>  has submitted a new session on the module with below details.
                                    <br />
                                    <br />
                                    Date: " + formatedDate + @"
                                    <br />
                                    Juz: " + d.session.juz + @"
                                    <br />
                                    Pages: " + d.session.pages + @"
                                    <br />
                                    Total Participants: " + models.Count() + @"
                                    <br />
                                    Present Participants: " + models.Where(x => x.isPresent).Count() + @"
                                    <br />
                                    Absent Participants: " + models.Where(x => !x.isPresent).Count() + @"
                                    <br />
                                    <br />
                                     For details, kindly visit www.mahadalzahra.org > Faculty login > Side menu bar > Academic > Maqraat > Maqraat history.";

                    string emailto = d.studentIts.officialEmailAddress != null ? d.studentIts.officialEmailAddress : d.studentIts.emailAddress;

                    try
                    {
                        _notificationService.SendStandardHTMLEmail("Maqraat - New session added - Participants", emailbody, emailto, "no-reply");
                    }
                    catch (Exception e) { }

                    List<string> num = new List<string> { d.studentIts.c_codeWhatsapp + d.studentIts.whatsappNo };
                    if (d.studentIts.whatsappNo != null)
                    {
                        try
                        {
                            _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2, "Maqaraat Module Notification");
                        }
                        catch (Exception e)
                        { }
                    }

                }
                khidmat_guzaar masool = session.teacherIts.mauzeNavigation.qism?.its?.its;

                if (masool != null)
                {
                    string formatedDate = session.sessionDate.ToString("dd/MM/yyyy");
                    String msg2 = "Salam jameel,\n*" + masool.fullName + "* \n\nPlease be informed that " + session.teacherIts.fullName + " has submitted a new maqra'at session on the module with below details.\n\nDate: " + formatedDate + "\nJuz: *" + session.juz + "* \nPages: *" + session.pages + "*\nTotal participants: " + models.Count() + "\nPresent participants: " + models.Where(x => x.isPresent).Count() + "\nAbsent participants: " + models.Where(x => !x.isPresent).Count() + "\n\nRequest to occasionally discuss with the khidmatguzaar regarding the maqraat sessions and its results on his and participant's murajaat.\n\nShukran,\nWa al-Salaam.";
                    //String msg = "testing";
                    string emailbody = @"<b>" + masool.fullName + @"</b>
                                    <br />
                                    <br />
                                    Please be informed that <b>" + session.teacherIts.fullName + @"</b>  has submitted a new maqra'at session on the module with below details.
                                    <br />
                                    <br />
                                    Date: " + formatedDate + @"
                                    <br />
                                    Juz: " + session.juz + @"
                                    <br />
                                    Pages: " + session.pages + @"
                                    <br />
                                    Total Participants: " + models.Count() + @"
                                    <br />
                                    Present Participants: " + models.Where(x => x.isPresent).Count() + @"
                                    <br />
                                    Absent Participants: " + models.Where(x => !x.isPresent).Count() + @"
                                    <br />
                                    <br />
                                     Request to occasionally discuss with the khidmatguzaar regarding the maqraat sessions and its results on his and participant's murajaat.";
                    string emailto = masool.officialEmailAddress != null ? masool.officialEmailAddress : masool.emailAddress;
                    try
                    {
                        _notificationService.SendStandardHTMLEmail("Maqraat - New session added - Venue masul", emailbody, emailto, "no-reply");
                    }
                    catch (Exception e) { }

                    List<string> num = new List<string> { masool.c_codeWhatsapp + masool.whatsappNo };
                    if (masool.whatsappNo != null)
                    {
                        try
                        {
                            _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2, "Maqaraat Module Notification");
                        }
                        catch (Exception e)
                        { }
                    }
                }



                return Ok();


            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        //[Route("deletmaqaaratTeacher/{id}")]
        //[HttpDelete]
        //public async Task<IActionResult> DeleteTeacher(int id)
        //{
        //    string api = "deletmaqaaratTeacher";


        //    string token = _tokenService.ExtractTokenFromRequest(HttpContext);
        //    AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
        //    try
        //    {



        //            wafdprofile_maqaraat_teacher m = _context.wafdprofile_maqaraat_teacher.Where(x => x.itsId == id).FirstOrDefault();

        //            _context.wafdprofile_maqaraat_teacher.Remove(m);
        //            _context.SaveChanges();




        //        return Ok("succesfully Submitted");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest( new { message = "error");
        //    }

        //}

        [Route("notificationronjob")]
        [HttpGet]
        public async Task<IActionResult> notificationronjob()
        {
            try
            {

                List<MaqaraatModel> model = new List<MaqaraatModel>();

                List<wafdprofile_maqaraat_teacher> teacher = _context.wafdprofile_maqaraat_teacher.ToList();


                DateTime today = DateTime.Today;
                DateTime endOfMonthdate = _hijriCalenderService.gregorianDateForEndOfCurrentHijriMonth(today).engDate;
                bool isLastDayOfMonth = today == endOfMonthdate;
                bool isLastDayOfWeek = today.DayOfWeek == DayOfWeek.Saturday;
                int c = 0;


                foreach (wafdprofile_maqaraat_teacher i in teacher)
                {
                    if (isLastDayOfMonth)
                    {
                        String msg2 = "Salam jameel,\n" + i.its.fullName + "\n\nToday is the last day of the current hijri month.\nKindly enter all the maqraat sessions details on the module by the end of the day.\nFrom tomorrow, module will be refreshed for the next month and this month's data cannot be edited/entered.\n\nRequest to occasionally discuss with the khidmatguzaar regarding the maqraat sessions and its results on his and participant's murajaat.\n\nShukran,\nWa al-Salaam.";
                        //String msg = "testing";
                        string emailbody = @"<b>" + i.its.fullName + @"</b>
                                    <br />
                                    <br />
                                    Today is the last day of the current hijri month.\nKindly enter all the maqraat sessions details on the module by the end of the day.
                                    <br />
                                    <br />
                                    From tomorrow, module will be refreshed for the next month and this month's data cannot be edited/entered.
                                    <br />
                                    <br />m Request to occasionally discuss with the khidmatguzaar regarding the maqraat sessions and its results on his and participant's murajaat.
                                    <br />
                                     Request to occasionally discuss with the khidmatguzaar regarding the maqraat sessions and its results on his and participant's murajaat.";
                        string emailto = i.its.officialEmailAddress != null ? i.its.officialEmailAddress : i.its.emailAddress;
                        _notificationService.SendStandardHTMLEmail("Reminder - Submit maqraat marks", emailbody, emailto, "no-reply");
                        List<string> num = new List<string> { i.its.c_codeWhatsapp + i.its.whatsappNo };
                        if (i.its.whatsappNo != null)
                        {
                            _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2, "Maqaraat Module Notification");
                        }
                    }
                    if (isLastDayOfWeek)
                    {
                        String msg2 = "Salam jameel,\n" + i.its.fullName + "\n\nReminder to enter the maqraat marks for this week or previous weeks of this month.\n\nKindly ignore the email if already submitted.\n\nShukran,\nWa al-Salaam.";
                        //String msg = "testing";
                        string emailbody = @"<b>" + i.its.fullName + @"</b>
                                    <br />
                                    <br />
                                    Reminder to enter the maqraat marks for this week or previous weeks of this month.
                                    <br />
                                    <br />
                                    Kindly ignore the email if already submitted.
                                    <br />
                                     Request to occasionally discuss with the khidmatguzaar regarding the maqraat sessions and its results on his and participant's murajaat.";
                        string emailto = i.its.officialEmailAddress != null ? i.its.officialEmailAddress : i.its.emailAddress;
                        _notificationService.SendStandardHTMLEmail("Reminder - Submit maqraat marks", emailbody, emailto, "no-reply");
                        List<string> num = new List<string> { i.its.c_codeWhatsapp + i.its.whatsappNo };
                        if (i.its.whatsappNo != null)
                        {
                            _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2, "Maqaraat Module Notification");
                        }
                    }
                    //model.Add(m);
                }
                return Ok("Success");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }

    public class fetchMaqaraatSessionByDate
    {
        public DateOnly sessionDate { get; set; }
    }
}
