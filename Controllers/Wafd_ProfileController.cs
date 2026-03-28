using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Wafd_ProfileController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;
        private readonly HijriCalenderService _hijriCalenderService;

        public Wafd_ProfileController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _salaryService = new SalaryService(context);
            _helperService = new HelperService(context);
            _hijriCalenderService = new HijriCalenderService(context);
            _itsService = new ItsServiceRemote();
            _jhsService = new IJHSServiceRemote();
            _globalConstants = new globalConstants();
        }
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);


        [Route("getdegreedropdown")]
        [HttpGet]
        public async Task<ActionResult> getdegreedropdown()
        {
            string api = "getdegreedropdown";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<wafdprofile_dropdown_degree> d = _context.wafdprofile_dropdown_degree.ToList();

                d = d.OrderBy(x => x.name).ToList();
                return Ok(d);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getEnglishExamLink")]
        [HttpGet]
        public async Task<ActionResult> getEnglishExamLink()
        {
            string api = "getEnglishExamLink";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                wafdprofile_english_assessment d = _context.wafdprofile_english_assessment.Where(x => x.itsId == authUser.ItsId).FirstOrDefault();


                return Ok(d);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getstagedegreedropdown")]
        [HttpGet]
        public async Task<ActionResult> getstagedegreedropdown()
        {
            string api = "getstagedegreedropdown";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<wafdprofile_qualification_stage_degree> d = _context.wafdprofile_qualification_stage_degree.ToList();


                List<string> s = d.Select(x => x.stage).Distinct().ToList();
                List<dropdown_dataset_options> d1 = new List<dropdown_dataset_options>();

                foreach (var i in s)
                {
                    d1.Add(new dropdown_dataset_options { name = i });
                }




                return Ok(new { stage = d1, s_d = d });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getcategorySubcategorydropdown")]
        [HttpGet]
        public async Task<ActionResult> getcategorysubcategorydropdown()
        {
            string api = "getcategorySubcategorydropdown";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<wafdprofile_workshops_category_subcategory> d = _context.wafdprofile_workshops_category_subcategory.ToList();


                List<string> s = d.Select(x => x.category).Distinct().ToList();
                List<dropdown_dataset_options> d1 = new List<dropdown_dataset_options>();

                foreach (var i in s)
                {
                    d1.Add(new dropdown_dataset_options { name = i });
                }

                return Ok(new { cat = d1, c_cs = d });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getfieldofinterestdropdown")]
        [HttpGet]
        public async Task<ActionResult> getfieldofinterestdropdown()
        {
            string api = "getfieldofinterestdropdown";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<wafdprofile_dropdown_fieldofinterest> d = _context.wafdprofile_dropdown_fieldofinterest.ToList();
                d = d.OrderBy(x => x.name).ToList();
                return Ok(d);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getcategorydropdown")]
        [HttpGet]
        public async Task<ActionResult> getcategorydropdown()
        {
            string api = "getcategorydropdown";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<wafdprofile_dropdown_category> d = _context.wafdprofile_dropdown_category.ToList();
                d = d.OrderBy(x => x.name).ToList();
                return Ok(d);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getauthoredtitledropdown")]
        [HttpGet]
        public async Task<ActionResult> getauthoredtitledropdown()
        {
            string api = "getauthoredtitledropdown";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<wafdprofile_dropdown_authoredcategory> d = _context.wafdprofile_dropdown_authoredcategory.ToList();
                d = d.OrderBy(x => x.name).ToList();
                return Ok(d);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("gettitlecategorydropdown")]
        [HttpGet]
        public async Task<ActionResult> gettitlecategorydropdown()
        {
            string api = "gettitlecategorydropdown";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<wafdprofile_dropdown_titlecategory> d = _context.wafdprofile_dropdown_titlecategory.ToList();
                d = d.OrderBy(x => x.name).ToList();
                return Ok(d);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }



        [Route("getmodedropdown")]
        [HttpGet]
        public async Task<ActionResult> getModedropdown()
        {
            string api = "getmodedropdown";
            //// Add_ApiLogs(api);

            try
            {

                List<wafdprofile_dropdown_mode> d = _context.wafdprofile_dropdown_mode.ToList();
                d = d.OrderBy(x => x.name).ToList();
                return Ok(d);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getWorkshopcategorydropdown")]
        [HttpGet]
        public async Task<ActionResult> getWorkshopcategorydropdown()
        {
            string api = "getWorkshopcategorydropdown";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<wafdprofile_dropdown_workshopcategory> d = _context.wafdprofile_dropdown_workshopcategory.ToList();
                d = d.OrderBy(x => x.name).ToList();
                return Ok(d);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getqualificationdatanew")]
        [HttpGet]
        public async Task<ActionResult> getQualification_NewData()
        {
            string api = "getqualificationdatanew";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<wafdprofile_qualification_new> d = _context.wafdprofile_qualification_new.Where(x => x.itsid == authUser.ItsId).Select(x => new wafdprofile_qualification_new
                {
                    id = x.id,
                    country = x.country,
                    degree = x.degree,
                    institutionName = x.institutionName,
                    itsid = x.itsid,
                    mediumOfEducation = x.mediumOfEducation,
                    pursuingYear = x.pursuingYear,
                    stage = x.stage,
                    status = x.status,
                    year = x.year,
                    attachment = x.attachment
                }).ToList().OrderByDescending(x => x.year).ToList();

                khidmat_guzaar w = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId).FirstOrDefault();

                int year = 0;

                if (w.dobGregorian != null)
                {

                    List<string> dates = w.dobGregorian.Split('/').ToList();
                    year = Convert.ToInt32(dates[2]);
                    // year= Convert.ToInt32(w.DOB.Substring(w.DOB.Length - 4));
                }


                return Ok(new { data = d, year = year });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getqualificationdatanewnew/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getQualification_NewData(int itsId)
        {
            string api = "getqualificationdatanewnew/{itsId}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<wafdprofile_qualification_new> d = await _context.wafdprofile_qualification_new
                  .Where(x => x.itsid == authUser.ItsId)
                  .OrderByDescending(x => x.year)
                  .ToListAsync();


                khidmat_guzaar w = _context.khidmat_guzaar.Where(x => x.itsId == itsId).FirstOrDefault();

                int year = 0;

                if (w.dobGregorian != null)
                {

                    List<string> dates = w.dobGregorian.Split('/').ToList();
                    year = Convert.ToInt32(dates[2]);
                    // year= Convert.ToInt32(w.DOB.Substring(w.DOB.Length - 4));
                }


                return Ok(new { data = d, year = year });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getworkshopdatanew")]
        [HttpGet]
        public async Task<ActionResult> getWorkshop_NewData()
        {
            string api = "getworkshopdatanew";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<wafdprofile_workshop_data> d = _context.wafdprofile_workshop_data.Where(x => x.itsId == authUser.ItsId).ToList().OrderByDescending(x => x.academicYear).ToList();


                khidmat_guzaar w = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId).FirstOrDefault();

                int year = 0;

                //if (w.DOB != null)
                //{
                //    year = Convert.ToInt32(w.DOB.Substring(w.DOB.Length - 4));
                //}


                return Ok(new { data = d, year = year });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getworkshopdatanewnewnew/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getWorkshop_NewData_New(int itsId)
        {
            string api = "getworkshopdatanewnewnew/{itsId}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<wafdprofile_workshop_data> d = _context.wafdprofile_workshop_data.Where(x => x.itsId == itsId).ToList().OrderByDescending(x => x.completionDate).ToList();

                //khidmat_guzaar w = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId).FirstOrDefault();

                int year = 0;

                //if (w.DOB != null)
                //{
                //    year = Convert.ToInt32(w.DOB.Substring(w.DOB.Length - 4));
                //}


                return Ok(new { data = d, year = year });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getcurrentdarajahandacedemicyear")]
        [HttpGet]
        public async Task<ActionResult> getCurrentdarajahandAcedemicYear()
        {
            string api = "getcurrentdarajahandacedemicyear";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                HijriCalenderService service = new HijriCalenderService();
                string currentAcedemicYear = service.getAcedemicYear(DateTime.Today).acedemicYearName;
                int darajah = 0;
                //khidmat_guzaar wafd = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId && x.employeeType == "Khidmatguzaar").FirstOrDefault();
                employee_academic_details wafd = (from kh in _context.khidmat_guzaar
                                                  where kh.itsId == authUser.ItsId
                                                  join ead in _context.employee_academic_details on kh.itsId equals ead.itsId
                                                  select ead).FirstOrDefault();
                if (wafd != null)
                {
                    //darajah = insha.getWafdCurrentClass(wafd.FariqYear, wafd.Daras_darajah);

                    if (wafd.wafdClassId != null)
                    {
                        darajah = _context.nisaab_classes.Where(x => x.id == wafd.wafdClassId).FirstOrDefault().std ?? 0;

                    }
                }


                return Ok(new { acedemicYearName = currentAcedemicYear, darajah = darajah });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("addqualificationnew")]
        [HttpPost]
        public async Task<ActionResult> AddQualification_New(WafdprofileQualification model)
        {
            string api = "addqualificationnew";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                byte[] binaryData1 = Convert.FromBase64String(model.attachment ?? "");
                MemoryStream stream1 = new MemoryStream(binaryData1);
                if (!string.IsNullOrEmpty(model.attachment))
                {
                    model.attachment = await _helperService.UploadFileToS3(stream1, model.itsid + "_" + model.institutionName + "_" + model.degree + "_" + model.attachmentfilename, "uploads/qualification");
                }

                var i = new wafdprofile_qualification_new
                {
                    country = model.country,
                    degree = model.degree,
                    institutionName = model.institutionName,
                    itsid = authUser.ItsId,
                    mediumOfEducation = model.mediumOfEducation,
                    pursuingYear = model.pursuingYear,
                    stage = model.stage,
                    status = model.status,
                    year = model.year,
                    attachment = model.attachment
                };
                _context.wafdprofile_qualification_new.Add(i);

                _context.SaveChanges();

                return Ok(i.id);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("addworkshopnew")]
        [HttpPost]
        public async Task<ActionResult> AddWorkshop_New(WorkShopModel model)
        {
            string api = "addworkshopnew";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


                CalenderModel todayacedemic = _hijriCalenderService.getAcedemicYear(new DateTime(model.completionDate?.Year ?? 0, model.completionDate?.Month ?? 0, model.completionDate?.Day ?? 0));

                if (model.type == "Workshop")
                {
                    int h = (model.totalDays ?? 0) * (model.hoursPerDay ?? 0);

                    model.totalHours = h;
                }

                var ii = _context.wafdprofile_workshop_data.Where(x => x.itsId == authUser.ItsId && x.cetificateCredentials == model.cetificateCredentials).FirstOrDefault();

                if (ii != null)
                {
                    return BadRequest(new { message = "Entered Certificate Credentials already exists!" });
                }


                byte[] binaryData1 = Convert.FromBase64String(model.attachment ?? "");
                MemoryStream stream1 = new MemoryStream(binaryData1);
                if (!string.IsNullOrEmpty(model.attachment))
                {
                    model.attachment = await _helperService.UploadFileToS3(stream1, authUser.ItsId + "_" + model.courseName + "_" + model.category + "_" + model.attachmentFileName, "uploads/qualification");
                }

                var i = new wafdprofile_workshop_data
                {
                    academicYear = todayacedemic.acedemicYear,
                    completionDate = model.completionDate,
                    hoursPerDay = Convert.ToInt32(model.hoursPerDay),
                    totalDays = Convert.ToInt32(model.totalDays),
                    totalHours = Convert.ToInt32(model.totalHours),
                    course = model.course,
                    keypoints = model.keypoints,
                    courseName = model.courseName,
                    subCategory = model.subCategory,
                    mode = model.mode,
                    type = model.type,
                    category = model.category,
                    cetificateCredentials = model.cetificateCredentials,
                    itsId = authUser.ItsId,
                    year = model.completionDate?.Year.ToString(),
                    attachment = model.attachment
                };
                _context.wafdprofile_workshop_data.Add(i);

                _context.SaveChanges();

                return Ok(i.id);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("deletequalificationnew/{id}")]
        [HttpDelete]
        public async Task<ActionResult> deleteQualificationNew(int id)
        {
            string api = "deletequalificationnew/{id}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                wafdprofile_qualification_new m = _context.wafdprofile_qualification_new.Where(x => x.id == id).FirstOrDefault();

                _context.wafdprofile_qualification_new.Remove(m);
                _context.SaveChanges();


                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("deleteworkshopnew/{id}")]
        [HttpDelete]
        public async Task<ActionResult> deleteWorkshopNew(int id)
        {
            string api = "deleteworkshopnew/{id}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                wafdprofile_workshop_data m = _context.wafdprofile_workshop_data.Where(x => x.id == id).FirstOrDefault();

                _context.wafdprofile_workshop_data.Remove(m);
                _context.SaveChanges();


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        private int GetWeekdayInRange(DateTime from, DateTime to, int day)
        {
            const int daysInWeek = 7;
            var result = new List<DateTime>();
            var daysToAdd = (day - (int)from.DayOfWeek + daysInWeek) % daysInWeek;

            do
            {
                from = from.AddDays(daysToAdd);
                result.Add(from);
                daysToAdd = daysInWeek;
            } while (from < to);

            return result.Count;
        }
    }
}
