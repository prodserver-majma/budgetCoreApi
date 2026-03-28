using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace mahadalzahrawebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly HelperService _helperService;
        private readonly NotificationService _notificationService;
        private readonly WhatsAppApiService _whatsAppApiService;
        private readonly string adminEmails =
            "admin@mahadalzahra.com, juzerdiwan@jameasaifiyah.edu";
        private readonly globalConstants _globalConstants;
        private readonly ModuleService _moduleService;

        public ModuleController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _helperService = new HelperService(context);
            _notificationService = new NotificationService();
            _globalConstants = new globalConstants();
            _whatsAppApiService = new WhatsAppApiService(context);
            _moduleService = new ModuleService(context);
        }

        [Route("getrights/{itsId}")]
        [HttpGet]
        public async Task<IActionResult> getRights(int itsId)
        {
            string api = "getrights/{itsId}";
                        
            //ServiceFactory.GetHelperService().//
            if (itsId == 500)
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                itsId = authUser.ItsId;
                return Ok(_moduleService.getRights(itsId, authUser.loginName));
            }else
            {
            return Ok(_moduleService.getRights(itsId, "Admin"));

            }

        }

        [Route("getsubrights/{itsId}/{moduleId}")]
        [HttpGet]
        public async Task<IActionResult> getSubRights(int itsId, int moduleId)
        {
            string api = "getsubrights/{itsId}/{moduleId}";

            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            //ServiceFactory.GetHelperService().//

            if (itsId == 500)
            {
                itsId = authUser.ItsId;
            }
            return Ok(_moduleService.getSubRights(itsId, moduleId, authUser.loginName));
        }

        [Route("availablemodules/{qismId}")]
        [HttpGet]
        public async Task<IActionResult> getAllAvailableModules(int qismId)
        {
            string api = "availablerights/{qismId}";

            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            //ServiceFactory.GetHelperService().//

            return Ok(_moduleService.getAllAvailableRights(qismId, authUser));
        }

        [Route("getmoduleonoffdata/{moduleId}")]
        [HttpGet]
        public async Task<IActionResult> getModuleOnOffData(int moduleId)
        {
            string api = "getmoduleonoffdata/{moduleId}";

            return Ok(
                _context.mz_on_off_modules.Where(x => x.id == moduleId).FirstOrDefault().status
            );
        }

        [Route("availableusermodules/{qismId}")]
        [HttpGet]
        public async Task<IActionResult> getAllAvailableModules(
            [FromRoute] int qismId,
            [FromQuery] int? userId
        )
        {
            string api = "availableusermodules/{userId}/{qismId}";

            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            //ServiceFactory.GetHelperService().//
            {
                userId = authUser.ItsId;
            }

            return Ok(_moduleService.getAllUserModuleRights(userId ?? 0, qismId, authUser));
        }

        [Route("updaterolerights/{roleId}")]
        [HttpPost]
        public async Task<IActionResult> updateRoleRights(int roleId, List<RoleRightsModel> rights)
        {
            string api = "updaterolerights/{roleId}";
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            //ServiceFactory.GetHelperService().//
            return Ok(_moduleService.updateRoleRights(roleId, rights));
        }

        [Route("submitmoduleonoff/{moduleId}")]
        [HttpPost]
        public async Task<IActionResult> submitModuleOnOff(int moduleId)
        {
            string api = "submitmoduleonoff/{moduleId}";
            //

            try
            {
                mz_on_off_modules m = _context
                    .mz_on_off_modules.Where(x => x.id == moduleId)
                    .FirstOrDefault();

                if (m.status == true)
                {
                    m.status = false;
                }
                else
                {
                    m.status = true;
                }
                _context.SaveChanges();

                return Ok("succesfully Submitted");
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
            return BadRequest(new { message = "Error" });
        }

        [Route("getallpages")]
        [HttpGet]
        public async Task<IActionResult> getAllPages()
        {
            string api = "getrights/{itsId}";

            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            return Ok(_moduleService.getAllPages());
        }



        [Route("getallbutton")]
        [HttpGet]
        public async Task<IActionResult> getAllButtons()
        {
            string api = "getrights/{itsId}";

            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<buttonResultModel> buttons = _context
                .platform_button.ToList()
                .Select(x => new buttonResultModel { id = x.id, name = x.name })
                .ToList();
            return Ok(buttons);
        }

        public struct buttonToAdd
        {
            public string name;
        }

        [Route("addButton")]
        [HttpPost]
        public async Task<IActionResult> AddButton(buttonToAdd button)
        {
            string api = "addButton";

            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            platform_button b = new platform_button { name = button.name };
            _context.platform_button.Add(b);
            _context.SaveChanges();
            return Ok(b);
        }

        [Route("modulestatusforauthuser/{moduleId}")]
        [HttpGet]
        public async Task<IActionResult> submitModuleOnOffException(int moduleId)
        {
            string api = "submitmoduleonoff/{moduleId}";
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            bool result = false;

            try
            {
                mz_off_module_exception m = _context
                    .mz_off_module_exception.Where(x =>
                        x.moduleId == moduleId && x.itsId == authUser.ItsId
                    )
                    .FirstOrDefault();

                if (m != null)
                {
                    result = true;
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error" });
            }
        }

        public struct pageToAdd
        {
            public string pageName;
            public string description;
        }

        [Route("addPage")]
        [HttpPost]
        public async Task<IActionResult> AddPage(pageToAdd page)
        {
            string api = "addButton";
            //
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            platform_page p = new platform_page
            {
                pageName = page.pageName,
                description = page.description
            };
            _context.platform_page.Add(p);
            _context.SaveChanges();
            return Ok(p);
        }

        [Route("addModules/{pageId}/{buttonId}")]
        [HttpGet]
        public async Task<IActionResult> addModules(int pageId, int buttonId)
        {
            string api = "addModules";
            //
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            platform_module pm = _context
                .platform_module.Where(x => x.pageId == pageId && x.buttonId == buttonId)
                .FirstOrDefault();
            if (pm != null)
            {
                return BadRequest(new { message = "module already exists" });
            }

            _context.platform_module.Add(
                new platform_module
                {
                    pageId = pageId,
                    buttonId = buttonId,
                    isDefault = false
                }
            );

            _context.SaveChanges();
            return Ok("Module successfully created");
        }

        [Route("removeModule/{moduleId}")]
        [HttpGet]
        public async Task<IActionResult> addModules(int moduleId)
        {
            string api = "addModules";
            //
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            platform_module pm = _context
                .platform_module.Where(x => x.id == moduleId)
                .FirstOrDefault();
            if (pm == null)
            {
                return BadRequest(new { message = "module does not exists" });
            }

            _context.platform_module.Remove(pm);

            _context.SaveChanges();
            return Ok("Module successfully removed");
        }

        [Route("getrolerights/{roleId}")]
        [HttpGet]
        public async Task<IActionResult> getRoleRights(int roleId)
        {
            string api = "getrolerights/{roleId}";
            //

            return Ok(_moduleService.getRoleRights(roleId));
        }

        [Route("submitrolerights/{roleId}")]
        [HttpPost]
        public async Task<IActionResult> submitRoleRights(int roleId, List<RoleRightsModel> rights)
        {
            string api = "submitrolerights/{roleId}";
            //

            try
            {
                List<role_module> toDelete = _context
                    .role_module.Where(x => x.roleId == roleId)
                    .ToList();
                List<role_rights> rtoDelete = _context
                    .role_rights.Where(x => x.roleId == roleId)
                    .ToList();
                foreach (role_module d in toDelete)
                {
                    _context.role_module.Remove(d);
                }
                foreach (role_rights d in rtoDelete)
                {
                    _context.role_rights.Remove(d);
                }
                foreach (RoleRightsModel r in rights)
                {
                    if (r.moduleRight)
                    {
                        _context.role_module.Add(
                            new role_module { roleId = roleId, moduleId = r.moduleId }
                        );
                        foreach (ModuleSubRightsModel sr in r.subRights)
                        {
                            if (sr.subRight)
                            {
                                _context.role_rights.Add(
                                    new role_rights { roleId = roleId, rightsId = sr.rightsId }
                                );
                            }
                        }
                    }
                }
                _context.SaveChanges();

                return Ok("succesfully Submitted");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error" });
            }
        }
    }
    public class buttonResultModel
    {
        public int id;
        public string name;
    }
}
