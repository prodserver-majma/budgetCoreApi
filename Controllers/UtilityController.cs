using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        public UtilityController(mzdbContext context, IMapper mapper, TokenService tokenService)
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

        [Route("getcalender")]
        [HttpPost]
        public async Task<ActionResult> getcalender(calenderRetrieveModel model)
        {
            string api = "api/calender/getcalender";
            //// Add_ApiLogs(api);

            try
            {
                DateTime refEngDate = new DateTime();
                CalenderModel resultStartDate = new CalenderModel();
                CalenderModel resultEndDate = new CalenderModel();
                DateTime today = DateTime.Today;

                List<WeekModel> weeks = new List<WeekModel>();

                CalenderModel currentDate = _helperService.getHijriDate(today);

                if (model.hijriYear != 0)
                {

                    CalenderModel refDate = new CalenderModel
                    {
                        hijDay = 1,
                        hijMonth = model.hijriMonthId,
                        hijYear = model.hijriYear
                    };

                    refEngDate = _helperService.getEngDate(refDate);

                    resultStartDate = gregorianDateForStartOfCurrentHijriMonth(refEngDate);
                    resultEndDate = gregorianDateForEndOfCurrentHijriMonth(refEngDate);

                }
                else
                {
                    resultStartDate = gregorianDateForStartOfCurrentHijriMonth(today);
                    resultEndDate = gregorianDateForEndOfCurrentHijriMonth(today);
                }

                weeks = _helperService.GenerateCalendar(resultStartDate, resultEndDate);
                return Ok(new MonthlyCallenderModel
                {
                    startDate = resultStartDate.engDate,
                    endDate = resultEndDate.engDate,
                    weeks = weeks,
                    hijriYear = model.hijriYear == 0 ? currentDate.hijYear : model.hijriYear,
                    hijriMonth = model.hijriYear == 0 ? currentDate.hijMonth : model.hijriMonthId
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Route("gregoriandateforcurrenthijrimonth")]
        [HttpGet]
        public async Task<ActionResult> apiForStartofCurrentHijriMonth()
        {
            string api = "api/utility/gregoriandateForcurrenthijrimonth";
            DateTime todayDate = DateTime.Today;


            return Ok(gregorianDateForStartOfCurrentHijriMonth(todayDate).engDate);
        }

        private CalenderModel gregorianDateForStartOfCurrentHijriMonth(DateTime todayDate)
        {
            string api = "api/utility/gregoriandateForcurrenthijrimonth";
            //// Add_ApiLogs(api);

            CalenderModel dateModel = _helperService.getHijriDate(todayDate);
            dateModel.hijDay = 1;
            dateModel.engDate = _helperService.getEngDate(dateModel);
            return dateModel;
        }

        private CalenderModel gregorianDateForEndOfCurrentHijriMonth(DateTime todayDate)
        {
            string api = "api/utility/gregoriandateForcurrenthijrimonth";
            //// Add_ApiLogs(api);

            CalenderModel dateModel = _helperService.getHijriDate(todayDate);
            dateModel.hijDay = 30;
            dateModel.engDate = _helperService.getEngDate(dateModel);
            if (dateModel.engDate != DateTime.MinValue)
            {
                return dateModel;
            }
            dateModel.hijDay = 29;
            dateModel.engDate = _helperService.getEngDate(dateModel);
            return dateModel;
        }

    }

    public class calenderRetrieveModel
    {
        public DateTime date { get; set; }
        public int hijriMonthId { get; set; }
        public int hijriYear { get; set; }
    }
}
