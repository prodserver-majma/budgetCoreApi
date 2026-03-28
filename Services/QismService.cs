using Amazon;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings.User;
using mahadalzahrawebapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace mahadalzahrawebapi.Services
{
    public class QismService
    {
        private readonly mzdbContext _context;

        private readonly string accessKey = "";
        private readonly string secretKey = "";
        private string bucketName = "";
        public RegionEndpoint region = RegionEndpoint.APSouth1; // Replace with your desired region

        public globalConstants _globalConstants;
        public NotificationService _notificationService;

        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata"));

        public QismService(mzdbContext context)
        {
            _context = context;
            _globalConstants = new globalConstants();
            _notificationService = new NotificationService();
        }


        public List<DeptVenueRightModel> getAllDeptVenue()
        {
            try
            {
                List<DeptVenueRightModel> deptvenue = new List<DeptVenueRightModel>();

                List<dept_venue> dv = _context.dept_venue.Where(x => x.status.Equals("active")).OrderBy(x => x.deptName).ToList();
                foreach (var i in dv)
                {
                    deptvenue.Add(new DeptVenueRightModel { id = i.id, deptId = i.deptId, venueId = i.venueId, deptName = i.deptName, venueName = i.venueName, right = false, isTagged = i.qismId == null ? false : true, qismId = i.qismId });
                }

                return deptvenue;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ReportsRightsModel> getAllRegistrationName()
        {
            try
            {

                List<ReportsRightsModel> list = (from pset in _context.registrationform_dropdown_set
                                                 join p in _context.registrationform_programs on pset.programId equals p.id
                                                 join sp in _context.registrationform_subprograms on pset.subprogramId equals sp.id
                                                 join v in _context.venue on pset.venueId equals (int)v.Id
                                                 select new ReportsRightsModel { rId = pset.id, reportName = p.name + "/" + sp.name + "/" + v.displayName, isTagged = pset.qismId == null ? false : true, right = false, qismId = pset.qismId }).ToList();


                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public string PopulateQism(QismAlTahfeezModel2 nq)
        {
            var q = nq.user;
            var deptVenueRights = nq.dv;
            var reportsRights = nq.rr;
            var existingQism = _context.qism_al_tahfeez.FirstOrDefault(x => x.emailId == q.emailId);
            var admin = _context.branch_user.FirstOrDefault(x => x.itsId == 1);
            var qismIds = _context.qism_al_tahfeez
                .Where(x => x.emailId == q.emailId)
                .Select(x => x.id)
                .ToList();

            var deptVenues = _context.dept_venue
                .Where(x => x.qismId == null || qismIds.Contains(x.qismId ?? 0))
                .Include(x => x.user)
                .Include(x => x.venue)
                .Include(x => x.user_deptvenues)
                .Include(x => x.qism)
                .ToList();
            var deptVenIds = deptVenues.Select(x => x.id).ToList();

            List<qism_al_tahfeez_user_deptvenue> qismAlTahfeezUserDeptvenue = _context.qism_al_tahfeez_user_deptvenue.Where(x => deptVenIds.Contains(x.deptVenueId)).ToList();

            var dropdownSets = _context.registrationform_dropdown_set.Where(x => x.qismId == null || qismIds.Contains(x.qismId ?? 0)).Include(x => x.qism).ToList();
            var venues = _context.venue
                .Where(x => x.qismId == null || qismIds.Contains(x.qismId ?? 0))
                .Include(x => x.branch_users)
                .ToList();

            if (q.id == 0)
            {
                if (existingQism != null)
                {
                    return "This Qism Al Tahfeez already exists";
                }

                var newQism = new qism_al_tahfeez
                {
                    name = q.name,
                    emailId = q.emailId,
                    isActive = true,
                    password = q.password
                };
                _context.qism_al_tahfeez.Add(newQism);

                var defaultRoles = _context.platform_role
                    .Where(x => x.isDefault)
                    .ToList();

                foreach (var role in defaultRoles)
                {
                    _context.platform_user_role.Add(new platform_user_role
                    {
                        qism = newQism,
                        user = admin,
                        role = role
                    });

                    foreach (var module in role.module)
                    {
                        _context.platform_user_module.Add(new platform_user_module
                        {
                            qism = newQism,
                            module = module,
                            user = admin
                        });
                    }
                }

                foreach (var dvRight in deptVenueRights)
                {
                    if (dvRight.right)
                    {
                        var deptVenue = deptVenues.FirstOrDefault(x => x.id == dvRight.id);
                        if (deptVenue != null)
                        {

                            if (deptVenue?.qismId == null)
                            {
                                deptVenue.qism = newQism;
                            }
                        }
                    }
                }

                foreach (var rrRight in reportsRights)
                {
                    if (rrRight.right)
                    {
                        var dropdownSet = dropdownSets.FirstOrDefault(x => x.id == rrRight.rId);
                        if (dropdownSet != null)
                        {
                            if (dropdownSet?.qismId == null)
                            {
                                dropdownSet.qism = newQism;
                            }
                        }
                    }
                }

                _context.SaveChanges();

                return "Qism Al Tahfeez is successfully created";
            }
            else
            {
                var existingQismById = _context.qism_al_tahfeez
                    .Include(x => x.dept_venue)
                    .Include(x => x.registrationform_dropdown_set)
                    .FirstOrDefault(x => x.id == q.id);

                if (existingQismById == null)
                {
                    return "Qism Al Tahfeez not found";
                }

                existingQismById.name = q.name;
                existingQismById.emailId = q.emailId;

                foreach (var dvRight in deptVenueRights)
                {
                    var deptVenue = deptVenues.FirstOrDefault(x => x.id == dvRight.id);

                    if (deptVenue != null)
                    {
                        if (dvRight.right)
                        {
                            deptVenue.qismId = q.id;
                        }
                        else
                        {
                            // Remove the relation without deleting the deptVenue
                            deptVenue.qismId = null;

                            foreach (var item in qismAlTahfeezUserDeptvenue.Where(x => x.deptVenueId == deptVenue.id))
                            {
                                _context.qism_al_tahfeez_user_deptvenue.Remove(item);
                            }
                        }
                    }
                }

                foreach (var rrRight in reportsRights)
                {
                    var dropdownSet = dropdownSets.FirstOrDefault(x => x.id == rrRight.rId && (x.qismId == null || x.qismId == q.id));

                    if (dropdownSet != null)
                    {
                        if (rrRight.right)
                        {
                            dropdownSet.qismId = q.id;
                            existingQismById.registrationform_dropdown_set.Add(dropdownSet);
                        }
                        else
                        {
                            dropdownSet.qismId = null;
                            existingQismById.registrationform_dropdown_set.Remove(dropdownSet);
                        }
                    }
                }

                _context.SaveChanges();

                return "Qism Al Tahfeez is successfully updated";
            }
        }


        public List<QismModel> getAllQism()
        {
            try
            {

                List<qism_al_tahfeez> qisms = _context.qism_al_tahfeez.Include(x => x.its).ThenInclude(x => x.its).ToList();
                List<QismModel> l = new List<QismModel>();
                qisms.ForEach(x =>
                {
                    BranchUserModel u = new BranchUserModel();
                    if (x.itsId != null)
                    {
                        u.itsId = x.itsId ?? 0;
                        u.userName = x.its.its.fullName;
                    }
                    l.Add(new QismModel()
                    {
                        id = x.id,
                        emailId = x.emailId,
                        name = x.name,
                        user = u,
                    });
                });

                return l;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public QismModel getQismById(int id)
        {
            try
            {


                qism_al_tahfeez q = _context.qism_al_tahfeez.Where(x => x.id == id).FirstOrDefault();

                QismModel l = new QismModel()
                {
                    id = q.id,
                    emailId = q.emailId,
                    name = q.name,
                };

                if (q.itsId != null)
                {
                    l.user = new BranchUserModel()
                    {
                        itsId = q.its.itsId,
                        userName = q.its.its.fullName,
                        emailId = q.its.emailId,
                        mobile = q.its.its.mobileNo,
                    };
                }

                List<platform_role> role = new List<platform_role>();
                List<PageModel> Pages = new List<PageModel>();

                q.platform_user_role.Where(x => x.qismId == id).ToList().ForEach(x =>
                  {
                      if (!role.Contains(x.role))
                      {
                          role.Add(x.role);

                          x.role.module.GroupBy(y => y.pageId).Select(y => y.First()).ToList().ForEach(y =>
                          {
                              Pages.Add(new PageModel
                              {
                                  pageId = y.page.id,
                                  name = y.page.pageName,
                                  description = y.page.description,
                              });
                          });

                          x.role.module.ToList().ForEach(y =>
                          {
                              Pages.Where(z => z.pageId == y.pageId).FirstOrDefault().module.Add(new ModuleModel
                              {
                                  moduleId = y.id,
                                  buttonName = y.button.name,
                                  isDefault = y.isDefault,
                              });

                          });

                      }
                  });


                q.platform_user_module.ToList().ForEach(x =>
                {
                    bool toAdd = false;

                    PageModel p = Pages.Where(y => y.pageId == x.module.pageId).FirstOrDefault();
                    ModuleModel m = new ModuleModel
                    {
                        moduleId = x.moduleId,
                        buttonName = x.module.button.name,
                        isDefault = x.module.isDefault,
                    };
                    if (p != null)
                    {
                        if (!p.module.Contains(m))
                        {
                            p.module.Add(m);

                        }
                    }
                    else
                    {
                        Pages.Add(new PageModel
                        {
                            pageId = x.module.page.id,
                            name = x.module.page.pageName,
                            description = x.module.page.description,
                            module = new List<ModuleModel>() { m },
                        });
                    }
                });

                return l;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<DeptVenueRightModel> getAllDeptVenueForQism(int qismId)
        {
            try
            {

                List<DeptVenueRightModel> l = (from dept in _context.dept_venue
                                               where (dept.status == "active" && dept.qismId == qismId)
                                               select new DeptVenueRightModel
                                               {
                                                   id = dept.id,
                                                   deptId = dept.deptId,
                                                   venueId = dept.venueId,
                                                   deptName = dept.deptName,
                                                   venueName = dept.venueName,
                                                   right = false,
                                                   isTagged = dept.qismId == null ? false : true
                                               }).ToList();


                return l;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<RoleModel> getUserRoleRights(int itsId, int qismId)
        {

            try
            {
                List<platform_role> role = _context.platform_role.ToList();
                List<RoleModel> resultRoles = new List<RoleModel>();

                branch_user bru = _context.branch_user.Where(x => x.itsId == itsId).FirstOrDefault();

                if (bru == null)
                {
                    throw new Exception("Branch user does not Exist");
                }

                List<PageModel> Pages = new List<PageModel>();

                role.ForEach(x =>
                {
                    RoleModel r = new RoleModel
                    {
                        description = x.description,
                        isDefault = x.isDefault,
                        name = x.name,
                        roleId = x.id,
                        isSelected = false,
                    };
                    List<ModuleModel> mods = new List<ModuleModel>();

                    x.module.ToList().ForEach(y =>
                    {
                        mods.Add(new ModuleModel
                        {
                            buttonName = y.button.name,
                            isChecked = false,
                            moduleId = y.id,
                        });
                    });
                    platform_user_role ur = x.platform_user_role.Where(y => y.qismId == qismId && y.userId == itsId).FirstOrDefault();
                    if (ur != null)
                    {
                        r.isSelected = true;
                    }
                    r.module = mods;
                    resultRoles.Add(r);
                });

                return resultRoles;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public String createRole(RoleModel role)
        {

            _context.platform_role.Add(new platform_role
            {
                name = role.name,
                description = role.description,
                icon = role.icon,
            });
            _context.SaveChanges();
            return "Role Successfully Added";

        }

        public RoleModel getRoleRights(int roleId)
        {

            try
            {
                RoleModel role = new RoleModel();
                platform_role r = _context.platform_role.Where(x => x.id == roleId).FirstOrDefault();
                List<platform_module> pm = _context.platform_module.ToList();

                List<PageModel> Pages = new List<PageModel>();

                pm.ForEach(x =>
                {
                    ModuleModel m = new ModuleModel
                    {
                        isChecked = x.role.Any(y => y.id == roleId),
                        moduleId = x.id,
                        buttonName = x.button.name,
                        buttonId = x.buttonId,
                        isDefault = x.isDefault
                    };

                    PageModel p = Pages.Where(y => y.pageId == x.pageId).FirstOrDefault();
                    if (p == null)
                    {
                        p = new PageModel
                        {
                            pageId = x.pageId,
                            description = x.page.description,
                            name = x.page.pageName,
                            module = new List<ModuleModel> { m }
                        };
                        Pages.Add(p);
                    }
                    else
                    {
                        p.module.Add(m);
                    }
                });

                role.roleId = r.id;
                role.pages = Pages;
                role.name = r.name;
                role.description = r.description;
                role.isDefault = r.isDefault;

                return role;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public String toggleDefaultRole(int roleId)
        {

            platform_role r = _context.platform_role.Where(x => x.id == roleId).FirstOrDefault();
            List<qism_al_tahfeez> q = _context.qism_al_tahfeez.ToList();
            List<qism_al_tahfeez> unAssigned = q.Where(x => x.itsId == null).ToList();
            List<qism_al_tahfeez> brus = q.Where(x => x.itsId != null).ToList();

            List<platform_user_role> purs = _context.platform_user_role.ToList();
            List<platform_user_module> pums = _context.platform_user_module.ToList();

            if (r.isDefault)
            {
                r.isDefault = false;
            }
            else
            {
                r.isDefault = true;
                foreach (qism_al_tahfeez z in brus)
                {
                    platform_user_role pur = new platform_user_role { qismId = z.id, userId = (z.itsId ?? 0), roleId = roleId };
                    if (!purs.Any(y => y.qismId == pur.qismId && y.userId == pur.userId && y.roleId == pur.roleId))
                    {
                        _context.platform_user_role.Add(pur);
                    }
                    foreach (platform_module x in r.module.ToList())
                    {
                        platform_user_module pum = new platform_user_module { qismId = z.id, userId = (z.itsId ?? 0), moduleId = x.id };
                        if (!pums.Any(y => y.qismId == pum.qismId && y.userId == pum.userId && y.moduleId == pum.moduleId))
                        {
                            _context.platform_user_module.Add(pum);
                        }
                    };
                };
                foreach (qism_al_tahfeez z in unAssigned)
                {
                    platform_user_role pur = new platform_user_role { qismId = z.id, userId = 1, roleId = roleId };
                    if (!purs.Any(y => y.qismId == pur.qismId && y.userId == pur.userId && y.roleId == pur.roleId))
                    {
                        _context.platform_user_role.Add(pur);
                    }
                    foreach (platform_module x in r.module.ToList())
                    {
                        platform_user_module pum = new platform_user_module { qismId = z.id, userId = 1, moduleId = x.id };
                        if (!pums.Any(y => y.qismId == pum.qismId && y.userId == pum.userId && y.moduleId == pum.moduleId))
                        {
                            _context.platform_user_module.Add(pum);
                        }
                    };
                };
            }
            _context.SaveChanges();
            return "Role Defauls status successfully set to " + (r.isDefault);

        }

        public String toggleDefaultModule(int moduleId)
        {

            platform_module r = _context.platform_module.Where(x => x.id == moduleId).FirstOrDefault();
            List<qism_al_tahfeez> q = _context.qism_al_tahfeez.ToList();
            List<qism_al_tahfeez> unAssigned = q.Where(x => x.itsId == null).ToList();
            List<qism_al_tahfeez> brus = q.Where(x => x.itsId != null).ToList();

            List<platform_user_module> pums = _context.platform_user_module.ToList();

            if (r.isDefault)
            {
                r.isDefault = false;
            }
            else
            {
                r.isDefault = true;
                foreach (qism_al_tahfeez z in brus)
                {

                    platform_user_module pum = new platform_user_module { qismId = z.id, userId = (z.itsId ?? 0), moduleId = r.id };
                    if (!pums.Any(y => y.qismId == pum.qismId && y.userId == pum.userId && y.moduleId == pum.moduleId))
                    {
                        _context.platform_user_module.Add(pum);
                        pum.userId = 1;
                        _context.platform_user_module.Add(pum);
                    }
                };
                foreach (qism_al_tahfeez z in unAssigned)
                {
                    platform_user_module pum = new platform_user_module { qismId = z.id, userId = 1, moduleId = r.id };
                    if (!pums.Any(y => y.qismId == pum.qismId && y.userId == pum.userId && y.moduleId == pum.moduleId))
                    {
                        _context.platform_user_module.Add(pum);
                    }
                };
            }
            _context.SaveChanges();
            return "module Defauls status successfully set to " + (r.isDefault);

        }


        public RoleModel updateRoleRights(RoleModel role)
        {

            platform_role r = _context.platform_role.Where(x => x.id == role.roleId).FirstOrDefault();
            List<qism_al_tahfeez> q = _context.qism_al_tahfeez.ToList();
            List<qism_al_tahfeez> unAssigned = q.Where(x => x.itsId == null).ToList();
            List<branch_user> brus = q.Where(x => x.itsId != null).Select(x => x.its).ToList();

            r.name = role.name;
            r.description = role.description;

            if (r == null)
            {
                throw new Exception("Role Not found");
            }

            role.pages.ForEach(y =>
            {
                y.module.ForEach(x =>
                {
                    platform_module m = _context.platform_module.Where(k => k.id == x.moduleId).FirstOrDefault();
                    if (x.isChecked == true && !r.module.Contains(m))
                    {
                        r.module.Add(m);
                    }
                    else if (x.isChecked == false && r.module.Contains(m))
                    {
                        r.module.Remove(m);
                    }
                });
            });

            _context.SaveChanges();

            return role;

        }

        public List<PageModel> getUserPageRights(int itsId, int qismId)
        {

            try
            {
                List<platform_module> module = _context.platform_module.ToList();
                branch_user bru = _context.branch_user.Where(x => x.itsId == itsId).FirstOrDefault();
                if (bru == null)
                {
                    throw new Exception("Branch user does not Exist");
                }

                List<PageModel> Pages = new List<PageModel>();

                module.ForEach(x =>
                {
                    ModuleModel m = new ModuleModel
                    {
                        isChecked = false,
                        moduleId = x.id,
                        buttonName = x.button.name,
                        buttonId = x.buttonId,
                        isDefault = x.isDefault
                    };
                    platform_user_module um = x.platform_user_module.Where(y => y.qismId == qismId && y.userId == itsId).FirstOrDefault();
                    if (um != null)
                    {
                        m.isChecked = true;
                    }
                    PageModel p = Pages.Where(y => y.pageId == x.pageId).FirstOrDefault();
                    if (p == null)
                    {
                        p = new PageModel
                        {
                            pageId = x.pageId,
                            description = x.page.description,
                            name = x.page.pageName,
                            module = new List<ModuleModel> { m }
                        };
                        Pages.Add(p);
                    }
                    else
                    {
                        p.module.Add(m);
                    }
                    p.module = p.module.OrderBy(y => y.buttonId).ToList();
                });
                return Pages;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public List<RoleModel> updateUserRoleRights(int itsId, int qismId, List<RoleModel> rights)
        {

            rights.ForEach(x =>
            {
                platform_user_role r = _context.platform_user_role.Where(y => y.qismId == qismId && y.userId == itsId && y.roleId == x.roleId).FirstOrDefault();
                if (x.isSelected == true && r == null)
                {
                    _context.platform_user_role.Add(new platform_user_role { qismId = qismId, roleId = x.roleId, userId = itsId });
                }
                else if (x.isSelected == false && r != null)
                {
                    _context.platform_user_role.Remove(r);
                }
            });

            _context.SaveChanges();
            return rights;

        }

        public List<PageModel> updateUserPageRights(int itsId, int qismId, List<PageModel> rights)
        {


            rights.ForEach(y =>
            {
                y.module.ForEach(x =>
                {
                    platform_user_module r = _context.platform_user_module.Where(z => z.qismId == qismId && z.userId == itsId && z.moduleId == x.moduleId).FirstOrDefault();
                    platform_user_module r2 = _context.platform_user_module.Where(z => z.qismId == qismId && z.userId == 1 && z.moduleId == x.moduleId).FirstOrDefault();
                    if (x.isChecked == true && r == null)
                    {
                        _context.platform_user_module.Add(new platform_user_module { qismId = qismId, moduleId = x.moduleId, userId = itsId });
                        if (itsId != 1 && r2 == null)
                        {
                            _context.platform_user_module.Add(new platform_user_module { qismId = qismId, moduleId = x.moduleId, userId = 1 });
                        }
                    }
                    else if (x.isChecked == false && r != null)
                    {
                        _context.platform_user_module.Remove(r);
                        if (itsId != 1 && r2 != null)
                        {
                            _context.platform_user_module.Remove(r2);
                        }
                    }
                });
            });

            _context.SaveChanges();

            return rights;

        }

        public String tagQismAdmin(int userId, int qismId, AuthUser authUser)
        {

            PasswordGenerator psw = new PasswordGenerator();

            qism_al_tahfeez qt = (from q in _context.qism_al_tahfeez where q.id == qismId select q).FirstOrDefault();

            if (qt == null) throw new Exception("qism not found");

            if (qt.itsId != null) throw new Exception("Admin already assigned remove qism admin first");

            branch_user bru = _context.branch_user.Where(x => x.itsId == userId)
                .Include(x => x.deptVenue)
                .Include(x => x.branchuser_deptvenue)
                .Include(x => x.pset)
                .Include(x => x.platform_user_module)
                .FirstOrDefault();

            branch_user admin = _context.branch_user.Where(x => x.itsId == 1)
                .Include(x => x.platform_user_role)
                .Include(x => x.platform_user_module).ThenInclude(x => x.module).AsNoTracking()
                .FirstOrDefault();

            if (bru == null)
            {
                string password = psw.GeneratePassword(7);
                khidmat_guzaar kh = _context.khidmat_guzaar.Where(x => x.itsId == userId).FirstOrDefault();
                bru = new branch_user
                {
                    emailId = kh.officialEmailAddress == null ? kh.emailAddress : kh.officialEmailAddress,
                    itsId = userId,
                    password = password,
                };
                qt.password = password;
                _context.branch_user.Add(bru);
                _context.SaveChanges();
                string msg = @"
                        <div style=""background-color: #fff; border-radius: 10px; padding: 20px; max-width: 600px; margin: 0 auto;"">
                            <p style=""color: #333;"">Salaam Jameel,</p>
                            <p><span style=""color: red;"">" + kh.fullName + @"</span></p>
                            <p><span style=""color: red;"">" + kh.itsId + @"</span></p>

                            <p>Please be informed that you have been assigned as Qism al-Tahfeez " + qt.name + @" - Masool</p>
                            <p>Please login with below credentials in Branch Login</p>

                            <p>Your login details are as below:</p>
                            <ul style=""list-style-type: none; padding: 0;"">
                                <li>Website: <a href=""http://www.mahadalzahra.org"" style=""color: #007acc; text-decoration: none;"">www.mahadalzahra.org > Branch Login</a></li>
                                <li>Username: <b>" + qt.emailId + @"</b></li>
                                <li>Password: <b>" + bru.password + @"</b></li><br>
                                <p>Password can be reset after the first login. </p>
                            </ul>

                            <p>Shukran,</p>
                            <p>Wa al-Salaam.</p>
                        </div>";

                _notificationService.SendStandardHTMLEmail("Welcome to Mahad al-Zahra Branch Login ", msg, bru.emailId, "no-reply");

            }
            else
            {
                if (bru.platform_user_role.Count != 0 || bru.platform_user_module.Count != 0) throw new Exception("Selected user already in " + bru.deptVenue.OrderBy(x => x.qismId).FirstOrDefault().qism.name + ". ask qism admin to remove this user");
            }

            List<platform_user_role> pur = admin.platform_user_role.Where(x => x.qismId == qt.id).ToList();
            foreach (platform_user_role x in pur)
            {
                platform_user_role y = new platform_user_role
                {
                    qismId = x.qismId,
                    roleId = x.roleId,
                    userId = bru.itsId
                };
                _context.platform_user_role.Add(y);
            };

            admin.platform_user_module.Where(x => x.qismId == qt.id || x.module.isDefault).ToList().ForEach(x =>
            {
                // Check if the module already exists for this user
                var existingModule = bru.platform_user_module
                    .FirstOrDefault(pum => pum.qismId == qismId && pum.moduleId == x.moduleId);

                // If it does not exist, add the new module
                if (existingModule == null)
                {
                    platform_user_module y = new platform_user_module { qismId = qismId, moduleId = x.moduleId, userId = bru.itsId };
                    bru.platform_user_module.Add(y);
                }
            });

            qt.itsId = userId;
            qt.its = bru;

            List<dept_venue> deptVenueToTag = (from dpvn in _context.dept_venue where dpvn.qismId == qt.id select dpvn).ToList();

            foreach (dept_venue d in deptVenueToTag)
            {
                bru.branchuser_deptvenue.Add(new qism_al_tahfeez_user_deptvenue { deptVenueId = d.id, userId = userId });
                bru.deptVenue.Add(d);
            }


            List<registrationform_dropdown_set> psetToTag = (from pset in _context.registrationform_dropdown_set where pset.qismId == qt.id select pset).ToList();

            foreach (registrationform_dropdown_set p in psetToTag)
            {
                bru.pset.Add(p);
            }

            //user_logs newLog = new user_logs { userId = userId, description = "asigned user as branch admin for qism -" + qt.name, createdOn = DateTime.Now, createdBy = authUser.Name };
            //_context.user_logs.Add(newLog);

            _context.SaveChanges();

            return "User tagged as a admin successfully";

        }

        public String removeQismAdmin(int userId, AuthUser authUser)
        {
            qism_al_tahfeez qt = _context.qism_al_tahfeez.Where(x => x.itsId == userId)
                .Include(x => x.its).ThenInclude(x => x.pset)
                .Include(x => x.its).ThenInclude(x => x.deptVenue)
                .Include(x => x.its).ThenInclude(x => x.branchuser_deptvenue)
                .Include(x => x.its).ThenInclude(x => x.platform_user_module)
                .Include(x => x.its).ThenInclude(x => x.platform_user_role)
                .FirstOrDefault();
            if (qt == null) throw new Exception("qism admin not found");


            qt.its.platform_user_role.Where(x => x.qismId == qt.id).ToList().ForEach(x => _context.platform_user_role.Remove(x));
            qt.its.platform_user_module.Where(x => x.qismId == qt.id).ToList().ForEach(x => _context.platform_user_module.Remove(x));
            qt.its.deptVenue.Where(x => x.qismId == qt.id).ToList().ForEach(x => qt.its.deptVenue.Remove(x));
            qt.its.branchuser_deptvenue.ToList().ForEach(x => qt.its.branchuser_deptvenue.Remove(x));
            qt.its.pset.Where(x => x.qismId == qt.id).ToList().ForEach(x => qt.its.pset.Remove(x));

            qt.itsId = null;

            //user_logs newLog = new user_logs { userId = userId, description = "removed user as branch admin for qism -" + qt.name, createdOn = DateTime.Now, createdBy = authUser.Name };
            //_context.user_logs.Add(newLog);

            _context.SaveChanges();

            return "User untagged as a admin successfully";
        }
    }
}
