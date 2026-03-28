using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Wafd_ul_HuffazController : ControllerBase
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

        public Wafd_ul_HuffazController(mzdbContext context, IMapper mapper, TokenService tokenService)
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
        // log4net.ILog log = // log4net.LogManager.GetLogger(typeof(Wafd_ul_HuffazController));
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        //CacheService cache = new CacheService();

        private static readonly string WafdAlHuffaz = "Wafd al-Huffaz";
        private static readonly string MahadAlZahra_KHDGZ = "Mahad al-Zahra KHDGZ";
        private static readonly string Jamea_KG = "Other Idara KG";

        private static readonly string Approved = "Approved";
        private static readonly string Rejected = "Rejected";
        private static readonly string InProgress = "InProgress";
        private static readonly string Pending = "Pending";
        private static readonly string Cleared = "Cleared";
        private static readonly string Surat = "Surat";
        private static readonly string Mumbai = "Mumbai";
        private static readonly string DatePassed = "Date Passed";

        public struct muhaffizDD
        {
            public int itsId;
            public string name;
        }

        public struct programDD
        {
            public string name;
            public List<muhaffizDD> muhaffiz;
        }

        public struct venueDD
        {
            public string name;
            public List<programDD> programs;
        }
        [Route("getvenueprogrammuhaffiz")]
        [HttpGet]
        public async Task<ActionResult> getVenueProgramMuhaffiz()
        {
            string api = "api/getvenueprogrammuhaffiz";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<registrationform_dropdown_set> psets = _context.registrationform_dropdown_set.Where(x => x.activeStatus == true).ToList();
                List<registrationform_dropdown_set> venues = psets.GroupBy(x => x.deptVenue?.venueId).Select(x => x.First()).ToList();
                List<khidmat_guzaar> kgs = _context.khidmat_guzaar.Where(x => x.employeeType == "Khidmatguzaar" || x.mz_idara == "Muhaffiz").ToList();
                List<venueDD> result = new List<venueDD>();

                try
                {
                    foreach (registrationform_dropdown_set v in venues)
                    {
                        venueDD r = new venueDD();
                        r.name = v.deptVenue.venue.displayName;
                        List<registrationform_dropdown_set> programs = psets.Where(x => x.deptVenue.venueId == v.deptVenue.venueId).ToList();
                        foreach (registrationform_dropdown_set program in programs)
                        {
                            programDD p = new programDD();
                            p.name = program.subprogram.name;

                            List<khidmat_guzaar> muhafiz = kgs.Where(x => x.employee_dept_salary.Any(y => y.deptVenueId == program.deptVenueId)).ToList();
                            if (muhafiz.Count == 0)
                            {
                                continue;
                            }
                            muhafiz.ForEach(x =>
                            {
                                p.muhaffiz.Add(new muhaffizDD
                                {
                                    itsId = x.itsId,
                                    name = x.fullName,
                                });
                            });
                            r.programs.Add(p);
                        }
                        if (r.programs.Count == 0)
                        {
                            continue;
                        }
                        result.Add(r);
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getwafdotheridaramawaze/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getwafdotheridaramawaze(int itsId)
        {
            string api = "getwafdotheridaramawaze/{itsId}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                if (itsId == 500)
                {
                    itsId = authUser.ItsId;
                }

                List<wafd_otheridara_mawaze> huffazLeaveModel = _context.wafd_otheridara_mawaze.Where(x => x.itsId == itsId).ToList();




                return Ok(huffazLeaveModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getwafdmahadpastmawaze/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getwafdmahadpastmawaze(int itsId)
        {
            string api = "getwafdmahadpastmawaze/{itsId}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<wafd_mahad_past_mawaze> masterModel = new List<wafd_mahad_past_mawaze>();

                if (itsId == 500)
                {
                    itsId = authUser.ItsId;
                }

                List<wafd_mahad_past_mawaze> model1 = _context.wafd_mahad_past_mawaze.Where(x => x.itsIs == itsId).ToList();


                var model2 = model1
                    .Select(m => new { m.fromYear, m.toYear })
                    .Distinct()
                    .ToList();

                HijriCalenderService hService = new HijriCalenderService();

                CalenderModel todayacedemic = hService.getAcedemicYear(DateTime.Today);

                foreach (var m in model2.Where(x => !(x.fromYear == (todayacedemic.acedemicYear) && x.toYear == (todayacedemic.acedemicYear))).ToList())
                {


                    List<wafd_mahad_past_mawaze> model3 = _context.wafd_mahad_past_mawaze.Where(x => x.itsIs == itsId && x.fromYear == m.fromYear && x.toYear == m.toYear).ToList();

                    string programs = "";

                    foreach (var i in model3)
                    {
                        if (programs == "")
                        {
                            programs = i.program;
                        }
                        else
                        {
                            programs = programs + " | " + i.program;

                        }
                    }

                    wafd_mahad_past_mawaze mmm = new wafd_mahad_past_mawaze()
                    {
                        id = model3.FirstOrDefault()?.id ?? 0,
                        fromYear = m.fromYear,
                        toYear = m.toYear,
                        mauze = model3.FirstOrDefault()?.mauze,
                        program = programs,

                    };

                    masterModel.Add(mmm);
                }
                return Ok(masterModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getnameanditsid")]
        [HttpGet]
        public async Task<ActionResult> getNameAndItsId()
        {
            string api = "getnameanditsid";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                khidmat_guzaar w = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId).FirstOrDefault();



                return Ok(new { name = w.fullName, itsid = w.itsId });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getnameandphoto/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getNameAndPhoto(int itsId)
        {
            string api = "getnameandphoto/{itsId}";

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                khidmat_guzaar kgs = _context.khidmat_guzaar.Where(x => x.itsId == itsId).FirstOrDefault();

                if (kgs != null)
                {
                    return BadRequest(new { message = "Searched ITS already exists!" });
                }

                ItsUser user = await _itsService.GetItsUser(itsId);
                string photoUrl;
                try
                {
                    photoUrl = await _helperService.SaveITSImage(user.Photo, itsId);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

                return Ok(new { name = user.Name, photo = photoUrl });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getwafdcurrency")]
        [HttpGet]
        public async Task<ActionResult> getWafdCurrency()
        {
            string api = "getwafdcurrency";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                khidmat_guzaar w = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId && x.employeeType == "Khidmatguzaar").FirstOrDefault();
                int v = w.mauze ?? 0;

                venue vv = _context.venue.Where(x => x.Id == v).FirstOrDefault();


                if (vv.currency == null)
                {
                    throw new Exception("Your Currency has not set");
                }



                return Ok(new { currency = vv.currency });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getwafdname/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getWafdName(int itsId)
        {
            string api = "getwafdname/{itsId}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                khidmat_guzaar w = _context.khidmat_guzaar.Where(x => x.itsId == itsId && x.employeeType == "Khidmatguzaar").FirstOrDefault();



                return Ok(new { name = w.fullName, itsid = w.itsId });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("GetHuffazData/{ItsIdara1}")]
        [HttpGet]
        public async Task<ActionResult> GetHuffazData(string ItsIdara1)
        {
            string api = "api/GetHuffazData/{ItsIdara1}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<wafd_ul_huffaz_Model> huffazModel = new List<wafd_ul_huffaz_Model>();
                List<khidmat_guzaar> huffazData = new List<khidmat_guzaar>();
                List<ExportToExcelModel> etexcel = new List<ExportToExcelModel>();
                HijriCalenderService hService = new HijriCalenderService();
                if (ItsIdara1.Equals(WafdAlHuffaz))
                {

                    huffazData = _context.khidmat_guzaar.Where(X => X.activeStatus == true && X.mz_idara.Equals(WafdAlHuffaz)).ToList();


                }
                else if (ItsIdara1.Equals(MahadAlZahra_KHDGZ))
                {

                    huffazData = _context.khidmat_guzaar.Where(X => X.activeStatus == true && X.mz_idara.Equals(MahadAlZahra_KHDGZ)).ToList();

                }
                else if (ItsIdara1.Equals(Jamea_KG))
                {

                    huffazData = _context.khidmat_guzaar.Where(X => X.activeStatus == true && X.mz_idara.Equals(Jamea_KG)).ToList();

                }


                int i = hService.getAcedemicYear(DateTime.Today).acedemicYear;
                //huffazData.ForEach(x => huffazModel.Add(Translater.ToModel(x, i)));

                PropertyInfo[] propertyInfos = typeof(wafd_ul_huffaz_Model).GetProperties();

                // writes all the property names
                int id = 1;
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {

                    etexcel.Add(new ExportToExcelModel { id = id, propertyName = propertyInfo.Name, status = false });
                    id++;
                }


                return Ok(new { huffazModel = huffazModel, etexcel = etexcel });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("GetHuffazDataNewtest/{ItsIdara1}")]
        [HttpGet]
        public async Task<ActionResult> GetHuffazDataNew(string ItsIdara1)
        {
            string api = "GetHuffazDataNewtest";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<wafd_ul_huffaz_Model> huffazModel = new List<wafd_ul_huffaz_Model>();
                List<khidmat_guzaar> huffazData = new List<khidmat_guzaar>();
                List<ExportToExcelModel> etexcel = new List<ExportToExcelModel>();
                HijriCalenderService hService = new HijriCalenderService();
                if (ItsIdara1.Equals(WafdAlHuffaz))
                {

                    huffazData = _context.khidmat_guzaar.Where(X => X.activeStatus == true && X.mz_idara.Equals(WafdAlHuffaz)).ToList();

                }
                else if (ItsIdara1.Equals(MahadAlZahra_KHDGZ))
                {

                    huffazData = _context.khidmat_guzaar.Where(X => X.activeStatus == true && X.mz_idara.Equals(MahadAlZahra_KHDGZ)).ToList();

                }
                else if (ItsIdara1.Equals(Jamea_KG))
                {

                    huffazData = _context.khidmat_guzaar.Where(X => X.activeStatus == true && X.mz_idara.Equals(Jamea_KG)).ToList();

                }


                int i = hService.getAcedemicYear(DateTime.Today).acedemicYear;
                //huffazData.ForEach(x => huffazModel.Add(Translater.ToModel(x, i)));

                PropertyInfo[] propertyInfos = typeof(wafd_ul_huffaz_Model).GetProperties();

                // writes all the property names
                int id = 1;
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {

                    etexcel.Add(new ExportToExcelModel { id = id, propertyName = propertyInfo.Name, status = false });
                    id++;
                }


                return Ok(new { huffazModel = huffazModel, etexcel = etexcel });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("api/employee/getallemployeedata/{employeetype}")]
        [HttpGet]
        public async Task<ActionResult> getAllEmployeeData(string employeeType)
        {
            string api = "api/employee/getAllemployeeData/" + employeeType;
            //// Add_ApiLogs(api);
            List<wafd_ul_huffaz_Model> huffazModel = null;

            //huffazModel = cache.GetItem<List<wafd_ul_huffaz_Model>>("getAllEmployeeData" + employeeType);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                // ServiceFactory.getHelperService().Add_ApiLogs(api, authUser, Request);

                List<khidmat_guzaar> huffazData = new List<khidmat_guzaar>();
                List<ExportToExcelModel> etexcel = new List<ExportToExcelModel>();
                List<ExportToExcel_WafdulhufazModel> etexcel2 = new List<ExportToExcel_WafdulhufazModel>();
                List<dropdown_dataset_options> mozeDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> farigDarajDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> aljameaDegreeDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> categoryDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> qismTahfeezDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> nationalityDD = new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> ageDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> dawattileDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> hifzSanadYearDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> khidmatYearDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> khidmatinmahadDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> tayeeninmahadDD = new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> itsIdDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> batchIdDD = new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> houseStatusDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> houseTypeDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> housestatusPersonalDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> houseTypePersonalDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> hifzstatusmodifiedDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> hifzstatusDD = new List<dropdown_dataset_options>();


                List<dropdown_dataset_options> trainingDarajaDD = new List<dropdown_dataset_options>();

                List<dropdown_dataset_options> nameDD = new List<dropdown_dataset_options>();

                HijriCalenderService hService = new HijriCalenderService();

                List<export_category> ExportCategory = _context.export_category.GroupBy(x => x.categoryId).Select(x => x.FirstOrDefault()).ToList();

                List<export_category> ExportCategory2 = _context.export_category.ToList();

                if (huffazModel == null)
                {
                    huffazModel = new List<wafd_ul_huffaz_Model>();
                    huffazData = _context.khidmat_guzaar.Where(x => x.employeeType == employeeType).ToList();

                    int i = hService.getAcedemicYear(DateTime.Today).acedemicYear;
                    //huffazData.ForEach(x => huffazModel.Add(Translater.ToModel1(x, i)));

                    huffazModel = huffazModel.Where(x => x.itsId != 0).OrderBy(x => x.activeStatus).ThenBy(x => x.mz_idara).ThenByDescending(x => x.batchId).ThenBy(x => x.category).ThenByDescending(x => x.farigDarajah).ThenByDescending(x => x.age).ToList();

                    //cache.AddItem("getAllEmployeeData" + employeeType, huffazModel, DateTime.Now.AddDays(30));
                }


                mozeDD = huffazModel.OrderBy(x => x.moze).GroupBy(x => x.moze).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.moze?.ToString() }).ToList();
                farigDarajDD = huffazModel.OrderBy(x => x.farigDarajah).GroupBy(x => x.farigDarajah).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.farigDarajah?.ToString() }).ToList();
                aljameaDegreeDD = huffazModel.OrderBy(x => x.alJameaDegree).GroupBy(x => x.alJameaDegree).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.alJameaDegree?.ToString() }).ToList();
                categoryDD = huffazModel.OrderBy(x => x.category).GroupBy(x => x.category).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.category?.ToString() }).ToList();
                qismTahfeezDD = huffazModel.OrderBy(x => x.qismTahfeez).GroupBy(x => x.qismTahfeez).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.qismTahfeez?.ToString() }).ToList();
                nationalityDD = huffazModel.OrderBy(x => x.nationality).GroupBy(x => x.nationality).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.nationality?.ToString() }).ToList();
                ageDD = huffazModel.OrderBy(x => x.age).GroupBy(x => x.age).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.age?.ToString() }).ToList();
                dawattileDD = huffazModel.OrderBy(x => x.title).GroupBy(x => x.title).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.title?.ToString() }).ToList();
                hifzSanadYearDD = huffazModel.OrderBy(x => x.hifzSanadYear).GroupBy(x => x.hifzSanadYear).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.hifzSanadYear?.ToString() }).ToList();
                khidmatYearDD = huffazModel.OrderBy(x => x.khidmatYear).GroupBy(x => x.khidmatYear).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.khidmatYear.ToString() }).ToList();
                khidmatinmahadDD = huffazModel.OrderBy(x => x.mahad_khidmatYear).GroupBy(x => x.mahad_khidmatYear).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.mahad_khidmatYear?.ToString() }).ToList();
                tayeeninmahadDD = huffazModel.OrderBy(x => x.tayeenYear).GroupBy(x => x.tayeenYear).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.tayeenYear?.ToString() }).ToList();
                itsIdDD = huffazModel.OrderBy(x => x.itsId).GroupBy(x => x.itsId).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.itsId.ToString() }).ToList();
                batchIdDD = huffazModel.OrderBy(x => x.batchId).GroupBy(x => x.batchId).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.batchId.ToString() }).ToList();
                houseStatusDD = huffazModel.OrderBy(x => x.khidmatMauzeHouseStatus).GroupBy(x => x.khidmatMauzeHouseStatus).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.khidmatMauzeHouseStatus?.ToString() }).ToList();
                houseTypePersonalDD = huffazModel.OrderBy(x => x.personalHouseType).GroupBy(x => x.personalHouseType).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.personalHouseType?.ToString() }).ToList();
                houseTypeDD = huffazModel.OrderBy(x => x.khdimatMauzeHouseType).GroupBy(x => x.khdimatMauzeHouseType).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.khdimatMauzeHouseType?.ToString() }).ToList();
                housestatusPersonalDD = huffazModel.OrderBy(x => x.personalHouseStatus).GroupBy(x => x.personalHouseStatus).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.personalHouseStatus?.ToString() }).ToList();
                trainingDarajaDD = huffazModel.OrderBy(x => x.currentDarajah).GroupBy(x => x.currentDarajah).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.currentDarajah }).ToList();
                nameDD = huffazModel.OrderBy(x => x.fullName).GroupBy(x => x.fullName).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.fullName }).ToList();
                hifzstatusDD = huffazModel.OrderBy(x => x.hifzStatus).GroupBy(x => x.hifzStatus).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.hifzStatus }).ToList();
                hifzstatusmodifiedDD = huffazModel.OrderBy(x => x.hifzStatusModified).GroupBy(x => x.hifzStatusModified).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.hifzStatusModified }).ToList();

                PropertyInfo[] propertyInfos = typeof(wafd_ul_huffaz_Model).GetProperties();

                //writes all the property names
                int id = 1;
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    etexcel.Add(new ExportToExcelModel { id = id, propertyName = propertyInfo.Name, status = false });
                    id++;
                }

                foreach (var k in ExportCategory2)
                {
                    etexcel.Add(new ExportToExcelModel { categoryId = k.categoryId ?? 0, id = id, propertyName = k.fieldActualName, propertyDisplayName = k.fieldDisplayName, status = false });
                    id++;
                }

                foreach (var k in ExportCategory)
                {
                    etexcel2.Add(new ExportToExcel_WafdulhufazModel { categoryId = k.categoryId ?? 0, categoryName = k.categoryName, toRemove = etexcel.Where(x => x.categoryId == k.categoryId).ToList() });
                }
                return Ok(new
                {
                    hifzstatusmodifiedDD = hifzstatusmodifiedDD,
                    hifzstatusDD = hifzstatusDD,
                    nameDD = nameDD,
                    ExportCategory2 = ExportCategory2,
                    ExportCategory = ExportCategory,
                    huffazModel = huffazModel,
                    etexcel = etexcel,
                    etexcel2 = etexcel2,
                    mozeDD = mozeDD,
                    farigDarajDD = farigDarajDD,
                    aljameaDegreeDD = aljameaDegreeDD,
                    categoryDD = categoryDD,
                    qismTahfeezDD = qismTahfeezDD,
                    nationalityDD = nationalityDD,
                    ageDD = ageDD,
                    dawattileDD = dawattileDD,
                    hifzSanadYearDD = hifzSanadYearDD,
                    khidmatYearDD = khidmatYearDD,
                    khidmatinmahadDD = khidmatinmahadDD,
                    tayeeninmahadDD = tayeeninmahadDD,
                    itsIdDD = itsIdDD,
                    trainingDarajaDD = trainingDarajaDD,
                    batchIdDD = batchIdDD,
                    housestatusPersonalDD = housestatusPersonalDD,
                    houseTypeDD = houseTypeDD,
                    houseTypePersonalDD = houseTypePersonalDD,
                    houseStatusDD = houseStatusDD
                });


            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getWorkTypeId")]
        [HttpGet]
        public async Task<ActionResult> getWorkTypeId()
        {
            string api = "getWorkTypeId";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<dropdown_dataset_options> worType = new List<dropdown_dataset_options>();

                khidmat_guzaar k = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId && x.employeeType == "Khidmatguzaar").FirstOrDefault();

                int vid = _context.employee_dept_salary.Where(y => y.itsId == k.itsId).FirstOrDefault()?.deptVenue.venueId ?? 0;
                List<kg_venue_worktype> types = _context.kg_venue_worktype.Where(x => x.venueId == vid).ToList();

                foreach (var i in types)
                {
                    kg_worktype kk = _context.kg_worktype.Where(x => x.id == i.workTypeId).FirstOrDefault();

                    worType.Add(new dropdown_dataset_options { id = i.workTypeId ?? 0, name = kk.typeName });
                }


                return Ok(worType);



            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getWorkTypeIdforadmin/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getWorkTypeId_forAdmin(int itsId)
        {
            string api = "getWorkTypeIdforadmin/{itsId}";
            //// Add_ApiLogs(api);

            try
            {




                List<dropdown_dataset_options> worType = new List<dropdown_dataset_options>();

                khidmat_guzaar k = _context.khidmat_guzaar.Where(x => x.itsId == itsId && x.employeeType == "Khidmatguzaar").FirstOrDefault();

                int vid = _context.employee_dept_salary.Where(y => y.itsId == k.itsId).FirstOrDefault()?.deptVenue.venueId ?? 0;

                List<kg_venue_worktype> types = _context.kg_venue_worktype.Where(x => x.venueId == vid).ToList();

                foreach (var i in types)
                {
                    kg_worktype kk = _context.kg_worktype.Where(x => x.id == i.workTypeId).FirstOrDefault();

                    worType.Add(new dropdown_dataset_options { id = i.workTypeId ?? 0, name = kk.typeName });


                }


                return Ok(worType);



            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getwafdfaimalydetails")]
        [HttpGet]
        public async Task<ActionResult> getWafdFaimalyDetails()
        {
            string api = "getwafdfaimalydetails";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<kg_faimalydetails> users = _context.kg_faimalydetails.Where(x => x.hofItsId == authUser.ItsId).OrderByDescending(x => x.age).ToList();


                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getwafdfaimalydetailsnew/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getWafdFaimalyDetailsNew(int itsId)
        {
            string api = "getwafdfaimalydetailsnew/{itsId}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<kg_faimalydetails> users = _context.kg_faimalydetails.Where(x => x.hofItsId == itsId).OrderByDescending(x => x.age).ToList();

                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getWafdprofileRight/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getWafdProfileRight(int itsId)
        {
            string api = "getWafdprofileRight/{itsId}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                user_deptvenue r = (from ud in _context.user_deptvenue
                                    join eds in _context.employee_dept_salary on new { x = itsId, y = ud.deptVenueId } equals new { x = eds.itsId, y = eds.deptVenueId }
                                    where ud.itsId == authUser.ItsId
                                    select ud).FirstOrDefault();

                if (r != null)
                {

                    return Ok();
                }
                else
                {
                    return BadRequest(new { message = "You do not have access to view profile of this ITS!" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getfaimalyusername/{itsid}")]
        [HttpGet]
        public async Task<ActionResult> getfaimalyusername(int itsid)
        {
            string api = "getfaimalyusername/{itsid}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);



                List<kg_faimalydetails_its> users = _context.kg_faimalydetails_its.Where(x => x.hofItsId == authUser.ItsId).ToList();

                var find = users.Where(x => x.itsId == itsid).FirstOrDefault();

                if (find == null)
                {
                    return BadRequest(new { message = "Entered ITS is not your relative" });
                }
                else
                {
                    return Ok(find.name);
                }



            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getfaimalyusernamenew/{itsid}/itsId2")]
        [HttpGet]
        public async Task<ActionResult> getfaimalyusername2(int itsid, int itsId2)
        {
            string api = "getfaimalyusernamenew/{itsid}/{itsId2}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);



                List<kg_faimalydetails_its> users = _context.kg_faimalydetails_its.Where(x => x.hofItsId == itsId2).ToList();

                var find = users.Where(x => x.itsId == itsid).FirstOrDefault();

                if (find == null)
                {
                    return BadRequest(new { message = "Entered ITS is not your relative" });
                }
                else
                {
                    return Ok(find.name);
                }



            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getidentitycarddetails")]
        [HttpGet]
        public async Task<ActionResult> getidentitycarddetails()
        {
            string api = "getidentitycarddetails";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<kg_identitycards> users = _context.kg_identitycards.Where(x => x.itsId == authUser.ItsId).ToList();


                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [AllowAnonymous]
        [Route("updateFamilyDetails")]
        [HttpGet]
        public async Task<ActionResult> updateFamilyDetails()
        {
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<kg_faimalydetails_its> users = _context.kg_faimalydetails_its.OrderByDescending(x => x.age).ToList();
                string r = await _itsService.SerializeFamilyMembers(4043403);

                return Ok(new { users, r });
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Route("getidentitycarddetailsnew/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getidentitycarddetails(int itsId)
        {
            string api = "getidentitycarddetailsnew/{itsId}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<kg_identitycards> users = _context.kg_identitycards.Where(x => x.itsId == itsId).ToList();


                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("get/hijriYearsfixeddata")]
        [HttpGet]
        public async Task<ActionResult> getHijriYears_FixedData()
        {
            string api = "get/hijriYearsfixeddata";
            //// Add_ApiLogs(api);

            string h = MahadFixedData.HijriYears;

            List<int> y = _helperService.parseIds(h);

            return Ok(y);

        }

        [Route("allInactiveStudent/{ItsIdara}/{deptVenueId}/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> GetAllInactiveHuffaz(string ItsIdara, int deptVenueId, int itsId)
        {
            string api = "allInactiveStudent/{ItsIdara}/{deptVenueId}/{itsId}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<wafd_ul_huffaz_Model> huffazModel = new List<wafd_ul_huffaz_Model>();
                List<khidmat_guzaar> huffazData = new List<khidmat_guzaar>();
                if (ItsIdara == WafdAlHuffaz)
                {

                    if (deptVenueId != 500)
                    {
                        huffazData = (from eds in _context.employee_dept_salary
                                      where eds.deptVenueId == deptVenueId
                                      join kh in _context.khidmat_guzaar on eds.itsId equals kh.itsId
                                      where kh.activeStatus == false && kh.mz_idara == WafdAlHuffaz
                                      select kh).ToList();

                    }
                    if (deptVenueId == 500)
                    {
                        List<user_deptvenue> dd = _context.user_deptvenue.Where(x => x.itsId == itsId).ToList();
                        foreach (var i in dd)
                        {
                            List<khidmat_guzaar> ww = (from eds in _context.employee_dept_salary
                                                       where eds.deptVenueId == deptVenueId
                                                       join kh in _context.khidmat_guzaar on eds.itsId equals kh.itsId
                                                       where kh.activeStatus == false && kh.mz_idara == WafdAlHuffaz
                                                       select kh).ToList();
                            foreach (var j in ww)
                            {
                                huffazData.Add(j);
                            }
                        }
                    }

                }
                else if (ItsIdara == MahadAlZahra_KHDGZ)
                {


                    if (deptVenueId != 500)
                    {
                        dept_venue d = _context.dept_venue.Where(x => x.id == deptVenueId).FirstOrDefault();
                        List<khidmat_guzaar> w1 = (from eds in _context.employee_dept_salary
                                                   where eds.deptVenueId == deptVenueId
                                                   join kh in _context.khidmat_guzaar on eds.itsId equals kh.itsId
                                                   where kh.activeStatus == false && kh.mz_idara == MahadAlZahra_KHDGZ
                                                   select kh).ToList();
                        foreach (var i in w1)
                        {
                            huffazData.Add(i);
                        }
                    }
                    if (deptVenueId == 500)
                    {
                        List<user_deptvenue> dd = _context.user_deptvenue.Where(x => x.itsId == itsId).ToList();
                        foreach (var i in dd)
                        {
                            dept_venue d = _context.dept_venue.Where(x => x.id == i.deptVenueId).FirstOrDefault();

                            List<khidmat_guzaar> w1 = (from eds in _context.employee_dept_salary
                                                       where eds.deptVenueId == deptVenueId
                                                       join kh in _context.khidmat_guzaar on eds.itsId equals kh.itsId
                                                       where kh.activeStatus == false && kh.mz_idara == MahadAlZahra_KHDGZ
                                                       select kh).ToList();
                            foreach (var j in w1)
                            {
                                huffazData.Add(j);
                            }


                        }


                    }


                }
                HijriCalenderService hService = new HijriCalenderService();
                int iii = hService.getAcedemicYear(DateTime.Today).acedemicYear;

                //huffazData.ForEach(x => huffazModel.Add(Translater.ToModel(x, iii)));
                return Ok(huffazModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("update/ITSDATA")]
        [HttpPut]
        public async Task<ActionResult> UpdateItsData_Huffaz()
        {
            string api = "update/ITSDATA";
            // //// Add_ApiLogs(api);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                List<wafd_ul_huffaz_Model> huffazModels = new List<wafd_ul_huffaz_Model>();
                string arabicName = "";


                List<khidmat_guzaar> huf = _context.khidmat_guzaar.Where(x => x.isMumin == true && x.employeeType == "Khidmatguzaar").ToList();
                // List<khidmat_guzaar> hgm = (from kh in _context.khidmat_guzaar
                //                             where kh.isMumin == true
                //                             select kh).ToList();

                var hgm = _context.khidmat_guzaar.Where(x => x.isMumin == true && x.employeeType != "Khidmatguzaar");

                int count = hgm.Count();
                int dayoftheWeek = (int)DateTime.UtcNow.DayOfWeek;
                int dailyCount = count / 7;
                int skipCount = dailyCount * dayoftheWeek;

                List<khidmat_guzaar> hgm2 = new List<khidmat_guzaar>();
                hgm2 = hgm.Skip(skipCount).Take(dailyCount).ToList();
                hgm2.AddRange(huf);

                foreach (khidmat_guzaar huffaz1 in hgm2)
                {
                    //cache.DeleteItem("getEmployeeData" + huffaz1.itsId);
                    //cache.DeleteItem("getAllEmployeeData" + huffaz1.employeeType);
                    try
                    {
                        EmployeeModel huffaz = _mapper.Map<EmployeeModel>(huffaz1);

                        ItsUser? user = await _itsService.GetItsUser(huffaz1.itsId);
                        JHSAcademicData jHSAcademicData = await _jhsService.GetJHSAcademicData(huffaz1.itsId);
                        ItsUser user_wife = new ItsUser();

                        // log.DebugFormat("ItsData == " + user);
                        if (user == null)
                        {
                            huffazModels.Add(new wafd_ul_huffaz_Model
                            {
                                itsId = huffaz1.itsId,
                                fullName = huffaz1.fullName
                            });
                            continue;
                        }
                        else
                        {
                            arabicName = user.Arabic_FullName;
                            if (huffaz.academicDetails != null)
                            {
                                huffaz.academicDetails.hifzStatus = user.hifzStatus;
                                huffaz.academicDetails.hifzSanadYear = Convert.ToInt32(user.hifzYear);
                            }
                            else
                            {
                                _context.employee_academic_details.Add(new employee_academic_details
                                {
                                    itsId = huffaz1.itsId,
                                    hifzStatus = user.hifzStatus,
                                    hifzSanadYear = Convert.ToInt32(user.hifzYear)
                                });
                            }

                            huffaz1.dobGregorian = user.Dob.Date.ToString("dd/MM/yyyy");

                            huffaz1.haddiyatYear = Convert.ToInt32(user.haddiyatYear);
                            huffaz1.mafsuhiyatYear = Convert.ToInt32(user.mafsuhiyatYear);

                            huffaz1.nationality = user.Nationality;

                            huffaz1.jamiat = user.Jamiat;
                            huffaz1.dawat_title = user.Title;
                            huffaz1.muqam = user.Maqaam;
                            huffaz1.maritalStatus = user.MaritalStatus;
                            huffaz1.its_preferredIdara = user.Idara;
                            huffaz1.jamaat = user.Jamaat;
                            huffaz1.bloodGroup = user.BloodGroup;
                            huffaz1.watanArabic = user.Vatan_Arabic;
                            huffaz1.watan = user.Vatan;
                            huffaz1.dobHijri = user.DOB_Hijri;
                            huffaz1.mobileNo = user.MobileNo;
                            huffaz1.emailAddress = user.EmailId;
                            huffaz1.age = user.Age;
                            huffaz1.fullNameArabic = arabicName;
                            huffaz1.fullName = user.Name;
                            huffaz1.photoBase64 = Convert.ToBase64String(user.Photo, 0, user.Photo.Length);
                            _context.SaveChanges();
                        }

                        await _helperService.SaveITSImage(user.Photo, huffaz1.itsId);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                _context.SaveChanges();


            }
            catch (Exception ex)
            {
                //throw new Exception(ex.ToString());
            }
            return Ok();
        }

        [AllowAnonymous]
        [Route("update/ITSDATA")]
        [HttpGet]
        public async Task<ActionResult> dailyitsupdate()
        {
            string api = "update/ITSDATA";
            // //// Add_ApiLogs(api);

            try
            {
                List<wafd_ul_huffaz_Model> huffazModels = new List<wafd_ul_huffaz_Model>();
                string arabicName = "";


                var hgm = _context.khidmat_guzaar.Where(x => x.isMumin == true);

                int count = hgm.Count();
                int dayoftheWeek = (int)DateTime.UtcNow.DayOfWeek;
                int dailyCount = count / 7;
                int skipCount = dailyCount * dayoftheWeek;

                List<khidmat_guzaar> hgm2 = hgm.Skip(skipCount).Take(dailyCount).ToList();
                foreach (khidmat_guzaar huffaz1 in hgm2)
                {
                    //cache.DeleteItem("getEmployeeData" + huffaz1.itsId);
                    //cache.DeleteItem("getAllEmployeeData" + huffaz1.employeeType);
                    try
                    {
                        var huffaz = _mapper.Map<EmployeeModel>(huffaz1);

                        ItsUser? user = await _itsService.GetItsUser(huffaz1.itsId);
                        ItsUser user_wife = new ItsUser();
                        JHSAcademicData jHSAcademicData = await _jhsService.GetJHSAcademicData(huffaz1.itsId);

                        int currentAcedemicYear = _helperService.getAcedemicYear(DateTime.Today).acedemicYear;

                        // log.DebugFormat("ItsData == " + user);
                        if (user == null)
                        {
                            huffazModels.Add(new wafd_ul_huffaz_Model
                            {
                                itsId = huffaz1.itsId,
                                fullName = huffaz1.fullName
                            });
                            continue;
                        }
                        else
                        {
                            huffaz1.UpdatedOn = DateTime.Now;
                            arabicName = user.Arabic_FullName;
                            try
                            {
                                if (huffaz.academicDetails != null)
                                {
                                    huffaz1.employee_academic_details.hifzStatus = user.hifzStatus;
                                    try
                                    {
                                        huffaz1.employee_academic_details.hifzSanadYear = Convert.ToInt32(user.hifzYear);
                                    }
                                    catch (Exception e)
                                    {
                                    }

                                    if (jHSAcademicData != null)
                                    {
                                        huffaz1.employee_academic_details.category = "Aljamea-tus-Saifiyah";
                                        huffaz1.employee_academic_details.farigDarajah = jHSAcademicData.farighDarajah;
                                        huffaz1.employee_academic_details.farigYear = jHSAcademicData.farighYear;
                                        huffaz1.employee_academic_details.aljameaDegree = jHSAcademicData.jameaDegree;
                                        huffaz1.employee_academic_details.batchId = _helperService.getWafdCurrentClass(jHSAcademicData.farighYear, jHSAcademicData.farighDarajah);
                                        huffaz1.batchId = huffaz1.employee_academic_details.batchId;

                                    }
                                    else
                                    {
                                        nisaab_alumni alum = _context.nisaab_alumni.Where(x => x.itsId == huffaz.basicDetails.itsId).FirstOrDefault();
                                        if (alum != null)
                                        {
                                            huffaz1.employee_academic_details.category = "Nisaab Mahad al-Zahra";
                                            huffaz1.employee_academic_details.farigDarajah = alum.farigDarajah;
                                            huffaz1.employee_academic_details.farigYear = alum.farigYear;
                                            huffaz1.employee_academic_details.aljameaDegree = alum.degree;
                                            huffaz1.employee_academic_details.batchId = _helperService.getWafdCurrentClass(alum.farigYear, alum.farigDarajah);
                                            huffaz1.batchId = huffaz1.employee_academic_details.batchId;
                                        }
                                    }

                                }
                                else
                                {
                                    _context.employee_academic_details.Add(new employee_academic_details
                                    {
                                        itsId = huffaz1.itsId,
                                        hifzStatus = user.hifzStatus,
                                        hifzSanadYear = Convert.ToInt32(user.hifzYear)
                                    });
                                }
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.dobGregorian = user.Dob.Date.ToString("dd/MM/yyyy");
                                _context.SaveChanges();
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.haddiyatYear = Convert.ToInt32(user.haddiyatYear);
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.mafsuhiyatYear = Convert.ToInt32(user.mafsuhiyatYear);
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }


                            try
                            {
                                huffaz1.nationality = user.Nationality;

                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.jamiat = user.Jamiat;

                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.dawat_title = user.Title;
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.muqam = user.Maqaam;
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.maritalStatus = user.MaritalStatus;
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.its_preferredIdara = user.Idara;
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.jamaat = user.Jamaat;
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.bloodGroup = user.BloodGroup;

                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.watanArabic = user.Vatan_Arabic;
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.watan = user.Vatan;
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.dobHijri = user.DOB_Hijri;
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.mobileNo = user.MobileNo;
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.emailAddress = user.EmailId;
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.age = user.Age;
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.fullNameArabic = arabicName;
                                _context.SaveChanges();

                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                huffaz1.fullName = user.Name;


                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                huffaz1.photoBase64 = Convert.ToBase64String(user.Photo, 0, user.Photo.Length);
                                _context.SaveChanges();
                            }
                            catch (Exception ex)
                            {

                            }

                            _context.SaveChanges();
                        }

                        // await _helperService.SaveITSImage(user.Photo, huffaz1.itsId);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                _context.SaveChanges();


            }
            catch (Exception ex)
            {
                //throw new Exception(ex.ToString());
            }
            return Ok();
        }

        [Route("update/ITSDATA/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> itsupdatespecific(int itsId)
        {
            string api = "update/ITSDATA/" + itsId;
            // //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                List<wafd_ul_huffaz_Model> huffazModels = new List<wafd_ul_huffaz_Model>();
                string arabicName = "";

                if (itsId == 500)
                {
                    itsId = authUser.ItsId;
                }

                khidmat_guzaar hgm = _context.khidmat_guzaar.Where(x => x.itsId == itsId).Include(x => x.employee_academic_details).FirstOrDefault();

                //cache.DeleteItem("getEmployeeData" + hgm.itsId);
                //cache.DeleteItem("getAllEmployeeData" + hgm.employeeType);
                try
                {
                    ItsUser user = await _itsService.GetItsUser(hgm.itsId);
                    ItsUser user_wife = new ItsUser();

                    JHSAcademicData jHSAcademicData = await _jhsService.GetJHSAcademicData(itsId);

                    int currentAcedemicYear = _hijriCalenderService.getAcedemicYear(DateTime.Today).acedemicYear;
                    // log.DebugFormat("ItsData == " + user);
                    if (user == null)
                    {
                        huffazModels.Add(new wafd_ul_huffaz_Model
                        {
                            itsId = hgm.itsId,
                            fullName = hgm.fullName
                        });
                    }
                    else
                    {
                        arabicName = user.Arabic_FullName;
                        try
                        {
                            if (hgm.employee_academic_details != null)
                            {
                                hgm.employee_academic_details.hifzStatus = user.hifzStatus;
                                try
                                {
                                    hgm.employee_academic_details.hifzSanadYear = Convert.ToInt32(user.hifzYear);
                                }
                                catch (Exception e)
                                { }

                                if (jHSAcademicData != null)
                                {
                                    hgm.employee_academic_details.category = "Aljamea-tus-Saifiyah";
                                    hgm.employee_academic_details.farigDarajah = jHSAcademicData.farighDarajah;
                                    hgm.employee_academic_details.farigYear = jHSAcademicData.farighYear;
                                    hgm.employee_academic_details.aljameaDegree = jHSAcademicData.jameaDegree;
                                    hgm.employee_academic_details.batchId = _helperService.getWafdCurrentClass(jHSAcademicData.farighYear, jHSAcademicData.farighDarajah);
                                    hgm.batchId = hgm.employee_academic_details.batchId;
                                }
                                else
                                {
                                    nisaab_alumni alum = _context.nisaab_alumni.Where(x => x.itsId == itsId).FirstOrDefault();
                                    if (alum != null)
                                    {
                                        hgm.employee_academic_details.category = "Nisaab Mahad al-Zahra";
                                        hgm.employee_academic_details.farigDarajah = alum.farigDarajah;
                                        hgm.employee_academic_details.farigYear = alum.farigYear;
                                        hgm.employee_academic_details.aljameaDegree = alum.degree;
                                        hgm.batchId = _helperService.getWafdCurrentClass(alum.farigYear, alum.farigDarajah);
                                        hgm.employee_academic_details.batchId = hgm.batchId;
                                    }
                                }
                            }
                            else
                            {
                                _context.employee_academic_details.Add(new employee_academic_details
                                {
                                    itsId = hgm.itsId,
                                    hifzStatus = user.hifzStatus,
                                    hifzSanadYear = Convert.ToInt32(user.hifzYear)
                                });
                            }
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.dobGregorian = user.Dob.Date.ToString("dd/MM/yyyy");
                            _context.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.haddiyatYear = Convert.ToInt32(user.haddiyatYear);
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.mafsuhiyatYear = Convert.ToInt32(user.mafsuhiyatYear);
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }


                        try
                        {
                            hgm.nationality = user.Nationality;

                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.jamiat = user.Jamiat;

                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.dawat_title = user.Title;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.muqam = user.Maqaam;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.maritalStatus = user.MaritalStatus;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.its_preferredIdara = user.Idara;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.jamaat = user.Jamaat;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.bloodGroup = user.BloodGroup;

                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.watanArabic = user.Vatan_Arabic;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.watan = user.Vatan;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.dobHijri = user.DOB_Hijri;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.mobileNo = user.MobileNo;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.emailAddress = user.EmailId;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.age = user.Age;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.fullNameArabic = arabicName;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            hgm.fullName = user.Name;
                            _context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                        }

                        try
                        {
                            hgm.photoBase64 = Convert.ToBase64String(user.Photo, 0, user.Photo.Length);
                            _context.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }

                        _context.SaveChanges();
                    }

                    //SaveITSImage(user.Photo, hgm.itsId);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString() + " " + itsId);
                }

                _context.SaveChanges();


                return Ok(hgm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        [Route("addidentitycard")]
        [HttpPost]
        public async Task<ActionResult> addidentitycard(KGIdentitycards card)
        {
            string api = "addidentitycard";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                kg_identitycards i = new kg_identitycards
                {
                    itsId = authUser.ItsId,
                    cardNumber = card.cardNumber,
                    cardType = card.cardType,
                    country = card.country,
                    nameOnCard = card.nameOnCard,
                };

                try
                {
                    byte[] binaryData1 = Convert.FromBase64String(card.attachment ?? "");
                    MemoryStream stream1 = new MemoryStream(binaryData1);
                    byte[] binaryData2 = Convert.FromBase64String(card.back_attachment ?? "");
                    MemoryStream stream2 = new MemoryStream(binaryData2);
                    if (!string.IsNullOrEmpty(card.attachment))
                    {
                        i.attachment = await _helperService.UploadFileToS3(stream1, card.itsId + "_" + card.cardType.Split(" ")[0] + "_front_" + card.frontAttachName, "uploads/identityCard", "mz-mahadalzahra-org");
                    }
                    if (!string.IsNullOrEmpty(card.back_attachment))
                    {
                        i.back_attachment = await _helperService.UploadFileToS3(stream2, card.itsId + "_" + card.cardType.Split(" ")[0] + "_back_" + card.backAttachName, "uploads/identityCard", "mz-mahadalzahra-org");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                _context.kg_identitycards.Add(i);

                _context.SaveChanges();

                return Ok(i.id);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("addfaimaly")]
        [HttpPost]
        public async Task<ActionResult> addFaimalyDetails(kg_faimalydetails card)
        {
            string api = "addfaimaly";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);



                var find = _context.kg_faimalydetails_its.Where(x => x.itsId == card.itsId).FirstOrDefault();
                var find2 = _context.kg_faimalydetails.Where(x => x.itsId == card.itsId).FirstOrDefault();
                var find3 = _context.kg_faimalydetails.Where(x => x.relation == card.relation && x.hofItsId == find.hofItsId).FirstOrDefault();

                if (find2 != null)
                {
                    return BadRequest(new { message = "Entered ITS already exists" });
                }
                if (find3 != null)
                {
                    return BadRequest(new { message = "You have already added this relation" });
                }

                var i = new kg_faimalydetails
                {
                    itsId = find.itsId,
                    age = find.age.ToString(),
                    hifzStatus = find.hifzStatus,
                    hofItsId = find.hofItsId,
                    idara = find.idara,
                    jamaat = find.jamaat,
                    name = find.name,
                    nationality = find.nationality,
                    occupation = find.occupation,
                    relation = card.relation,
                    dob = find.dob,
                    bloodGroup = find.bloodGroup
                };

                _context.kg_faimalydetails.Add(i);

                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        //[Route("cardattachments/{uniqueId}")]
        //[HttpPost]
        //public async Task<ActionResult> cardAttachment(int uniqueId)
        //{
        //    string api = "cardattachments/{uniqueId}";
        //    //// Add_ApiLogs(api);
        //    HttpResponseMessage result = null;
        //    var httpRequest = HttpContext.Current.Request;
        //    if (httpRequest.Files.Count > 0)
        //    {
        //        string filePath = "";
        //        string filePath_1 = "";
        //        string path = "";
        //        string path_1 = "";
        //        var docfiles = new List<string>();
        //        foreach (string file in httpRequest.Files)
        //        {
        //            var postedFile = httpRequest.Files[file];
        //            var extension = postedFile.ContentType.Replace("/", ".").Replace("application", "");


        //            path = "/" + _globalConstants.uploadFolder + "/identityCards/" + uniqueId + extension;
        //            path_1 = "/" + _globalConstants.assetsFolder + "/identityCards/" + uniqueId + extension;

        //            filePath = HttpContext.Current.Server.MapPath("~/" + path);
        //            filePath_1 = HttpContext.Current.Server.MapPath("~/" + path_1);


        //            if (File.Exists(filePath_1))
        //            {
        //                File.Delete(filePath_1);
        //            }

        //            if (File.Exists(filePath))
        //            {
        //                File.Delete(filePath);
        //            }

        //            postedFile.SaveAs(filePath);

        //            postedFile.SaveAs(filePath_1);

        //            try
        //            {

        //                var bill = _context.kg_identitycards.Where(x => x.id == uniqueId).FirstOrDefault();
        //                bill.attachment = path;
        //                _context.SaveChanges();

        //            }
        //            catch (Exception ex)
        //            {
        //                return BadRequest(ex.ToString());
        //            }


        //        }

        //        return Ok(new { filePath = filePath, filePath1 = path }); ;
        //    }
        //    else
        //    {
        //        return BadRequest( new { message = "file not found");

        //    }


        //}

        [Route("deleteidentitycard/{id}")]
        [HttpDelete]
        public async Task<ActionResult> deleteidentitycard(int id)
        {
            string api = "deleteidentitycard/{id}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                kg_identitycards m = _context.kg_identitycards.Where(x => x.id == id).FirstOrDefault();


                _context.kg_identitycards.Remove(m);
                _context.SaveChanges();



                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        //[Route("chequeattachments/{uniqueId}")]
        //[HttpPost]
        //public async Task<ActionResult> ReviewAttachments(int uniqueId)
        //{
        //    string api = "chequeattachments/{uniqueId}";
        //    //// Add_ApiLogs(api);
        //    HttpResponseMessage result = null;
        //    var httpRequest = HttpContext.Current.Request;
        //    if (httpRequest.Files.Count > 0)
        //    {
        //        string filePath = "";
        //        string filePath_1 = "";
        //        string path = "";
        //        string path_1 = "";
        //        var docfiles = new List<string>();
        //        foreach (string file in httpRequest.Files)
        //        {
        //            var postedFile = httpRequest.Files[file];
        //            var extension = postedFile.ContentType.Replace("/", ".").Replace("application", "");


        //            path = "/" + _globalConstants.uploadFolder + "/cheques/" + uniqueId + extension;
        //            path_1 = "/" + _globalConstants.assetsFolder + "/cheques/" + uniqueId + extension;

        //            filePath = HttpContext.Current.Server.MapPath("~/" + path);
        //            filePath_1 = HttpContext.Current.Server.MapPath("~/" + path_1);


        //            if (File.Exists(filePath_1))
        //            {
        //                File.Delete(filePath_1);
        //            }

        //            if (File.Exists(filePath))
        //            {
        //                File.Delete(filePath);
        //            }

        //            postedFile.SaveAs(filePath);

        //            postedFile.SaveAs(filePath_1);


        //            try
        //            {
        //                var bankDetails = _context.employee_bank_details.Where(x => x.itsId == uniqueId).FirstOrDefault();
        //                if (bankDetails == null)
        //                {
        //                    _context.employee_bank_details.Add(new employee_bank_details { itsId = uniqueId, chequeAttachment = path });
        //                }
        //                else
        //                {
        //                    bankDetails.chequeAttachment = path;
        //                }
        //                _context.SaveChanges();

        //            }
        //            catch (Exception ex)
        //            {
        //                return BadRequest(ex.ToString());
        //            }


        //        }

        //        return Ok(new { filePath = filePath, filePath1 = path });
        //    }
        //    else
        //    {
        //        return BadRequest( new { message = "file not found");

        //    }


        //}


        //[Route("personalityAttachment/{uniqueId}")]
        //[HttpPost]
        //public async Task<ActionResult> personalityAttachment(int uniqueId)
        //{
        //    string api = "personalityAttachment/{uniqueId}";
        //    //// Add_ApiLogs(api);
        //    HttpResponseMessage result = null;
        //    //var httpRequest = HttpContext.Current.Request;

        //    if (httpRequest.Files.Count > 0)
        //    {
        //        string filePath = "";
        //        string filePath_1 = "";
        //        string path = "";
        //        string path_1 = "";
        //        var docfiles = new List<string>();
        //        foreach (string file in httpRequest.Files)
        //        {
        //            var postedFile = httpRequest.Files[file];
        //            var extension = postedFile.ContentType.Replace("/", ".").Replace("application", "");

        //            contenttypewithextention_data ext = _context.contenttypewithextention_data.Where(x => x.contentType.Equals(postedFile.ContentType)).FirstOrDefault();

        //            if (ext != null)
        //            {
        //                extension = ext.extention;
        //            }


        //            path = "/" + _globalConstants.uploadFolder + "/personalityReport/" + uniqueId + extension;
        //            path_1 = "/" + _globalConstants.assetsFolder + "/personalityReport/" + uniqueId + extension;

        //            filePath = Path.Combine(Directory.GetCurrentDirectory(), _globalConstants.rootpath, path);
        //            filePath_1 = Path.Combine(Directory.GetCurrentDirectory(), _globalConstants.rootpath, path_1);


        //            //if (File.Exists(filePath_1))
        //            //{
        //            //    File.Delete(filePath_1);
        //            //}

        //            //if (File.Exists(filePath))
        //            //{
        //            //    File.Delete(filePath);
        //            //}

        //            postedFile.SaveAs(filePath);

        //            postedFile.SaveAs(filePath_1);

        //            try
        //            {

        //                kg_self_assessment sa = _context.kg_self_assessment.Where(x => x.itsId == uniqueId).FirstOrDefault();
        //                if (sa == null)
        //                {
        //                    _context.kg_self_assessment.Add(new kg_self_assessment { itsId = uniqueId, personalityReport = path });
        //                }
        //                else
        //                {
        //                    sa.personalityReport = path;
        //                }
        //                _context.SaveChanges();

        //            }
        //            catch (Exception ex)
        //            {
        //                return BadRequest(ex.ToString());
        //            }


        //        }

        //        return Ok(new { filePath = filePath, filePath1 = path }); ;
        //    }
        //    else
        //    {
        //        return BadRequest( new { message = "file not found");

        //    }


        //}


        //[Route("passportcopyattachment/{uniqueId}")]
        //[HttpPost]
        //public async Task<ActionResult> PassportcopyAttachment(int uniqueId)
        //{
        //    string api = "passportcopyattachment/{uniqueId}";
        //    //// Add_ApiLogs(api);
        //    HttpResponseMessage result = null;
        //    var httpRequest = HttpContext.Current.Request;
        //    if (httpRequest.Files.Count > 0)
        //    {
        //        string filePath = "";
        //        string filePath_1 = "";
        //        string path = "";
        //        string path_1 = "";
        //        var docfiles = new List<string>();
        //        foreach (string file in httpRequest.Files)
        //        {
        //            var postedFile = httpRequest.Files[file];
        //            var extension = postedFile.ContentType.Replace("/", ".");

        //            extension = extension.Replace("application", "");


        //            path = "/" + _globalConstants.uploadFolder + "/passport/" + uniqueId + extension;
        //            path_1 = "/" + _globalConstants.assetsFolder + "/passport/" + uniqueId + extension;

        //            filePath = Path.Combine(Directory.GetCurrentDirectory(), _globalConstants.rootpath, path);
        //            filePath_1 = Path.Combine(Directory.GetCurrentDirectory(), _globalConstants.rootpath, path_1);


        //            //if (File.Exists(filePath_1))
        //            //{
        //            //    File.Delete(filePath_1);
        //            //}

        //            //if (File.Exists(filePath))
        //            //{
        //            //    File.Delete(filePath);
        //            //}

        //            postedFile.SaveAs(filePath);

        //            postedFile.SaveAs(filePath_1);


        //            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
        //            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
        //            try
        //            {

        //                employee_passport_details epd = _context.employee_passport_details.Where(x => x.itsId == uniqueId).FirstOrDefault();
        //                if (epd == null)
        //                {
        //                    _context.employee_passport_details.Add(new employee_passport_details { itsId = uniqueId, passportCopy = path });
        //                }
        //                else
        //                {
        //                    epd.passportCopy = path;
        //                }
        //                _context.SaveChanges();

        //            }
        //            catch (Exception ex)
        //            {
        //                return BadRequest(ex.ToString());
        //            }


        //        }

        //        return Ok(new { filePath = filePath, filePath1 = path }); ;
        //    }
        //    else
        //    {
        //        return BadRequest( new { message = "file not found");

        //    }


        //}


        [Route("update/wafdclassid")]
        [HttpPost]
        public async Task<ActionResult> UpdateWafdClassID()
        {
            string api = "update/wafdclassid";
            //// Add_ApiLogs(api);
            string h1 = "";
            string h2 = "";

            HijriCalenderService service = new HijriCalenderService();
            int currentAcedemicYear = service.getAcedemicYear(DateTime.Today).acedemicYear;

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<employee_academic_details> huffaz = (from kh in _context.khidmat_guzaar
                                                          where kh.mz_idara.Equals(WafdAlHuffaz)
                                                          join ead in _context.employee_academic_details on kh.itsId equals ead.itsId
                                                          select ead).ToList();

                //List<employee_academic_details> huffaz2 = (from kh in context.khidmat_guzaar
                //                                           where kh.activeStatus == true && kh.employeeType == "Khidmatguzaar"
                //                                           join ead in context.employee_academic_details on kh.itsId equals ead.itsId
                //                                           select ead).ToList();

                List<khidmat_guzaar> huffaz2 = _context.khidmat_guzaar.Where(x => x.employeeType == "Khidmatguzaar").ToList();

                //cache.DeleteItem("getAllEmployeeDataKhidmatguzaar");

                foreach (khidmat_guzaar i in huffaz2)
                {
                    h2 = i.itsId.ToString();

                    employee_academic_details ead = i.employee_academic_details ?? new employee_academic_details();

                    int? farigOn = ead?.farigYear;
                    int? farigAt = ead?.farigDarajah;

                    int darajah = _helperService.getWafdCurrentClass(farigOn, farigAt);

                    ead.batchId = darajah;
                    i.batchId = darajah;
                    _context.SaveChanges();

                }


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString() + "l1: " + h1 + " l2: " + h2);
            }
        }


        [Route("addwafdotheridaramawaze")]
        [HttpPost]
        public async Task<ActionResult> addwafdotheridaramawaze(wafd_otheridara_mawaze model)
        {
            string api = "addwafdotheridaramawaze";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                int from = Convert.ToInt32(model.fromYear);
                int to = Convert.ToInt32(model.toYear);

                if (to < from)
                {
                    return BadRequest(new { message = "From year cannot be larger than To year" });
                }

                model.itsId = authUser.ItsId;
                _context.wafd_otheridara_mawaze.Add(model);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("addwafdmahadpastmawaze")]
        [HttpPost]
        public async Task<ActionResult> addwafdmahadpastmawaze(WafdMahadPastMawazeModel model)
        {
            string api = "addwafdmahadpastmawaze";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                model.itsIs = authUser.ItsId;


                int from = Convert.ToInt32(model.fromYear);
                int to = Convert.ToInt32(model.toYear);

                if (to < from)
                {
                    return BadRequest(new { message = "From year cannot be larger than To year" });
                }
                var m1 = _context.wafd_mahad_past_mawaze.Where(x => x.fromYear == model.fromYear && x.itsIs == model.itsIs && x.toYear == model.toYear).ToList();


                foreach (var i in m1)
                {
                    _context.wafd_mahad_past_mawaze.Remove(i);
                    _context.SaveChanges();

                }

                foreach (var i in model.programList)
                {
                    wafd_mahad_past_mawaze mm = new wafd_mahad_past_mawaze { fromYear = model.fromYear, itsIs = model.itsIs, mauze = model.mauze, program = i, toYear = model.toYear };
                    _context.wafd_mahad_past_mawaze.Add(mm);
                    _context.SaveChanges();
                }

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }



        [Route("deletewafdotheridaramawaze/{id}")]
        [HttpDelete]
        public async Task<ActionResult> deletewafdotheridaramawaze(int id)
        {
            string api = "deletewafdotheridaramawaze/{id}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                wafd_otheridara_mawaze m = _context.wafd_otheridara_mawaze.Where(x => x.id == id).FirstOrDefault();

                _context.wafd_otheridara_mawaze.Remove(m);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("deletewafdmahadpastmawaze/{id}")]
        [HttpDelete]
        public async Task<ActionResult> deletewafdmahadpastmawaze(int id)
        {
            string api = "deletewafdmahadpastmawaze";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                wafd_mahad_past_mawaze m = _context.wafd_mahad_past_mawaze.Where(x => x.id == id).FirstOrDefault();

                List<wafd_mahad_past_mawaze> m1 = _context.wafd_mahad_past_mawaze.Where(x => x.fromYear == m.fromYear && x.toYear == m.toYear).ToList();


                foreach (var i in m1)
                {
                    _context.wafd_mahad_past_mawaze.Remove(i);
                    _context.SaveChanges();
                }


                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("deletewafdfaimalymember/{id}")]
        [HttpDelete]
        public async Task<ActionResult> deleteFaimalymember(int id)
        {
            string api = "deletewafdfaimalymember/{id}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                kg_faimalydetails m = _context.kg_faimalydetails.Where(x => x.id == id).FirstOrDefault();

                _context.kg_faimalydetails.Remove(m);
                _context.SaveChanges();


                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("famalydetails")]
        [HttpGet]
        public async Task<ActionResult> updatefromitsFaimalymember()
        {
            try
            {
                ;
                List<kg_faimalydetails> m = _context.kg_faimalydetails.ToList();

                return Ok(m);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("secret/employees/clearcacheof/{its}")]
        [HttpGet]
        public async Task<ActionResult> clearAllEmployeeData(int its)
        {


            return Ok("Cache cleared of " + its.ToString());

        }

        //[Route("api/secret/bills/clearallyear/{year}")]
        //[HttpGet]
        //public async Task<ActionResult> clearbillscache (int year)
        //{
        //    using (var context = new mzmanageEntities())
        //    {
        //        List<dept_venue> dvs = _context.dept_venue.Where(x => x.status == 1).toList();

        //        cache.DeleteItem("getEmployeeData" + year);
        //        return Ok("Cache cleared");
        //    }
        //}

        [Route("secret/employees/clearallcache")]
        [HttpGet]
        public async Task<ActionResult> clearAllEmployeeData()
        {
            List<string> empTypes = _context.khidmat_guzaar.GroupBy(x => x.employeeType).Select(x => x.FirstOrDefault()).Select(x => x.employeeType).ToList();
            foreach (string emptype in empTypes)
            {
                //cache.DeleteItem("getAllEmployeeData" + emptype);
            }
            return Ok("Cache clear all");

        }

        [Route("secret/individualemployees/clearallcache")]
        [HttpGet]
        public async Task<ActionResult> clearIndividualEmployeeData()
        {
            List<khidmat_guzaar> empTypes = _context.khidmat_guzaar.ToList();
            foreach (khidmat_guzaar emptype in empTypes)
            {
                //cache.DeleteItem("getEmployeeData" + emptype.itsId);
            }
            return Ok("Cache clear all");

        }




    }


}
