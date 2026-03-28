using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings.User;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QismAlTahfeezController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;
        private readonly QismService _qismService;

        public QismAlTahfeezController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _salaryService = new SalaryService(context);
            _helperService = new HelperService(context);
            _itsService = new ItsServiceRemote();
            _jhsService = new IJHSServiceRemote();
            _globalConstants = new globalConstants();
            _qismService = new QismService(context);
        }

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);


        [Route("addnewqism")]
        [HttpPost]
        public async Task<IActionResult> addNewQism(QismAlTahfeezModel2 nu)
        {
            string api = "addnewqism";
            //// Add_ApiLogs(api);

            string response = _qismService.PopulateQism(nu);

            if (response.Contains("success"))
            {
                return Ok();
            }

            return BadRequest();

        }

        [Route("getalldeptvenue")]
        [HttpGet]
        public async Task<IActionResult> getAllDeptVenue()
        {
            string api = "getalldeptvenue";
            //// Add_ApiLogs(api);
            try
            {

                List<DeptVenueRightModel> data = _qismService.getAllDeptVenue();

                return Ok(data);


            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [Route("getallregistrationname")]
        [HttpGet]
        public async Task<IActionResult> getAllRegistrationName()
        {


            string api = "getallregistrationname";
            //// Add_ApiLogs(api);

            return Ok(_qismService.getAllRegistrationName());
        }

        [Route("getallqism")]
        [HttpGet]
        public async Task<IActionResult> getAllQism()
        {

            string api = "getallqism";
            // Add_ApiLogs(api);

            return Ok(_qismService.getAllQism());
        }

        [Route("getallroles")]
        [HttpGet]
        public async Task<IActionResult> getAllRoles()
        {
            string api = "getallroles";
            // Add_ApiLogs(api);

            List<RoleResultModel> r = new List<RoleResultModel>();
            _context.platform_role.ToList().ForEach(x =>
            {
                r.Add(new RoleResultModel
                {
                    name = x.name,
                    description = x.description,
                    id = x.id,
                    isDefault = x.isDefault,
                    icon = x.icon
                });
            });

            return Ok(r);
        }

        [Route("getrole/{id}")]
        [HttpGet]
        public async Task<IActionResult> getRoleById(int id)
        {

            string api = "getqism/{id}";
            // Add_ApiLogs(api);

            return Ok(_qismService.getRoleRights(id));
        }

        [Route("getqism/{id}")]
        [HttpGet]
        public async Task<IActionResult> getQismById(int id)
        {

            string api = "getqism/{id}";
            // Add_ApiLogs(api);

            return Ok(_qismService.getQismById(id));
        }

        [Route("getrolerights/{itsId}/{qismId}")]
        [HttpGet]
        public async Task<IActionResult> getRoleRights(int itsId, int qismId)
        {
            string api = "getrolerights/{itsId}";
            // Add_ApiLogs(api);

            return Ok(_qismService.getUserRoleRights(itsId, qismId));
        }

        [Route("getrights/{itsId}/{qismId}")]
        [HttpGet]
        public async Task<IActionResult> getRights(int itsId, int qismId)
        {
            string api = "getrolerights/{itsId}";
            // Add_ApiLogs(api);
            dynamic d = new
            {
                role = _qismService.getUserRoleRights(itsId, qismId),
                pages = _qismService.getUserPageRights(itsId, qismId),
            };
            return Ok(d);
        }

        [Route("createrole")]
        [HttpPost]
        public async Task<IActionResult> createRole(RoleModel role)
        {
            string api = "createrole";
            // Add_ApiLogs(api);

            try
            {
                String l = _qismService.createRole(role);
                return Ok(l);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [Route("toggledefaultrole/{roleId}")]
        [HttpGet]
        public async Task<IActionResult> toggleDefaultRole(int roleId)
        {
            string api = "toggledefaultrole";
            // Add_ApiLogs(api);

            try
            {
                String l = _qismService.toggleDefaultRole(roleId);
                return Ok(l);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [Route("toggledefaultmodule/{moduleId}")]
        [HttpGet]
        public async Task<IActionResult> toggleDefaultModule(int moduleId)
        {
            string api = "toggledefaultmodule/{moduleId}";
            // Add_ApiLogs(api);

            try
            {
                String l = _qismService.toggleDefaultModule(moduleId);
                return Ok(l);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [Route("updaterole")]
        [HttpPost]
        public async Task<IActionResult> updateRole(RoleModel rights)
        {
            string api = "updaterolerights/{roleId}";
            // Add_ApiLogs(api);

            try
            {
                RoleModel l = _qismService.updateRoleRights(rights);
                return Ok(l);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [Route("updaterolerights/{userId}/{qismId}")]
        [HttpPost]
        public async Task<IActionResult> submitRoleRights(int userId, int qismId, List<RoleModel> rights)
        {
            string api = "updaterolerights/{roleId}";
            // Add_ApiLogs(api);


            try
            {
                List<RoleModel> l = _qismService.updateUserRoleRights(userId, qismId, rights);
                return Ok(l);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [Route("updatepagerights/{userId}/{qismId}")]
        [HttpPost]
        public async Task<IActionResult> submitpageRights(int userId, int qismId, List<PageModel> rights)
        {
            string api = "updatepagerights/{roleId}";
            // Add_ApiLogs(api);


            try
            {
                List<PageModel> l = _qismService.updateUserPageRights(userId, qismId, rights);
                return Ok(l);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }


        }


        [Route("tagqismadmin/{userId}/{qismId}")]
        [HttpGet]
        public async Task<IActionResult> tagQismAdmin(int userId, int qismId)
        {
            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                string api = "tagqismadmin/{userId}/{qismId}";
                // Add_ApiLogs(api);
                try
                {
                    _qismService.tagQismAdmin(userId, qismId, authUser);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("removeqismadmin/{userId}")]
        [HttpGet]
        public async Task<IActionResult> removeQismAdmin(int userId)
        {
            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                string api = "removeqismadmin/{userId}";
                // Add_ApiLogs(api);

                return Ok(_qismService.removeQismAdmin(userId, authUser));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

    public class RoleResultModel
    {
        public string name { get; set; }
        public string description { get; set; }
        public int id { get; set; }
        public bool isDefault { get; set; }
        public string icon { get; set; }
    }
}
