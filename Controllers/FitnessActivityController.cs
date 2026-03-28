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
    public class FitnessActivityController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        public FitnessActivityController(mzdbContext context, IMapper mapper, TokenService tokenService)
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
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);


        [Route("getfitnessdata")]
        [HttpGet]
        public async Task<ActionResult> getFitnessData()
        {
            string api = "getfitnessdata";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<fitness_activity> cat = _context.fitness_activity.Where(x => x.itsId == authUser.ItsId).ToList();
                return Ok(cat);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getfitnessdataofsingleuser/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getFitnessDataOfSingleUser(int itsId)
        {
            string api = "getfitnessdataofsingleuser/{itsId}";
            //// Add_ApiLogs(api);

            try
            {
                //AuthUser authUser = ServiceFactory.GetAuthService().GetAuthUser(HttpContext.Current.User);

                List<fitness_activity> cat = _context.fitness_activity.Where(x => x.itsId == itsId).ToList();
                return Ok(cat);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }



        [Route("submitfitnessdata")]
        [HttpPost]
        public async Task<ActionResult> SubmitFitnessData(fitnessActivity data)
        {
            string api = "submitfitnessdata";
            //// Add_ApiLogs(api);


            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            try
            {


                CalenderModel todayacedemic = _helperService.getAcedemicYear(DateTime.Today);

                byte[] binaryData1 = Convert.FromBase64String(data.attachmentFile ?? "");
                MemoryStream stream1 = new MemoryStream(binaryData1);
                if (!string.IsNullOrEmpty(data.attachmentFile))
                {
                    data.attachmentFile = await _helperService.UploadFileToS3(stream1, authUser.ItsId + "_" + data.activityName + "_" + data.attachmentFileName, "uploads/fitnessDocument");
                }

                fitness_activity ii = new fitness_activity
                {
                    academicYear = todayacedemic.acedemicYear,
                    routine = data.routine,
                    activityName = data.activityName,
                    createdOn = DateTime.Now,
                    hours = data.hours,
                    itsId = authUser.ItsId,
                    venue = data.venue,
                    minutes = data.minutes,
                    attachmentFile = data.attachmentFile
                };

                _context.fitness_activity.Add(ii);
                _context.SaveChanges();
                return Ok(ii.id);


            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "error" });
            }

        }

        [Route("deleteFitness/{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteFitness(int id)
        {
            string api = "deleteFitness/{id}";
            //// Add_ApiLogs(api);


            var f = _context.fitness_activity.Where(x => x.id == id).FirstOrDefault();
            if (f != null)
            {
                _context.fitness_activity.Remove(f);
                _context.SaveChanges();

            }

            return Ok();


        }


    }
}
