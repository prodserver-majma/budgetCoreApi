using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using mahadalzahrawebapi.Templates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RazorLight;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace mahadalzahrawebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly HelperService _helperService;
        private readonly HijriCalenderService _hijriCalenderService;
        private readonly NotificationService _notificationService;
        private readonly globalConstants _globalConstants;
        private readonly WhatsAppApiService _whatsAppApiService;
        private readonly SalaryService _salaryService;
        private readonly IConverter _converter;

        public ReportsController(
            mzdbContext context,
            IMapper mapper,
            TokenService tokenService,
            IConverter converter
        )
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _helperService = new HelperService(context);
            _hijriCalenderService = new HijriCalenderService(context);
            _notificationService = new NotificationService();
            _globalConstants = new globalConstants();
            _whatsAppApiService = new WhatsAppApiService(context);
            _salaryService = new SalaryService(context);
            _converter = converter;
        }

        [Route("wafd_ul_huffazSectionwizeExport")]
        [HttpPost]
        public async Task<ActionResult> ExportToExcel_Wafd_SectionWize(
            ExportToExcel_WafdulhufazModel w
        )
        {
            string api = "wafd_ul_huffazSectionwizeExport";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                var exportObject = getSectionExportData(w.categoryId ?? 0, w.model);

                //List<JObject> exportObject = new List<JObject>();
                //dynamic jsonObj = JsonConvert.DeserializeObject(huffazModeljson2);

                //foreach (dynamic obj in jsonObj)
                //{
                //    string s = Convert.ToString(obj);
                //    var j = JObject.Parse(s);
                //    exportObject.Add(j);
                //}

                return Ok(new { export = exportObject });
                //return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("excel/exportHelpers")]
        [HttpGet]
        public async Task<ActionResult> ExportHelpers()
        {
            string api = "wafd_ul_huffaz";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                List<ExportToExcel_WafdulhufazModel> etexcel2 =
                    new List<ExportToExcel_WafdulhufazModel>();

                List<export_category> ExportCategory2 = _context.export_category.ToList();

                List<ExportToExcelModel> etexcel = new List<ExportToExcelModel>();
                List<export_category> ExportCategory = ExportCategory2
                    .GroupBy(x => x.categoryId)
                    .Select(x => x.FirstOrDefault())
                    .ToList();

                PropertyInfo[] propertyInfos = typeof(wafd_ul_huffaz_Model).GetProperties();

                //writes all the property names
                int id = 1;
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    etexcel.Add(
                        new ExportToExcelModel
                        {
                            id = id,
                            propertyName = propertyInfo.Name,
                            status = false
                        }
                    );
                    id++;
                }

                foreach (var k in ExportCategory2)
                {
                    etexcel.Add(
                        new ExportToExcelModel
                        {
                            categoryId = k.categoryId ?? 0,
                            id = id,
                            propertyName = k.fieldActualName,
                            propertyDisplayName = k.fieldDisplayName,
                            status = false
                        }
                    );
                    id++;
                }

                foreach (var k in ExportCategory)
                {
                    etexcel2.Add(
                        new ExportToExcel_WafdulhufazModel
                        {
                            categoryId = k.categoryId ?? 0,
                            categoryName = k.categoryName,
                            toRemove = etexcel.Where(x => x.categoryId == k.categoryId).ToList()
                        }
                    );
                }

                return Ok(new { etexcel2 = etexcel2, ExportCategory2 = ExportCategory2 });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("excel/wafd_ul_huffaznew")]
        [HttpPost]
        public async Task<ActionResult> ExportToExcel_New(ExportToExcel_WafdulhufazModel w)
        {
            string api = "api/exporttoexcelnew/wafd_ul_huffaznew";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                List<khidmat_guzaar> khidmat_Guzaars = _context
                    .khidmat_guzaar.Where(x => w.model.Contains(x.itsId))
                    .Include(x => x.employee_academic_details)
                    .Include(x => x.employee_khidmat_details)
                    .Include(x => x.employee_passport_details)
                    .Include(x => x.mauzeNavigation)
                    .ThenInclude(x => x.qism)
                    .Include(x => x.employee_bank_details)
                    .ToList();
                List<kg_self_assessment> kg_Self_Assessments = _context
                    .kg_self_assessment.Where(x => w.model.Contains(x.itsId))
                    .ToList();

                List<ExportToExcelModel> toRemove3 = new List<ExportToExcelModel>();
                List<Wafd_ul_huffaz_ExportModel> model3 = new List<Wafd_ul_huffaz_ExportModel>();

                List<kg_faimalydetails_its> kg_Faimalydetails_Itses =
                    new List<kg_faimalydetails_its>();

                if (w.toRemove.Find(x => x.propertyName == "childCount").status == true)
                {
                    kg_Faimalydetails_Itses = _context
                        .kg_faimalydetails_its.Where(x => w.model.Contains(x.hofItsId ?? 0))
                        .OrderByDescending(x => x.age)
                        .ToList();
                }

                int c = 1;
                foreach (khidmat_guzaar i in khidmat_Guzaars)
                {
                    string email = i.emailAddress;
                    if (i.officialEmailAddress != null)
                    {
                        email = i.officialEmailAddress;
                    }

                    kg_self_assessment k = kg_Self_Assessments
                        .Where(x => x.itsId == i.itsId)
                        .FirstOrDefault();
                    kg_identitycards kg_aadhar = _context
                        .kg_identitycards.Where(x =>
                            x.itsId == i.itsId && x.cardType == "Aadhaar Card"
                        )
                        .FirstOrDefault();
                    wafdprofile_qualification_new wafdprofile_Qualification_New = _context
                        .wafdprofile_qualification_new.Where(x => x.itsid == i.itsId)
                        .OrderByDescending(x => x.year)
                        .FirstOrDefault();

                    List<wafd_otheridara_mawaze> wafd_Otheridara_Mawazes = _context
                        .wafd_otheridara_mawaze.Where(x => x.itsId == i.itsId)
                        .OrderByDescending(x => x.fromYear)
                        .ToList();
                    string otheridaramawaze = string.Join(
                        ", ",
                        wafd_Otheridara_Mawazes.Select(x =>
                            x.khidmatNature
                            + " - "
                            + x.mauze
                            + " - "
                            + x.fromYear
                            + " - "
                            + x.toYear
                        )
                    );

                    List<wafd_mahad_past_mawaze> wafd_Mahad_Past_Mawaze = _context
                        .wafd_mahad_past_mawaze.Where(x => x.itsIs == i.itsId)
                        .OrderByDescending(x => x.fromYear)
                        .ToList();
                    string pastmavaze = string.Join(
                        ", ",
                        wafd_Mahad_Past_Mawaze.Select(x => x.mauze)
                    );

                    List<kg_faimalydetails_its> kgfamily = kg_Faimalydetails_Itses
                        .Where(x => x.hofItsId == i.itsId)
                        .ToList();

                    model3.Add(
                        new Wafd_ul_huffaz_ExportModel
                        {
                            mz_idara = i.mz_idara,
                            tayeenDuration = (
                                _globalConstants.currentHijriYear
                                - (
                                    i.employee_khidmat_details?.FirstOrDefault()?.tayeenYear
                                    ?? _globalConstants.currentHijriYear
                                )
                            ).ToString(),
                            personalHouseAddress = i.personalHouseAddress,
                            khdimatMauzeHouseType = i
                                .employee_khidmat_details?.FirstOrDefault()
                                ?.khdimatMauzeHouseType,
                            khidmatMauzeHouseStatus = i
                                .employee_khidmat_details?.FirstOrDefault()
                                ?.khidmatMauzeHouseStatus,
                            personalHouseArea = i.personalHouseArea,
                            personalHouseStatus = i.personalHouseStatus,
                            personalHouseType = i.personalHouseType,
                            primaryEmailAddress = email,
                            pancardNumber = i.employee_bank_details?.FirstOrDefault()?.panCard,
                            srNo = c.ToString(),
                            age = i.age?.ToString(),
                            accountType = i
                                .employee_bank_details?.FirstOrDefault()
                                ?.bankAccountType,
                            alJameaDegree = i.employee_academic_details?.aljameaDegree,
                            batchId = i.employee_academic_details?.batchId.ToString(),
                            bankBranch = i.employee_bank_details?.FirstOrDefault()?.bankBranch,
                            bloodGroup = i.bloodGroup,
                            category = i.category,
                            bankName = i.employee_bank_details?.FirstOrDefault()?.bankName,
                            dateOfIssue = i
                                .employee_passport_details?.FirstOrDefault()
                                ?.dateOfIssue,
                            dateOfExpiry = i
                                .employee_passport_details?.FirstOrDefault()
                                ?.dateOfExpiry,
                            dobPassport = i
                                .employee_passport_details?.FirstOrDefault()
                                ?.dobPassport,
                            dob = i.dobGregorian?.ToString(),
                            fullName = i.fullName,
                            dobArabic = i.dobHijri,
                            domacileParents = i.domicileParent,
                            bankAccountName = i
                                .employee_bank_details?.FirstOrDefault()
                                ?.bankAccountName,
                            currentDarajah = i.training_class?.FirstOrDefault()?.className,
                            emailAddress = i.emailAddress,
                            farigDarajah = i.employee_academic_details?.farigDarajah?.ToString(),
                            fariqYear = i.employee_academic_details?.farigYear?.ToString(),
                            haddiyatYear = i.haddiyatYear?.ToString(),
                            fullNameArabic = i.fullNameArabic,
                            ifsc = i.employee_bank_details?.FirstOrDefault()?.ifsc,
                            itsId = i.itsId.ToString(),
                            its_idaras = i.its_idaras,
                            hifzSanadYear = i.employee_academic_details?.hifzSanadYear?.ToString(),
                            jamaat = i.jamaat,
                            jamiat = i.jamiat,
                            bankAccountNumber = i
                                .employee_bank_details?.FirstOrDefault()
                                ?.bankAccountNumber,
                            khidmatMauzeAddress = i.currentAddress?.ToString(),
                            khidmatYear = i
                                .employee_khidmat_details?.FirstOrDefault()
                                ?.khidmatYear?.ToString(),
                            mafsuhiyatYear = i.mafsuhiyatYear?.ToString(),
                            mobileNo = i.mobileNo?.ToString(),
                            moze = i.mauzeNavigation?.displayName,
                            maritalStatus = i.maritalStatus?.ToString(),
                            mahad_khidmatYear = i
                                .employee_khidmat_details?.FirstOrDefault()
                                ?.mahad_khidmatYear?.ToString(),
                            nationality = i.nationality?.ToString(),
                            passportName = i
                                .employee_passport_details?.FirstOrDefault()
                                ?.passportName,
                            passportNumber = i
                                .employee_passport_details?.FirstOrDefault()
                                ?.passportNo,
                            officialEmailAddress = i.officialEmailAddress?.ToString(),
                            placeOfIssue = i
                                .employee_passport_details?.FirstOrDefault()
                                ?.placeOfIssue?.ToString(),
                            qismTahfeez = i.mauzeNavigation?.qism?.name,
                            title = i.dawat_title,
                            trNo = i.employee_academic_details?.trNo?.ToString(),
                            watan = i.watan?.ToString(),
                            whatsappNo = i.c_codeWhatsapp + (i.whatsappNo?.ToString()),
                            tayeenYear = i
                                .employee_khidmat_details?.FirstOrDefault()
                                ?.tayeenYear?.ToString(),
                            totalKhidmatYear = i
                                .employee_khidmat_details?.FirstOrDefault()
                                ?.khidmatYear?.ToString(),
                            watanAddress = i.watanAdress,
                            domacileParentsAddress = i.domicileAddressParents,
                            its_preferredIdara = i.its_preferredIdara,
                            photo2 = i.photoBase64,
                            passportBirthPlace = i
                                .employee_passport_details?.FirstOrDefault()
                                ?.passportPlaceOfBirth,
                            aadharCardName = kg_aadhar?.nameOnCard,
                            aadharCardNo = kg_aadhar?.cardNumber,
                            alternativeCareerPath = k?.alternativeCareerPath,
                            longTermGoal = k?.longTermGoal,
                            changeAboutYourself = k?.changeAboutYourself,
                            roleModel = k?.roleModel,
                            strength = k?.strength,
                            weakness = k?.weakness,
                            mzPastMawaze = pastmavaze,
                            otherIdaraMawaze = otheridaramawaze,
                            latestQualifications = wafdprofile_Qualification_New?.degree,
                            aboutYourSelf = k?.aboutYourSelf?.ToString(),
                            personalityType = k?.personalitytype?.ToString(),
                            childCount = kgfamily
                                .Where(x => x.relation == "Daughter" || x.relation == "Son")
                                .ToList()
                                .Count,
                        }
                    );
                    c = c + 1;
                }

                List<ExportToExcelModel> toRemove2 = w
                    .toRemove.Where(x => x.status == false)
                    .ToList();
                foreach (var j in toRemove2)
                {
                    toRemove3.Add(j);
                }

                HashSet<string> propertiesToExclude = new HashSet<string>(
                    toRemove3.Select(x => x.propertyName)
                );

                List<dynamic> transformedModels = model3
                    .Select(m => _helperService.TransformModelForExport(m, propertiesToExclude))
                    .ToList();

                return Ok(new { export = transformedModels });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private List<dynamic> getSectionExportData(int id, List<int> models)
        {
            List<dynamic> data = new List<dynamic>();

            List<khidmat_guzaar> khidmat_Guzaars = _context
                .khidmat_guzaar.Where(x => models.Contains(x.itsId))
                .Include(x => x.employee_academic_details)
                .Include(x => x.employee_khidmat_details)
                .Include(x => x.mauzeNavigation)
                .ThenInclude(x => x.qism)
                .Include(x => x.employee_bank_details)
                .ToList();

            if (id == 3)
            {
                List<ExportToExcel_FaimalyDetails> datas = new List<ExportToExcel_FaimalyDetails>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    List<kg_faimalydetails_its> users = _context
                        .kg_faimalydetails_its.Where(x => x.hofItsId == i.itsId)
                        .OrderByDescending(x => x.age)
                        .ToList();

                    foreach (var j in users)
                    {
                        datas.Add(
                            new ExportToExcel_FaimalyDetails
                            {
                                r_age = j.age,
                                r_hifzStatus = j.hifzStatus,
                                r_idara = j.idara,
                                r_itsId = j.itsId?.ToString(),
                                r_jamaat = j.jamaat,
                                r_name = j.name,
                                r_nationality = j.nationality,
                                r_occupation = j.occupation,
                                r_relation = j.relation,
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString(),
                                dob = j.dob.ToString(),
                                bloodGrp = j.bloodGroup,
                            }
                        );
                    }

                    if (users.Count != 0)
                    {
                        c = c + 1;
                    }
                }

                data = _helperService.ConvertToDynamicList(datas);
            }
            else if (id == 4)
            {
                List<ExportToExcel_Qualification> datas = new List<ExportToExcel_Qualification>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    List<wafdprofile_qualification_new> d = _context
                        .wafdprofile_qualification_new.Where(x => x.itsid == i.itsId)
                        .ToList()
                        .OrderByDescending(x => x.year)
                        .ToList();

                    foreach (var j in d)
                    {
                        datas.Add(
                            new ExportToExcel_Qualification
                            {
                                country = j.country,
                                degree = j.degree,
                                institutionName = j.institutionName,
                                mediumOfEducation = j.mediumOfEducation,
                                stage = j.stage,
                                status = j.status,
                                year = j.year,
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }

                    if (d.Count != 0)
                    {
                        c = c + 1;
                    }
                }
                data = _helperService.ConvertToDynamicList(datas);
            }
            else if (id == 5)
            {
                List<ExportToExcel_CoursesWorkshop> datas =
                    new List<ExportToExcel_CoursesWorkshop>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    List<wafdprofile_workshop_data> d = _context
                        .wafdprofile_workshop_data.Where(x => x.itsId == i.itsId)
                        .ToList()
                        .OrderByDescending(x => x.year)
                        .ToList();

                    foreach (var j in d)
                    {
                        datas.Add(
                            new ExportToExcel_CoursesWorkshop
                            {
                                category = j.category,
                                certificatecridentials = j.cetificateCredentials,
                                keyPoints = j.keypoints,
                                mode = j.mode,
                                subCategory = j.subCategory,
                                topic = j.courseName,
                                type = j.type,
                                year = j.completionDate?.ToString("yyyy"),
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }
                    if (d.Count != 0)
                    {
                        c = c + 1;
                    }
                }
                data = _helperService.ConvertToDynamicList(datas);
            }
            else if (id == 6)
            {
                List<ExportToExcel_LanguageProficiency> datas =
                    new List<ExportToExcel_LanguageProficiency>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    List<wafd_languageproficiency> datas2 = _context
                        .wafd_languageproficiency.Where(x => x.itsId == i.itsId)
                        .ToList();
                    List<wafd_languageproficiency> datas1 = new List<wafd_languageproficiency>();
                    foreach (var i1 in datas2)
                    {
                        int a =
                            (
                                Convert.ToInt32(i1.listening)
                                + Convert.ToInt32(i1.reading)
                                + Convert.ToInt32(i1.speaking)
                                + Convert.ToInt32(i1.writing)
                            ) / 4;

                        i1.selfRanking = a;
                        datas1.Add(i1);
                    }
                    datas1 = datas1.OrderByDescending(x => x.selfRanking).ToList();

                    foreach (var j in datas1)
                    {
                        datas.Add(
                            new ExportToExcel_LanguageProficiency
                            {
                                average = j.selfRanking?.ToString(),
                                language = j.language,
                                listening = j.listening,
                                reading = j.reading,
                                speaking = j.speaking,
                                writing = j.writing,
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }

                    if (datas1.Count != 0)
                    {
                        c = c + 1;
                    }
                }
                data = _helperService.ConvertToDynamicList(datas);
            }
            else if (id == 7)
            {
                List<ExportToExcel_FieldOfInterest> datas =
                    new List<ExportToExcel_FieldOfInterest>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    List<wafd_fieldofinterest> d = _context
                        .wafd_fieldofinterest.Where(x => x.itsId == i.itsId)
                        .OrderByDescending(x => x.selfRanking)
                        .ToList();

                    foreach (var j in d)
                    {
                        datas.Add(
                            new ExportToExcel_FieldOfInterest
                            {
                                interest = j.fieldofInterest,
                                ranking = j.selfRanking,
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }

                    if (d.Count != 0)
                    {
                        c = c + 1;
                    }
                }
                data = _helperService.ConvertToDynamicList(datas);
            }
            else if (id == 8)
            {
                List<ExportToExcel_PhysicalFitness> datas =
                    new List<ExportToExcel_PhysicalFitness>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    List<wafd_physicalfitness> d = _context
                        .wafd_physicalfitness.Where(x => x.itsId == i.itsId)
                        .OrderByDescending(x => x.selfRanking)
                        .ToList();

                    foreach (var j in d)
                    {
                        datas.Add(
                            new ExportToExcel_PhysicalFitness
                            {
                                physicalFitness = j.sports,
                                ranking = j.selfRanking?.ToString(),
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }

                    if (d.Count != 0)
                    {
                        c = c + 1;
                    }
                }
                data = _helperService.ConvertToDynamicList(datas);
            }
            else if (id == 9)
            {
                List<ExportToExcel_mahadpastmawaze> datas =
                    new List<ExportToExcel_mahadpastmawaze>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    List<wafd_mahad_past_mawaze> masterModel = new List<wafd_mahad_past_mawaze>();
                    List<wafd_mahad_past_mawaze> model1 = _context
                        .wafd_mahad_past_mawaze.Where(x => x.itsIs == i.itsId)
                        .ToList();

                    var model2 = model1
                        .Select(m => new { m.fromYear, m.toYear })
                        .Distinct()
                        .ToList();

                    foreach (var m in model2)
                    {
                        List<wafd_mahad_past_mawaze> model3 = _context
                            .wafd_mahad_past_mawaze.Where(x =>
                                x.itsIs == i.itsId
                                && x.fromYear == m.fromYear
                                && x.toYear == m.toYear
                            )
                            .ToList();

                        string programs = "";

                        foreach (var i1 in model3)
                        {
                            if (programs == "")
                            {
                                programs = i1.program;
                            }
                            else
                            {
                                programs = programs + " | " + i1.program;
                            }
                        }

                        wafd_mahad_past_mawaze mmm = new wafd_mahad_past_mawaze()
                        {
                            id = model3.FirstOrDefault().id,
                            fromYear = m.fromYear,
                            toYear = m.toYear,
                            mauze = model3.FirstOrDefault().mauze,
                            program = programs,
                        };

                        masterModel.Add(mmm);
                    }

                    foreach (var j in masterModel)
                    {
                        datas.Add(
                            new ExportToExcel_mahadpastmawaze
                            {
                                mauze = j.mauze,
                                program = j.program,
                                year = j.fromYear + " - " + j.toYear,
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }

                    if (masterModel.Count != 0)
                    {
                        c = c + 1;
                    }
                }
                data = _helperService.ConvertToDynamicList(datas);
            }
            else if (id == 10)
            {
                List<ExportToExcel_OtherIdaraMawaze> datas =
                    new List<ExportToExcel_OtherIdaraMawaze>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    List<wafd_otheridara_mawaze> d = _context
                        .wafd_otheridara_mawaze.Where(x => x.itsId == i.itsId)
                        .ToList();

                    foreach (var j in d)
                    {
                        datas.Add(
                            new ExportToExcel_OtherIdaraMawaze
                            {
                                khidmatNature = j.khidmatNature,
                                mauze = j.mauze,
                                year = j.fromYear + " - " + j.toYear,
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }

                    if (d.Count != 0)
                    {
                        c = c + 1;
                    }
                }
                data = _helperService.ConvertToDynamicList(datas);
            }
            //else if (id == 11)
            //{
            //    List<ExportToExcel_MawazeHistory> datas = new List<ExportToExcel_MawazeHistory>();
            //    int c = 1;
            //    foreach (var i in models)
            //    {
            //        List<wafdulhuffaz_khidmat_mawaze> d = _context.wafdulhuffaz_khidmat_mawaze.Where(x => x.itsId == i).OrderByDescending(x => x.hijriYear).ThenBy(x => x.khidmatMainType).ToList();


            //        foreach (var j in d)
            //        {
            //            datas.Add(new ExportToExcel_MawazeHistory { khidmatNature = j.khidmatMainType, mauze = j.mozeName, year = j.hijriYear?.ToString(), age = i.age?.ToString(), farigYear = i.fariqYear?.ToString(), batchId = i.batchId.ToString(), farigDarajah = i.farigDarajah?.ToString(), itsId = i.itsId.ToString(), name = i.fullName, srNo = c.ToString() });


            //        }

            //        if (d.Count != 0)
            //        {
            //            c = c + 1;
            //        }

            //    }
            //    data = _helperService.ConvertToDynamicList(datas);
            //}
            else if (id == 12)
            {
                List<ExportToExcel_IdentityCard> datas = new List<ExportToExcel_IdentityCard>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    List<kg_identitycards> users = _context
                        .kg_identitycards.Where(x => x.itsId == i.itsId)
                        .ToList();

                    foreach (var j in users)
                    {
                        datas.Add(
                            new ExportToExcel_IdentityCard
                            {
                                cardNo = j.cardNumber,
                                cardType = j.cardType,
                                country = j.country,
                                nameOnCard = j.nameOnCard,
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }
                    if (users.Count != 0)
                    {
                        c = c + 1;
                    }
                }
                data = _helperService.ConvertToDynamicList(datas);
            }
            else if (id == 33)
            {
                List<ExportToExcel_StrengthWeakness> datas =
                    new List<ExportToExcel_StrengthWeakness>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    kg_self_assessment j = _context
                        .kg_self_assessment.Where(x => x.itsId == i.itsId)
                        .FirstOrDefault();
                    if (j == null)
                    {
                        datas.Add(
                            new ExportToExcel_StrengthWeakness
                            {
                                strength = "Not Filled",
                                weakness = "Not Filled",
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }
                    else
                    {
                        datas.Add(
                            new ExportToExcel_StrengthWeakness
                            {
                                strength = j.strength,
                                weakness = j.weakness,
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }

                    c = c + 1;
                }
                data = _helperService.ConvertToDynamicList(datas);
            }
            else if (id == 34)
            {
                List<ExportToExcel_Aspirations> datas = new List<ExportToExcel_Aspirations>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    kg_self_assessment j = _context
                        .kg_self_assessment.Where(x => x.itsId == i.itsId)
                        .FirstOrDefault();

                    if (j == null)
                    {
                        datas.Add(
                            new ExportToExcel_Aspirations
                            {
                                roleModel = "Not Filled",
                                alternativeCareerPath = "Not Filled",
                                changeAboutYourself = "Not Filled",
                                longTermGoal = "Not Filled",
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }
                    else
                    {
                        datas.Add(
                            new ExportToExcel_Aspirations
                            {
                                roleModel = j.roleModel,
                                alternativeCareerPath = j.alternativeCareerPath,
                                changeAboutYourself = j.changeAboutYourself,
                                longTermGoal = j.longTermGoal,
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }
                    c = c + 1;
                }

                data = _helperService.ConvertToDynamicList(datas);
            }
            else if (id == 59)
            {
                List<ExportToExcel_Aspirations> datas = new List<ExportToExcel_Aspirations>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    kg_self_assessment j = _context
                        .kg_self_assessment.Where(x => x.itsId == i.itsId)
                        .FirstOrDefault();

                    if (j == null)
                    {
                        datas.Add(
                            new ExportToExcel_Aspirations
                            {
                                roleModel = "Not Filled",
                                alternativeCareerPath = "Not Filled",
                                changeAboutYourself = "Not Filled",
                                longTermGoal = "Not Filled",
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }
                    else
                    {
                        datas.Add(
                            new ExportToExcel_Aspirations
                            {
                                roleModel = j.roleModel,
                                alternativeCareerPath = j.alternativeCareerPath,
                                changeAboutYourself = j.changeAboutYourself,
                                longTermGoal = j.longTermGoal,
                                age = i.age?.ToString(),
                                batchId = i.batchId.ToString(),
                                farigYear = i.employee_academic_details?.farigYear?.ToString(),
                                farigDarajah =
                                    i.employee_academic_details?.farigDarajah?.ToString(),
                                itsId = i.itsId.ToString(),
                                name = i.fullName,
                                srNo = c.ToString()
                            }
                        );
                    }

                    c = c + 1;
                }

                data = _helperService.ConvertToDynamicList(datas);
            }
            else if (id == 62)
            {
                List<ExportToExcel_SalaryDetails> datas = new List<ExportToExcel_SalaryDetails>();
                int c = 1;
                foreach (var i in khidmat_Guzaars)
                {
                    employee_salary? user = _context
                        .employee_salary.Where(x => x.itsId == i.itsId)
                        .FirstOrDefault();
                    int netSalary = _salaryService.netSalary(user);
                    int ctc = _salaryService.caculateCTC(user);
                    //foreach (var j in users)
                    //{
                    datas.Add(
                        new ExportToExcel_SalaryDetails
                        {
                            age = i.age?.ToString(),
                            batchId = i.batchId.ToString(),
                            farigYear = i.employee_academic_details?.farigYear?.ToString(),
                            farigDarajah = i.employee_academic_details?.farigDarajah?.ToString(),
                            itsId = i.itsId.ToString(),
                            name = i.fullName,
                            srNo = c.ToString(),
                            bqhs = user.bqhs,
                            conveyanceAllowance = user.conveyanceAllowance,
                            extraAllowance = user.extraAllowance,
                            fmbAllowance = user.fmbAllowance,
                            fmbDeduction = user.fmbDeduction,
                            isMahadSalary = user.isMahadSalary == true ? "Yes" : "No",
                            marafiqKhairiyah = user.marafiqKhairiyah,
                            marriageAllowance = user.marriageAllowance,
                            mauze = user.its.mauzeNavigation.displayName,
                            mumbaiAllowance = user.mumbaiAllowance,
                            professionTax = user.professionTax,
                            qardanHasanah = user.qardanHasanah,
                            rentAllowance = user.rentAllowance,
                            sabeel = user.sabeel,
                            tds = user.tds,
                            grossSalary = user.grossSalary,
                            netSalary = netSalary,
                            ctc = ctc
                        }
                    );

                    //}

                    //if (users.Count != 0)
                    //{
                    c = c + 1;
                    //}
                }

                data = _helperService.ConvertToDynamicList<ExportToExcel_SalaryDetails>(datas);
            }
            return data;
        }

        [Route("getFeeReceipt")]
        [HttpPost]
        public async Task<ActionResult> GetReport_6Async(Report_FiltersModel filter)
        {
            try
            {
                // Fetch the receipt and related data
                mz_student_receipt receipt = _context.mz_student_receipt.FirstOrDefault(x =>
                    x.id == filter.reciptId
                );
                if (receipt == null)
                {
                    return NotFound("Receipt not found");
                }
                string strAmount = receipt.amount?.ToString() ?? "0";
                string amountInWords = _helperService.ChangeToWords(strAmount);
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                DateOnly date = receipt.recieptDate ?? today;
                mz_student student = _context.mz_student.FirstOrDefault(x =>
                    x.mz_id == receipt.studentId
                );

                // Prepare the model for the template
                var model = new ReceiptModel
                {
                    ReceiptNo = receipt.recieptNumber.ToString(),
                    Date = date.ToString("dd/MM/yyyy"),
                    Name = student.nameEng,
                    ItsId = student.itsID.ToString(),
                    PaymentMode = receipt.paymentMode.ToUpper(),
                    ChequeNo =
                        receipt.paymentMode == "Cheque" ? receipt.transactionId : string.Empty,
                    BankName = receipt.bankName,
                    ChequeDate =
                        receipt.paymentMode == "Cheque"
                            ? receipt.chequeDate?.ToString("dd/MM/yyyy")
                            : string.Empty,
                    FeePaidAmount = String.Format(
                        CultureInfo.CreateSpecificCulture("en-IN"),
                        "{0:C}",
                        receipt.amount
                    ),
                    AmountInWord = amountInWords,
                    PreparedBy = receipt.createdBy,
                };

                // Define the template path
                string templatePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Templates",
                    "FeeReceiptTemplate.cshtml"
                );

                // Check if the file exists
                if (!System.IO.File.Exists(templatePath))
                {
                    throw new FileNotFoundException(
                        $"The template file was not found at path: {templatePath}"
                    );
                }

                // Read the template content
                string templateContent = await System.IO.File.ReadAllTextAsync(templatePath);

                // Configure RazorLight to use a file system project
                var engine = new RazorLightEngineBuilder()
                    .UseFileSystemProject(Path.GetDirectoryName(templatePath)) // Set the base directory for templates
                    .UseMemoryCachingProvider()
                    .Build();

                // Correctly call CompileRenderAsync with the model and template content
                string renderedHtml = await engine.CompileRenderAsync<ReceiptModel>(
                    Path.GetFileName(templatePath),
                    model
                );

                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings =
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                    },
                    Objects =
                    {
                        new ObjectSettings()
                        {
                            PagesCount = true,
                            HtmlContent = renderedHtml,
                            WebSettings = { DefaultEncoding = "utf-8" }
                        }
                    }
                };

                byte[] pdf = _converter.Convert(doc);

                // Save PDF to a MemoryStream and upload to S3
                using (var ms = new MemoryStream(pdf))
                {
                    string pdfUrl = await _helperService.UploadFileToS3(
                        ms,
                        filter.reciptId + "_Online_Receipt.pdf",
                        "Receipts"
                    );
                    receipt.recieptUrl = pdfUrl;
                    _context.SaveChanges();
                    return Ok(new { url = pdfUrl });
                }
                // Save PDF to a MemoryStream
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while generating the report: {ex.Message}");
            }
        }

        [Route("kgProfile")]
        [HttpPost]
        public async Task<ActionResult> GetFullProfile(Report_FiltersModel filter)
        {
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<int> itsIds = new List<int>();
                if (string.IsNullOrEmpty(filter.itsId))
                {
                    itsIds.Add(authUser.ItsId);
                }
                else
                {
                    itsIds = _helperService.parseItsId(filter.itsId);
                }

                List<EmployeeModel> w1 = new List<EmployeeModel>();
                List<khidmat_guzaar> kgs = _context
                    .khidmat_guzaar.Include(x => x.employee_dept_salary)
                    .Include(x => x.mauzeNavigation)
                    .Include(x => x.employee_bank_details)
                    .Include(x => x.employee_academic_details)
                    .Include(x => x.employee_family_details)
                    .Include(x => x.employee_khidmat_details)
                    .Include(x => x.employee_salary)
                    .Where(x => itsIds.Contains(x.itsId))
                    .ToList();

                w1 = _mapper.Map<List<EmployeeModel>>(kgs);

                List<KgprofileModel> model = new List<KgprofileModel>();
                KgprofileList kgprofileList = new KgprofileList();
                List<Kg_profile_CourseWorkshop> model_cw = new List<Kg_profile_CourseWorkshop>();
                List<Kg_profile_FamilyMember> model_fd = new List<Kg_profile_FamilyMember>();
                List<Kg_profile_FieldOfInterest> model_foi = new List<Kg_profile_FieldOfInterest>();
                List<Kg_profile_KhidmatMawaze> model_km = new List<Kg_profile_KhidmatMawaze>();
                List<Kg_profile_LanguageProficiency> model_lp =
                    new List<Kg_profile_LanguageProficiency>();
                List<Kg_profile_Mawaze> model_oim = new List<Kg_profile_Mawaze>();
                List<Kg_profile_Mawaze> model_pmm = new List<Kg_profile_Mawaze>();
                List<Kg_profile_Qualification> model_q = new List<Kg_profile_Qualification>();
                List<Kg_profile_Performance> model_kgPerformance =
                    new List<Kg_profile_Performance>();
                suppressDetails model_suppress = new suppressDetails();

                model_suppress = filter.supress;

                foreach (var iii in itsIds)
                {
                    model_cw = new List<Kg_profile_CourseWorkshop>();
                    model_fd = new List<Kg_profile_FamilyMember>();
                    model_foi = new List<Kg_profile_FieldOfInterest>();
                    model_km = new List<Kg_profile_KhidmatMawaze>();
                    model_lp = new List<Kg_profile_LanguageProficiency>();
                    model_oim = new List<Kg_profile_Mawaze>();
                    model_pmm = new List<Kg_profile_Mawaze>();
                    model_q = new List<Kg_profile_Qualification>();
                    model_kgPerformance = new List<Kg_profile_Performance>();

                    KgprofileModel kgprofile = new KgprofileModel();

                    kgprofileList.suppressDetails = model_suppress ?? new suppressDetails();

                    int itsid = iii;

                    EmployeeModel w =
                        w1.Where(x => x.basicDetails?.itsId == itsid).FirstOrDefault()
                        ?? new EmployeeModel();

                    venue v =
                        kgs.Where(x => x.itsId == itsid).FirstOrDefault()?.mauzeNavigation
                        ?? new venue();

                    if (model_suppress?.perfomance == true)
                    {
                        List<ikhtibaar> ikhs = _context
                            .ikhtibaar.Where(x => x.name.Contains("Perfomance"))
                            .ToList();
                        foreach (ikhtibaar i in ikhs)
                        {
                            try
                            {
                                ikhtibaar_marksheet marks = _context
                                    .ikhtibaar_marksheet.Where(x =>
                                        x.itsId == itsid && x.ikhtibaarId == i.id
                                    )
                                    .FirstOrDefault();
                                if (marks != null)
                                {
                                    var m = JsonConvert.DeserializeObject<
                                        Dictionary<string, string>
                                    >(marks.marks);
                                    m = m.ToDictionary(x => x.Key.Trim(), x => x.Value);
                                    List<ikhtibaar_questionnaire> questions = _context
                                        .ikhtibaar_questionnaire.Where(x => x.ikhtibaarId == i.id)
                                        .ToList();
                                    Kg_profile_Performance kgp = new Kg_profile_Performance();
                                    kgp.Year = marks.type;
                                    kgp.Remarks = marks.remarks;
                                    if (m.ContainsKey("venue"))
                                    {
                                        kgp.KGVenue = m["venue"] ?? "";
                                        m.Remove("venue");
                                    }
                                    kgp.CommentedByITS = marks.mukhtabir;
                                    int mkh = Int32.Parse(marks.mukhtabir);
                                    kgp.CommentedBy =
                                        _context
                                            .khidmat_guzaar.Where(x => x.itsId == mkh)
                                            .FirstOrDefault()
                                            ?.fullName ?? "Not Found";
                                    if (m.ContainsKey("Area Of Improvement"))
                                    {
                                        kgp.AreaOfImprovement = m["Area Of Improvement"] ?? "";
                                        m.Remove("Area Of Improvement ");
                                    }
                                    questions.ForEach(x =>
                                    {
                                        kgp.Marking +=
                                            x.question
                                            + ": "
                                            + m[x.id.ToString()]
                                            + "/"
                                            + x.weightage
                                            + " | ";
                                        m.Remove(x.id.ToString());
                                    });
                                    foreach (KeyValuePair<String, String> pair in m)
                                    {
                                        kgp.Marking += pair.Key + ": " + pair.Value + " | ";
                                    }
                                    kgp.Marking += "Total : " + marks.totalMarks.ToString() + "%";
                                    model_kgPerformance.Add(kgp);
                                }
                            }
                            catch (Exception e)
                            {
                                continue;
                            }
                        }
                    }

                    //Strength Weakness
                    if (
                        model_suppress?.strengthsWeakness == true
                        || model_suppress?.aspiration == true
                    )
                    {
                        kg_self_assessment? kgSelfAssessment = await _context
                            .kg_self_assessment.Where(x => x.itsId == itsid)
                            .FirstOrDefaultAsync();

                        if (kgSelfAssessment == null)
                        {
                            w.selfAssessment = new EmployeeSelfAssessmentModel();
                        }
                        else
                        {
                            w.selfAssessment = _mapper.Map<EmployeeSelfAssessmentModel>(
                                kgSelfAssessment
                            );
                        }
                        EmployeeSelfAssessmentModel sa = w.selfAssessment;

                        if (sa != null)
                        {
                            //model_asp.Add(new Report_Table_Model.P_R_24_Aspirations
                            //{
                            kgprofile.AlternateCareerPath = sa.alternativeCareerPath ?? "N/A";
                            kgprofile.ChangeInMyself = sa.changeAboutYourself ?? "N/A";
                            kgprofile.LongTermGoals = sa.longTermGoal ?? "N/A";
                            kgprofile.RoleModel = sa.roleModel ?? "N/A";
                            //});
                            //model_sw.Add(new Report_Table_Model.P_R_24_StrengthWeakness
                            //{
                            kgprofile.Strengths = sa.strength ?? "N/A";
                            kgprofile.Weaknesses = sa.weakness ?? "N/A";
                            //});
                        }
                    }
                    //personality
                    if (model_suppress?.personality == true)
                    {
                        //try
                        //{
                        //    if (w.selfAssessment == null)
                        //    {
                        //        continue;
                        //    }
                        //    //var ppp = _context.personality_type_details.Where(x => x.type == w.selfAssessment.personalitytype).FirstOrDefault();
                        //    //var ppp = null;
                        //    //if (ppp != null)
                        //    //{
                        //    //kgprofile. = w.selfAssessment.personalitytype + " - " + ppp.name, strengths = ppp?.strength, weakness = ppp?.weakness });

                        //    //}
                        //}
                        //catch (Exception e)
                        //{
                        //    continue;
                        //}
                    }

                    string pp1 = "";

                    pp1 = @"~\uploads\Its_Photos\" + itsid + ".jpeg";

                    string email = w.basicDetails?.officialEmailAddress ?? "N/A";
                    if (w.basicDetails?.officialEmailAddress == null)
                    {
                        email = w.basicDetails?.emailAddress ?? "N/A";
                    }

                    string darajah = "NA";

                    if (w.academicDetails?.wafdClassId != null)
                    {
                        nisaab_classes c =
                            _context
                                .nisaab_classes.Where(x => x.id == w.academicDetails.wafdClassId)
                                .FirstOrDefault() ?? new nisaab_classes();

                        darajah = (c.std ?? 0).ToString();
                    }

                    string quali = "";
                    List<wafdprofile_qualification_new> q = _context
                        .wafdprofile_qualification_new.Where(x => x.itsid == itsid)
                        .ToList()
                        .OrderByDescending(x => x.year)
                        .ToList();

                    if (q.Count != 0)
                    {
                        quali = q.First().degree + " ( " + q.First().status + " )";
                    }

                    int currentAcedemicYear = _hijriCalenderService
                        .getAcedemicYear(DateTime.Today)
                        .acedemicYear;
                    CalenderModel todayHijriDate = _hijriCalenderService.getHijriDate(
                        DateTime.Today
                    );

                    string dptvenue = "";
                    dept_venue_dto? d = w.deptSalaries?.FirstOrDefault()?.dept_venue;

                    if (d != null)
                    {
                        dptvenue = d.deptName + "_" + d.venueName;
                    }

                    string hifzstatus = w.academicDetails?.hifzStatus ?? "";
                    if (
                        w.academicDetails?.hifzSanadYear != null
                        || w.academicDetails?.hifzSanadYear != 0
                    )
                    {
                        hifzstatus =
                            w.academicDetails?.hifzSanadYear?.ToString()
                            + " - "
                            + w.academicDetails?.hifzStatus;
                    }

                    string Kduration = "";

                    if (w.khidmatDetails?.khidmatYear != null && w.khidmatDetails?.khidmatYear != 0)
                    {
                        string monthS = "";
                        string yearS = "";

                        int y = (todayHijriDate.hijYear) - (w.khidmatDetails?.khidmatYear ?? 0);

                        int m = (todayHijriDate.hijMonth) - (w.khidmatDetails?.khidmatMonth ?? 1);

                        if (m < 0)
                        {
                            y = y - 1;

                            m = 12 + (m);
                        }

                        if (m == 0)
                        {
                            monthS = "";
                        }
                        else if (m == 1)
                        {
                            monthS = "1 month";
                        }
                        else
                        {
                            monthS = m + " months";
                        }

                        if (y == 0)
                        {
                            yearS = "";
                        }
                        else if (y == 1)
                        {
                            yearS = "1 year";
                        }
                        else
                        {
                            yearS = y + " years";
                        }

                        Kduration = yearS + " " + monthS;
                    }

                    string Tduration = "";

                    if (w.khidmatDetails?.tayeenYear != null && w.khidmatDetails?.tayeenYear != 0)
                    {
                        string monthS = "";
                        string yearS = "";

                        int y = (todayHijriDate.hijYear) - (w.khidmatDetails?.tayeenYear ?? 0);

                        int m = (todayHijriDate.hijMonth) - (w.khidmatDetails?.tayeenMonth ?? 1);

                        if (m < 0)
                        {
                            y = y - 1;

                            m = 12 + (m);
                        }

                        if (m == 0)
                        {
                            monthS = "";
                        }
                        else if (m == 1)
                        {
                            monthS = "1 month";
                        }
                        else
                        {
                            monthS = m + " months";
                        }

                        if (y == 0)
                        {
                            yearS = "";
                        }
                        else if (y == 1)
                        {
                            yearS = "1 year";
                        }
                        else
                        {
                            yearS = y + " years";
                        }

                        Tduration = yearS + " " + monthS;
                    }

                    kgprofile.Age = w.basicDetails?.age?.ToString();
                    kgprofile.AboutMe = w.selfAssessment?.aboutYourSelf;
                    kgprofile.AljameaDegree = w.academicDetails?.aljameaDegree;
                    kgprofile.BatchId = w.academicDetails?.batchId?.ToString();
                    kgprofile.Category = w.academicDetails?.category;
                    kgprofile.CurrentMauze = v?.displayName;
                    kgprofile.DawatTitle = w.basicDetails?.dawat_title;
                    kgprofile.FarigDarajah = w.academicDetails?.farigDarajah?.ToString();
                    kgprofile.FarigYear = w.academicDetails?.farigYear?.ToString();
                    kgprofile.HifzStatus = hifzstatus;
                    kgprofile.Id = w.basicDetails?.itsId.ToString();
                    kgprofile.Idara = w.basicDetails?.its_preferredIdara;
                    kgprofile.Jamaat = w.basicDetails?.jamaat;
                    kgprofile.Jamiat = w.basicDetails?.jamiat;
                    kgprofile.PrimaryEmail = email;
                    kgprofile.HouseAddress = w.basicDetails?.currentAddress;
                    kgprofile.HouseStatus = w.khidmatDetails?.khidmatMauzeHouseStatus;
                    kgprofile.HouseType = w.khidmatDetails?.khdimatMauzeHouseType;
                    kgprofile.KhidmatInMZ = w.khidmatDetails?.mahad_khidmatYear?.ToString();
                    kgprofile.KhidmatYear = w.khidmatDetails?.khidmatYear?.ToString();
                    kgprofile.MaritalStatus = w.basicDetails?.maritalStatus;
                    kgprofile.MobileNo = w.basicDetails?.mobileNo;
                    kgprofile.PrimaryEmail = w.basicDetails?.officialEmailAddress;
                    kgprofile.Name = w.basicDetails?.fullName;
                    kgprofile.QismAlTahfeez = v.qismTahfeez;
                    kgprofile.Nationality = w.basicDetails?.nationality;
                    //kgprofile.PrimaryEmail = w.basicDetails?.emailAddress;
                    kgprofile.TayeenInMZ = w.khidmatDetails?.tayeenYear?.ToString();
                    kgprofile.DomicileParents = w.basicDetails?.domicileParent;
                    kgprofile.Watan = w.basicDetails?.watan;
                    kgprofile.WhatsAppNo = w.basicDetails?.whatsappNo;
                    kgprofile.MZCategory =
                        w.basicDetails?.mz_idara == "Mahad al-Zahra KHDGZ"
                            ? "MZ KHDGZ (Tayeen)"
                            : "MZ KHDGZ (Non Tayeen)";
                    //kgprofile.Photo = p2;
                    kgprofile.TrainingDarajah = darajah;
                    kgprofile.LatestQualification = quali;
                    kgprofile.TayeenDuration = Tduration.ToString();
                    kgprofile.KhidmatDuration = Kduration.ToString();
                    kgprofile.salarydetails = w.employeeSalary ?? new EmployeeSalaryDetailsModel();
                    kgprofile.NetSalary = _salaryService
                        .netSalary(kgprofile.salarydetails)
                        .ToString();
                    kgprofile.TotalAllowance = _salaryService
                        .sumAllowances(kgprofile.salarydetails)
                        .ToString();
                    kgprofile.TotalDeduction = _salaryService
                        .sumDeduction(kgprofile.salarydetails)
                        .ToString();

                    //faimaly details
                    if (model_suppress?.family == true)
                    {
                        List<kg_faimalydetails_its> users = _context
                            .kg_faimalydetails_its.Where(x =>
                                x.hofItsId == itsid && !x.relation.Equals("Self")
                            )
                            .OrderByDescending(x => x.age)
                            .ToList();

                        foreach (var i in users)
                        {
                            string pp = "";
                            pp = @"~\uploads\Its_Photos\\uploads\Its_Photos\" + i.itsId + ".jpeg";

                            model_fd.Add(
                                new Kg_profile_FamilyMember
                                {
                                    Age = Convert.ToInt32(i.age),
                                    Id = i.itsId ?? 0,
                                    Jamaat = i.jamaat,
                                    Name = i.name,
                                    Location = i.nationality,
                                    Relation = i.relation
                                }
                            );
                            model_fd = model_fd.OrderByDescending(x => x.Age).ToList();
                        }
                    }
                    //qualification
                    if (model_suppress?.qualification == true)
                    {
                        List<wafdprofile_qualification_new> data_q = _context
                            .wafdprofile_qualification_new.Where(x => x.itsid == itsid)
                            .ToList()
                            .OrderByDescending(x => x.year)
                            .ToList();

                        foreach (var i in data_q)
                        {
                            model_q.Add(
                                new Kg_profile_Qualification
                                {
                                    Country = i.country,
                                    Institution = i.institutionName,
                                    Medium = i.mediumOfEducation,
                                    PDYear =
                                        i.pursuingYear == "" || i.pursuingYear == null
                                            ? "[N/A]"
                                            : i.pursuingYear,
                                    Status = i.status,
                                    StandardDegree = i.degree,
                                    Year = i.year,
                                }
                            );
                        }
                    }

                    //language proficiency
                    if (model_suppress?.lp == true)
                    {
                        List<wafd_languageproficiency> datas = _context
                            .wafd_languageproficiency.Where(x => x.itsId == itsid)
                            .ToList();
                        List<wafd_languageproficiency> datas1 =
                            new List<wafd_languageproficiency>();
                        foreach (var i in datas)
                        {
                            int a = (
                                Convert.ToInt32(i.listening)
                                + Convert.ToInt32(i.reading)
                                + Convert.ToInt32(i.speaking)
                                + Convert.ToInt32(i.writing)
                            );

                            i.selfRanking = a;
                            datas1.Add(i);
                        }
                        datas1 = datas1.OrderByDescending(x => x.selfRanking).ToList();
                        foreach (var i in datas1)
                        {
                            model_lp.Add(
                                new Kg_profile_LanguageProficiency
                                {
                                    Language = i.language,
                                    Listening = i.listening,
                                    Reading = i.reading,
                                    Speaking = i.speaking,
                                    Total = i.selfRanking?.ToString() + "/20",
                                    Writing = i.writing,
                                }
                            );
                        }
                    }

                    //field of interest

                    if (model_suppress?.foi == true)
                    {
                        List<wafd_fieldofinterest> datas_foi = _context
                            .wafd_fieldofinterest.Where(x => x.itsId == itsid)
                            .OrderByDescending(x => x.selfRanking)
                            .ToList();

                        foreach (var i in datas_foi)
                        {
                            model_foi.Add(
                                new Kg_profile_FieldOfInterest
                                {
                                    Field = i.fieldofInterest,
                                    Interest = i.selfRanking + "/5",
                                }
                            );
                        }
                    }

                    //course and workshop
                    if (model_suppress?.cw == true)
                    {
                        List<wafdprofile_workshop_data> data_cw = _context
                            .wafdprofile_workshop_data.Where(x => x.itsId == itsid)
                            .ToList()
                            .OrderByDescending(x => x.year)
                            .ToList();
                        foreach (var i in data_cw)
                        {
                            model_cw.Add(
                                new Kg_profile_CourseWorkshop
                                {
                                    Category = i.category,
                                    Mode = i.mode,
                                    SubCategory = i.subCategory,
                                    Topic = i.courseName,
                                    Type = i.type,
                                    Year = i.completionDate.Value.Year,
                                }
                            );
                        }
                    }

                    //other idara mawaze
                    if (model_suppress?.otheridara == true)
                    {
                        List<wafd_otheridara_mawaze> data_oim = _context
                            .wafd_otheridara_mawaze.Where(x => x.itsId == itsid)
                            .ToList();

                        data_oim = data_oim.OrderByDescending(x => x.fromYear).ToList();

                        foreach (var i in data_oim)
                        {
                            model_oim.Add(
                                new Kg_profile_Mawaze
                                {
                                    khidmat = i.khidmatNature,
                                    Location = i.mauze,
                                    Year = i.fromYear + " - " + i.toYear,
                                }
                            );
                        }
                    }

                    //mahad past mawaze
                    if (model_suppress?.pastmm == true)
                    {
                        List<wafd_mahad_past_mawaze> masterModel =
                            new List<wafd_mahad_past_mawaze>();

                        List<wafd_mahad_past_mawaze> model1 = _context
                            .wafd_mahad_past_mawaze.Where(x => x.itsIs == itsid)
                            .ToList();

                        var model2 = model1
                            .OrderByDescending(x => x.fromYear)
                            .Select(m => new { m.fromYear, m.toYear })
                            .Distinct()
                            .ToList();

                        CalenderModel todayacedemic = _hijriCalenderService.getAcedemicYear(
                            DateTime.Today
                        );

                        foreach (
                            var m in model2
                                .Where(x =>
                                    !(
                                        x.fromYear == (todayacedemic.acedemicYear)
                                        && x.toYear == (todayacedemic.acedemicYear)
                                    )
                                )
                                .ToList()
                        )
                        {
                            List<wafd_mahad_past_mawaze> model3 = _context
                                .wafd_mahad_past_mawaze.Where(x =>
                                    x.itsIs == itsid
                                    && x.fromYear == m.fromYear
                                    && x.toYear == m.toYear
                                )
                                .ToList();

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
                                id = model3.FirstOrDefault().id,
                                fromYear = m.fromYear,
                                toYear = m.toYear,
                                mauze = model3.FirstOrDefault().mauze,
                                program = programs,
                            };

                            masterModel.Add(mmm);
                        }

                        foreach (var i in masterModel)
                        {
                            model_pmm.Add(
                                new Kg_profile_Mawaze
                                {
                                    Location = i.mauze,
                                    khidmat = i.program,
                                    Year = i.fromYear + " - " + i.toYear,
                                }
                            );
                        }
                    }

                    //khidmat mawaze
                    if (model_suppress?.khidmatm == true)
                    {
                        List<wafdulhuffaz_khidmat_mawaze> mawaze =
                            new List<wafdulhuffaz_khidmat_mawaze>();
                        List<wafdulhuffaz_khidmat_mawaze> mawaze2 =
                            new List<wafdulhuffaz_khidmat_mawaze>();

                        List<WafdMawazeModel> mawaze3 = new List<WafdMawazeModel>();
                        List<WafdMawazeModel> mawaze4 = new List<WafdMawazeModel>();

                        mawaze = _context
                            .wafdulhuffaz_khidmat_mawaze.Where(x => x.itsId == itsid)
                            .OrderByDescending(x => x.hijriYear)
                            .ThenBy(x => x.khidmatMainType)
                            .ToList();
                        if (mawaze.Count != 0)
                        {
                            mawaze2 = mawaze
                                .GroupBy(x => x.khidmatMainType)
                                .Select(x => x.FirstOrDefault())
                                .ToList();
                            if (mawaze2.Count != 0)
                            {
                                foreach (
                                    var i in mawaze2.Where(x => x.khidmatSubType != null).ToList()
                                )
                                {
                                    var listmawaze = mawaze
                                        .Where(x => x.khidmatMainType.Equals(i.khidmatMainType))
                                        .GroupBy(x => x.khidmatSubType)
                                        .Select(x => x.FirstOrDefault())
                                        .ToList();
                                    var count1 = mawaze
                                        .Where(x => x.khidmatMainType.Equals(i.khidmatMainType))
                                        .ToList()
                                        .Count;
                                    int c = 1;
                                    foreach (var j in listmawaze)
                                    {
                                        bool dbool = false;
                                        if (c == 1)
                                        {
                                            dbool = true;
                                        }
                                        var count = mawaze
                                            .Where(x =>
                                                x.khidmatMainType.Equals(i.khidmatMainType)
                                                && x.khidmatSubType.Equals(j.khidmatSubType)
                                            )
                                            .ToList()
                                            .Count;
                                        mawaze3.Add(
                                            new WafdMawazeModel
                                            {
                                                colSpan = 1,
                                                display_subType = true,
                                                rowSpan = listmawaze.Count,
                                                khidmatMainType = i.khidmatMainType,
                                                khidmatSubType = j.khidmatSubType,
                                                mainTypeCount = count1,
                                                subTypeCount = count,
                                                display = dbool
                                            }
                                        );
                                        c = c + 1;
                                    }
                                }
                                foreach (
                                    var i in mawaze2.Where(x => x.khidmatSubType == null).ToList()
                                )
                                {
                                    var count1 = mawaze
                                        .Where(x => x.khidmatMainType.Equals(i.khidmatMainType))
                                        .ToList()
                                        .Count;

                                    var count = mawaze
                                        .Where(x =>
                                            x.khidmatMainType.Equals(i.khidmatMainType)
                                            && x.khidmatSubType == null
                                        )
                                        .ToList()
                                        .Count;

                                    mawaze3.Add(
                                        new WafdMawazeModel
                                        {
                                            colSpan = 2,
                                            display_subType = false,
                                            rowSpan = 1,
                                            khidmatMainType = i.khidmatMainType,
                                            khidmatSubType = "-",
                                            mainTypeCount = count1,
                                            subTypeCount = count,
                                            display = true
                                        }
                                    );
                                }

                                foreach (
                                    var i in mawaze.Where(x => x.khidmatSubType == null).ToList()
                                )
                                {
                                    mawaze4.Add(
                                        new WafdMawazeModel
                                        {
                                            id = i.id,
                                            hijriYear = i.hijriYear,
                                            mozeName = i.mozeName,
                                            colSpan = 2,
                                            display_subType = false,
                                            rowSpan = 1,
                                            khidmatMainType = i.khidmatMainType,
                                            khidmatSubType = "-",
                                            mainTypeCount = 0,
                                            subTypeCount = 0,
                                            display = true
                                        }
                                    );
                                }
                                foreach (
                                    var i in mawaze.Where(x => x.khidmatSubType != null).ToList()
                                )
                                {
                                    mawaze4.Add(
                                        new WafdMawazeModel
                                        {
                                            id = i.id,
                                            hijriYear = i.hijriYear,
                                            mozeName = i.mozeName,
                                            colSpan = 1,
                                            display_subType = true,
                                            rowSpan = 1,
                                            khidmatMainType = i.khidmatMainType,
                                            khidmatSubType = i.khidmatSubType,
                                            mainTypeCount = 0,
                                            subTypeCount = 0,
                                            display = true
                                        }
                                    );
                                }

                                foreach (var i in mawaze3)
                                {
                                    model_km.Add(
                                        new Kg_profile_KhidmatMawaze
                                        {
                                            Count = i.subTypeCount,
                                            Event = i.khidmatMainType,
                                            KhidmatNature = i.khidmatSubType,
                                            Total = i.mainTypeCount,
                                        }
                                    );
                                }
                            }
                        }
                    }

                    kgprofile.FamilyMembers = model_fd;
                    kgprofile.Qualifications = model_q;
                    kgprofile.LanguageProficiencies = model_lp;
                    kgprofile.FieldOfInterest = model_foi;
                    kgprofile.CoursesWorkshops = model_cw;
                    kgprofile.KhidmatMawaze = model_km;
                    kgprofile.OtherIdaraMawaze = model_oim;
                    kgprofile.PastMZMawaze = model_pmm;
                    kgprofile.Performances = model_kgPerformance;

                    //byte[] imageArray = System.IO.File.ReadAllBytes(w.basicDetails?.photo);
                    if (!string.IsNullOrEmpty(w.basicDetails?.photo))
                    {
                        kgprofile.kgprofileimgBase64 = await _helperService.GetBase64ImageFromUrl(
                            w.basicDetails?.photo
                        );
                    }
                    else
                    {
                        kgprofile.kgprofileimgBase64 =
                            "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEBLAEsAAD/4QBaRXhpZgAATU0AKgAAAAgABQMBAAUAAAABAAAASgMDAAEAAAABAAAAAFEQAAEAAAABAQAAAFERAAQAAAABAAAuI1ESAAQAAAABAAAuIwAAAAAAAYagAACxj//bAEMACAYGBwYFCAcHBwkJCAoMFA0MCwsMGRITDxQdGh8eHRocHCAkLicgIiwjHBwoNyksMDE0NDQfJzk9ODI8LjM0Mv/bAEMBCQkJDAsMGA0NGDIhHCEyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMv/AABEIAHYAYgMBIgACEQEDEQH/xAAfAAABBQEBAQEBAQAAAAAAAAAAAQIDBAUGBwgJCgv/xAC1EAACAQMDAgQDBQUEBAAAAX0BAgMABBEFEiExQQYTUWEHInEUMoGRoQgjQrHBFVLR8CQzYnKCCQoWFxgZGiUmJygpKjQ1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4eLj5OXm5+jp6vHy8/T19vf4+fr/xAAfAQADAQEBAQEBAQEBAAAAAAAAAQIDBAUGBwgJCgv/xAC1EQACAQIEBAMEBwUEBAABAncAAQIDEQQFITEGEkFRB2FxEyIygQgUQpGhscEJIzNS8BVictEKFiQ04SXxFxgZGiYnKCkqNTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqCg4SFhoeIiYqSk5SVlpeYmZqio6Slpqeoqaqys7S1tre4ubrCw8TFxsfIycrS09TV1tfY2dri4+Tl5ufo6ery8/T19vf4+fr/2gAMAwEAAhEDEQA/APUsGjBpcUYr4LlPSEwaSnYqKeYQLwgkkIyse4At+f4fnVQpuclGO4XsPxltoxnGcZHSmxuJApTJUjLkDmMdsjrzVYbWc+cVdYiWYzgIQi8EhumC3+eKuGOSWXypWYeb+8kzxtA6BXHGf8a9all9NfHqyHPsIFyiYBZ5OUUD7w9c+uOcGkypZtjB1UBtw6YPt7d/SpuQJLn5d5ykYA6L3yAfTgEU2SJgY7ZtrbSZJWzyMjnjrwMAevNXLAUZLRWJ52MxjilwaYzshaQ7ijOI4owMnHqDnkcgflUmB1BzXmYjCyoy12ZpGVxMGjBpcUYrn5RiYNFLiijlAdtoxUmBRgVtyiuR4rIv50F4xdoD5SgBZkIOeCMN6Z21t4FcL4p12SC/ksLfKyZ/eNkEbCPT1NduBilNsVm9EaF/4ksdKDW7yOZCFEccLGXci4HTtknnoD61j/8ACY6rPvlstDIDHa5nnWMtg9wAfU9zzVHTowdp272bli3JP41upCzLgj8MV6as9zVUjPj8X6zBs+2aLG6qQV8ifdsPHQMuP73fuBWvpnjS2vHaC5SeyupgFMVypA3ZGQrdDknHB4wTioY4GDsR+tV763Z4ipVSpHII60O1tAdLzOtM6bvNQhvKhBBbOMknnHTu3PGMc8CpYo9i7AAAuOgxzjnv65rgrDWJNNuFimZ3tCcnk7o+c/ln8s+nFd3ZP58Rmyp3ktle+Sf6YrnxPvUWmZOLiybbRtqTAowK8flHcj20VJgUUcoXH4oxUmKMV08hHMRhea8KW5e78Qa1LIMMb+VeDwNrFf6V7wSEBZiFVRuZmOAAOpNeCW5WGTUJ1iLbr6cqB/FmRiOfcV3YSDtL5DhL3jrtOX5VKjkVtZYID6iuFh8QXtnGrDT3kB5+U/drY0/xjFdOqTwNDk4BccGu32UkjZVItnTLJjjGSRTbgEx9KqSa5ZQo8rOgCLk4Nc9eeODJMYraylkXP3sgfzxSVOT2HOaRJqAxKSOOa6j4e3UlzodzDJ1tbkwjPpgMP0auHF7czsWng2qx65+6fSu08ATW0UF/bGVVuJbsuIyfmYeWgyPyP5VhiKbVNpmc5prQ7LFGKkxRivM5DK5HiipMUUcgXHYoxUmKMV1chNznvGmR4RvkG794EjJU4wGdQfwwTXlFvbJc2IuYfkld2YMe4LHGf0r17xcB/wAIpqCllUlAFLNj5sgj+VeP+EpxcaPGjDLIShB65Brsw8XGPMu5rTaa5SmbPUJYpQ9zidZBsVx+7K9wR1/GrX9kLAfPt8tlFDhUC5YgZ4HGM9O9dclhNL+88q2cHu5wapyrK9+sR8ry4SGZYgcBu2SevHP410e0Zp7JXK9/osFrplpLDbr5qYE5U8tkYz+dc9No0iOht51ikLnzDLEGBUjjGf4utd/PDusSSuUcYxVGwFw6tbOYWkiONzZyR2PBpKcipUo9Dm7Wzk+2TZlf7MvEWeT7AnHr6cVoBfI1FTB8ssEkUkeOxJwf5Gty4gki/wBds4wQEXj29c1zWnTxy/Ea2tppfLgEOZeCcgPnH6Y/Gk7zuiXaGp7ZikxUvXn15pMV5vIc1yPFFSYoo5AuSbaNtSYpcV1chFzifiYYh4RdJ+Y2mTKAkM2D2x0Oce1eQ+DbgKtxEGPyy7hu68+tev8AxPt5H8JSTxJl4WBzgEAEjOQe3FeCaddvp2rZYgLMAT835V2U4XpNIIT5Zpnq39oCKHDuqgDq3QVyWreJBpm9dLeOWaeQs7yKSB78Y9hUesXH9ppawwzFC7bXP90Ve03SrWCBQ1wRt65VQfzxURSWsjscpS0Whmy+MNRGlQlJIGmc8/I3QcHjNadn4ntZzFMJSt0AEYsNqv7VokWMo8k3QJHCjKD9Quawdf0WzazlcSOZoxuR14AP09Kr3HpYTU1rc6+W9Nzaq+ccV5/5kk3jmR4GIeNOMfeGOcj3Haty51SKz0iIFuI4wCT3Nclo1vJqN293IWJllwyIRuIznp1Iq6MN2ZV56JI+nNMdZdMt3R967AA2Qc4q1tqj4fgaDQLNGJJ2buVCkAnIGK08VxuGpimRbaKlxRS5AH4oxT8Y5qheanDbLiMea/seB+NdPIQGq2MWo6Rd2c5xFNEyscdBivlfxFZi11SSEOcqxAz1ODX0nNcz3YxK+A38C8LivHPHOhMdTnuYsmMsI8FhwfUd66KK5WTNaHCxarN9phdyP3eMcdx3rsrDXLOQLFLHuz1Y8fyrgbhPLJTbwKI5TG2UZlYVrOkpBGtKDPRrG90eC8uiyPgHcGLkj+dZXiLXYp42htlG1up71x/nyEtuJ+br70ffbjO4+lSqCTuy5YhtWL81zcaisVsoO1OvPevRPhzoSSXPnyQhkVtqMxK9+v6mub8O6C8qh5YG6gqSMgj659K9r8NabHpyTKnJbaxyPVR/XNObSVkQrvVnWxRiOJUHIAxmnYqsH8mLcDx6GpILtJlyw2H36VyuBZLiinceooo5BFB0lumPmsRGP4BwD9apzW4J6YA6YFbOz5cVWlj3MwA4QfrWzQzHRd0zt2UYFYWq+HodY0pkZR5wlaVGPXPPeunWAovuTmoEh2qwwMbulJMGj5/1zw1c2UjGS3MZHQMME/jXOixcgHZ05Jr6Y1DRbbUodk8Suuc8ivJf7FiTV/sqZHzKQPUcGtIzZm4o4FrCVJCDE49sVdsLEPLh9y8ZHHU16xceFN2VCYb1pPB3heK5+0NcLhVJVgfZiKpzDlHeANHaa3SUqWTIVi3RsMcH+teiQJnULxx0LKB+AxTra2is7Xy7eMIo9BjJqWyiKbs9zWTdy0rD5lJjA9amt4QIulKyExrjqDirUYCoBQlcG7EHkJ/dH5UVYoquUnmEY7VJ9KiiXMOT1Y5NFFLqCGNEu8Cq1xCFjLD1zRRSZaJreMSW57EZANcI+kwnUrW4/jBAz7EYoooQjuWs4d+dvSsXwzbopvmx/wAtS2O2SzGiigZuTL9xP7xyTUwiCHI7UUUIljyvX2NSfxLRRVIljqKKKok//9k=";
                    }
                    kgprofile.qismLogoBase64 = _globalConstants.qismLogoBase64;

                    model.Add(kgprofile);
                }

                kgprofileList.kgprofileModels = model;

                string filePath1 = "";

                // Define the template path
                string templatePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Templates",
                    "kgprofile.cshtml"
                );

                // Check if the file exists
                if (!System.IO.File.Exists(templatePath))
                {
                    throw new FileNotFoundException(
                        $"The template file was not found at path: {templatePath}"
                    );
                }

                // Read the template content
                string templateContent = await System.IO.File.ReadAllTextAsync(templatePath);

                // Configure RazorLight to use a file system project
                var engine = new RazorLightEngineBuilder()
                    .UseFileSystemProject(Path.GetDirectoryName(templatePath)) // Set the base directory for templates
                    .UseMemoryCachingProvider()
                    .Build();

                // Correctly call CompileRenderAsync with the model and template content
                string renderedHtml = await engine.CompileRenderAsync<KgprofileList>(
                    Path.GetFileName(templatePath),
                    kgprofileList
                );

                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings =
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings()
                        {
                            Top = 5,
                            Bottom = 10,
                            Left = 5,
                            Right = 7
                        },
                    },
                    Objects =
                    {
                        new ObjectSettings()
                        {
                            HtmlContent = renderedHtml,
                            WebSettings =
                            {
                                DefaultEncoding = "utf-8",
                                LoadImages = true,
                                EnableIntelligentShrinking = false, // Prevents automatic scaling
                            },
                            FooterSettings =
                            {
                                FontName = "Arial",
                                FontSize = 9,
                                Right = "Page [page] of [toPage]",
                                //Left = "Mahad al-Zahra KG Profile",
                            }
                            //HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = false },
                            //FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Generated by [Your Application Name]" }
                        }
                    },
                };

                byte[] pdf = _converter.Convert(doc);

                var fileStr = itsIds.Count > 1 ? "bulk_report" : itsIds[0].ToString();

                // Save PDF to a MemoryStream and upload to S3
                using (var ms = new MemoryStream(pdf))
                {
                    string pdfUrl = await _helperService.UploadFileToS3(
                        ms,
                        fileStr + "_Profile.pdf",
                        "Kgprofile"
                    );
                    return Ok(new { url = pdfUrl });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [Route("getReportById")]
        [HttpPost]
        public async Task<ActionResult> getReportData(Report_FiltersModel filter)
        {
            string api = "api/queryReport/getReportData";

            DateRange d = new DateRange();

            d.FromDate = filter.fromDate;
            d.ToDate = filter.toDate;

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                var name = _context
                    .reports_names.Where(x => x.id == filter.id)
                    .FirstOrDefault()
                    ?.name;
                string data = "";

                if (filter.id == 50)
                {
                    // BillManagementController bmc = new BillManagementController();
                    // bmc.getallbills();
                    data = Newtonsoft.Json.JsonConvert.SerializeObject(
                        await ExpensePaidReport(d, authUser)
                    );
                }

                if (filter.id == 43)
                {
                    // BillManagementController bmc = new BillManagementController();
                    // bmc.getallbills();
                    data = Newtonsoft.Json.JsonConvert.SerializeObject(IncomeRecieved(d, authUser));
                }
                return Ok(new { rData = data, name = name });
                // return Ok(new { rData = getDataAsync(filter.id, d, filter.itsId, filter.deptVenueId ,authUser.ItsId, filter.reciptId), name = name });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        private async Task<List<BillManagementModel>> LoadPaidBills(
            List<int> deptIds,
            DateRange dateRange,
            int financialYear
        )
        {
            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.MinValue);
            List<BillManagementModel> model = new List<BillManagementModel>();

            // Sequential async calls to avoid DbContext concurrency issues
            List<mz_expense_bill_master> allBills = await _context
                .mz_expense_bill_master.Where(x =>
                    deptIds.Contains(x.deptVenueId ?? 0)
                    && x.status == "Paid"
                    && x.financialYear == financialYear
                )
                .ToListAsync();

            List<dept_venue> allDV = await _context
                .dept_venue.Where(x => deptIds.Contains(x.id))
                .ToListAsync();

            List<int> vendorIds = allBills.Select(x => x.vendorId ?? 0).ToList();
            List<int> baseItemIds = allBills.Select(x => x.baseItemId ?? 0).ToList();
            List<int> billIds = allBills.Select(x => x.id).ToList();

            List<mz_expense_vendor_master> allVendors = await _context
                .mz_expense_vendor_master.Where(x => vendorIds.Contains(x.id))
                .ToListAsync();

            List<mz_expense_procurement_baseitem> allBaseItems = await _context
                .mz_expense_procurement_baseitem.Where(x => baseItemIds.Contains(x.id))
                .ToListAsync();

            List<user> allUsers = await _context.user.ToListAsync();

            // Sequential async call for the items
            List<BillItemListModel> allItems = await _context
                .mz_expense_bill_item.Where(x => billIds.Contains(x.billId ?? 0))
                .Join(
                    _context.mz_expense_procurement_item,
                    bi => bi.itemId,
                    pi => pi.id,
                    (bi, pi) =>
                        new BillItemListModel
                        {
                            itemName = pi.name,
                            remark = bi.remarks,
                            billId = bi.billId
                        }
                )
                .ToListAsync();

            List<mz_expense_vendor_payment> vendorPayments = await _context
                .mz_expense_vendor_payment.Where(x => vendorIds.Contains(x.vendorId ?? 0))
                .ToListAsync();

            List<mz_expense_vendor_transaction> vendorTransactions = await _context
                .mz_expense_vendor_transaction.Where(x =>
                    vendorIds.Contains(x.vendorId ?? 0) && x.debit != null && x.paymentId != null
                )
                .ToListAsync();

            // Convert collections to dictionaries for faster lookup
            Dictionary<int, mz_expense_procurement_baseitem> baseItemDict =
                allBaseItems.ToDictionary(x => x.id);
            Dictionary<int, dept_venue> deptVenueDict = allDV.ToDictionary(x => x.id);
            Dictionary<int, mz_expense_vendor_master> vendorDict = allVendors.ToDictionary(x =>
                x.id
            );
            Dictionary<int, user> userDict = allUsers.ToDictionary(x => x.ItsId);

            Dictionary<int, List<mz_expense_vendor_transaction>> paymentDict = vendorTransactions
                .GroupBy(x => x.billId ?? 0)
                .ToDictionary(g => g.Key, g => g.ToList());

            Dictionary<int, mz_expense_vendor_payment> actualPaymentDict =
                vendorPayments.ToDictionary(x => x.id);

            // Process each bill
            foreach (mz_expense_bill_master i in allBills)
            {
                // Lookup data from dictionaries
                mz_expense_procurement_baseitem? item = baseItemDict.GetValueOrDefault(
                    i.baseItemId ?? 0
                );
                dept_venue? dv = deptVenueDict.GetValueOrDefault(i.deptVenueId ?? 0);
                mz_expense_vendor_master? vendor = vendorDict.GetValueOrDefault(i.vendorId ?? 0);
                user? u = userDict.GetValueOrDefault(Convert.ToInt32(i.createdBy));

                List<BillItemListModel> items = allItems.Where(x => x.billId == i.id).ToList();

                StringBuilder itemSb = new StringBuilder();
                StringBuilder remarksSb = new StringBuilder();

                foreach (BillItemListModel k in items)
                {
                    if (itemSb.Length > 0)
                        itemSb.Append(" || ");
                    if (remarksSb.Length > 0)
                        remarksSb.Append(" || ");
                    itemSb.Append(k.itemName);
                    remarksSb.Append(k.remark);
                }

                BillManagementModel m = new BillManagementModel
                {
                    billDateString = (i.billDate ?? dateOnly).ToString("dd-MM-yyyy"),
                    entryDateString = (i.createdOn ?? DateTime.Today).ToString("dd-MM-yyyy"),
                    remarks = remarksSb.ToString(),
                    itemName = itemSb.ToString(),
                    isFundRequestedString =
                        (i.isFundRequested ?? false) ? "Requested" : "Not Requested",
                    isFundRequested = i.isFundRequested ?? false,
                    id = i.id,
                    baseItemId = i.baseItemId ?? 0,
                    billDate = i.billDate ?? dateOnly,
                    billNumber = i.billNo,
                    deptVenueId = i.deptVenueId ?? 0,
                    totalAmount = i.billAmount ?? 0,
                    vendorId = i.vendorId ?? 0,
                    baseItemName = item?.name,
                    deptVenueName = dv?.deptName + "_" + dv?.venueName,
                    deptName = dv?.deptName,
                    venueName = dv?.venueName,
                    vendorName = vendor?.name,
                    createdOn = i.createdOn ?? DateTime.Now,
                    createdBy = u?.Username,
                    paymentTo_AccName = i.paymentTo_AccName,
                    paymentTo_AccNum = i.paymentTo_AccNum,
                    paymentTo_BankName = i.paymentTo_BankName,
                    paymentTo_ifsc = i.paymentTo_ifsc,
                    isWaived = i.isWaived ?? false,
                    paymentMode_User = i.paymentMode_User,
                    status = i.status,
                    paymentMode_Admin = i.paymentMode_Admin,
                    gstAmount = i.gstAmount,
                    gstPercentage = i.gstPercentage ?? 0,
                    isReconciled = i.isReconciled ?? false,
                    tdsAmount = i.tdsAmount,
                    tdsApplicableAmount = i.tdsApplicableAmount,
                    tdsPercentage = i.tdsPercentage,
                    conveyanceAmount = i.conveyanceAmount,
                    billAttachment = i.billAttachment
                };

                if (paymentDict.ContainsKey(i.id))
                {
                    mz_expense_vendor_transaction paymentT = paymentDict[i.id].FirstOrDefault();
                    if (actualPaymentDict.ContainsKey(paymentT.paymentId ?? 0))
                    {
                        mz_expense_vendor_payment? actualPayment =
                            actualPaymentDict.GetValueOrDefault(paymentT.paymentId ?? 0);
                        m.paymentDate = actualPayment?.paymentDate ?? dateOnly;
                        m.paymentDateString = (actualPayment?.paymentDate ?? dateOnly).ToString(
                            "dd-MM-yyyy"
                        );
                        mz_expense_vendor_transaction? payment = vendorTransactions.FirstOrDefault(
                            x => x.billId == i.id
                        );
                        if (payment != null)
                        {
                            m.txnId = payment.transactionId;
                        }
                    }
                }

                model.Add(m);
            }

            model = model.OrderBy(x => x.id).ToList();

            return model;
        }

        private async Task<List<datewiseExpenseReport>> ExpensePaidReport(
            DateRange dateRange,
            AuthUser authUser
        )
        {
            List<datewiseExpenseReport> reportModel = new List<datewiseExpenseReport>();
            List<datewiseExpenseReport> salarysavedModel = new List<datewiseExpenseReport>();

            if (authUser == null)
            {
                throw new InvalidOperationException("Authentication user cannot be null.");
            }

            List<user_dept_venue_baseitem> udvbi = await _context
                .user_dept_venue_baseitem.Where(x => x.itsId == authUser.ItsId)
                .ToListAsync();

            if (udvbi == null || !udvbi.Any())
            {
                throw new InvalidOperationException("No user department venue base items found.");
            }

            List<int> dptven = udvbi.Select(x => x.dept_venueId ?? 0).Distinct().ToList();

            string mainStep = "0";
            string intermediateStep = "0";

            try
            {
                DateOnly fromDate = DateOnly.FromDateTime(dateRange.FromDate ?? DateTime.MinValue);
                DateOnly toDate = DateOnly.FromDateTime(dateRange.ToDate ?? DateTime.MaxValue);

                // Asynchronous call to load paid bills
                List<BillManagementModel> savedBills = await LoadPaidBills(
                    dptven,
                    dateRange,
                    _helperService.GetFinancialYear(DateTime.Now)
                );

                if (savedBills.Count == 0)
                {
                    throw new Exception("Refresh the page and try again");
                }

                // Process the bills that match the date range and have the status 'Paid'
                foreach (BillManagementModel x in savedBills)
                {
                    if (x.paymentDate > fromDate && x.paymentDate < toDate && x.status == "Paid")
                    {
                        datewiseExpenseReport report = new datewiseExpenseReport()
                        {
                            AMOUNT = x.totalAmount ?? 0,
                            CHQ_NO = x.txnId,
                            DATE = x.paymentDate?.ToString("dd/MM/yyyy") ?? string.Empty,
                            d =
                                x.paymentDate?.ToDateTime(new TimeOnly(0, 0, 0))
                                ?? DateTime.MinValue,
                            Dept = x.deptName,
                            Venue = x.venueName,
                            ExpenseHead = x.baseItemName,
                            Item_Names = x.itemName,
                            Mode_of_payment = x.paymentFrom_BankName,
                            Remarks = x.remarks,
                            Unique_ID = x.id.ToString(),
                            Vendor_Name = x.vendorName,
                            Type_of_payment = x.paymentMode_Admin
                        };

                        reportModel.Add(report);
                    }
                }

                mainStep = "1";
                salarysavedModel = new List<datewiseExpenseReport>();

                // Fetch salary packages within the date range
                List<payroll_salary_packages> salaryPkg = await _context
                    .payroll_salary_packages.Where(x =>
                        x.paymentDate > dateRange.FromDate && x.paymentDate < dateRange.ToDate
                    )
                    .Include(x => x.salary_allocation_gegorian)
                    .ThenInclude(x => x.salary_generation_gegorgian)
                    .ThenInclude(x => x.deptVenue)
                    .ThenInclude(x => x.venue)
                    .Include(x => x.salary_allocation_hijri)
                    .ThenInclude(x => x.salary_generation_hijri)
                    .ThenInclude(x => x.deptVenue)
                    .ThenInclude(x => x.venue)
                    .ToListAsync();

                List<SalaryGeneration> salaryGenerations = new List<SalaryGeneration>();

                // Process both Gregorian and Hijri salary allocations
                foreach (payroll_salary_packages pkg in salaryPkg)
                {
                    intermediateStep = "p-" + pkg.id;

                    pkg.salary_allocation_gegorian.ToList()
                        .ForEach(alloc =>
                        {
                            salaryGenerations.AddRange(
                                alloc
                                    .salary_generation_gegorgian.Where(gen => gen.deptVenueId != 17)
                                    .Select(gen => _mapper.Map<SalaryGeneration>(gen))
                            );
                        });

                    pkg.salary_allocation_hijri.ToList()
                        .ForEach(alloc =>
                        {
                            salaryGenerations.AddRange(
                                alloc
                                    .salary_generation_hijri.Where(gen => gen.deptVenueId != 17)
                                    .Select(gen => _mapper.Map<SalaryGeneration>(gen))
                            );
                        });
                }

                mainStep = "2";

                // Group salary generations by department, month, year, and Hijri status
                List<SalaryGeneration> sgGroup = salaryGenerations
                    .GroupBy(sg => new
                    {
                        sg.deptVenueId,
                        sg.month,
                        sg.year,
                        sg.isHijri
                    })
                    .Select(grp => grp.First())
                    .ToList();

                // Fetch khidmat guzaar for salary processing
                List<khidmat_guzaar> khidmatGuzaars = await _context.khidmat_guzaar.ToListAsync();

                // Process each salary group
                foreach (SalaryGeneration sgItem in sgGroup)
                {
                    intermediateStep =
                        $"d-{sgItem.deptVenueId} m-{sgItem.month} y-{sgItem.year} h-{sgItem.isHijri}";

                    List<SalaryGeneration> facultySalaries = salaryGenerations
                        .Where(sg =>
                            sg.deptVenueId == sgItem.deptVenueId
                            && sg.month == sgItem.month
                            && sg.year == sgItem.year
                            && sg.isHijri == sgItem.isHijri
                        )
                        .Join(
                            khidmatGuzaars,
                            sg => sg.itsId,
                            kg => kg.itsId,
                            (sg, kg) => new { sg, kg }
                        )
                        .Where(joined => joined.kg.employeeType == "Khidmatguzaar")
                        .Select(joined => joined.sg)
                        .ToList();

                    List<SalaryGeneration> staffSalaries = salaryGenerations
                      .Where(sg =>
                          sg.deptVenueId == sgItem.deptVenueId
                          && sg.month == sgItem.month
                          && sg.year == sgItem.year
                          && sg.isHijri == sgItem.isHijri
                      )
                      .Join(
                          khidmatGuzaars,
                          sg => sg.itsId,
                          kg => kg.itsId,
                          (sg, kg) => new { sg, kg }
                      )
                      .Where(joined => joined.kg.employeeType == "Staff")
                      .Select(joined => joined.sg)
                      .ToList();

                    List<SalaryGeneration> visitingFacultySalaries = salaryGenerations
                      .Where(sg =>
                          sg.deptVenueId == sgItem.deptVenueId
                          && sg.month == sgItem.month
                          && sg.year == sgItem.year
                          && sg.isHijri == sgItem.isHijri
                      )
                      .Join(
                          khidmatGuzaars,
                          sg => sg.itsId,
                          kg => kg.itsId,
                          (sg, kg) => new { sg, kg }
                      )
                      .Where(joined => joined.kg.employeeType == "Visiting Faculty")
                      .Select(joined => joined.sg)
                      .ToList();
                    // Add salary reports
                    if (facultySalaries.Any())
                    {
                        SalaryGeneration facultySalary = facultySalaries.First();
                        datewiseExpenseReport facultyReport = new datewiseExpenseReport
                        {
                            DATE =
                                facultySalary.paymentDate?.ToString("d/M/yyyy")
                                ?? DateTime.Today.ToString("d/M/yyyy"),
                            d = facultySalary.paymentDate ?? DateTime.Today,
                            AMOUNT = facultySalaries.Sum(s => s.netSalary ?? 0),
                            CHQ_NO = "",
                            Dept = sgItem.departmentName ?? "N/A",
                            Venue = sgItem.venueName ?? "N/A",
                            ExpenseHead = "Salary - Faculty",
                            Mode_of_payment = "Kotak Mahindra Bank",
                            Remarks = "Kotak Mahindra Bank",
                            Unique_ID =
                                $"F{(sgItem.isHijri ? "H" : "G")}{sgItem.deptVenueId}{sgItem.month}{sgItem.year}",
                            Type_of_payment = "NEFT"
                        };

                        reportModel.Add(facultyReport);
                        salarysavedModel.Add(facultyReport);
                    }

                    if (staffSalaries.Any())
                    {
                        SalaryGeneration staffSalary = staffSalaries.First();
                        datewiseExpenseReport staffReport = new datewiseExpenseReport
                        {
                            DATE =
                                staffSalary.paymentDate?.ToString("d/M/yyyy")
                                ?? DateTime.Today.ToString("d/M/yyyy"),
                            d = staffSalary.paymentDate ?? DateTime.Today,
                            AMOUNT = staffSalaries.Sum(s => s.netSalary ?? 0),
                            CHQ_NO = "",
                            Dept = sgItem.departmentName ?? "N/A",
                            Venue = sgItem.venueName ?? "N/A",
                            ExpenseHead = "Salary - Staff",
                            Mode_of_payment = "Kotak Mahindra Bank",
                            Remarks = "Kotak Mahindra Bank",
                            Unique_ID =
                                $"S{(sgItem.isHijri ? "H" : "G")}{sgItem.deptVenueId}{sgItem.month}{sgItem.year}",
                            Type_of_payment = "NEFT"
                        };

                        reportModel.Add(staffReport);
                        salarysavedModel.Add(staffReport);
                    }

                    if (visitingFacultySalaries.Any())
                    {
                        SalaryGeneration visitingFacultySalary = visitingFacultySalaries.First();
                        datewiseExpenseReport visitingFacultyReport = new datewiseExpenseReport
                        {
                            DATE =
                                visitingFacultySalary.paymentDate?.ToString("d/M/yyyy")
                                ?? DateTime.Today.ToString("d/M/yyyy"),
                            d = visitingFacultySalary.paymentDate ?? DateTime.Today,
                            AMOUNT = visitingFacultySalaries.Sum(s => s.netSalary ?? 0),
                            CHQ_NO = "",
                            Dept = sgItem.departmentName ?? "N/A",
                            Venue = sgItem.venueName ?? "N/A",
                            ExpenseHead = "Salary - Visiting Faculty",
                            Mode_of_payment = "Kotak Mahindra Bank",
                            Remarks = "Kotak Mahindra Bank",
                            Unique_ID =
                                $"V{(sgItem.isHijri ? "H" : "G")}{sgItem.deptVenueId}{sgItem.month}{sgItem.year}",
                            Type_of_payment = "NEFT"
                        };

                        reportModel.Add(visitingFacultyReport);
                        salarysavedModel.Add(visitingFacultyReport);
                    }
                }

                mainStep = "3";

                reportModel = reportModel.OrderBy(x => x.d).ToList();

                return reportModel;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error at main: {mainStep} with code: {intermediateStep} - {ex}"
                );
            }
        }

        private List<ExportModel_IncomeReceived> IncomeRecieved(
            DateRange dateRange,
            AuthUser authUser
        )
        {
            List<ExportModel_IncomeReceived> reportModel = new List<ExportModel_IncomeReceived>();

            List<mz_student_fee_transaction> report = new List<mz_student_fee_transaction>();

            report = _context
                .mz_student_fee_transaction.Where(x =>
                    !x.paymentMode.Equals("Waive") && !string.IsNullOrEmpty(x.paymentMode)
                )
                .ToList();

            report = report
                .Where(x =>
                    x.createdOn >= dateRange.FromDate.Value.Date
                    && x.createdOn <= dateRange.ToDate.Value.Date.AddDays(1)
                )
                .ToList();

            List<mz_student_fee_allotment> allotments = _context.mz_student_fee_allotment.ToList();
            List<mz_student> students = _context.mz_student.ToList();
            List<registrationform_dropdown_set> allpsets = _context
                .registrationform_dropdown_set.Include(x => x.program)
                .Include(x => x.subprogram)
                .Include(x => x.venue)
                .ToList();
            List<mz_student_receipt> receipts = _context.mz_student_receipt.ToList();

            int c = 1;
            foreach (var i in report)
            {
                mz_student_fee_allotment? s = allotments
                    .Where(x => x.id == i.allotmentId)
                    .FirstOrDefault();
                mz_student? s1 = students.Where(x => x.mz_id == i.studentId).FirstOrDefault();

                string program = "";
                string subprogram = "";
                string venue = "";
                string receiptnumber = "";
                string receiptDate = "";
                if (s.pSetId != null)
                {
                    registrationform_dropdown_set? pset = allpsets
                        .Where(x => x.id == s.pSetId)
                        .FirstOrDefault();
                    registrationform_programs p = pset.program;
                    registrationform_subprograms sp = pset.subprogram;
                    venue v = pset.venue;
                    program = p.name;
                    subprogram = sp.name;
                    venue = v.CampVenue;
                }

                if (i.recieptId != null && i.recieptId != 0)
                {
                    mz_student_receipt receipt = receipts
                        .Where(x => x.id == i.recieptId)
                        .FirstOrDefault();

                    receiptnumber = receipt.id.ToString();
                    receiptDate = (
                        receipt.recieptDate ?? DateOnly.FromDateTime(DateTime.Now)
                    ).ToString("dd/MM/yyyy");
                }

                reportModel.Add(
                    new ExportModel_IncomeReceived
                    {
                        srNo = c.ToString(),
                        transactionId = i.id.ToString(),
                        paymentMode = i.paymentMode,
                        createdBy = i.createdBy,
                        credit = i.credit?.ToString(),
                        debit = i.debit?.ToString(),
                        remarks = i.remarks + " : " + (s.remarks ?? "N/A"),
                        transactionDate = (i.createdOn ?? DateTime.Now).ToString("dd/MM/yyyy"),
                        itsId = s1?.itsID.ToString(),
                        name = s1?.nameEng,
                        program = program,
                        subProgram = subprogram,
                        venue = venue,
                        receiptDate = receiptDate,
                        receitpNo = receiptnumber
                    }
                );
                c = c + 1;
            }

            return reportModel;
        }

        [Route("errorreport")]
        [HttpPost]
        public async Task<ActionResult> ReportErrors(ErrorReportingModel model)
        {
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                _notificationService.SendStandardHTMLEmail(
                    "Error: " + model.statusCode + "Application: " + model.application,
                    "<div><h1>Error Notification</h1><p>An error occurred in the application and the following details have been recorded:</p><ul>  <li><strong>Auth User:</strong>"
                        + authUser.ItsId
                        + "</li> <li><strong>Auth Name:</strong>"
                        + authUser.Name
                        + "</li> <li><strong>Application:</strong>"
                        + model.application
                        + "</li> <li><strong>Error Message:</strong>"
                        + model.errorMessage
                        + "</li>  <li><strong>Error payload:</strong>"
                        + model.payload
                        + "  <li><strong>Status Code:</strong>"
                        + model.statusCode
                        + "</li>    <li><strong>URL:</strong> "
                        + model.url
                        + "</li>    <li><strong>Method:</strong> "
                        + model.method
                        + "</li> </ul></div>",
                    "itsupport@mahadalzahra.com",
                    "error"
                );

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

    public class ReceiptModel
    {
        public string ReceiptNo { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string ItsId { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string ChequeDate { get; set; }
        public string FeePaidAmount { get; set; }
        public string AmountInWord { get; set; }
        public string PreparedBy { get; set; }
    }

    public class ErrorReportingModel
    {
        public string errorMessage { get; set; }
        public string statusCode { get; set; }
        public string url { get; set; }
        public string method { get; set; }
        public string? payload { get; set; }
        public string application { get; set; }
    }

    public class datewiseExpenseReport
    {
        public String DATE { get; set; }
        public DateTime d { get; set; }
        public String Unique_ID { get; set; }
        public String Dept { get; set; }
        public String Venue { get; set; }
        public String ExpenseHead { get; set; }
        public String Vendor_Name { get; set; }
        public String Item_Names { get; set; }
        public String Remarks { get; set; }
        public String Type_of_payment { get; set; }
        public String Mode_of_payment { get; set; }
        public String CHQ_NO { get; set; }
        public int AMOUNT { get; set; }
    }

    public class ExportModel_IncomeReceived
    {
        public string srNo { get; set; }

        public string receitpNo { get; set; }
        public string receiptDate { get; set; }
        public string transactionId { get; set; }
        public string transactionDate { get; set; }
        public string itsId { get; set; }
        public string name { get; set; }
        public string debit { get; set; }
        public string credit { get; set; }
        public string paymentMode { get; set; }
        public string program { get; set; }
        public string subProgram { get; set; }
        public string venue { get; set; }
        public string remarks { get; set; }
        public string createdBy { get; set; }
    }
}
