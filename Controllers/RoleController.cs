using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace mahadalzahrawebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly HelperService _helperService;
        private readonly NotificationService _notificationService;
        private readonly WhatsAppApiService _whatsAppApiService;
        private readonly string adminEmails = "admin@mahadalzahra.com, juzerdiwan@jameasaifiyah.edu";
        private readonly globalConstants _globalConstants;
        private readonly RoleService _roleService;

        public RoleController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _helperService = new HelperService(context);
            _notificationService = new NotificationService();
            _globalConstants = new globalConstants();
            _whatsAppApiService = new WhatsAppApiService(context);
            _roleService = new RoleService(context);
        }

        [Route("getAllRoles")]
        [HttpGet]
        public async Task<IActionResult> getAllRoles()
        {
            string api = "api/role/getAllRoles";
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                return Ok(_roleService.getAllRoles(authUser));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("getRolesAll")]
        [HttpGet]
        public async Task<IActionResult> getRolesAll()
        {
            //string api = "api/role/getAllRoles";
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                return Ok(_context.role.Where(x => x.status == 1).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("addrole")]
        [HttpPost]
        public async Task<IActionResult> addRole(platform_role r)
        {
            string api = "api/role/addrole";
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                return Ok(_roleService.addRole(r, authUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("updaterole")]
        [HttpPost]
        public async Task<IActionResult> updateRole(platform_role r)
        {
            string api = "api/role/updateRole";
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


            try
            {
                return Ok(_roleService.updateRole(r));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("deleterole/{roleId}")]
        [HttpDelete]
        public async Task<IActionResult> deleteRole(int roleId)
        {
            string api = "api/role/deleteRole";
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                return Ok(_roleService.deleteRole(roleId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("getmenu/{qismId}")]
        [HttpGet]
        public async Task<IActionResult> updateRoleRights(int qismId)
        {
            string api = "api/module/updaterolerights/{roleId}";
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            return Ok(_roleService.getMenuList(qismId, authUser));

        }

        public struct menuMap
        {
            public int mainRole;
            public int subRole;
        }

        [Route("addmenurolemap")]
        [HttpPost]
        public async Task<IActionResult> mapMenuRole(menuMap map)
        {
            string api = "api/role/addmenurolemap";
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            platform_role mainMenu = _context.platform_role.Where(x => x.id == map.subRole).FirstOrDefault();
            platform_role subMenu = _context.platform_role.Where(x => x.id == map.mainRole).FirstOrDefault();

            if (mainMenu == null || subMenu == null)
            {
                return BadRequest(new { message = "Role not found" });
            }
            if (checkSubRoleEligible(mainMenu, subMenu))
            {
                return BadRequest(new { message = "Error this mapping is not possible as the selected main menu is the child of sub menu" });
            }

            mainMenu.mainRole.Add(subMenu);
            _context.SaveChanges();


            return Ok("Sucess");
        }

        [Route("removemenurolemap")]
        [HttpPost]
        public async Task<IActionResult> removeMenuRolemap(menuMap map)
        {
            string api = "api/module/updaterolerights/{roleId}";
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            platform_role mainMenu = _context.platform_role.Where(x => x.id == map.subRole).FirstOrDefault();
            platform_role subMenu = _context.platform_role.Where(x => x.id == map.mainRole).FirstOrDefault();

            if (mainMenu == null || subMenu == null)
            {
                return BadRequest(new { message = "Role not found" });
            }
            if (mainMenu.mainRole.Contains(subMenu))
            {
                _context.SaveChanges();
                return Ok("Successfully removed");
            }
            return BadRequest(new { message = "Error subchild not connected with main menu" });

        }

        private bool checkSubRoleEligible(platform_role main, platform_role sub)
        {
            if (sub.mainRole.Count == 0)
            {
                return true;
            }
            if (sub.mainRole.Contains(main))
            {
                return false;
            }

            bool res = true;

            foreach (platform_role s in sub.mainRole)
            {
                if (!checkSubRoleEligible(main, s))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
