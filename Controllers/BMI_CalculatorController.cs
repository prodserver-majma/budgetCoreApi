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
    public class BMI_CalculatorController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        public BMI_CalculatorController(mzdbContext context, IMapper mapper, TokenService tokenService)
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


        [Route("getbmidata")]
        [HttpGet]
        public async Task<ActionResult> getBmi_Data()
        {
            string api = "getbmidata";
            //// Add_ApiLogs(api);

            List<BMI_Model> Model = new List<BMI_Model>();
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<bmi_data> cat = _context.bmi_data.Where(x => x.itsId == authUser.ItsId).ToList();
                int c = 0;
                foreach (var i in cat)
                {
                    c = c + 1;
                    float? h = i.height_in_cemtimeter / 100;

                    string result = getResult(i.bmi);
                    string remarks = getRemarks(h);


                    BMI_Model c1 = new BMI_Model
                    {
                        srNo = c,
                        id = i.id,
                        height_in_Centimeter = i.height_in_cemtimeter,
                        Weight_in_kilogram = i.weight_in_kilogram,
                        remarks = remarks,
                        bmi = i.bmi,
                        createdOn = i.createdOn,
                        result = result
                    };

                    Model.Add(c1);
                }

                return Ok(Model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getbmidataforadmin/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getBmi_Data_forAdmin(int itsId)
        {
            string api = "getbmidataforadmin/{itsId}";
            //// Add_ApiLogs(api);

            List<BMI_Model> Model = new List<BMI_Model>();
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<bmi_data> cat = _context.bmi_data.Where(x => x.itsId == itsId).ToList();
                int c = 0;
                foreach (var i in cat)
                {
                    c = c + 1;
                    float? h = i.height_in_cemtimeter / 100;

                    string result = getResult(i.bmi);
                    string remarks = getRemarks(h);


                    BMI_Model c1 = new BMI_Model { srNo = c, height_in_Centimeter = i.height_in_cemtimeter, Weight_in_kilogram = i.weight_in_kilogram, remarks = remarks, bmi = i.bmi, createdOn = i.createdOn, result = result };

                    Model.Add(c1);
                }

                return Ok(Model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("submitbmidata")]
        [HttpPost]
        public async Task<ActionResult> SubmitBmi_Data(BMI_Model data)
        {
            string api = "submitbmidata";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            try
            {



                float w = data.Weight_in_kilogram ?? 0.0f;
                float h1 = data.height_in_Centimeter ?? 0.0f;


                float h = h1 / 100;


                float bmi = ((w) / (h * h));

                bmi_data ii = new bmi_data { bmi = bmi, createdOn = DateTime.Now, height_in_cemtimeter = h1, weight_in_kilogram = w, itsId = authUser.ItsId };

                _context.bmi_data.Add(ii);
                _context.SaveChanges();






                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "error" });
            }

        }


        [Route("deletebmi/{id}")]
        [HttpDelete]
        public async Task<ActionResult> getBmi_Data(int id)
        {
            string api = "deletebmi/{id}";

            List<BMI_Model> Model = new List<BMI_Model>();
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                bmi_data cat = _context.bmi_data.Where(x => x.id == id).FirstOrDefault();

                _context.bmi_data.Remove(cat);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        private string getResult(float? bmi)
        {
            string r = "";

            if (bmi < 16.0)
            {
                r = "Severely Underweight";
            }
            else if (bmi >= 16.0 && bmi <= 18.4)
            {
                r = "Underweight";
            }
            else if (bmi >= 18.5 && bmi <= 24.9)
            {
                r = "Normal";
            }
            else if (bmi >= 25.0 && bmi <= 29.9)
            {
                r = "Overweight";
            }
            else if (bmi >= 30.0 && bmi <= 34.9)
            {
                r = "Moderately Obese";
            }
            else if (bmi >= 35.0 && bmi <= 39.9)
            {
                r = "Severely Obese";
            }
            else if (bmi >= 40)
            {
                r = "Morbidly Obese";
            }

            return r;

        }

        private string getRemarks(float? height)
        {

            float bmi_from = 18.5f;
            float bmi_to = 24.9f;

            float h = (float)Math.Pow(Convert.ToDouble(height.ToString()), 2);

            double w_from = Math.Round((18.5f * (h)), 2);
            double w_to = Math.Round((24.9f * (h)), 2);



            string r = "Your Normal Weight Range is " + w_from + " kg to " + w_to + " kg";

            return r;
        }

    }
}
