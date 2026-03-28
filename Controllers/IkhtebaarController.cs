using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IkhtebaarController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        public IkhtebaarController(mzdbContext context, IMapper mapper, TokenService tokenService)
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
        readonly DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);


        [Route("gettopperformersincategory/{type}/{cutoffmarks}")]
        [HttpGet]
        public async Task<IActionResult> gettoppersinCategotry(string type, int cutoffmarks)
        {
            List<dynamic> model = new List<dynamic>();
            string api = "api/ikhtebaar/createteacher";
            ////// Add_ApiLogs(api);


            //AuthUser authUser = ServiceFactory.GetAuthService().GetAuthUser(Http_context.Current.User);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            try
            {

                List<ikhtibaar_marksheet> toppers = _context.ikhtibaar_marksheet.Where(x => x.type == type && x.totalMarks >= cutoffmarks && x.ikhtibaarId == 1).ToList();
                List<ikhtibaar_questionnaire> ikh = _context.ikhtibaar_questionnaire.Where(x => x.ikhtibaarId == 1).ToList();

                if (toppers.Count == 0)
                {
                    return BadRequest(new { message = "no marksheet found with type -" + type + " & cutOff Marks above " + cutoffmarks.ToString() });
                }

                foreach (ikhtibaar_marksheet marksheets in toppers)
                {
                    khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == marksheets.itsId).FirstOrDefault();
                    int venid = _context.employee_dept_salary.Where(x => x.itsId == marksheets.itsId).FirstOrDefault().its.mauze ?? 0;
                    string ven = _context.venue.Where(x => x.Id == venid).FirstOrDefault().displayName;
                    int hyear = _context.employee_academic_details.Where(x => x.itsId == marksheets.itsId).FirstOrDefault().hifzSanadYear ?? 0;
                    var marks = JsonConvert.DeserializeObject<Dictionary<string, string>>(marksheets.marks);

                    model.Add(new
                    {
                        venue = ven,
                        idara = kg.its_preferredIdara,
                        hifzyear = hyear,
                        question1marks = int.Parse(marks["1"]),
                        question2marks = int.Parse(marks["2"]),
                        question3marks = int.Parse(marks["3"]),
                        ahkaamMarks = int.Parse(marks["5"]),
                        question4marks = int.Parse(marks["4"]),
                        ravaniMarks = int.Parse(marks["6"]),
                        its = kg.itsId.ToString(),
                        kgname = kg.fullName,
                        mukhtabir = marksheets.mukhtabir,
                        name = marksheets.ikhtibaar.name,
                        remarks = marksheets.remarks,
                        status = marksheets.hasAttempted ? "Attempted" : "Not Attemted",
                        totalweight = "100",
                        totalMarks = marksheets.totalMarks,
                        type = marksheets.type
                    }); ;
                }


                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("getround2toppers")]
        [HttpGet]
        public async Task<IActionResult> getround2toppers()
        {
            List<dynamic> model = new List<dynamic>();
            string api = "getround2toppers";
            ////// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            try
            {

                List<ikhtibaar_marksheet> toppers1 = _context.ikhtibaar_marksheet.Where(x => x.type == "Category-1 (Age 17-24)" && x.ikhtibaarId == 4).OrderByDescending(x => x.totalMarks).Take(3).ToList();
                List<ikhtibaar_marksheet> toppers2 = _context.ikhtibaar_marksheet.Where(x => x.type == "Category-2 (Age 25-55)" && x.ikhtibaarId == 4).OrderByDescending(x => x.totalMarks).Take(4).ToList();
                List<ikhtibaar_questionnaire> ikh = _context.ikhtibaar_questionnaire.Where(x => x.ikhtibaarId == 4).ToList();

                if (toppers1.Count == 0)
                {
                    return BadRequest(new { message = "round 2 toppers not found in category 1" });
                }
                if (toppers2.Count == 0)
                {
                    return BadRequest(new { message = "round 2 toppers not found in category 2" });
                }

                foreach (ikhtibaar_marksheet marksheets in toppers2)
                {
                    khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == marksheets.itsId).FirstOrDefault();
                    int venid = _context.employee_dept_salary.Where(x => x.itsId == marksheets.itsId).FirstOrDefault().its.mauze ?? 0;
                    string ven = _context.venue.Where(x => x.Id == venid).FirstOrDefault().displayName;
                    int hyear = _context.employee_academic_details.Where(x => x.itsId == marksheets.itsId).FirstOrDefault().hifzSanadYear ?? 0;
                    var marks = JsonConvert.DeserializeObject<Dictionary<string, string>>(marksheets.marks);

                    model.Add(new
                    {
                        venue = ven,
                        idara = kg.its_preferredIdara,
                        hifzyear = hyear,
                        question1marks = int.Parse(marks["11"]),
                        question2marks = int.Parse(marks["12"]),
                        question3marks = int.Parse(marks["13"]),
                        ahkaamMarks = int.Parse(marks["15"]),
                        question4marks = int.Parse(marks["14"]),
                        ravaniMarks = int.Parse(marks["16"]),
                        its = kg.itsId.ToString(),
                        kgname = kg.fullName,
                        mukhtabir = marksheets.mukhtabir,
                        name = marksheets.ikhtibaar.name,
                        remarks = marksheets.remarks,
                        status = marksheets.hasAttempted ? "Attempted" : "Not Attemted",
                        totalweight = "100",
                        totalMarks = marksheets.totalMarks,
                        type = marksheets.type
                    }); ;
                }
                foreach (ikhtibaar_marksheet marksheets in toppers1)
                {
                    khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == marksheets.itsId).FirstOrDefault();
                    int venid = _context.employee_dept_salary.Where(x => x.itsId == marksheets.itsId).FirstOrDefault().its.mauze ?? 0;
                    string ven = _context.venue.Where(x => x.Id == venid).FirstOrDefault().displayName;
                    int hyear = _context.employee_academic_details.Where(x => x.itsId == marksheets.itsId).FirstOrDefault().hifzSanadYear ?? 0;
                    var marks = JsonConvert.DeserializeObject<Dictionary<string, string>>(marksheets.marks);

                    model.Add(new
                    {
                        venue = ven,
                        idara = kg.its_preferredIdara,
                        hifzyear = hyear,
                        question1marks = int.Parse(marks["11"]),
                        question2marks = int.Parse(marks["12"]),
                        question3marks = int.Parse(marks["13"]),
                        ahkaamMarks = int.Parse(marks["15"]),
                        question4marks = int.Parse(marks["14"]),
                        ravaniMarks = int.Parse(marks["16"]),
                        its = kg.itsId.ToString(),
                        kgname = kg.fullName,
                        mukhtabir = marksheets.mukhtabir,
                        name = marksheets.ikhtibaar.name,
                        remarks = marksheets.remarks,
                        status = marksheets.hasAttempted ? "Attempted" : "Not Attemted",
                        totalweight = "100",
                        totalMarks = marksheets.totalMarks,
                        type = marksheets.type
                    }); ;
                }


                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("trainingresult")]
        [HttpGet]
        public async Task<IActionResult> getTrainingResult()
        {
            List<List<trainingData>> modelFinal = new List<List<trainingData>>();
            string api = "getround2toppers";
            ////Add_ApiLogs(api)
            string itsId = "";
            string type = "";
            string step = "";

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            try
            {
                List<ikhtibaar_marksheet> marksheetData = _context.ikhtibaar_marksheet.Where(x => x.ikhtibaarId == 7).ToList();
                marksheetData = marksheetData.OrderByDescending(x => Int32.Parse(x.type)).ToList();
                List<ikhtibaar_marksheet> marksheetGrp = marksheetData.GroupBy(x => x.type).Select(x => x.First()).ToList();

                step = "1";
                foreach (ikhtibaar_marksheet marksheets in marksheetGrp)
                {
                    List<ikhtibaar_marksheet> marksheetDatafiltered = marksheetData.Where(x => x.type == marksheets.type).ToList();
                    List<trainingData> model = new List<trainingData>();

                    step = "3";
                    type = marksheets.type;

                    foreach (ikhtibaar_marksheet individualMarksheets in marksheetDatafiltered)
                    {
                        itsId = individualMarksheets.itsId.ToString();
                        khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == individualMarksheets.itsId).Include(x => x.employee_academic_details).FirstOrDefault();
                        if (kg == null)
                        {
                            continue;
                        }
                        employee_academic_details acd = kg.employee_academic_details;
                        var marks = JsonConvert.DeserializeObject<Dictionary<string, string>>(individualMarksheets.marks);
                        int outOfMarks = Int32.Parse(marks["71"]);
                        step = "4";

                        trainingData mod = new trainingData();

                        mod.rank = marks["72"] ?? "N/A";
                        mod.its = itsId ?? "N/A";
                        mod.name = kg.fullName ?? "N/A";
                        mod.trainingDarajah = individualMarksheets?.type ?? "N/A";
                        mod.prevRank = marks["60"] ?? "N/A";
                        mod.sabaqHazri = marks["73"] ?? "N/A";
                        mod.mauze = marks["62"] ?? "N/A";
                        mod.farigDarajah = acd?.farigDarajah ?? 0;
                        mod.hifzyear = acd?.hifzSanadYear ?? 0;
                        mod.quran = marks["63"] ?? "N/A";
                        step = "5";
                        mod.maqaraat = marks["64"] ?? "N/A";
                        mod.essay = marks["65"] ?? "N/A";
                        mod.bookReview = marks["66"] ?? "N/A";
                        mod.nazam = marks["67"] ?? "N/A";
                        mod.istinsakh = marks["68"] ?? "";
                        mod.performanceReview = marks["74"] ?? "";
                        mod.qualification = marks["78"] ?? "";
                        mod.qualificationMarks = marks["69"] ?? "";
                        mod.courses = marks["70"] ?? "";
                        mod.coursesNames = marks["77"] ?? "";
                        mod.hoursSpent = marks["75"] ?? "";
                        mod.coursesMarks = marks["76"] ?? "";
                        mod.totalweight = marks["71"] ?? "";
                        step = "6";
                        mod.totalMarks = "" + (individualMarksheets?.totalMarks ?? 0);
                        mod.percentage = "" + ((Double)((individualMarksheets?.totalMarks ?? 0) / outOfMarks) * 100).ToString("#.##");
                        mod.type = individualMarksheets?.type ?? "N/A";
                        step = "7";
                        model.Add(mod);
                        step = "8";
                    }
                    model = model.OrderBy(x => Int32.Parse(x.rank)).ToList();
                    modelFinal.Add(model);
                }

                return Ok(modelFinal);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString() + "step = " + step + " its = " + itsId + " type = " + type);
            }
        }

        [Route("Nisaab/imtehaanresult/{itsId}")]
        [HttpGet]
        public async Task<IActionResult> getImtehaanResult(int itsId)
        {
            string api = "api/NisaabTalabatData/imtehaanresult/{itsId}";

            try
            {
                nisaabtalabat_results result = new nisaabtalabat_results();

                result = _context.nisaabtalabat_results.Where(x => x.itsId == itsId && x.resultYear == 1445).FirstOrDefault();





                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("selftrainingresult")]
        [HttpGet]
        public async Task<IActionResult> getselfTrainingResult()
        {
            string api = "selftrainingresult";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            try
            {

                ikhtibaar_marksheet marksheetData = _context.ikhtibaar_marksheet.Where(x => x.ikhtibaarId == 7 && x.itsId == authUser.ItsId).FirstOrDefault();

                khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == marksheetData.itsId).Include(x => x.employee_academic_details).FirstOrDefault();
                employee_academic_details acd = kg.employee_academic_details;
                if (acd == null)
                {
                    acd = new employee_academic_details();
                }
                var marks = JsonConvert.DeserializeObject<Dictionary<string, string>>(marksheetData.marks);

                trainingData model = new trainingData
                {
                    rank = marks["72"],
                    its = kg.itsId.ToString(),
                    name = kg.fullName,
                    trainingDarajah = marksheetData.type,
                    prevRank = marks["60"],
                    mauze = marks["62"],
                    farigDarajah = acd.farigDarajah ?? 0,
                    hifzyear = acd.hifzSanadYear ?? 0,
                    quran = marks["63"],
                    maqaraat = marks["64"],
                    essay = marks["65"],
                    bookReview = marks["66"],
                    nazam = marks["67"],
                    istinsakh = marks["68"],
                    qualification = marks["78"],
                    qualificationMarks = marks["69"],
                    courses = marks["70"],
                    totalweight = marks["71"],
                    coursesMarks = marks["76"],
                    coursesNames = marks["77"],
                    hoursSpent = marks["75"],
                    sabaqHazri = marks["73"],
                    performanceReview = marks["74"],
                    totalMarks = "" + marksheetData.totalMarks,
                    percentage = "" + ((double)(marksheetData.totalMarks / Int32.Parse(marks["71"])) * 100).ToString("#.##"),
                    type = marksheetData.type
                };
                return Ok(model);

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("selfbatchtrainingresult")]
        [HttpGet]
        public async Task<IActionResult> getselfbatchTrainingResult()
        {
            string api = "selftrainingresult";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            try
            {
                List<trainingData> models = new List<trainingData>();
                ikhtibaar_marksheet marksheetData1 = _context.ikhtibaar_marksheet.Where(x => x.ikhtibaarId == 7 && x.itsId == authUser.ItsId).FirstOrDefault();
                if (marksheetData1 == null)
                {
                    return BadRequest(new { message = "No marksheet found for itsId = " + authUser.ItsId });
                }
                List<ikhtibaar_marksheet> marksheetDatas = _context.ikhtibaar_marksheet.Where(x => x.ikhtibaarId == 7 && x.type == marksheetData1.type).ToList();
                List<int> itsIDs = marksheetDatas.Select(x => x.itsId).ToList();

                var kgs = _context.khidmat_guzaar.Where(x => itsIDs.Contains(x.itsId)).Include(x => x.employee_academic_details);

                foreach (ikhtibaar_marksheet marksheetData in marksheetDatas)
                {

                    khidmat_guzaar kg = kgs.Where(x => x.itsId == marksheetData.itsId).FirstOrDefault();
                    employee_academic_details acd = kg.employee_academic_details;
                    if (acd == null)
                    {
                        acd = new employee_academic_details();
                    }
                    var marks = JsonConvert.DeserializeObject<Dictionary<string, string>>(marksheetData.marks);

                    trainingData model = new trainingData
                    {
                        rank = marks["72"],
                        its = kg.itsId.ToString(),
                        name = kg.fullName,
                        trainingDarajah = marksheetData.type,
                        prevRank = marks["60"],
                        mauze = marks["62"],
                        farigDarajah = acd.farigDarajah ?? 0,
                        hifzyear = acd.hifzSanadYear ?? 0,
                        quran = marks["63"],
                        maqaraat = marks["64"],
                        essay = marks["65"],
                        bookReview = marks["66"],
                        nazam = marks["67"],
                        istinsakh = marks["68"],
                        qualification = marks["78"],
                        qualificationMarks = marks["69"],
                        courses = marks["70"],
                        totalweight = marks["71"],
                        coursesMarks = marks["76"],
                        coursesNames = marks["77"],
                        hoursSpent = marks["75"],
                        sabaqHazri = marks["73"],
                        performanceReview = marks["74"],
                        totalMarks = "" + marksheetData.totalMarks,
                        percentage = "" + ((double)(marksheetData.totalMarks / Int32.Parse(marks["71"])) * 100).ToString("#.##"),
                        type = marksheetData.type
                    };


                    models.Add(model);
                }
                models = models.OrderByDescending(x => Int32.Parse(x.type)).ThenBy(x => Int32.Parse(x.rank)).ToList();
                return Ok(models);

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("qismtrainingresult")]
        [HttpGet]
        public async Task<IActionResult> getqismTrainingResult()
        {
            string api = "selftrainingresult";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            try
            {

                List<trainingData> models = new List<trainingData>();

                List<int> venueId = _context.venue.Where(x => x.qismId == authUser.qismId).Select(x => x.Id).ToList();

                List<ikhtibaar_marksheet> marksheetDatas = _context.ikhtibaar_marksheet.Where(x => x.ikhtibaarId == 7).ToList();
                List<int> itsIDs = marksheetDatas.Select(x => x.itsId).ToList();

                var kgs = _context.khidmat_guzaar.Where(x => itsIDs.Contains(x.itsId) && venueId.Contains(x.mauze ?? 0));

                foreach (ikhtibaar_marksheet marksheetData in marksheetDatas)
                {

                    khidmat_guzaar kg = kgs.Where(x => x.itsId == marksheetData.itsId).FirstOrDefault();
                    if (kg == null)
                    {
                        continue;
                    }

                    employee_academic_details acd = kg.employee_academic_details;
                    if (acd == null)
                    {
                        acd = new employee_academic_details();
                    }
                    var marks = JsonConvert.DeserializeObject<Dictionary<string, string>>(marksheetData.marks);

                    trainingData model = new trainingData
                    {
                        rank = marks["72"],
                        its = kg.itsId.ToString(),
                        name = kg.fullName,
                        trainingDarajah = marksheetData.type,
                        prevRank = marks["60"],
                        mauze = marks["62"],
                        farigDarajah = acd.farigDarajah ?? 0,
                        hifzyear = acd.hifzSanadYear ?? 0,
                        quran = marks["63"],
                        maqaraat = marks["64"],
                        essay = marks["65"],
                        bookReview = marks["66"],
                        nazam = marks["67"],
                        istinsakh = marks["68"],
                        qualification = marks["78"],
                        qualificationMarks = marks["69"],
                        courses = marks["70"],
                        totalweight = marks["71"],
                        coursesMarks = marks["76"],
                        coursesNames = marks["77"],
                        hoursSpent = marks["75"],
                        sabaqHazri = marks["73"],
                        performanceReview = marks["74"],
                        totalMarks = "" + marksheetData.totalMarks,
                        percentage = "" + ((double)(marksheetData.totalMarks / Int32.Parse(marks["71"])) * 100).ToString("#.##"),
                        type = marksheetData.type
                    };


                    models.Add(model);
                }
                models = models.OrderByDescending(x => Int32.Parse(x.type)).ThenBy(x => Int32.Parse(x.rank)).ToList();
                return Ok(models);

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("gethifzMusabaqat")]
        [HttpGet]
        public async Task<IActionResult> getHifzTopScorers([FromQuery] int? cutoffmarks, [FromQuery] int? qismId, [FromQuery] int? batchId, [FromQuery] string? type = "Mukammal Quran")
        {
            List<dynamic> model = new List<dynamic>();
            string api = "api/ikhtebaar/createteacher";
            ////// Add_ApiLogs(api);


            //AuthUser authUser = ServiceFactory.GetAuthService().GetAuthUser(Http_context.Current.User);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            try
            {

                var ikhtibaars = _context.ikhtibaar_marksheet.Where(x => x.ikhtibaarId == 8);

                if (type != null)
                {
                    ikhtibaars = ikhtibaars.Where(x => x.type == type);
                }

                if (cutoffmarks != null)
                {
                    ikhtibaars = ikhtibaars.Where(x => x.totalMarks >= cutoffmarks);
                }

                List<ikhtibaar_marksheet> ikhtibaarList = ikhtibaars.ToList();
                List<int> itsId = ikhtibaarList.Select(x => x.itsId).ToList();

                var kgs = _context.khidmat_guzaar.Where(x => itsId.Contains(x.itsId));
                if (qismId != null)
                {
                    List<int> venueId = _context.venue.Where(x => x.qismId == qismId).Select(x => x.Id).ToList();
                    kgs = kgs.Where(x => venueId.Contains(x.mauze ?? 0));
                }

                if (batchId != null)
                {
                    kgs = kgs.Where(x => x.employee_academic_details.batchId == batchId);
                }

                List<khidmat_guzaar> kg = kgs.Include(x => x.employee_academic_details).ToList();

                List<ikhtibaar_questionnaire> ikh = _context.ikhtibaar_questionnaire.Where(x => x.ikhtibaarId == 8).Include(x => x.ikhtibaar).ToList();

                if (kg.Count == 0)
                {
                    return BadRequest(new { message = "no marksheet found with type -" + type + " & cutOff Marks above " + cutoffmarks.ToString() });
                }

                foreach (khidmat_guzaar kh in kg)
                {
                    ikhtibaar_marksheet marksheets = ikhtibaarList.Where(x => x.itsId == kh.itsId).FirstOrDefault();
                    var marks = JsonConvert.DeserializeObject<Dictionary<string, string>>(marksheets.marks);

                    model.Add(new
                    {
                        venue = marks["80"],
                        idara = marks["81"],
                        hifzyear = kh.employee_academic_details.hifzSanadYear,
                        its = kh.itsId.ToString(),
                        kgname = kh.fullName,
                        name = ikh.FirstOrDefault().ikhtibaar.name,
                        remarks = marksheets.remarks,
                        status = marksheets.hasAttempted ? "Participated" : "Not Participated",
                        totalweight = "100",
                        totalMarks = marksheets.totalMarks,
                        type = marksheets.type
                    }); ;
                }

                model = model.OrderByDescending(x => x.totalMarks).ToList();

                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }

    public class trainingData
    {
        public string rank { get; set; }
        public string its { get; set; }
        public string name { get; set; }
        public string trainingDarajah { get; set; }
        public string prevRank { get; set; }
        public string mauze { get; set; }
        public int farigDarajah { get; set; }
        public int hifzyear { get; set; }
        public string quran { get; set; }
        public string maqaraat { get; set; }
        public string essay { get; set; }
        public string bookReview { get; set; }
        public string performanceReview { get; set; }
        public string nazam { get; set; }
        public string istinsakh { get; set; }
        public string sabaqHazri { get; set; }
        public string qualification { get; set; }
        public string qualificationMarks { get; set; }
        public string courses { get; set; }
        public string hoursSpent { get; set; }
        public string coursesMarks { get; set; }
        public string coursesNames { get; set; }
        public string totalweight { get; set; }
        public string totalMarks { get; set; }
        public string percentage { get; set; }
        public string type { get; set; }
    }
}
