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
    public class WajebaatController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;
        private readonly WhatsAppApiService _whatsappApiService;

        public WajebaatController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _salaryService = new SalaryService(context);
            _helperService = new HelperService(context);
            _itsService = new ItsServiceRemote();
            _jhsService = new IJHSServiceRemote();
            _globalConstants = new globalConstants();
            _whatsappApiService = new WhatsAppApiService(context);
        }
        private static readonly int currentHijriYear = 1446;
        private static readonly int LastHijriyear = 1445;

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);


        [Route("submitwajeebataraz")]
        [HttpPost]
        public async Task<ActionResult> SubmitWajeebatAraz(WajebaatArazModel m)
        {
            string api = "submitwajeebataraz";
            //// Add_ApiLogs(api);

            int? id = null;
            try
            {
                mz_kg_wajebaat_araz model = _mapper.Map<mz_kg_wajebaat_araz>(m);

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                model.itsId = authUser.ItsId;
                mz_kg_wajebaat_araz waj = _context.mz_kg_wajebaat_araz.Where(x => x.itsId == authUser.ItsId && x.hijriYear == model.hijriYear).FirstOrDefault();
                currency_converter_new cc = _context.currency_converter_new.Where(x => x.fromCurrencyName == model.currency && x.toCurrencyName == "INR").FirstOrDefault();
                float rate = 1f;

                bool isFamily = model.wajebaatType.Contains("Family");
                bool emptyCC = cc == null;

                if (!emptyCC)
                {
                    emptyCC = cc.value == null;
                }
                if (!isFamily && emptyCC)
                {
                    if (model.currency != "INR")
                    {
                        throw new Exception("currency conversion not found");
                    }
                }
                else
                {
                    rate = cc?.value ?? 1f;
                }

                if (waj == null)
                {
                    model.currencyRate = rate;
                    model.displayCurrency = "INR";
                    model.createdOn = indianTime;
                    model.createdBy = authUser.Name;
                    if (isFamily)
                    {
                        model.niyyatAmount = 0;
                        model.takhmeenAmount = 0;
                    }
                    else if (model.wajebaatType == "Takhmeen Done")
                    {
                        model.takhmeenAmount = model.niyyatAmount;
                    }
                    model.stage = "Initialized";
                    _context.mz_kg_wajebaat_araz.Add(model);
                }
                else
                {
                    waj.currency = model.currency;
                    waj.bankName = model.bankName;
                    waj.draftDate = model.draftDate;
                    waj.draftNo = model.draftNo;
                    if (waj.draftNo != null && waj.draftNo != "")
                    {
                        waj.stage = "Submitted";
                    }
                    waj.niyyatAmount = model.niyyatAmount;
                    waj.officeRemarks = model.officeRemarks;
                    waj.paidAmount = model.paidAmount;
                    waj.userRemarks = model.userRemarks;
                    waj.createdBy = authUser.Name;
                    waj.createdOn = indianTime;
                    if (model.wajebaatType == "With Family")
                    {
                        waj.takhmeenAmount = 0;
                    }
                    else if (model.wajebaatType == "Takhmeen Done")
                    {
                        waj.takhmeenAmount = model.niyyatAmount;
                    }
                    waj.displayCurrency = "INR";
                    waj.currencyRate = rate;
                    waj.wajebaatType = model.wajebaatType;
                }
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("submitwajebaattakhmin")]
        [HttpPost]
        public async Task<ActionResult> SubmitTakhmin(List<WajebaatModel> model)
        {
            string api = "submitwajebaattakhmin";
            //// Add_ApiLogs(api);

            int? id = null;
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<khidmat_guzaar> kgs = _context.khidmat_guzaar.ToList();

                foreach (var i in model)
                {
                    khidmat_guzaar kg = kgs.Where(x => x.itsId == i.itsId).FirstOrDefault();

                    mz_kg_wajebaat_araz waj = _context.mz_kg_wajebaat_araz.Where(x => x.id == i.id).FirstOrDefault();
                    waj.updatedBy = authUser.Name;
                    waj.updatedOn = indianTime;


                    if (i.wajebaatType == "With Family")
                    {
                        waj.takhmeenAmount = 0;
                    }
                    else
                    {
                        waj.takhmeenAmount = i.takhmeenAmount / i.thisYear_currencyrate;
                    }

                    if (kg != null && !string.IsNullOrEmpty(kg.whatsappNo) && !string.IsNullOrEmpty(kg.c_codeWhatsapp))
                    {
                        List<string> num = new List<string> { kg.c_codeWhatsapp + kg.whatsappNo };

                        try
                        {
                            _whatsappApiService.sendStarMarketingGeneralWhatsapp(num, "Salaam Jameel,\n\nYour Wajebaat Takhmeen has been done\n\nkindly check on mahadalzahra.org\n\nShukran\nWa al-Salaam", "Maqaraat Module Notification");
                        }
                        catch (Exception e)
                        { }
                    }

                }
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Exception" });
            }
        }


        [Route("getWajebaatDataforFaculty")]
        [HttpGet]
        public async Task<ActionResult> getsuratAuditedData()
        {
            string api = "getWajebaatDataforFaculty";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                mz_kg_wajebaat_araz currentHijriYearData = new mz_kg_wajebaat_araz();
                mz_kg_wajebaat_araz lastHijriYearData = new mz_kg_wajebaat_araz();
                currentHijriYearData = _context.mz_kg_wajebaat_araz.Where(x => x.itsId == authUser.ItsId && x.hijriYear == currentHijriYear).FirstOrDefault();
                lastHijriYearData = _context.mz_kg_wajebaat_araz.Where(x => x.itsId == authUser.ItsId && x.hijriYear == LastHijriyear).FirstOrDefault();

                khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId && x.employeeType == "Khidmatguzaar").Include(x => x.mauzeNavigation).FirstOrDefault();

                venue v = kg.mauzeNavigation;
                if (currentHijriYearData == null)
                {
                    currentHijriYearData = new mz_kg_wajebaat_araz();
                }
                if (lastHijriYearData == null)
                {
                    lastHijriYearData = new mz_kg_wajebaat_araz();
                }

                currentHijriYearData.hijriYear = currentHijriYear;
                lastHijriYearData.hijriYear = LastHijriyear;


                return Ok(new { lastHijriYearData = lastHijriYearData, currentHijriYearData = currentHijriYearData });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getWajebaatDataforTakhmin")]
        [HttpGet]
        public async Task<ActionResult> getWajebaatDataForTakhmin()
        {
            string api = "getWajebaatDataforTakhmin";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<WajebaatModel> models = new List<WajebaatModel>();

                List<mz_kg_wajebaat_araz> currentHijriYearData = new List<mz_kg_wajebaat_araz>();

                currentHijriYearData = _context.mz_kg_wajebaat_araz.Where(x => x.hijriYear == currentHijriYear && x.takhmeenAmount == null && x.niyyatAmount != null).ToList();

                foreach (var i in currentHijriYearData)
                {
                    mz_kg_wajebaat_araz lastYear = _context.mz_kg_wajebaat_araz.Where(x => x.itsId == i.itsId && x.hijriYear == LastHijriyear).FirstOrDefault();
                    khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == i.itsId && x.employeeType == "Khidmatguzaar").Include(x => x.mauzeNavigation).FirstOrDefault();

                    venue v = kg.mauzeNavigation;

                    var m = new WajebaatModel
                    {
                        khidmatMoze = v?.displayName,
                        itsId = i.itsId,
                        id = i.id,
                        age = kg.age?.ToString(),
                        hijriYear = currentHijriYear,
                        lastYearWajebaat = lastYear?.takhmeenAmount ?? 0,
                        lastYear_currencyrate = lastYear?.currencyRate ?? 0,
                        thisYear_currencyrate = i.currencyRate ?? 0,
                        lastYear_currency = lastYear?.currency,
                        thisYear_currency = i.currency,
                        name = kg.fullName,
                        takhmeenAmount = Convert.ToInt32(i.niyyatAmount * i.currencyRate),
                        mzIdara = kg.its_preferredIdara,
                        niyyatAmount = i.niyyatAmount,
                        wajebaatType = i.wajebaatType,
                        stage = i.stage
                    };
                    models.Add(m);

                }
                return Ok(new { models = models.OrderByDescending(x => x.takhmeenAmount).ToList(), lastHijriYear = LastHijriyear, currentHijriyear = currentHijriYear });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getWajebaatDataAfterTakhmin")]
        [HttpGet]
        public async Task<ActionResult> getWajebaatDataAfterAdmin()
        {
            string api = "getWajebaatDataAfterTakhmin";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<WajebaatModel> models = new List<WajebaatModel>();

                List<mz_kg_wajebaat_araz> currentHijriYearData = new List<mz_kg_wajebaat_araz>();

                currentHijriYearData = _context.mz_kg_wajebaat_araz.Where(x => x.hijriYear == currentHijriYear && x.takhmeenAmount != null).ToList();

                foreach (var i in currentHijriYearData)
                {
                    mz_kg_wajebaat_araz lastYear = _context.mz_kg_wajebaat_araz.Where(x => x.itsId == i.itsId && x.hijriYear == LastHijriyear).FirstOrDefault();
                    khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == i.itsId && x.employeeType == "Khidmatguzaar").Include(x => x.mauzeNavigation).FirstOrDefault();

                    venue v = kg?.mauzeNavigation;

                    string lAmount = "";

                    if (lastYear == null)
                    {
                        lAmount = "Not Found";
                    }
                    else
                    {
                        lAmount = lastYear.takhmeenAmount?.ToString() + " " + lastYear.currency;
                    }

                    var m = new WajebaatModel
                    {
                        khidmatMoze = v?.displayName ?? "N/A",
                        itsId = i.itsId,
                        id = i.id,
                        age = kg.age?.ToString(),
                        hijriYear = currentHijriYear,
                        lastYearWajebaat = lastYear?.takhmeenAmount ?? 0,
                        lastYear_currencyrate = lastYear?.currencyRate ?? 0,
                        thisYear_currencyrate = i.currencyRate ?? 0,
                        lastYear_currency = lastYear?.currency,
                        thisYear_currency = i.currency,
                        name = kg.fullName,
                        takhmeenAmount = i.takhmeenAmount ?? 0,
                        mzIdara = kg.its_preferredIdara,
                        niyyatAmount = i.niyyatAmount,
                        wajebaatType = i.wajebaatType,
                        stage = i.stage
                    };

                    models.Add(m);
                }

                return Ok(models.OrderByDescending(x => x.takhmeenAmount * x.thisYear_currencyrate).ToList());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getsubmittedWajebaatData")]
        [HttpGet]
        public async Task<ActionResult> getSubmittedWajebaatData()
        {
            string api = "getWajebaatDataAfterTakhmin";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<WajebaatModel> models = new List<WajebaatModel>();

                List<mz_kg_wajebaat_araz> currentHijriYearData = new List<mz_kg_wajebaat_araz>();

                currentHijriYearData = _context.mz_kg_wajebaat_araz.Where(x => x.hijriYear == currentHijriYear && x.takhmeenAmount != null && (x.stage == "Submitted" || x.stage == "Verified")).ToList();

                foreach (var i in currentHijriYearData)
                {
                    mz_kg_wajebaat_araz lastYear = _context.mz_kg_wajebaat_araz.Where(x => x.itsId == i.itsId && x.hijriYear == LastHijriyear).FirstOrDefault();
                    khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == i.itsId && x.employeeType == "Khidmatguzaar").Include(x => x.mauzeNavigation).FirstOrDefault();

                    if (kg == null)
                    {
                        continue;
                    }

                    venue v = kg?.mauzeNavigation ?? new venue();

                    string lAmount = "";

                    if (lastYear == null)
                    {
                        lAmount = "Not Found";
                    }
                    else
                    {
                        lAmount = lastYear.takhmeenAmount?.ToString() + " " + lastYear.currency;
                    }

                    var m = new WajebaatModel
                    {
                        khidmatMoze = v?.displayName,
                        itsId = i.itsId,
                        id = i.id,
                        age = kg.age?.ToString(),
                        hijriYear = currentHijriYear,
                        lastYearWajebaat = lastYear?.takhmeenAmount ?? 0,
                        lastYear_currencyrate = lastYear?.currencyRate ?? 0,
                        thisYear_currencyrate = i.currencyRate ?? 0,
                        lastYear_currency = lastYear?.currency,
                        thisYear_currency = i.currency,
                        name = kg.fullName,
                        takhmeenAmount = i.takhmeenAmount ?? 0,
                        mzIdara = kg.its_preferredIdara,
                        niyyatAmount = i.niyyatAmount,
                        wajebaatType = i.wajebaatType,
                        draftNo = i.draftNo,
                        draftDate = i.draftDate,
                        bankName = i.bankName,
                        paidAmount = i.paidAmount,
                        stage = i.stage
                    };

                    models.Add(m);
                }

                return Ok(models.OrderBy(x => x.stage).ThenByDescending(x => x.takhmeenAmount * x.thisYear_currencyrate).ToList());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("receivedsubmission")]
        [HttpPost]
        public async Task<ActionResult> approveRejectSubmission(WajebaatModel review)
        {

            mz_kg_wajebaat_araz araz = _context.mz_kg_wajebaat_araz.Where(x => x.id == review.id).FirstOrDefault();
            if (araz == null)
            {
                return BadRequest(new { message = "wajebaat araz not found" });
            }

            araz.draftDate = review.draftDate;
            araz.draftNo = review.draftNo;
            araz.paidAmount = review.paidAmount;
            araz.bankName = review.bankName;

            String msg = "Your Wajebaat draft has been recieved with details:\\\\n\\\\n Draft No: *" + araz.draftNo + "* \\\\n Date: *" + araz.draftDate?.ToString("dd/MM/yyyy") + "* \\\\n Bank Name: *" + araz.bankName + "*\\\\n Amount: *" + araz.paidAmount + "* \\\\n\\\\nFor any queries contact us: accounts.surat@mahadalzahra.com\\\\n\\\\nMahad al-Zahra Accounts Office";

            araz.stage = "Verified";
            araz.verifiedOn = DateTime.Now;
            _context.SaveChanges();
            string msgResponse = "";

            try
            {
                msgResponse = _whatsappApiService.sendGeneralMessage(review.itsId ?? 0, msg, "approveRejectSubmission-Wajebaat", "Wajebaat-recieved-noti");

            }
            catch (Exception ex)
            {
                return Ok("Varified but msg not sent error:" + ex.ToString() + " , msgError: " + msgResponse);
            }

            return Ok("Verified and message sent");

        }

        [Route("addcurrencyConversion/{fromCurrency}/{toCurrency}/{rate}")]
        [HttpGet]
        public async Task<ActionResult> addCurrencyConversion(string fromCurrency, string toCurrency, float rate)
        {
            string api = "addcurrencyConversion/{fromCurrency}/{toCurrency}/{rate}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<WajebaatModel> models = new List<WajebaatModel>();

                currency_converter_new currency = new currency_converter_new();

                currency.fromCurrencyName = fromCurrency;
                currency.toCurrencyName = toCurrency;
                currency.value = rate / 10000;

                var cc = _context.currency_converter_new.Where(x => x.fromCurrencyName == fromCurrency && x.toCurrencyName == toCurrency).FirstOrDefault();
                if (cc == null)
                {
                    _context.currency_converter_new.Add(currency);
                }
                else
                {
                    cc.value = rate / 10000;

                }
                _context.SaveChanges();


                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getcurrencyConversionData")]
        [HttpGet]
        public async Task<ActionResult> getCurrencyConversion()
        {
            string api = "getcurrencyConversionData";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<WajebaatModel> models = new List<WajebaatModel>();


                List<currency_converter_new> currencies = new List<currency_converter_new>();
                currencies = _context.currency_converter_new.ToList();

                return Ok(currencies.OrderBy(x => x.fromCurrencyName).ToList());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
