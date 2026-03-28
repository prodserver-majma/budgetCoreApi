using Amazon;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings.User;
using mahadalzahrawebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace mahadalzahrawebapi.Services
{
    public interface IModuleService
    {
        List<int> getRights(int itsNo, string loginName);
        List<int> getSubRights(int itsNo, int moduleId, string loginName);
        List<ModulePageModel> getAllAvailableRights(int qismId, AuthUser authUser);
        List<ModulePageModel> getAllUserModuleRights(int itsId, int qismId, AuthUser authUser);
        String updateRoleRights(int roleId, List<RoleRightsModel> rights);

        List<RoleRightsModel> getRoleRights(int itsNo);

        List<PageModel> getAllPages();
    }

    public class ModuleService : IModuleService
    {
        private readonly mzdbContext _context;

        private readonly string accessKey = "";
        private readonly string secretKey = "";
        private string bucketName = "";
        public RegionEndpoint region = RegionEndpoint.APSouth1; // Replace with your desired region

        public globalConstants _globalConstants;

        public ModuleService(mzdbContext context)
        {
            _context = context;
            _globalConstants = new globalConstants();
        }
        public ModuleService()
        {
            _context = null;
            _globalConstants = new globalConstants();
        }
        public List<ModulePageModel> getAllAvailableRights(int qismId, AuthUser authUser)
        {
            List<platform_module> pm = _context.platform_user_module
                .Include(x => x.module)
                .ThenInclude(x => x.button)
                .Include(x => x.module)
                .ThenInclude(x => x.page)
                .Where(x => x.qismId == qismId && x.userId == 1).Select(x => x.module).ToList();
            List<ModulePageModel> Pages = new List<ModulePageModel>();

            pm.ForEach(x =>
            {
                ModuleModel m = new ModuleModel
                {
                    isChecked = false,
                    moduleId = x.id,
                    buttonName = x.button.name,
                    buttonId = x.buttonId,
                    isDefault = x.isDefault ? true : false
                };
                ModulePageModel p = Pages.Where(y => y.pageId == x.pageId).FirstOrDefault();
                if (p == null)
                {
                    p = new ModulePageModel
                    {
                        pageId = x.pageId,
                        description = x.page.description,
                        name = x.page.pageName,
                        modules = new List<ModuleModel> { m }
                    };
                    Pages.Add(p);
                }
                else
                {
                    p.modules.Add(m);
                }
                p.modules = p.modules.OrderBy(y => y.buttonId).ToList();
            });

            return Pages;

        }

        public List<ModulePageModel> getAllUserModuleRights(int itsId, int qismId, AuthUser authUser)
        {
            branch_user bru = _context.branch_user.Where(x => x.itsId == itsId).Include(x => x.platform_user_module).FirstOrDefault();

            List<platform_module> pm = _context.platform_user_module.Where(x => x.qismId == qismId && x.userId == authUser.ItsId)
                .Include(x => x.module)
                .ThenInclude(x => x.button)
                .Include(x => x.module)
                .ThenInclude(x => x.page)
                .Select(x => x.module).ToList();
            List<ModulePageModel> Pages = new List<ModulePageModel>();

            pm.ForEach(x =>
            {
                ModuleModel m = new ModuleModel
                {
                    isChecked = itsId == authUser.ItsId ? false : bru.platform_user_module.Any(y => y.module == x && y.qismId == qismId),
                    moduleId = x.id,
                    buttonName = x.button.name,
                    buttonId = x.buttonId,
                    isDefault = x.isDefault ? true : false
                };
                ModulePageModel p = Pages.Where(y => y.pageId == x.pageId).FirstOrDefault();
                if (p == null)
                {
                    p = new ModulePageModel
                    {
                        pageId = x.pageId,
                        description = x.page.description,
                        name = x.page.pageName,
                        modules = new List<ModuleModel> { m }
                    };
                    Pages.Add(p);
                }
                else
                {
                    p.modules.Add(m);
                }
                p.modules = p.modules.OrderBy(y => y.buttonId).ToList();
            });

            return Pages;

        }

        public List<int> getRights(int itsNo, string loginName)
        {
            List<int> moduleIds = new List<int>();
            try
            {
                if (loginName.Contains("Branch"))
                {
                    branch_user bru = _context.branch_user.Where(x => x.itsId == itsNo).Include(x => x.platform_user_module).FirstOrDefault();
                    moduleIds = bru?.platform_user_module.ToList().Select(x => x.moduleId).ToList();
                }
                if (loginName.Contains("Admin") || loginName.Contains("HR"))
                {
                    List<role_module> module = new List<role_module>();
                    user u = _context.user.Where(x => x.ItsId == itsNo).FirstOrDefault();
                    int? roleid = u.roleId;
                    module = _context.role_module.Where(x => x.roleId == roleid).ToList();
                    if (module != null)
                    {
                        module.ForEach(x => moduleIds.Add(x.moduleId ?? 0));
                    }
                }
                return moduleIds;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        public List<int> getSubRights(int itsNo, int moduleId, string loginName)
        {
            try
            {
                List<int> subRightsIds = new List<int>();

                if (loginName.Contains("Branch"))
                {

                    branch_user u = _context.branch_user.Where(x => x.itsId == itsNo).Include(x => x.platform_user_module).FirstOrDefault();
                    subRightsIds = u.platform_user_module.ToList().Select(x => x.moduleId).ToList();
                }
                if (loginName.Contains("Admin") || loginName.Contains("HR"))
                {
                    List<role_rights> subRights = new List<role_rights>();
                    user u = _context.user.Where(x => x.ItsId == itsNo).FirstOrDefault();
                    int? roleid = u.roleId;
                    subRights = _context.role_rights.Where(x => x.roleId == roleid).ToList();
                    if (subRights != null)
                    {
                        subRights.ForEach(x => subRightsIds.Add(x.rightsId ?? 0));
                    }
                }


                return subRightsIds;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public String updateRoleRights(int roleId, List<RoleRightsModel> rights)
        {
            List<platform_user_role> rtoDelete = _context.platform_user_role.Where(x => x.roleId == roleId).ToList();
            foreach (platform_user_role d in rtoDelete)
            {
                _context.platform_user_role.Remove(d);
                _context.SaveChanges();
            }
            foreach (RoleRightsModel r in rights)
            {
                if (r.moduleRight)
                {
                    //_context.platform_role_module.Add(new platform_role_module { roleId = roleId, moduleId = r.moduleId });
                    foreach (ModuleSubRightsModel sr in r.subRights)
                    {
                        if (sr.subRight)
                        {
                            //_context.platform_user_role.Add(new platform_user_role { roleId = roleId, rightId = sr.rightsId });
                        }
                    }
                    _context.SaveChanges();
                }

            }
            _context.SaveChanges();


            return "succesfully Submitted";
        }

        public List<PageModel> getAllPages()
        {

            List<platform_page> pm = _context.platform_page
            .Include(x => x.platform_module)
            .ThenInclude(x => x.button).ToList();
            List<PageModel> Pages = new List<PageModel>();

            pm.ForEach(y =>
            {

                PageModel p = new PageModel
                {
                    pageId = y.id,
                    description = y.description,
                    name = y.pageName,
                    module = new List<ModuleModel>()
                };

                y.platform_module.ToList().ForEach(x =>
                {
                    ModuleModel m = new ModuleModel
                    {
                        isChecked = true,
                        moduleId = x.id,
                        buttonName = x.button.name,
                        buttonId = x.buttonId,
                        isDefault = x.isDefault,
                    };
                    p.module.Add(m);
                });

                Pages.Add(p);
            });
            return Pages;

        }

        public List<RoleRightsModel> getRoleRights(int roleId)
        {

            try
            {
                List<module> module = _context.module.ToList();
                List<RoleRightsModel> moduleRights = new List<RoleRightsModel>();


                foreach (module m in module)
                {
                    List<ModuleSubRightsModel> subRights = new List<ModuleSubRightsModel>();
                    List<module_rights> sr = _context.module_rights.Where(x => x.moduleId == m.moduleId).ToList();
                    foreach (var i in sr)
                    {
                        bool sright = false;

                        role_rights rr = _context.role_rights.Where(x => x.roleId == roleId && x.rightsId == i.rightsId).FirstOrDefault();
                        if (rr != null)
                        {
                            sright = true;

                        }
                        subRights.Add(new ModuleSubRightsModel { rightsId = i.rightsId, rightsName = _context.rights.Where(x => x.rightsId == i.rightsId).FirstOrDefault().rightsName, subRight = sright });

                    }


                    bool right = false;

                    role_module r = _context.role_module.Where(x => x.roleId == roleId && x.moduleId == m.moduleId).FirstOrDefault();
                    if (r != null)
                    {
                        right = true;

                    }
                    moduleRights.Add(new RoleRightsModel { moduleId = m.moduleId, moduleName = m.moduleName, moduleRight = right, subRights = subRights });


                }

                return moduleRights;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

    }
}
