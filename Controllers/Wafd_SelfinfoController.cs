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
    public class Wafd_SelfinfoController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        public Wafd_SelfinfoController(mzdbContext context, IMapper mapper, TokenService tokenService)
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


        [Route("getlanguageproficiencydetails/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getlanguageproficiencydetails(int itsId)
        {
            string api = "getlanguageproficiencydetails/{itsId}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                if (itsId == 500)
                {
                    itsId = authUser.ItsId;
                }


                List<wafd_languageproficiency> datas = _context.wafd_languageproficiency.Where(x => x.itsId == itsId).ToList();
                List<wafd_languageproficiency> datas1 = new List<wafd_languageproficiency>();
                foreach (var i in datas)
                {
                    int a = (Convert.ToInt32(i.listening) + Convert.ToInt32(i.reading) + Convert.ToInt32(i.speaking) + Convert.ToInt32(i.writing)) / 4;

                    i.selfRanking = a;
                    datas1.Add(i);
                }
                datas1 = datas1.OrderByDescending(x => x.selfRanking).ToList();


                return Ok(datas1);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }



        [Route("getphysicalfitnessdetails/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getphysicalfitnessdetails(int itsId)
        {
            string api = "getphysicalfitnessdetails/{itsId}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                if (itsId == 500)
                {
                    itsId = authUser.ItsId;
                }
                List<wafd_physicalfitness> datas = _context.wafd_physicalfitness.Where(x => x.itsId == itsId).OrderByDescending(x => x.selfRanking).ToList();


                return Ok(datas);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getfieldofinterestdetails/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getfieldofinterestdetails(int itsId)
        {
            string api = "getfieldofinterestdetails/{itsId}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                if (itsId == 500)
                {
                    itsId = authUser.ItsId;
                }
                List<wafd_fieldofinterest> datas = _context.wafd_fieldofinterest.Where(x => x.itsId == itsId).OrderByDescending(x => x.selfRanking).ToList();


                return Ok(datas);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("submitphysicalfitnessdetails")]
        [HttpPost]
        public async Task<ActionResult> submitphysicalfitnessdetails(wafd_physicalfitness model)
        {
            string api = "submitphysicalfitnessdetails";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                model.itsId = authUser.ItsId;

                _context.wafd_physicalfitness.Add(model);

                _context.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("submitfieldofinterestdetails")]
        [HttpPost]
        public async Task<ActionResult> submitfieldofinterestdetails(Wafd_FieldOfInterest model)
        {
            string api = "submitfieldofinterestdetails";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                model.itsId = authUser.ItsId.ToString();

                _context.wafd_fieldofinterest.Add(new wafd_fieldofinterest { fieldofInterest = model.fieldofinterest, itsId = authUser.ItsId, selfRanking = model.selfRanking });

                _context.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("submitlanguageproficiencydetails")]
        [HttpPost]
        public async Task<ActionResult> submitlanguageproficiencydetails(WafdLanguageProficiency model)
        {
            string api = "submitlanguageproficiencydetails";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                model.itsId = authUser.ItsId;
                wafd_languageproficiency wafd_Languageproficiency = _mapper.Map<wafd_languageproficiency>(model);

                var i = _context.wafd_languageproficiency.Where(x => x.language == model.language && x.itsId == model.itsId).FirstOrDefault();

                if (i != null)
                {
                    _context.wafd_languageproficiency.Remove(i);
                    _context.wafd_languageproficiency.Add(wafd_Languageproficiency);
                }
                else
                {
                    _context.wafd_languageproficiency.Add(wafd_Languageproficiency);
                }
                _context.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("deletephysicalfitness/{id}")]
        [HttpDelete]
        public async Task<ActionResult> deletephysicalfitness(int id)
        {
            string api = "deletephysicalfitness/{id}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


                wafd_physicalfitness f = _context.wafd_physicalfitness.Where(x => x.id == id).FirstOrDefault();

                _context.wafd_physicalfitness.Remove(f);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("deletelanguageproficiency/{id}")]
        [HttpDelete]
        public async Task<ActionResult> deletelanguageproficiency(int id)
        {
            string api = "deletelanguageproficiency/{id}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                wafd_languageproficiency f = _context.wafd_languageproficiency.Where(x => x.id == id).FirstOrDefault();

                _context.wafd_languageproficiency.Remove(f);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("deletefieldofinterest/{id}")]
        [HttpDelete]
        public async Task<ActionResult> deletefieldofinterest(int id)
        {
            string api = "deletefieldofinterest/{id}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


                wafd_fieldofinterest f = _context.wafd_fieldofinterest.Where(x => x.id == id).FirstOrDefault();

                _context.wafd_fieldofinterest.Remove(f);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("submitSelfAssessment")]
        [HttpPost]
        public async Task<ActionResult> submitSelfAssessment(EmployeeSelfAssessmentModel model)
        {
            string api = "submitSelfAssessment";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                model.itsId = authUser.ItsId;

                bool savefile = false;
                try
                {
                    byte[] binaryData1 = Convert.FromBase64String(model.personalityReport ?? "");
                    MemoryStream stream1 = new MemoryStream(binaryData1);

                    if (!string.IsNullOrEmpty(model.personalityReport))
                    {
                        model.personalityReport = await _helperService.UploadFileToS3(stream1, authUser.ItsId + "_" + model.personalitytype + "_" + model.personalityReportFileName, "uploads/personalitytype");
                        savefile = true;
                    }
                }
                catch (Exception ex)
                {
                    savefile = false;
                }

                kg_self_assessment t = (from sa in _context.kg_self_assessment where sa.itsId == model.itsId select sa).FirstOrDefault();
                if (t != null)
                {
                    t.longTermGoal = model.longTermGoal;
                    t.roleModel = model.roleModel;
                    t.strength = model.strength;
                    t.weakness = model.weakness;
                    t.changeAboutYourself = model.changeAboutYourself;
                    t.alternativeCareerPath = model.alternativeCareerPath;
                    if (savefile)
                    {
                        t.personalityReport = model.personalityReport;
                    }

                    _context.SaveChanges();
                    return Ok();
                }



                kg_self_assessment n = new kg_self_assessment();

                n.itsId = model.itsId;
                n.longTermGoal = model.longTermGoal;
                n.roleModel = model.roleModel;
                n.strength = model.strength;
                n.weakness = model.weakness;
                n.changeAboutYourself = model.changeAboutYourself;
                n.alternativeCareerPath = model.alternativeCareerPath;
                if (savefile)
                {
                    n.personalityReport = model.personalityReport;
                }

                _context.kg_self_assessment.Add(n);

                _context.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getSelfAssessment")]
        [HttpGet]
        public async Task<ActionResult> getSelfAssessment()
        {
            string api = "getSelfAssessment";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                kg_self_assessment n = new kg_self_assessment();
                kg_self_assessment t = (from sa in _context.kg_self_assessment where sa.itsId == authUser.ItsId select sa).FirstOrDefault();

                if (t != null)
                {
                    n = t;
                }

                return Ok(n);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}