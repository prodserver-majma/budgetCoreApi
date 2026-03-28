using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings.Training;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace mahadalzahrawebapi.Controllers
{
    public class CustomErrorResponse
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string TargetSite { get; set; }
        public string CustomMessage { get; set; }
        public string innerException { get; set; }
        public string baseType { get; set; }
        public string saveResponse { get; set; }
    }

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;
        private readonly NotificationService _notificationService;
        private readonly WhatsAppApiService _whatsAppApiService;

        public TrainingController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _salaryService = new SalaryService(context);
            _helperService = new HelperService(context);
            _itsService = new ItsServiceRemote();
            _jhsService = new IJHSServiceRemote();
            _globalConstants = new globalConstants();
            _notificationService = new NotificationService();
            _whatsAppApiService = new WhatsAppApiService(context);
        }

        [Route("class")]
        [HttpPost]
        public async Task<IActionResult> addClass(TrainingClassModel classModel)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            string errormsg = "step 0";
            string api = "api/v2/employee/addemployee";

            try
            {
                int academicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);

                training_class classDB = _context.training_class.Where(x => x.id == classModel.id).FirstOrDefault();
                if (classModel.id == 0)
                {
                    classDB = new training_class()
                    {
                        className = classModel.className,
                        masoolIts = classModel.masoolIts,
                        academicYear = academicYear
                    };

                    _context.training_class.Add(classDB);
                    _context.SaveChanges();
                    return Ok(classModel);
                }
                if (classDB == null)
                {
                    return BadRequest(new { message = "Class with the given Id Not Found" });

                }

                classDB.className = classModel.className;
                classDB.masoolIts = classModel.masoolIts;
                _context.SaveChanges();
                return Ok(classModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("teachers")]
        [HttpGet]
        public async Task<IActionResult> getTeachers()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "teachers";

            try
            {

                List<trainingCandidate> faculties = new List<trainingCandidate>();
                List<trainingCandidateDropDown> classDB = _context.khidmat_guzaar
                    .Where(x => (x.employeeType == "Khidmatguzaar" || x.employeeType == "Visiting Faculty") && x.activeStatus == true)
                    .Select(x => new trainingCandidateDropDown
                    {
                        name = x.fullName,
                        itsId = x.itsId
                    })
                    .ToList();
                //classDB = classDB.OrderByDescending(x => x.employee_academic_details?.batchId).ToList();

                //faculties = _mapper.Map<List<trainingCandidate>>(classDB);

                return Ok(classDB);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("mybatchmates")]
        [HttpGet]
        public async Task<IActionResult> getBatchmates()
        {

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "mybatchmates";

            try
            {
                List<trainingCandidate> faculties = new List<trainingCandidate>();
                khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId).Include(x => x.training_class_student).FirstOrDefault();
                //int academicYear = hijriCal.getAcedemicYear(DateTime.Now).acedemicYear;
                //int academicYear = 1445;
                int academicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);

                List<training_class_student> classes = kg.training_class_student.Where(y => y.academicYear == academicYear).ToList();
                List<int> classDBIds = new List<int>();
                classes.ForEach(x => classDBIds.Add(x.classId));
                List<training_class_student> classDB = await _context.training_class_student.Where(x => classDBIds.Contains(x.classId))
                    .Include(x => x.studentITSNavigation)
                    .ThenInclude(x => x.mauzeNavigation)
                    .Include(x => x.studentITSNavigation)
                    .ThenInclude(x => x.employee_academic_details)
                    .ToListAsync();

                //faculties = _mapper.Map<List<trainingCandidate>>(classDB);
                faculties = _mapper.Map<List<trainingCandidate>>(classDB.Select(x => x.studentITSNavigation).ToList());

                return Ok(faculties);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("myStudents")]
        [HttpGet]
        public async Task<IActionResult> getmyStudents()
        {

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "myStudents";

            try
            {
                List<trainingCandidate> faculties = new List<trainingCandidate>();

                int academicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);
                khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId).Include(x => x.training_class_subject_teacher.Where(x => x.acedemicYear == academicYear)).AsNoTracking().FirstOrDefault();

                List<training_class_subject_teacher> myClasses = kg.training_class_subject_teacher.ToList();

                List<int> classIds = myClasses.Select(x => x.classId).ToList();
                List<training_class_student> classes = await _context.training_class_student.Where(x => classIds.Contains(x.classId))
                    .Include(x => x.studentITSNavigation)
                        .ThenInclude(x => x.employee_academic_details)
                    .Include(x => x.studentITSNavigation)
                        .ThenInclude(x => x.mauzeNavigation)
                    .Include(x => x._class)
                    .AsNoTracking().ToListAsync();

                List<training_class_student> classDB = new List<training_class_student>();
                classDB = classes.GroupBy(x => x.studentITS).Select(x => x.FirstOrDefault()).OrderByDescending(x => x.studentITSNavigation.employee_academic_details.batchId).ToList();
                //classDB = classes.ToList();

                faculties = _mapper.Map<List<trainingCandidate>>(classDB);

                return Ok(faculties);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("teacher/classsubject")]
        [HttpGet]
        public async Task<IActionResult> getTeacherClassSubject()
        {
            string errormsg = "step 0";
            string api = "teacher/classes";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {

                List<TrainingClassSubjectTeacherModel> classes = new List<TrainingClassSubjectTeacherModel>();
                khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId)
                    .Include(x => x.training_class_subject_teacher.Where(x => x.training_student_subject_marksheet.Count() > 0))
                        .ThenInclude(x => x._class)
                    .Include(x => x.training_class_subject_teacher.Where(x => x.training_student_subject_marksheet.Count() > 0))
                        .ThenInclude(x => x.subject)
                    .FirstOrDefault();

                int academicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);

                List<training_class_subject_teacher> classDB = kg.training_class_subject_teacher.Where(x => x.teacherITS == authUser.ItsId && x.acedemicYear == academicYear).ToList();
                foreach (var item in classDB)
                {
                    TrainingClassSubjectTeacherModel classObj = _mapper.Map<TrainingClassSubjectTeacherModel>(item);
                    classObj.className = item._class.className;
                    classObj.subjectName = item.subject.name;
                    CalenderModel sdate = _helperService.getHijriDate(item.startDate);
                    CalenderModel edate = _helperService.getHijriDate(item.endDate);
                    classObj.hijriStart = sdate.hijDay + "-" + sdate.hijMonth + "-" + sdate.hijYear;
                    classObj.hijriEnd = edate.hijDay + "-" + edate.hijMonth + "-" + edate.hijYear;
                    classObj.hijriMonth = sdate.hijMonthName;
                    classes.Add(classObj);
                }

                return Ok(classes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("teacher/marksheets/{cstId}")]
        [HttpGet]
        public async Task<IActionResult> getTeacherClassSubject(int cstId)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token); string errormsg = "step 0";
            string api = "teacher/marksheets/{cstId}";

            try
            {
                List<TrainingStudentQuestionareModel> marksheets = new List<TrainingStudentQuestionareModel>();

                int academicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);

                training_class_subject_teacher cstDb = _context.training_class_subject_teacher
                  .Where(x => x.id == cstId && x.acedemicYear == academicYear && x.teacherITS == authUser.ItsId)
                  .Include(x => x.subject)
                  .Include(x => x.teacherITSNavigation)
                  .Include(x => x.training_student_subject_marksheet).ThenInclude(x => x.studentITSNavigation).ThenInclude(x => x.mauzeNavigation)
                .FirstOrDefault();

                if (cstDb == null)
                {
                    return BadRequest(new { message = "CST for Auth User Not Found" });
                }

                List<training_student_subject_marksheet> marksheetDB = cstDb.training_student_subject_marksheet.Where(x => x.acedemicYear == academicYear).ToList();

                foreach (var item in marksheetDB)
                {
                    TrainingStudentQuestionareModel classObj = _mapper.Map<TrainingStudentQuestionareModel>(item);
                    CalenderModel sdate = _helperService.getHijriDate(item.startDate);
                    CalenderModel edate = _helperService.getHijriDate(item.endDate);
                    classObj.hijriStart = sdate.hijDay + "-" + sdate.hijMonth + "-" + sdate.hijYear;
                    classObj.hijriEnd = edate.hijDay + "-" + edate.hijMonth + "-" + edate.hijYear;
                    classObj.hijriMonth = sdate.hijMonthName;
                    marksheets.Add(classObj);
                }

                marksheets = marksheets.OrderBy(x => x.startDate).ToList();

                return Ok(marksheets);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("report/generatecacheviacron/ni5gaaf5g4")]
        [HttpGet]
        public async Task<IActionResult> getReportCache()
        {
            getReportMarksheets(false);
            return Ok();
        }

        //[Route("report/deletecache")]
        //[HttpGet]
        //public async Task<IActionResult> deleteReportCache()
        //{
        //    cache.DeleteItem("Cahed-Training-Report");
        //    return Ok();
        //}

        [Route("report/marksheets")]
        [HttpGet]
        public async Task<IActionResult> getReportMarksheets(bool checkAuth = true)
        {
            try
            {
                AuthUser authUser = new AuthUser();

                if (checkAuth)
                {
                    string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                    authUser = _tokenService.GetAuthUserFromToken(token);
                }

                int academicYear = int.Parse(_context.global_constant
                    .Where(x => x.key == "trainingAcedemicYear")
                    .Select(x => x.value)
                    .FirstOrDefault());

                // Fetch related data upfront
                var maqaraatData = _context.wafdprofile_maqaraat_data
                    .Where(m => m.studentItsId != null)
                    .GroupBy(m => m.studentItsId)
                    .Select(g => new
                    {
                        ItsId = g.Key,
                        AvgMarks = Math.Round(g.Average(x => x.marks ?? 0), 0)
                    })
                    .ToDictionary(x => x.ItsId, x => x.AvgMarks);

                var workshopData = _context.wafdprofile_workshop_data
                    .Where(w => w.academicYear == academicYear)
                    .GroupBy(w => w.itsId)
                    .Select(g => new
                    {
                        ItsId = g.Key,
                        CourseCount = g.Count(),
                        CourseNames = string.Join(", ", g.Select(x => x.courseName))
                    })
                    .ToDictionary(x => x.ItsId, x => new { x.CourseCount, x.CourseNames });

                var qualifications = _context.wafdprofile_qualification_new
                    .Where(q => q.status == "Completed")
                    .OrderByDescending(q => q.year)
                    .GroupBy(q => q.itsid)
                    .Select(g => new { ItsId = g.Key, Degree = g.FirstOrDefault().degree })
                    .ToDictionary(x => x.ItsId, x => x.Degree);

                // Fetch necessary data
                var cstData = _context.training_class_subject_teacher
                    .Where(x => x.acedemicYear == academicYear)
                    .Select(cst => new
                    {
                        CSTId = cst.id,
                        Subject = new
                        {
                            cst.subjectId,
                            cst.subject.name
                        },
                        Teacher = new
                        {
                            cst.teacherITSNavigation.itsId,
                            cst.teacherITSNavigation.fullName,
                            Mauze = cst.teacherITSNavigation.mauzeNavigation.displayName
                        },
                        Class = new
                        {
                            cst.classId,
                            cst._class.className
                        },
                        Marksheets = cst.training_student_subject_marksheet
                            .Where(ms => ms.acedemicYear == academicYear)
                            .Select(ms => new
                            {
                                ms.id,
                                Student = new
                                {
                                    ms.studentITSNavigation.itsId,
                                    ms.studentITSNavigation.fullName,
                                    Mauze = ms.studentITSNavigation.mauzeNavigation.displayName,
                                    Email = String.IsNullOrEmpty(ms.studentITSNavigation.officialEmailAddress) ? ms.studentITSNavigation.emailAddress : ms.studentITSNavigation.officialEmailAddress,
                                    contactnum = ms.studentITSNavigation.mobileNo
                                },
                                ms.startDate,
                                ms.endDate,
                                marks = ms.marks == null ? (ms.status == "Submitted" ? "Not Checked" : "Not Submitted") : ms.marks.ToString()
                            })
                    })
                    .ToList();

                // Process data
                var rowHeaders = new List<trainingCandidate>();
                var colHeaders = new List<trainingReportModelColHeader>();
                var marksheets = new List<TrainingStudentQuestionareModel>();

                foreach (var cst in cstData)
                {
                    if (!colHeaders.Any(ch => ch.id == cst.Subject.subjectId))
                    {
                        colHeaders.Add(new trainingReportModelColHeader
                        {
                            id = cst.Subject.subjectId,
                            name = cst.Subject.name
                        });
                    }

                    foreach (var ms in cst.Marksheets)
                    {
                        if (!rowHeaders.Any(rh => rh.itsId == ms.Student.itsId))
                        {
                            rowHeaders.Add(new trainingCandidate
                            {
                                itsId = ms.Student.itsId,
                                name = ms.Student.fullName,
                                email = ms.Student.Email,
                                mauze = ms.Student.Mauze,
                                contactNum = ms.Student.contactnum,
                                latestQualification = qualifications.ContainsKey(ms.Student.itsId) ? qualifications[ms.Student.itsId] : null,
                                maqaraatAvg = maqaraatData.ContainsKey(ms.Student.itsId) ? maqaraatData[ms.Student.itsId] : 0,
                                courseCount = workshopData.ContainsKey(ms.Student.itsId) ? workshopData[ms.Student.itsId].CourseCount : 0,
                                courseNames = workshopData.ContainsKey(ms.Student.itsId) ? workshopData[ms.Student.itsId].CourseNames : "",
                                darajah = cst.Class.className
                            });
                        }

                        marksheets.Add(new TrainingStudentQuestionareModel
                        {
                            id = ms.id,
                            startDate = ms.startDate,
                            endDate = ms.endDate,
                            marks = ms.marks,
                            studentITS = ms.Student.itsId,
                            subjectId = cst.Subject.subjectId,
                            subject = new TrainingSubjectModel
                            {
                                id = cst.Subject.subjectId,
                                name = cst.Subject.name
                            },
                            teacherName = cst.Teacher.fullName,
                            teacher = new trainingCandidate
                            {
                                itsId = cst.Teacher.itsId,
                                name = cst.Teacher.fullName,
                                mauze = cst.Teacher.Mauze
                            }
                        });
                    }
                }

                var report = new TrainingReportModel
                {
                    colHeader = colHeaders.OrderBy(ch => ch.name).ToList(),
                    rowHeaders = rowHeaders.OrderBy(rh => rh.name).ToList(),
                    details = marksheets.OrderBy(ms => ms.startDate).ToList()
                };

                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    InnerException = ex.InnerException?.Message
                });
            }
        }



        [Route("report/downloadclasssubzip/{classId}/{subjectId}")]
        [HttpGet]
        public async Task<IActionResult> getReportZipForClassSubject(int classId, int subjectId)
        {
            AuthUser authUser = new AuthUser();
            string errormsg = "step 0";

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            authUser = _tokenService.GetAuthUserFromToken(token);
            string api = "teacher/report/downloadclasssubzip/{cstId}";

            try
            {

                //int academicYear = hijriCal.getAcedemicYear(DateTime.Now).acedemicYear;

                int academicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);

                List<TrainingStudentQuestionareModel> marksheets = new List<TrainingStudentQuestionareModel>();
                List<string> objectKeys = new List<string>();

                training_class_subject_teacher cstDb = _context.training_class_subject_teacher.Where(x => x.acedemicYear == academicYear && x.classId == classId && x.subjectId == subjectId).FirstOrDefault();
                if (cstDb == null)
                {
                    return BadRequest(new { message = "CST for Auth User Not Found" });
                }

                List<training_student_subject_marksheet> marksheetDB = cstDb.training_student_subject_marksheet.Where(y => y.acedemicYear == academicYear).ToList();


                foreach (training_student_subject_marksheet subjectMarksheet in marksheetDB)
                {

                    if (!String.IsNullOrEmpty(subjectMarksheet.answers))
                    {

                        JObject jsonObj = JsonConvert.DeserializeObject<JObject>(subjectMarksheet.answers);
                        foreach (JProperty property in jsonObj.Properties())
                        {
                            errormsg = "Step 2 key:" + property.Name;
                            if (property.Value.Type == JTokenType.Array)
                            {
                                JArray arr = new JArray();
                                bool isObj = false;
                                // Loop through each object in the array
                                foreach (var value in property.Value)
                                {
                                    errormsg = "Step 3 key:" + property.Name + " obj:" + value;
                                    if (value.Type == JTokenType.Object)
                                    {
                                        JObject obj = (JObject)value;
                                        if (obj["content"] != null && obj["name"] != null)
                                        {
                                            string[] contentSplit = ((string)obj["content"]).Split(',');
                                            string objKey = ((string)obj["content"]).Replace(contentSplit[0] + ",", "");
                                            objectKeys.Add(objKey);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }

                marksheets = marksheets.OrderBy(x => x.startDate).ToList();

                return Ok(_helperService.retriveAndZipFromBucket(objectKeys, cstDb._class.className + "-" + cstDb.subject.name));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [Route("class")]
        [HttpGet]
        public async Task<IActionResult> getClass()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "api/v2/employee/addemployee";

            try
            {
                int academicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);

                List<TrainingClassModel> classDB = _context.training_class.Where(x => x.academicYear == academicYear).Select(x => new TrainingClassModel
                {
                    className = x.className,
                    id = x.id,
                    masoolIts = x.masoolIts,
                    deletable = x.training_class_subject_teacher.Count > 0 ? false : true,
                    academicYear = x.academicYear
                }).ToList();


                return Ok(classDB);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("subject")]
        [HttpPost]
        public async Task<IActionResult> addSubject(TrainingSubjectModel subjectModel)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token); string errormsg = "step 0";
            string api = "subject";

            try
            {
                int academicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);

                subjectModel.academicYear = academicYear;

                training_subject subjectDB = _context.training_subject.Where(x => x.id == subjectModel.id).Include(x => x.training_class_subject_teacher).FirstOrDefault();
                string sanitizedJsonString = subjectModel.qustionare.Replace("\\n", "").Replace("\\\"", "'");
                JObject jsonObj = JObject.Parse(sanitizedJsonString);
                subjectModel.qustionareJson = JsonDocument.Parse(sanitizedJsonString).RootElement;

                if (subjectModel.id == 0)
                {
                    subjectDB = new training_subject()
                    {
                        name = subjectModel.name,
                        outOf = subjectModel.wheightage,
                        qustionare = subjectModel.qustionare,
                        status = "In Active",
                        accademicYear = academicYear
                    };

                    _context.training_subject.Add(subjectDB);
                    _context.SaveChanges();
                    return Ok(subjectModel);
                }
                if (subjectDB == null)
                {
                    return BadRequest(new { message = "Class with the given Id Not Found" });

                }

                subjectDB.name = subjectModel.name;
                subjectDB.outOf = subjectModel.wheightage;
                //subjectDB.accademicYear = subjectModel.academicYear;

                subjectDB.qustionare = subjectModel.qustionare;
                subjectDB.status = subjectModel.status;
                subjectModel.deletable = subjectDB.training_class_subject_teacher.Count() > 0 ? false : true;

                _context.SaveChanges();
                return Ok(subjectModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("subject")]
        [HttpGet]
        public async Task<IActionResult> getSubject([FromQuery] int? academicYear)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            string errormsg = "step 0";
            string api = "subject";

            try
            {
                if (academicYear == null)
                {
                    academicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);
                }

                List<TrainingSubjectModel> subjectModel = _context.training_subject.Where(x => x.accademicYear == academicYear).Include(x => x.training_class_subject_teacher).Select(x => new TrainingSubjectModel
                {
                    name = x.name,
                    id = x.id,
                    qustionare = x.qustionare,
                    status = x.status,
                    wheightage = x.outOf,
                    deletable = x.training_class_subject_teacher.Count > 0 ? false : true,
                    academicYear = x.accademicYear
                }).ToList();

                subjectModel.ForEach(x =>
                {
                    try
                    {
                        // Remove unnecessary escape characters and properly format the JSON string
                        string sanitizedJsonString = x.qustionare.Replace("\\n", "").Replace("\\\"", "\"").Replace("\\'", "'");

                        // Parse the JSON string into a JsonElement
                        x.qustionareJson = JsonDocument.Parse(sanitizedJsonString).RootElement;
                    }
                    catch (Exception jsonEx)
                    {
                        // Handle JSON parsing error
                        x.qustionareJson = default;
                        errormsg = jsonEx.Message;
                    }
                });

                return Ok(subjectModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }




        [Route("subject/{subjectId}")]
        [HttpDelete]
        public async Task<IActionResult> removeSubject(int subjectId)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "cubject";
            DateTime today = DateTime.Today;

            try
            {
                training_subject cstDb = _context.training_subject.Where(x => x.id == subjectId).FirstOrDefault();

                if (cstDb == null)
                {
                    return BadRequest(new { message = "No entries found" });
                }
                if (cstDb.training_class_subject_teacher.Count() > 0)
                {
                    return BadRequest(new { message = "cannot delete, data exists which rely on the selected data" });
                }

                _context.training_subject.Remove(cstDb);
                _context.SaveChanges();

                return Ok();


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("class/{classId}")]
        [HttpDelete]
        public async Task<IActionResult> removeClass(int classId)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "subject";
            DateTime today = DateTime.Today;

            try
            {
                training_class cstDb = _context.training_class.Where(x => x.id == classId).FirstOrDefault();

                if (cstDb == null)
                {
                    return BadRequest(new { message = "No entries found" });
                }
                if (cstDb.training_class_subject_teacher.Count() > 0)
                {
                    return BadRequest(new { message = "cannot delete, data exists which rely on the selected data" });
                }

                _context.training_class.Remove(cstDb);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("classstudents")]
        [HttpPost]
        public async Task<IActionResult> addClassStudents([FromBody] TrainingClassStudent classStudentModel)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "classstudents";
            DateTime today = DateTime.Today;

            try
            {
                List<training_class_student> classStudentDb = _context.training_class_student.Where(x => x.classId == classStudentModel.classId).ToList();
                classStudentDb = classStudentDb.Where(x => classStudentModel.studentsToTag.Contains(x.studentITS)).ToList();
                classStudentDb.ForEach(x =>
                {
                    classStudentModel.studentsToTag.Remove(x.studentITS);
                });

                int acedemicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault()?.value ?? "0");
                var kgs = _context.khidmat_guzaar.Where(x => x.employeeType != "Staff").Include(x => x.mauzeNavigation);
                foreach (int student in classStudentModel.studentsToTag)
                {
                    khidmat_guzaar stu = kgs.Where(x => x.itsId == student).FirstOrDefault();
                    training_class_student classStudent = new training_class_student()
                    {
                        classId = classStudentModel.classId,
                        studentITS = student,
                        academicYear = acedemicYear,
                        mauze = stu.mauzeNavigation?.displayName ?? ""
                    };

                    _context.training_class_student.Add(classStudent);
                    await _context.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                var errorDetails = new
                {
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                };
                return BadRequest(errorDetails);
            }
        }


        [Route("classstudents")]
        [HttpGet]
        public async Task<IActionResult> getClassStudents()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "classstudents";
            DateTime today = DateTime.Today;

            try
            {
                int acedemicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);
                List<training_class_student> classStudentDb = _context.training_class_student.Where(x => x.academicYear == acedemicYear).Include(x => x.studentITSNavigation).ThenInclude(x => x.employee_academic_details).Include(x => x._class).ToList();
                List<TrainingClassStudent> classStudentModels = new List<TrainingClassStudent>();

                if (classStudentDb.Count == 0)
                {
                    return Ok();
                }

                foreach (training_class_student cst in classStudentDb)
                {
                    TrainingClassStudent cstModel = new TrainingClassStudent();
                    cstModel.academicYear = cst.academicYear;
                    cstModel.studentName = cst.studentITSNavigation.fullName;
                    cstModel.className = cst._class.className;
                    cstModel.batchId = cst.studentITSNavigation.employee_academic_details.batchId ?? 0;
                    cstModel.id = cst.id;
                    cstModel.rank = cst.rank;
                    cstModel.prevRank = cst.prevRank;
                    cstModel.mauze = cst.mauze;
                    cstModel.marks = cst.marks ?? 0;
                    cstModel.classId = cst.classId;
                    cstModel.percentage = cst.percentage ?? 0;
                    cstModel.studentITS = cst.studentITS;
                    cstModel.age = cst.studentITSNavigation.age ?? 0;

                    classStudentModels.Add(cstModel);
                }
                return Ok(classStudentModels);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("classstudents/{classStudentId}")]
        [HttpDelete]
        public async Task<IActionResult> removeClassStudent(int classStudentId)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "subject";
            DateTime today = DateTime.Today;

            try
            {
                int acedemicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);
                training_class_student cstDb = _context.training_class_student.Where(x => x.id == classStudentId).FirstOrDefault();
                List<TrainingClassSubjectTeacherModel> cstModels = new List<TrainingClassSubjectTeacherModel>();

                if (cstDb == null)
                {
                    return BadRequest(new { message = "No entries found" });
                }

                List<training_class_subject_teacher> cstList = _context.training_class_subject_teacher.Where(x => x.classId == cstDb.classId).ToList();

                List<training_student_subject_marksheet> studentClasses = _context.training_student_subject_marksheet.Where(x => x.studentITS == cstDb.studentITS).ToList();

                studentClasses = studentClasses.Where(x => cstList.Any(y => x.cstId == y.id)).ToList();

                studentClasses.ForEach(x =>
                {
                    _context.training_student_subject_marksheet.Remove(x);
                });

                _context.training_class_student.Remove(cstDb);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("tagcst")]
        [HttpPost]
        public async Task<IActionResult> addCST(TrainingClassSubjectTeacherModel cstModel)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "subject";
            DateTime today = DateTime.Today;

            try
            {
                CalenderModel startDateModel = _helperService.getHijriDate(cstModel.startDate);
                CalenderModel endDateModel = _helperService.getHijriDate(cstModel.endDate);
                training_class_subject_teacher cstDb = _context.training_class_subject_teacher.Where(x => x.classId == cstModel.classId && x.subjectId == cstModel.subjectId).FirstOrDefault();
                if (cstModel.id == 0 && cstDb != null)
                {
                    return BadRequest(new { message = "Class subject tagging already exists" });
                }

                int acedemicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);

                if (cstModel.id == 0)
                {
                    cstDb = new training_class_subject_teacher()
                    {
                        classId = cstModel.classId,
                        subjectId = cstModel.subjectId,
                        acedemicYear = acedemicYear,
                        createdBy = authUser.ItsId,
                        createdOn = today,
                        startDate = cstModel.startDate,
                        endDate = cstModel.endDate,
                        teacherITS = cstModel.teacherITS,
                        status = "In Active",
                    };

                    if (cstModel.startDate <= DateOnly.FromDateTime(today) && cstModel.endDate >= DateOnly.FromDateTime(today))
                    {
                        cstDb.status = "Active";
                    }
                    _context.training_class_subject_teacher.Add(cstDb);
                    _context.SaveChanges();

                    cstModel.subjectName = cstDb.subject?.name;
                    cstModel.teacherName = cstDb.teacherITSNavigation?.fullName;
                    cstModel.className = cstDb._class?.className;
                    cstModel.status = cstDb.status;
                    cstModel.id = cstDb.id;
                    cstModel.deletable = cstDb.training_student_subject_marksheet.Count() > 0 ? false : true;


                    cstModel.hijriStart = startDateModel.hijDay + "-" + startDateModel.hijMonth + "-" + startDateModel.hijYear;
                    cstModel.hijriEnd = endDateModel.hijDay + "-" + endDateModel.hijMonth + "-" + endDateModel.hijYear;
                    cstModel.hijriMonth = startDateModel.hijMonthName;

                    return Ok(cstModel);
                }

                cstDb.teacherITS = cstModel.teacherITS;
                cstDb.status = cstModel.status;
                if (cstDb.startDate != cstModel.startDate)
                {
                    cstDb.startDate = cstModel.startDate;
                    cstDb.training_student_subject_marksheet.ToList().ForEach(x =>
                    {
                        x.startDate = cstModel.startDate;
                    });
                }
                if (cstDb.endDate != cstModel.endDate)
                {
                    cstDb.endDate = cstModel.endDate;
                    cstDb.training_student_subject_marksheet.ToList().ForEach(x =>
                    {
                        x.endDate = cstModel.endDate;
                    });
                }
                cstDb.updatedOn = DateTime.Now;

                _context.SaveChanges();

                cstModel.deletable = cstDb.training_student_subject_marksheet.Count() > 0 ? false : true;
                cstModel.id = cstDb.id;
                cstModel.subjectName = cstDb.subject.name;
                cstModel.teacherName = cstDb.teacherITSNavigation.fullName;
                cstModel.className = cstDb._class.className;

                cstModel.hijriStart = startDateModel.hijDay + "-" + startDateModel.hijMonth + "-" + startDateModel.hijYear;
                cstModel.hijriEnd = endDateModel.hijDay + "-" + endDateModel.hijMonth + "-" + endDateModel.hijYear;
                cstModel.hijriMonth = startDateModel.hijMonthName;

                return Ok(cstModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("tagcst")]
        [HttpGet]
        public async Task<IActionResult> getCST()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "subject";

            DateTime today = DateTime.Today;

            try
            {
                int acedemicYear = int.Parse(_context.global_constant.FirstOrDefault(x => x.key == "trainingAcedemicYear").value);
                List<training_class_subject_teacher> cstDb = _context.training_class_subject_teacher
                    .Where(x => x.acedemicYear == acedemicYear)
                    .Include(x => x._class)
                    .Include(x => x.subject)
                    .Include(x => x.teacherITSNavigation)
                    .Include(x => x.training_student_subject_marksheet)
                    .ToList();

                List<TrainingClassSubjectTeacherModel> cstModels = new List<TrainingClassSubjectTeacherModel>();

                if (cstDb.Count == 0)
                {
                    return Ok();
                }

                foreach (var item in cstDb)
                {
                    var temp = _mapper.Map<TrainingClassSubjectTeacherModel>(item);
                    var hijs = _helperService.getHijriDate(item.startDate);
                    var hije = _helperService.getHijriDate(item.endDate);
                    temp.hijriEnd = hije.hijDay + "-" + hije.hijMonth + "-" + hije.hijYear;
                    temp.hijriStart = hijs.hijDay + "-" + hijs.hijMonth + "-" + hijs.hijYear;
                    temp.hijriMonth = hijs.hijMonthName;
                    temp.deletable = item.training_student_subject_marksheet.Count() > 0 ? false : true;
                    cstModels.Add(temp);
                }

                return Ok(cstModels);
            }
            catch (Exception ex)
            {
                var errorDetails = new
                {
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                };
                return BadRequest(errorDetails);
            }
        }


        [Route("tagcst/{cstId}")]
        [HttpDelete]
        public async Task<IActionResult> removeCSTtag(int cstId)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "subject";
            DateTime today = DateTime.Today;

            try
            {
                int acedemicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);
                training_class_subject_teacher cstDb = _context.training_class_subject_teacher.Where(x => x.id == cstId).FirstOrDefault();
                List<TrainingClassSubjectTeacherModel> cstModels = new List<TrainingClassSubjectTeacherModel>();

                if (cstDb == null)
                {
                    return BadRequest(new { message = "No entries found" });
                }
                if (cstDb.training_student_subject_marksheet.Count() > 0)
                {
                    return BadRequest(new { message = "cannot delete, data exists which rely on the selected data" });
                }

                _context.training_class_subject_teacher.Remove(cstDb);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("generatestudentactivity/{cstId}")]
        [HttpGet]
        public async Task<IActionResult> generateStudentActivity(int cstId)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "generateactivity";
            DateTime today = DateTime.Today;

            try
            {
                int acedemicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);
                training_class_subject_teacher cstDb = _context.training_class_subject_teacher.Where(x => x.id == cstId)
                    .Include(x => x.training_student_subject_marksheet)
                    .Include(x => x._class)
                        .ThenInclude(x => x.training_class_student)
                        .FirstOrDefault();
                if (cstDb == null)
                {
                    return Ok();
                }

                List<training_student_subject_marksheet> marksheets = cstDb.training_student_subject_marksheet.ToList();
                foreach (training_class_student classStudent in cstDb._class.training_class_student)
                {
                    string status = "In Active";
                    if (cstDb.startDate <= DateOnly.FromDateTime(today) && cstDb.endDate >= DateOnly.FromDateTime(today))
                    {
                        status = "Active";
                    }
                    if (marksheets.Any(x => x.studentITS == classStudent.studentITS && x.acedemicYear == classStudent.academicYear))
                    {
                        continue;
                    }
                    training_student_subject_marksheet cstModel = new training_student_subject_marksheet()
                    {
                        studentITS = classStudent.studentITS,
                        cstId = cstId,
                        status = status,
                        acedemicYear = acedemicYear,
                        startDate = cstDb.startDate,
                        endDate = cstDb.endDate
                    };

                    _context.training_student_subject_marksheet.Add(cstModel);
                }
                _context.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("studentquestionare/{id}")]
        [HttpGet]
        public async Task<IActionResult> getStudentQuestionare(int id)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "studentquestionare";

            DateTime today = DateTime.Today;

            try
            {
                var marksheets = _context.training_student_subject_marksheet
                  .Where(x => x.id == id)
                  .Include(x => x.cst).ThenInclude(x => x.subject)
                  .Include(x => x.cst).ThenInclude(x => x.teacherITSNavigation)
                  .Include(x => x.studentITSNavigation).ThenInclude(x => x.mauzeNavigation)
                  .FirstOrDefault();

                if (marksheets == null)
                {
                    return BadRequest(new { message = "Marksheet with given Id not found" });
                }

                var questionareModel = _mapper.Map<TrainingStudentQuestionareModel>(marksheets);
                CalenderModel startDateModel = _helperService.getHijriDate(questionareModel.startDate);
                CalenderModel endDateModel = _helperService.getHijriDate(questionareModel.endDate);

                questionareModel.hijriStart = startDateModel.hijDay + "-" + startDateModel.hijMonth + "-" + startDateModel.hijYear;
                questionareModel.hijriEnd = endDateModel.hijDay + "-" + endDateModel.hijMonth + "-" + endDateModel.hijYear;
                questionareModel.hijriMonth = startDateModel.hijMonthName;

                if (!string.IsNullOrEmpty(marksheets.answers))
                {
                    using (JsonDocument document = JsonDocument.Parse(questionareModel.answers))
                    {
                        JsonElement root = document.RootElement;
                        var jsonObj = new Dictionary<string, JsonElement>();
                        var jsonSerializerOptions = new JsonSerializerOptions
                        {
                            WriteIndented = true
                        };

                        foreach (JsonProperty property in root.EnumerateObject())
                        {
                            errormsg = "Step 2 key:" + property.Name;
                            if (property.Value.ValueKind == JsonValueKind.Array)
                            {
                                var arr = new List<JsonElement>();
                                bool isObj = false;

                                foreach (JsonElement value in property.Value.EnumerateArray())
                                {
                                    errormsg = "Step 3 key:" + property.Name + " obj:" + value;
                                    if (value.ValueKind == JsonValueKind.Object)
                                    {
                                        isObj = true;
                                        var obj = value.Clone();

                                        if (obj.TryGetProperty("content", out JsonElement contentElement) &&
                                            obj.TryGetProperty("name", out JsonElement nameElement))
                                        {
                                            string[] contentSplit = contentElement.GetString().Split(',');
                                            string objKey = contentElement.GetString().Replace(contentSplit[0] + ",", "");
                                            string res = _helperService.retrieveBase64FromBucket(objKey);
                                            string newContent = contentSplit[0] + "," + res;

                                            // Modify the JSON object
                                            var jsonObject = new Dictionary<string, JsonElement>();
                                            foreach (JsonProperty element in obj.EnumerateObject())
                                            {
                                                if (element.NameEquals("content"))
                                                {
                                                    jsonObject[element.Name] = JsonDocument.Parse($"\"{newContent}\"").RootElement;
                                                }
                                                else
                                                {
                                                    jsonObject[element.Name] = element.Value.Clone();
                                                }
                                            }

                                            var updatedObj = System.Text.Json.JsonSerializer.SerializeToElement(jsonObject, jsonSerializerOptions);
                                            arr.Add(updatedObj);
                                            questionareModel.response += property.Name + ":" + res + ", ";
                                        }
                                    }
                                }

                                if (isObj)
                                {
                                    jsonObj[property.Name] = System.Text.Json.JsonSerializer.SerializeToElement(arr, jsonSerializerOptions);
                                }
                                else
                                {
                                    jsonObj[property.Name] = property.Value;
                                }
                            }
                            else
                            {
                                jsonObj[property.Name] = property.Value;
                            }
                        }

                        // Serialize the updated dictionary back to JSON string
                        questionareModel.answers = System.Text.Json.JsonSerializer.Serialize(jsonObj, jsonSerializerOptions);
                        questionareModel.answersOBJ = JsonDocument.Parse(questionareModel.answers).RootElement;
                        questionareModel.response += errormsg;
                    }
                }


                //if (string.IsNullOrEmpty(marksheets.answers))
                //{
                //    marksheets.answers = "[]";
                //}

                //var jsonObj = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(marksheets.answers);

                //if (jsonObj.ValueKind == JsonValueKind.Undefined)
                //{
                //    questionareModel.answers = "[]";
                //}

                var quesObj = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(questionareModel.qustionare);
                if (quesObj.ValueKind == JsonValueKind.Undefined)
                {
                    return BadRequest(new { message = "Failed to deserialize questionare to JsonElement" });
                }

                questionareModel.qustionareJson = quesObj;
                //questionareModel.answersOBJ = jsonObj;

                return Ok(questionareModel);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception: {ex.Message}");
            }
        }

        [Route("studentquestionare")]
        [HttpPost]
        public async Task<IActionResult> UpdateStudentQuestionare([FromBody] TrainingStudentQuestionareModel questionareModel)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "studentquestionare";
            DateTime today = DateTime.Today;

            if (questionareModel == null)
            {
                return BadRequest(new { message = "Invalid request payload." });
            }

            // Log the incoming payload for debugging
            System.Diagnostics.Debug.WriteLine("Received payload: " + System.Text.Json.JsonSerializer.Serialize(questionareModel));

            JsonElement jsonEle = new JsonElement();
            if (questionareModel.modifyAns == true)
            {
                if (string.IsNullOrEmpty(questionareModel.answers))
                {
                    return BadRequest(new { message = "answers cannot be null or empty when modifyAns is true " + questionareModel.answers });
                }

                jsonEle = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(questionareModel.answers);
                if (jsonEle.ValueKind == JsonValueKind.Undefined)
                {
                    return BadRequest(new { message = "Failed to deserialize answers to JsonElement" });
                }
            }
            string baseType = "";
            string saveResponse = "";
            try
            {
                var marksheets = _context.training_student_subject_marksheet
                    .Where(x => x.id == questionareModel.id)
                    .Include(x => x.cst).ThenInclude(x => x.subject)
                    .Include(x => x.cst).ThenInclude(x => x.teacherITSNavigation)
                    .Include(x => x.studentITSNavigation).ThenInclude(x => x.mauzeNavigation)
                    .FirstOrDefault();

                if (marksheets == null)
                {
                    return BadRequest(new { message = "Marksheet with given Id not found" });
                }

                errormsg = "Step 1: Marksheet found";

                if (questionareModel.modifyAns)
                {
                    if (string.IsNullOrEmpty(questionareModel.answers))
                    {
                        return BadRequest(new { message = "questionareModel.answers cannot be null or empty when modifyAns is true" });
                    }

                    errormsg = "Step 2: Parsing marksheets.answers";
                    using (JsonDocument document = JsonDocument.Parse(questionareModel.answers))
                    {
                        JsonElement root = document.RootElement;
                        var jsonObj = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(questionareModel.answers);

                        if (root.ValueKind == JsonValueKind.Object)
                        {
                            var properties = root.EnumerateObject().ToList();

                            foreach (var property in properties)
                            {
                                errormsg = "Step 3 key:" + property.Name;

                                if (property.Value.ValueKind == JsonValueKind.Array)
                                {
                                    var arr = new JsonArray();
                                    bool isObj = false;

                                    foreach (var value in property.Value.EnumerateArray())
                                    {
                                        errormsg = "Step 4 key:" + property.Name + " obj:" + value;

                                        if (value.ValueKind == JsonValueKind.Object)
                                        {
                                            var obj = value.Clone();
                                            isObj = true;

                                            if (obj.TryGetProperty("content", out JsonElement contentElement) && obj.TryGetProperty("name", out JsonElement nameElement))
                                            {
                                                string[] contentSplit = contentElement.GetString().Split(',');
                                                baseType = contentSplit[0];
                                                string base64 = contentElement.GetString().Replace(contentSplit[0] + ",", "");

                                                if (questionareModel.modifyUpload)
                                                {
                                                    saveResponse = await _helperService.saveBase64ToBucket("Training/" + (questionareModel.studentITS + "_" + questionareModel.subject.name + "_" + questionareModel.id + "_" + property.Name + "_" + nameElement.GetString() ?? "test.txt"), base64);
                                                    questionareModel.response += property.Name + ":" + saveResponse + ", ";
                                                }

                                                var newContentValue = baseType + "," + "Training/" + (questionareModel.studentITS + "_" + questionareModel.subject.name + "_" + questionareModel.id + "_" + property.Name + "_" + nameElement.GetString() ?? "test.txt");

                                                // Create a new JsonObject with updated content
                                                var jsonObject = new JsonObject();
                                                foreach (var element in obj.EnumerateObject())
                                                {
                                                    if (element.NameEquals("content"))
                                                    {
                                                        jsonObject[element.Name] = newContentValue;
                                                    }
                                                    else
                                                    {
                                                        jsonObject[element.Name] = JsonNode.Parse(element.Value.GetRawText());
                                                    }
                                                }

                                                arr.Add(jsonObject);
                                            }
                                        }

                                        // if (isObj)
                                        // {
                                        //   jsonObj[property.Name] = arr;
                                        // }
                                    }

                                    if (isObj)
                                    {
                                        jsonObj[property.Name] = arr;
                                    }
                                }
                            }

                            marksheets.answers = System.Text.Json.JsonSerializer.Serialize(jsonObj);
                        }
                    }
                }

                var startDateModel = _helperService.getHijriDate(questionareModel.startDate);
                var endDateModel = _helperService.getHijriDate(questionareModel.endDate);
                questionareModel.hijriStart = startDateModel.hijDay + "-" + startDateModel.hijMonth + "-" + startDateModel.hijYear;
                questionareModel.hijriEnd = endDateModel.hijDay + "-" + endDateModel.hijMonth + "-" + endDateModel.hijYear;
                questionareModel.hijriMonth = startDateModel.hijMonthName;

                marksheets.studentITS = questionareModel.studentITS;
                marksheets.cstId = questionareModel.cstId;
                marksheets.status = questionareModel.status;
                marksheets.acedemicYear = questionareModel.acedemicYear;
                marksheets.endDate = questionareModel.endDate;
                marksheets.startDate = questionareModel.startDate;

                if (questionareModel.modifyAns)
                {
                    questionareModel.answersOBJ = jsonEle;
                }

                if (marksheets.marks?.ToString() != questionareModel.marks)
                {
                    marksheets.gradedBy = authUser.ItsId;
                    questionareModel.gradedBy = authUser.ItsId;
                    marksheets.marks = Int32.Parse(questionareModel.marks);
                    marksheets.remarks = questionareModel.remarks;
                    questionareModel.teacher = new trainingCandidate
                    {
                        itsId = marksheets.cst.teacherITS,
                        name = marksheets.cst.teacherITSNavigation.fullName,
                        email = marksheets.cst.teacherITSNavigation.emailAddress,
                        contactNum = marksheets.cst.teacherITSNavigation.mobileNo
                    };

                }

                _context.SaveChanges();
                return Ok(questionareModel);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomErrorResponse
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    TargetSite = ex.TargetSite?.Name,
                    CustomMessage = errormsg,
                    innerException = ex.InnerException.Message,
                    baseType = baseType,
                    saveResponse = saveResponse
                };
                return Ok(new { error = errorResponse, obj = jsonEle, step = errormsg });
            }
        }

        [Route("studentquestionare")]
        [HttpGet]
        public async Task<IActionResult> GetStudentQuestionare()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string errormsg = "step 0";
            string api = "studentquestionare";

            DateTime today = DateTime.Today;

            try
            {
                int academicYear = Int32.Parse(_context.global_constant
                    .Where(x => x.key == "trainingAcedemicYear")
                    .FirstOrDefault()?.value ?? "1444");

                var marksheets = _context.training_student_subject_marksheet
                    .Where(x => x.studentITS == authUser.ItsId && x.acedemicYear == academicYear)
                    .Include(x => x.cst).ThenInclude(x => x.subject)
                    .Include(x => x.cst).ThenInclude(x => x.teacherITSNavigation).ThenInclude(x => x.mauzeNavigation)
                    .Include(x => x.studentITSNavigation).ThenInclude(x => x.mauzeNavigation)
                    .ToList();

                var studentQuestionnaire = new List<TrainingStudentQuestionareModel>();

                if (marksheets.Count < 1)
                {
                    return Ok(studentQuestionnaire);
                }

                foreach (var studentSubject in marksheets)
                {
                    errormsg = "step cst:" + studentSubject.cstId + " name:" + studentSubject?.studentITSNavigation?.fullName;
                    try
                    {
                        var sqa = _mapper.Map<TrainingStudentQuestionareModel>(studentSubject);
                        CalenderModel startDateModel = _helperService.getHijriDate(sqa.startDate);
                        CalenderModel endDateModel = _helperService.getHijriDate(sqa.endDate);

                        sqa.hijriStart = startDateModel.hijDay + "-" + startDateModel.hijMonth + "-" + startDateModel.hijYear;
                        sqa.hijriEnd = endDateModel.hijDay + "-" + endDateModel.hijMonth + "-" + endDateModel.hijYear;
                        sqa.hijriMonth = startDateModel.hijMonthName;

                        studentQuestionnaire.Add(sqa);
                    }
                    catch (Exception e)
                    {
                        return BadRequest($"Error in mapping studentSubjectId: {studentSubject.id}, Error: {e.Message}");
                    }
                }

                studentQuestionnaire = studentQuestionnaire.OrderBy(x => x.startDate).ToList();
                return Ok(studentQuestionnaire);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}, ErrorMsg: {errormsg}");
            }
        }

        [Route("classsubjectquestionare/{classId}/{subjectId}")]
        [HttpGet]
        public async Task<IActionResult> getStudentQuestionare(int classId, int subjectId)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            string errormsg = "step 0";
            string api = "classsubjectquestionare";
            DateTime today = DateTime.Today;

            try
            {
                int acedemicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault()?.value ?? "1444");
                training_class_subject_teacher cstEntity = _context.training_class_subject_teacher.Where(x => x.subjectId == subjectId && x.classId == classId).FirstOrDefault();
                if (cstEntity == null)
                {
                    return BadRequest(new { message = " CST Not found" });
                }
                List<training_student_subject_marksheet> marksheets = _context.training_student_subject_marksheet
                    .Where(x => x.cstId == cstEntity.id)
                    .Include(x => x.studentITSNavigation).ThenInclude(x => x.mauzeNavigation)
                    .Include(x => x.cst).ThenInclude(x => x.subject)
                    .Include(x => x.cst).ThenInclude(x => x.teacherITSNavigation).ThenInclude(x => x.mauzeNavigation)
                    .ToList();
                List<TrainingStudentQuestionareModel> studentQuationare = new List<TrainingStudentQuestionareModel>();

                if (marksheets.Count < 1)
                {
                    return Ok(studentQuationare);
                }

                foreach (training_student_subject_marksheet studentSubject in marksheets)
                {
                    errormsg = "step cst:" + studentSubject.cstId + " name:" + studentSubject?.studentITSNavigation?.fullName;
                    try
                    {
                        TrainingStudentQuestionareModel sqa = _mapper.Map<TrainingStudentQuestionareModel>(studentSubject);
                        CalenderModel startDateModel = _helperService.getHijriDate(sqa.startDate);
                        CalenderModel endDateModel = _helperService.getHijriDate(sqa.endDate);

                        sqa.hijriStart = startDateModel.hijDay + "-" + startDateModel.hijMonth + "-" + startDateModel.hijYear;
                        sqa.hijriEnd = endDateModel.hijDay + "-" + endDateModel.hijMonth + "-" + endDateModel.hijYear;
                        sqa.hijriMonth = startDateModel.hijMonthName;

                        studentQuationare.Add(sqa);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.ToString() + " issue in translator studentSUb-" + studentSubject.id);
                    }
                }
                studentQuationare = studentQuationare.OrderBy(x => x.startDate).ToList();
                return Ok(studentQuationare);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("updateAllTrainingStatus")]
        [HttpGet]
        public async Task<IActionResult> updateAllTrainingStatus()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            string msg = "step : 0 , ittaration: 0";
            //INotificationService notify = ServiceFactory.GetNotificationService();
            //WhatsAppApiService wa = new WhatsAppApiService();

            try
            {
                List<training_class_subject_teacher> allCSt = _context.training_class_subject_teacher.Where(x => x.status != "Completed").ToList();
                int count = 0;
                msg += "allcst :" + allCSt.Count();
                foreach (training_class_subject_teacher x in allCSt)
                {
                    msg += ", step : 1 , ittaration: " + count + " startDate: " + x.startDate + " endDate: " + x.endDate;

                    List<training_student_subject_marksheet> inActivemarksheets = x.training_student_subject_marksheet.Where(y => today < y.startDate).ToList();
                    inActivemarksheets.ForEach(y => y.status = "In Active");
                    msg += ", step : 2 -" + inActivemarksheets.Count() + " , ittaration: " + count;

                    List<training_student_subject_marksheet> activemarksheets = x.training_student_subject_marksheet.Where(y => today >= y.startDate && today <= y.endDate).ToList();
                    activemarksheets.ForEach(y =>
                    {
                        if (y.status != "Submitted")
                        {
                            y.status = "Active";
                        }
                        String stuName = y.studentITSNavigation.fullName;
                        String SubjectName = y.cst.subject.name;
                        try
                        {
                            if (y.startDate == today && y.status == "Active")
                            {
                                String msg2 = "Salam jameel,\n*" + stuName + "* \nYour next assignment start date of a " + SubjectName + " is today.\nKindly check the training module for more details.\nwww.mahadalzahra.org > HR login > Burger menu > Professional Development > Training Student.\nWa al-Salaam  \n\n\n*Do Not Reply To this message";
                                //String msg = "testing";
                                string emailbody = @"<b>" + stuName + @"</b>
                                    <br />
                                    <br />
                                    Your next assignment start date of a <b>" + SubjectName + @"</b> is today.
                                    <br />
                                    <br />
                                    Kindly check the training module for more details.
                                    <br />
                                    <br />
                                    www.mahadalzahra.org > HR login > Burger menu > Professional Development > Training Student.";
                                _notificationService.SendStandardHTMLEmail("Mahad al-Zahra Training " + SubjectName + " Started", emailbody, y.studentITSNavigation.emailAddress, "training");
                                if (y.studentITSNavigation.whatsappNo != null)
                                {
                                    List<string> num = new List<string> { y.studentITSNavigation.c_codeWhatsapp + y.studentITSNavigation.whatsappNo };
                                    _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2, "Training Module Notification");

                                }
                            }
                            if (y.endDate.AddDays(-3) <= today && y.status == "Active")
                            {
                                int difference = y.endDate.DayNumber - today.DayNumber;
                                String msg2 = "Salam jameel,\n*" + stuName + "* \nThe last date for submitting the " + SubjectName + " assignment is due in " + difference + " days.\nKindly check the training module for more details.\nwww.mahadalzahra.org > HR login > Burger menu > Professional Development > Training Student.\nWa al-Salaam  \n\n\n*Do Not Reply To this message";
                                //String msg = "testing";
                                string emailbody = @"<b>" + stuName + @"</b>
                                    <br />
                                    <br />
                                    The last date for submitting the <b>" + SubjectName + @"</b>  assignment is due in " + difference + @" days.
                                    < br />
                                    <br />
                                    Kindly check the training module for more details.
                                    <br />
                                    <br />
                                    www.mahadalzahra.org > HR login > Burger menu > Professional Development > Training Student.";
                                _notificationService.SendStandardHTMLEmail("Mahad al-Zahra Training " + SubjectName + " Started", emailbody, y.studentITSNavigation.emailAddress, "training");
                                if (y.studentITSNavigation.whatsappNo != null)
                                {
                                    List<string> num = new List<string> { y.studentITSNavigation.c_codeWhatsapp + y.studentITSNavigation.whatsappNo };
                                    _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2, "Training Module Notification");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            msg += ", notificationError " + stuName + " : " + e.ToString();
                        }
                    });
                    msg += ", step : 3 -" + activemarksheets.Count() + " , ittaration: " + count;


                    List<training_student_subject_marksheet> postMarksheets = x.training_student_subject_marksheet.Where(y => today > y.endDate).ToList();

                    postMarksheets.ForEach(y =>
                    {
                        if (String.IsNullOrEmpty(y.answers))
                        {
                            y.marks = 0;
                            y.status = "Expired";
                        }
                        if (y.marks == null && y.gradedBy == null && y.status == "Submitted")
                        {
                            y.status = "In Review";
                        }
                    });
                    msg += ", step : 4 -" + postMarksheets.Count() + " , ittaration: " + count;
                    count++;
                };

                List<training_class_subject_teacher> inactiveCst = allCSt.Where(x => today < x.startDate).ToList();
                inactiveCst.ForEach(x =>
                {
                    x.status = "In Active";
                });

                List<training_class_subject_teacher> activeCst = allCSt.Where(x => today >= x.startDate && today <= x.endDate).ToList();
                activeCst.ForEach(x =>
                {
                    x.status = "Active";
                    String teacherName = x.teacherITSNavigation.fullName;
                    String SubjectName = x.subject.name;
                    try
                    {
                        if (x.startDate == today && x.status == "Active")
                        {
                            String msg2 = "Salam jameel,\n*" + teacherName + "* \nYour assigned darajah's next " + SubjectName + " starts today.\nKindly check the training module for more details.\nwww.mahadalzahra.org > HR login > Burger menu > Academic > Training Teacher.\nWa al-Salaam  \n\n\n*Do Not Reply To this message";
                            //String msg = "testing";
                            string emailbody = @"<b>" + teacherName + @"</b>
                                    <br />
                                    <br />
                                    Your assigned darajah's next <b>" + SubjectName + @"</b> starts today.
                                    <br />
                                    <br />
                                    Kindly check the training module for more details.
                                    <br />
                                    <br />
                                    www.mahadalzahra.org > HR login > Burger menu > Academic > Training Teacher.";
                            _notificationService.SendStandardHTMLEmail("Mahad al-Zahra Training " + SubjectName + " Started", emailbody, x.teacherITSNavigation.emailAddress, "training");
                            if (x.teacherITSNavigation.whatsappNo != null)
                            {
                                List<string> num = new List<string> { x.teacherITSNavigation.c_codeWhatsapp + x.teacherITSNavigation.whatsappNo };
                                _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2, "Training Module Notification");
                            }
                        }
                        if (x.endDate.AddDays(-1) <= today && x.status == "Active")
                        {

                            String msg2 = "Salam jameel,\n*" + teacherName + "* \nToday is the last date for your darajah to submit the " + SubjectName + " assignment.\nKindly remind your students to submit if not done yet.\nAlso, request to start the evaluation from tomorrow.\nKindly check the training module for more details.\nwww.mahadalzahra.org > HR login > Burger menu > Professional Development > Training Student.\nWa al-Salaam  \n\n\n*Do Not Reply To this message";
                            //String msg = "testing";
                            string emailbody = @"<b>" + teacherName + @"</b>
                                    <br />
                                    <br />
                                    Today is the last date for your darajah to submit the " + SubjectName + @" assignment.
                                    <br />
                                    Kindly remind your students to submit if not done yet.
                                    <br />
                                    Also, request to start the evaluation from tomorrow.
                                    < br />
                                    <br />
                                    Kindly check the training module for more details.
                                    <br />
                                    <br />
                                    www.mahadalzahra.org > HR login > Burger menu > Academic > Training Teacher.";
                            _notificationService.SendStandardHTMLEmail("Mahad al-Zahra Training " + SubjectName + " Started", emailbody, x.teacherITSNavigation.emailAddress, "training");
                            if (x.teacherITSNavigation.whatsappNo != null)
                            {
                                List<string> num = new List<string> { x.teacherITSNavigation.c_codeWhatsapp + x.teacherITSNavigation.whatsappNo };
                                _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2, "Training Module Notification");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        msg += ", notificationError " + teacherName + " : " + e.ToString();
                    }
                });


                List<training_class_subject_teacher> expiredCst = allCSt.Where(x => today > x.endDate).ToList();
                expiredCst.ForEach(x =>
                {
                    x.status = "Expired";
                    if (!x.training_student_subject_marksheet.Any(y => x.status != "Checked"))
                    {
                        x.status = "Completed";
                    }
                });

                _context.SaveChanges();

                return Ok("today - " + today.ToString() + " update successfull -" + msg);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString() + msg);
            }

        }
    }
}
