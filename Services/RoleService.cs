using Amazon;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace mahadalzahrawebapi.Services
{
    public interface IRoleService
    {
        List<platform_role> getAllRoles(AuthUser authUser);
        String addRole(platform_role r, AuthUser authUser);
        String updateRole(platform_role r);
        String deleteRole(int roleId);
        List<MenueModel> getMenuList(int qismId, AuthUser authUser);
    }
    public class MenueModel
    {
        public int id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String link { get; set; }
        public String Icon { get; set; }
        public bool isRole { get; set; }
        public List<MenueModel> subMenu { get; set; }
    }

    class RoleService : IRoleService
    {
        private readonly mzdbContext _context;

        private readonly string accessKey = "";
        private readonly string secretKey = "";
        private string bucketName = "";
        public RegionEndpoint region = RegionEndpoint.APSouth1; // Replace with your desired region

        public globalConstants _globalConstants;

        public RoleService(mzdbContext context)
        {
            _context = context;
            _globalConstants = new globalConstants();
        }
        public RoleService()
        {
            _context = null;
            _globalConstants = new globalConstants();
        }

        public List<MenueModel> getMenuList(int qismId, AuthUser authUser)
        {
            List<platform_user_module> module = _context.platform_user_module.Where(x => x.qismId == qismId && x.userId == authUser.ItsId).Include(x => x.module).ThenInclude(x => x.page).ToList();
            List<platform_page> page = module.GroupBy(x => x.module.page).Select(x => x.First().module.page).ToList();
            List<platform_role> rall = _context.platform_role.Include(x => x.module).Include(x => x.mainRole).Include(x => x.subRole).ToList();
            List<platform_role> roles = rall.Where(x => x.module.Any(y => module.Any(z => z.moduleId == y.id))).ToList();
            List<MenueModel> data = new List<MenueModel>();
            foreach (platform_page p in page)
            {
                data.Add(new MenueModel
                {
                    name = p.pageName,
                    description = p.description,
                    Icon = p.icon,
                    id = p.id,
                    link = p.link,
                    isRole = false,
                });
            }
            roles = roles.OrderBy(x => x.subRole.Count()).ToList();
            foreach (platform_role r in roles)
            {
                data = reStructureMenue(data, r);
            }
            List<MenueModel> others = data.Where(x => x.isRole == false).ToList();
            if (others.Count > 0)
            {
                data.Add(new MenueModel
                {
                    name = "Others",
                    description = "Pages which are not assigned to any role",
                    isRole = true,
                    link = others.FirstOrDefault().link,
                    id = -1,
                    subMenu = others,
                });
            }
            data = data.Where(x => x.isRole == true).ToList();
            return data;

        }

        public List<MenueModel> reStructureMenue(List<MenueModel> menue, platform_role role)
        {

            if (menue.Any(x => x.name == role.name))
            {
                return menue;
            }

            List<MenueModel> subMenue = new List<MenueModel>();
            for (int i = 0; i < menue.Count(); i++)
            {
                MenueModel m = menue[i];
                if (m.isRole)
                {
                    if (role.subRole.Any(x => x.id == m.id))
                    {
                        menue.Remove(m);
                        i--;
                        subMenue.Add(m);
                    }
                }
                else
                {
                    if (role.module.Any(x => x.pageId == m.id))
                    {
                        menue.Remove(m);
                        i--;
                        subMenue.Add(m);
                    }
                }
            }

            foreach (platform_role r in role.subRole)
            {
                menue.Where(x => x.isRole == true && r.mainRole.Any(y => y.id == x.id)).ToList().ForEach(m =>
                {
                    subMenue.Add(m);
                    menue.Remove(m);
                });

                subMenue = reStructureMenue(subMenue, r);
            }

            if (subMenue.Count > 0)
            {
                menue.Add(new MenueModel
                {
                    name = role.name,
                    description = role.description,
                    Icon = role.icon,
                    id = role.id,
                    isRole = true,
                    link = role.link,
                    subMenu = subMenue,
                });
            }

            return menue;

        }

        public List<platform_role> getAllRoles(AuthUser authUser)
        {

            try
            {
                int? authRole = (from q in _context.qism_al_tahfeez where q.id == authUser.qismId select q.id).FirstOrDefault();
                if (authRole == null)
                {
                    throw new Exception("You are not a authorized user to acess this api requst");
                }

                //List<platform_role> l = (from qr in context.platform_role where authUser.qismId == qr.qismId && qr.id != authRole select qr).ToList();
                return new List<platform_role>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public String addRole(platform_role r, AuthUser authUser)
        {

            try
            {

                //context.platform_role.Add(new platform_role { name = r.name, description = r.description, qismId = authUser.qismId });
                _context.SaveChanges();

                return "Success role has been added";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public String updateRole(platform_role r)
        {

            try
            {

                var rr = _context.platform_role.Where(x => x.id == r.id).FirstOrDefault();
                rr.name = r.name;
                rr.description = r.description;
                _context.SaveChanges();

                return "Success role has been updated";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public String deleteRole(int roleId)
        {

            try
            {
                int? authRole = (from q in _context.qism_al_tahfeez where q.id == roleId select q.id).FirstOrDefault();
                if (authRole != null)
                {
                    throw new Exception("You are not a authorized to delete this role");
                }

                List<platform_user_role> rtoDelete = _context.platform_user_role.Where(x => x.roleId == roleId).ToList();
                foreach (platform_user_role d in rtoDelete)
                {
                    _context.platform_user_role.Remove(d);
                }
                _context.SaveChanges();
                var rr = _context.platform_role.Where(x => x.id == roleId).FirstOrDefault();
                _context.platform_role.Remove(rr);
                _context.SaveChanges();

                return "Success role has been deleted";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
