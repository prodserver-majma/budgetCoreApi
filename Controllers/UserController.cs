using Abp.Extensions;
using Amazon.S3.Model.Internal.MarshallTransformations;
using AutoMapper;
using Dapper;
using mahadalzahrawebapi.Api.v1;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Migrations;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Mapping;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace mahadalzahrawebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly HelperService _helperService;
        private NotificationService _notificationService;
        private readonly WhatsAppApiService _whatsAppApiService;
        private readonly string adminEmails = "admin@mahadalzahra.com, juzerdiwan@jameasaifiyah.edu";
        private globalConstants _globalConstants;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSServiceRemote _jhsService;
        public UserController(mzdbContext context, IMapper mapper, TokenService tokenService, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _helperService = new HelperService(context);
            _notificationService = new NotificationService();
            _globalConstants = new globalConstants();
            _whatsAppApiService = new WhatsAppApiService(context);
            _itsService = new ItsServiceRemote();
            _jhsService = new IJHSServiceRemote();
            _config = config;
        }

        [Route("asignRigts")]
        [HttpPost]
        public async Task<ActionResult<string>> asignRigts(rightsTobeAssigned rights)
        {
            string api = "user/asignRigts";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            PasswordGenerator psw = new PasswordGenerator();

            khidmat_guzaar kh = _context.khidmat_guzaar.Where(x => x.itsId == rights.itsId).Include(x => x.mauzeNavigation).FirstOrDefault();

            if (kh == null)
            {
                return BadRequest(new { message = "User does not exist" });
            }

            branch_user bru = _context.branch_user.Where(x => x.itsId == rights.itsId)
                .Include(x => x.venues)
                .Include(x => x.deptVenue)
                .Include(x => x.pset)
                .Include(x => x.platform_user_module)
                .FirstOrDefault();
            qism_al_tahfeez q = _context.qism_al_tahfeez.Where(x => x.id == rights.qismId).FirstOrDefault();

            if (bru == null)
            {
                string password = psw.GeneratePassword(7);
                branch_user b = _context.branch_user.Add(new branch_user
                {
                    itsId = rights.itsId,
                    password = password,
                    emailId = string.IsNullOrEmpty(kh.officialEmailAddress) ? kh.emailAddress : kh.officialEmailAddress
                }).Entity;

                kh.password = password;

                string msg = @"
                        <div style=""background-color: #fff; border-radius: 10px; padding: 20px; max-width: 600px; margin: 0 auto;"">
                            <p style=""color: #333;"">Salaam Jameel,</p>
                            <p><span style=""color: red;"">" + kh.fullName + @"</span></p>
                            <p><span style=""color: red;"">" + kh.itsId + @"</span></p>
                            <p><span style=""color: blue;"">" + kh.mauzeNavigation.displayName + @"</span> </p>
                            <p><span style=""color: blue;"">" + kh.employeeType + @"</span> </p>

                            <p>Please be informed that you have been created a user in Qism al-Tahfeez " + q.name + @", Branch login.</p>
                            <p>Please contact your branch masul or coordinator for more information.</p>

                            <p>Your login details are as below:</p>
                            <ul style=""list-style-type: none; padding: 0;"">
                                <li>Website: <a href=""http://www.mahadalzahra.org"" style=""color: #007acc; text-decoration: none;"">www.mahadalzahra.org > Branch Login</a></li>
                                <li>Username: <b>" + b.emailId + @"</b></li>
                                <li>Password: <b>" + b.password + @"</b></li><br>
                                <p>Password can be reset after the first login. </p>
                            </ul>

                            <p>Shukran,</p>
                            <p>Wa al-Salaam.</p>
                        </div>";

                _notificationService.SendStandardHTMLEmail("Welcome to Mahad al-Zahra Branch Login ", msg, b.emailId, "no-reply");

                _context.SaveChanges();
                bru = b;
            }
            else
            {
                string msg = @"
                        <div style=""background-color: #fff; border-radius: 10px; padding: 20px; max-width: 600px; margin: 0 auto;"">
                            <p style=""color: #333;"">Salaam Jameel,</p>
                            <p><span style=""color: red;"">" + kh.fullName + @"</span></p>
                            <p><span style=""color: red;"">" + kh.itsId + @"</span></p>
                            <p><span style=""color: blue;"">" + kh.mauzeNavigation.displayName + @"</span> </p>
                            <p><span style=""color: blue;"">" + kh.employeeType + @"</span> </p>

                            <p>Please be informed that you have been added as a user in Qism al-Tahfeez " + q.name + @", Branch login.</p>
                            <p>Please contact the branch masul or coordinator for more information.</p>
                            <p>You will be able to access it with your existing credentials</p>
                            <p>Shukran,</p>
                            <p>Wa al-Salaam.</p>
                        </div>";

                _notificationService.SendStandardHTMLEmail("Welcome to Mahad al-Zahra Branch Login ", msg, bru.emailId, q.emailId);
            }

            List<venue> v = _context.venue.Where(x => x.qismId > 0 && rights.venue.Contains(x.Id)).ToList();

            if (bru.venues != null)
            {
                List<venue> ev = bru.venues.Where(x => x.qismId == rights.qismId).ToList();
                ev.Where(x => !rights.venue.Any(y => x.Id == y)).ToList().ForEach(x => bru.venues.Remove(x));
            }

            v.ForEach(v =>
            {
                if (!bru.venues?.Any(x => x.Id == v.Id) == true)
                {
                    bru.venues.Add(v);
                }
            });

            List<dept_venue> d = _context.dept_venue.Where(x => rights.deptVenue.Contains(x.id)).ToList();
            if (bru.deptVenue != null)
            {
                bru.deptVenue.Where(x => x.qismId == rights.qismId && !rights.deptVenue.Any(y => x.id == y)).ToList().ForEach(x => bru.deptVenue.Remove(x));
            }

            d.ForEach(d =>
            {
                if (!bru.deptVenue?.Any(x => x.id == d.id) == true)
                {
                    bru.deptVenue.Add(d);
                }
            });

            if (rights.programs != null)
            {

                List<registrationform_dropdown_set> r = _context.registrationform_dropdown_set.Where(x => rights.programs.Contains(x.id)).ToList();
                if (bru.pset != null)
                {
                    bru.pset.Where(x => x.qismId == rights.qismId && !rights.programs.Any(y => x.id == y)).ToList().ForEach(x => bru.pset.Remove(x));
                }

                r.ForEach(r =>
                {
                    if (!bru.pset?.Any(x => x.id == r.id) == true)
                    {
                        bru.pset.Add(r);
                    }
                });
            }

            List<platform_module> m = _context.platform_module.Where(x => rights.modules.Contains(x.id)).ToList();
            if (bru.platform_user_module != null)
            {
                bru.platform_user_module.Where(x => x.qismId == rights.qismId && !rights.modules.Any(y => x.moduleId == y)).ToList().ForEach(x => bru.platform_user_module.Remove(x));
            }

            m.ForEach(m =>
            {
                if (!bru.platform_user_module?.Any(x => x.moduleId == m.id && x.qismId == rights.qismId) == true)
                {
                    bru.platform_user_module.Add(new platform_user_module
                    {
                        moduleId = m.id,
                        qismId = rights.qismId,
                        userId = rights.itsId
                    });
                }
            });


            _context.SaveChanges();

            //return Ok("User is successfully updated");
            return Ok();
        }

        [Route("getauthuser")]
        [HttpGet]
        public async Task<ActionResult<BranchUserModel>> getAuthUser()
        {

            string api = "user/getauthuser";

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


            try
            {

                branch_user y = _context.branch_user.Where(x => x.itsId == authUser.ItsId)
                    .Include(x => x.its)
                    .Include(x => x.deptVenue)
                    .Include(x => x.pset)
                    .Include(x => x.platform_user_module)
                    .FirstOrDefault();

                BranchUserModel z = new BranchUserModel
                {
                    emailId = y.emailId,
                    id = y.itsId,
                    itsId = y.itsId,
                    mobile = y.its.mobileNo,
                    modules = y.platform_user_module.ToList().Select(x => x.moduleId).ToList(),
                    password = y.password,
                    userName = y.its.fullName
                };

                z.qism = y.deptVenue.Where(x => x.qismId != null).GroupBy(x => x.qismId).Select(x => x.First()).ToList().Select(x => new QismModel
                {
                    emailId = x.qism.emailId,
                    id = x.qismId ?? 0,
                    name = x.qism.name,
                    modules = x.qism.platform_user_module.Where(k => k.userId == z.itsId).Select(k => k.moduleId).ToList()
                }).ToList();

                //users.ForEach(x => usersModel.Add(Translater.toModel(x, _context.roles.FirstOrDefault(y => y.roleId == x.roleId).roleName)));
                return Ok(z);

            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return BadRequest(e.Message);
            }


            // return Ok(ServiceFactory.GetUserService().getAllUsers());
        } 

        [Route("getsingleuser")]
        [HttpGet]
        public async Task<ActionResult> getSingleUser()
        {
            string api = "api/user/getsingleuser";
            // Add_ApiLogs(api);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            try
            {
                List<UserModel> usersModel = new List<UserModel>();
                user x = new user();

                x = _context.user.Where(y => y.ItsId == authUser.ItsId).FirstOrDefault();
                string roleName = null;
                if (x.roleId != null)
                {
                    roleName = _context.role.FirstOrDefault(y => y.roleId == x.roleId).roleName;
                }
                usersModel.Add(new UserModel
                {
                    id = x.Id,
                    itsId = x.ItsId,
                    userName = x.Username,
                    role = roleName != null ? roleName : "Not Available",
                    description = x.Accesslevel,
                    password = x.Password,
                    roleId = x.roleId,
                    status = x.loginStatus,
                    emailId = x.EmailId,
                    mobile = x.Mobile,
                });
                return Ok(usersModel.First());

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        [Route("getdeptvenuetrueright/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getDeptVenueTrueRight(int itsId)
        {
            string api = "api/user/getdeptvenuetrueright/{itsId}";
            // Add_ApiLogs(api);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (itsId == 500)
            {
                itsId = authUser.ItsId;
            }

            List<DeptVenueRightModel> deptvenue = new List<DeptVenueRightModel>();


            var deptVenue = _context.dept_venue.Where(x => x.status.Equals("active")).ToList();
            var d = _context.user_deptvenue.Where(x => x.itsId == itsId).ToList();
            var programSet = _context.registrationform_dropdown_set.ToList();
            var subprogram = _context.registrationform_subprograms.ToList();

            foreach (var i in d)
            {
                var dv = deptVenue.Where(x => x.id == i.deptVenueId).FirstOrDefault();

                if (dv != null)
                {
                    var program = programSet.Where(x => x.id == i.psetId).FirstOrDefault();
                    var className = subprogram.Where(x => x.id == program.subprogramId).FirstOrDefault();
                    deptvenue.Add(new DeptVenueRightModel
                    {
                        id = dv.id,
                        deptId = dv.deptId,
                        venueId = dv.venueId,
                        deptName = dv.deptName,
                        venueName = dv.venueName,
                        deptVenueName = className.name + "_" + dv.venueName + "_" + dv.deptName,
                        right = true,
                        psetId = i.psetId,
                        classId = className.id
                        
                    });
                }

                //foreach (var dv in deptVenue)
                //{
                //    if(dv.id == i.deptVenueId)
                //    {
                //        if (d != null)
                //        {
                            

                //        }
                //    }
                //}




            }

            deptvenue = deptvenue.OrderBy(x => x.classId).ToList();

            return Ok(deptvenue);
        }

        [Route("getregistrationpagetrueright/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getRegistrationPageTrueRight(int itsId)
        {
            string api = "api/user/getregistrationpagetrueright/{itsId}";
            // Add_ApiLogs(api);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);


            if (itsId == 500)
            {
                itsId = authUser.ItsId;
            }

            List<ReportsRightsModel> deptvenue = new List<ReportsRightsModel>();


            List<registrationform_dropdown_set> dv = _context.registrationform_dropdown_set.ToList();
            List<student_registration_rights> srr = _context.student_registration_rights.ToList();
            List<registrationform_programs> rp = _context.registrationform_programs.ToList();
            List<registrationform_subprograms> rs = _context.registrationform_subprograms.ToList();
            List<venue> vn = _context.venue.ToList();

            foreach (var i in dv)
            {
                student_registration_rights d = srr.Where(x => x.itsId == itsId && x.programSetId == i.id).FirstOrDefault();

                registrationform_programs p = rp.Where(x => x.id == i.programId).FirstOrDefault();
                registrationform_subprograms sp = rs.Where(x => x.id == i.subprogramId).FirstOrDefault();
                venue v = vn.Where(x => x.Id == i.venueId).FirstOrDefault();

                if (p != null && v != null && sp != null)
                {
                    if (d != null)
                    {
                        deptvenue.Add(new ReportsRightsModel { rId = i.id, reportName = p.name + " / " + v.CampVenue + " / " + sp.name, right = true });

                    }


                }



            }

            deptvenue = deptvenue.OrderBy(x => x.reportName).ToList();


            return Ok(deptvenue);
        }

        [Route("remove/{qismId}/{itsId}")]
        [HttpDelete]
        public async Task<ActionResult<BranchUserModel>> removeUser([FromRoute] int qismId, [FromRoute] int itsId)
        {
            string api = "get/all/user";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext); AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                branch_user users = _context.branch_user.Where(x => x.itsId == itsId)
                    .Include(x => x.its)
                    .ThenInclude(x => x.mauzeNavigation)
                    .Include(x => x.deptVenue.Where(x => x.qismId == qismId))
                    .Include(x => x.pset.Where(x => x.qismId == qismId))
                    .Include(x => x.platform_user_module)
                    .Include(x => x.venues.Where(x => x.qismId == qismId))
                    .FirstOrDefault();

                users.deptVenue.Where(x => x.qismId == qismId).ToList().ForEach(x => users.deptVenue.Remove(x));

                users.pset.Where(x => x.qismId == qismId).ToList().ForEach(x => users.pset.Remove(x));

                users.platform_user_module.Where(x => x.qismId == qismId).ToList().ForEach(x => users.platform_user_module.Remove(x));

                users.venues.Where(x => x.qismId == qismId).ToList().ForEach(x => users.venues.Remove(x));


                if (users.platform_user_module.Count == 0)
                {
                    _context.branch_user.Remove(users);
                }

                _context.SaveChanges();
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("get/all/{qismId}")]
        [HttpGet]
        public async Task<ActionResult<BranchUserModel>> getAllUser(int qismId)
        {
            string api = "get/all/user";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext); AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                List<BranchUserModel> users = _context.branch_user.Where(x => x.platform_user_module.Any(y => y.qismId == qismId) && x.itsId != 1 && x.itsId != authUser.ItsId && x.itsId != x.qism_al_tahfeez.itsId)
                    .Include(x => x.its)
                    .ThenInclude(x => x.mauzeNavigation)
                    .Select(x => new BranchUserModel
                    {
                        userName = x.its.fullName,
                        id = x.itsId,
                        emailId = x.emailId,
                        itsId = x.itsId,
                        mobile = x.its.mobileNo,
                        designation = x.its.designation,
                        mz_idara = x.its.mz_idara,
                        employeeType = x.its.employeeType,
                        mauze = x.its.mauzeNavigation.displayName,
                    }).ToList();

                return Ok(users);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("get/all/venues/{qismId}")]
        [HttpGet]
        public async Task<ActionResult<List<DeptVenueRightModel>>> getAllVenues([FromRoute] int qismId, [FromQuery] int? itsId)
        {
            string api = "get/all/deptVenue";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext); AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                if (itsId == null)
                {
                    itsId = authUser.ItsId;
                }

                List<DeptVenueRightModel> l1 = _context.venue.Where(x => x.qismId == qismId && x.branch_users.Any(y => y.itsId == itsId)).Select(x => new DeptVenueRightModel
                {
                    id = x.Id,
                    deptName = x.CampVenue,
                    venueName = x.displayName,
                    right = true,
                }).ToList();

                return Ok(l1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("get/all/deptvenue/{qismId}")]
        [HttpGet]
        public async Task<ActionResult<List<DeptVenueRightModel>>> getAllDeptVenue([FromRoute] int qismId, [FromQuery] int? itsId, [FromQuery] string? venueIds)
        {
            string api = "get/all/deptVenue";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext); AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                if (itsId == null)
                {
                    itsId = authUser.ItsId;
                }


                List<int> venueIdList = _helperService.parseIds(venueIds);

                List<DeptVenueRightModel> l = _context.dept_venue.Where(x => x.qismId > 0 && x.qismId == qismId && x.user.Any(y => y.itsId == itsId))
                                                .Include(x => x.user)
                                                .ToList().Select(x => new DeptVenueRightModel
                                                {
                                                    id = x.id,
                                                    deptId = x.deptId,
                                                    venueId = x.venueId,
                                                    deptName = x.deptName,
                                                    venueName = x.venueName,
                                                    right = true,
                                                    deptVenueName = x.deptName + "_" + x.venueName,
                                                    qismId = x.qismId,
                                                    isTagged = true
                                                }).ToList();
                if (venueIdList.Count > 0)
                {
                    l = l.Where(x => venueIdList.Any(y => y == x.venueId)).ToList();
                }

                return Ok(l);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("get/deptvenueforuser/{userId}/{qismId}")]
        [HttpGet]
        public async Task<ActionResult<List<DeptVenueRightModel>>> getDeptVenueForUser([FromRoute] int userId, [FromRoute] int qismId)
        {
            string api = "get/deptvenueforuser/{userId}";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext); AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                branch_user bru = _context.branch_user.Where(x => x.itsId == userId).Include(x => x.deptVenue).FirstOrDefault();
                qism_al_tahfeez q = _context.qism_al_tahfeez.Where(x => x.id == qismId).Include(x => x.dept_venue.Where(x => x.status == "active" && x.user.Contains(bru))).ThenInclude(x => x.user).FirstOrDefault();
                List<dept_venue> dptVens = q.dept_venue.Where(x => x.status == "active" && x.user.Contains(bru)).ToList();
                List<DeptVenueRightModel> l = new List<DeptVenueRightModel>();

                foreach (dept_venue d in dptVens)
                {
                    DeptVenueRightModel dvrm = new DeptVenueRightModel();
                    dvrm.id = d.id;
                    dvrm.deptName = d.deptName;
                    dvrm.venueName = d.venueName;
                    dvrm.qismId = d.qismId;
                    dvrm.right = bru.deptVenue.Contains(d);
                    dvrm.isTagged = true;
                    dvrm.deptId = d.deptId;
                    dvrm.deptVenueName = d.deptName + "_" + d.venueName;
                    dvrm.venueId = d.venueId;
                    l.Add(dvrm);
                }


                return Ok(l);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("get/all/pset/{qismId}")]
        [HttpGet]
        public async Task<ActionResult<List<ReportsRightsModel>>> getAllDeptPrograms([FromRoute] int qismId, [FromQuery] int? itsId, [FromQuery] string venueIds = "", [FromQuery] string deptVenueIds = "")
        {
            string api = "get/all/pset";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext); AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                if (itsId == null)
                {
                    itsId = authUser.ItsId;
                }


                List<int> venueIdList = _helperService.parseIds(venueIds);
                List<int> deptVenueIdList = _helperService.parseIds(deptVenueIds);


                List<registrationform_dropdown_set> list = _context.registrationform_dropdown_set.Where(x => x.qismId > 0
                    && x.qismId == qismId
                    && x.user.Any(y => y.itsId == itsId)
                ).Include(x => x.program).Include(x => x.subprogram).Include(x => x.venue).ToList();

                if (venueIdList.Count > 0)
                {
                    list = list.Where(x => venueIdList.Any(y => y == x.venueId)).ToList();
                }

                if (deptVenueIdList.Count > 0)
                {
                    list = list.Where(x => deptVenueIdList.Any(y => y == x.deptVenueId)).ToList();
                }

                List<ReportsRightsModel> l = list.Select(x => new ReportsRightsModel
                {
                    isTagged = true,
                    qismId = x.qismId,
                    reportName = x.program.name + "/" + x.subprogram.name + "/" + x.venue.displayName,
                    rId = x.id,
                    right = true
                }).ToList();

                return Ok(l);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [Route("get/psetsforuser/{userId}/{qismId}")]
        [HttpGet]
        public async Task<ActionResult<List<ReportsRightsModel>>> getDeptProgramsForUser(int userId, int qismId)
        {
            string api = "get/all/pset";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext); AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                branch_user bru = _context.branch_user.Where(x => x.itsId == userId).FirstOrDefault();

                List<ReportsRightsModel> list = _context.qism_al_tahfeez.Where(x => x.id == qismId)
                    .Include(x => x.registrationform_dropdown_set)

                    .FirstOrDefault()
                    .registrationform_dropdown_set.Where(x => x.qismId == qismId).ToList().Select(x => new ReportsRightsModel
                    {
                        isTagged = true,
                        qismId = x.qismId,
                        reportName = x.program.name + "/" + x.subprogram.name + "/" + x.venue.displayName,
                        rId = x.id,
                        right = bru.pset.Contains(x)
                    }).ToList();

                list = list.Where(x => x.right == true).ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("resetpassword")]
        [HttpPost]
        public async Task<ActionResult<string>> resetPassword(pswRest psw)
        {
            string api = "user/resetpassword/" + psw.newPassword;
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                branch_user bru = _context.branch_user.Where(x => x.itsId == authUser.ItsId).FirstOrDefault();
                qism_al_tahfeez qism = _context.qism_al_tahfeez.Where(x => x.itsId == authUser.ItsId).FirstOrDefault();
                khidmat_guzaar khidmat_Guzaar = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId).FirstOrDefault();

                if (bru.password != psw.oldPassword)
                {
                    if (qism != null && qism.password != psw.oldPassword)
                    {
                        //throw new Exception("Please enter the correct current qism or user password");
                        return BadRequest();
                    }
                    else
                    {
                        return BadRequest();
                        //throw new Exception("Please enter the correct current password");
                    }
                    //if (qism == null)
                    //{
                    //    if(bru != null && bru.password != psw.oldPassword)
                    //    {
                    //        throw new Exception("Please enter the correct current password");
                    //    }
                    //    if(bru == null)
                    //    {
                    //        throw new Exception("Invalid User");
                    //    }
                    //}
                }

                //if (psw.newPassword.Length < 6)
                //{
                //    throw new Exception("Password must be at least 6 characters long");
                //}

                khidmat_Guzaar.password = psw.newPassword;
                if (bru != null)
                {
                    bru.password = psw.newPassword;
                }

                if (qism != null)
                {
                    qism.password = psw.newPassword;
                }
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("post/populateusers")]
        [HttpPost]
        public async Task<ActionResult<string>> populateQism(QismAlTahfeezModel nq)
        {
            string api = "post/populateusers";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext); AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);


            BranchUserModel u = nq.user;
            List<DeptVenueRightModel> d = nq.dv;
            List<ReportsRightsModel> r = nq.rr;
            List<ModulePageModel> ModulePageModels = nq.pageModels;

            branch_user bru = _context.branch_user.Where(x => x.itsId == u.itsId).FirstOrDefault();
            if (u.password == "create")
            {

                branch_user u1 = _context.platform_user_module.Where(x => x.userId == u.itsId && x.qismId == u.id).Select(x => x.user).FirstOrDefault();

                if (u1 != null)
                {
                    return BadRequest(new { message = "User already exists!" });
                }

                string password = new PasswordGenerator().GeneratePassword(7);

                if (bru == null)
                {
                    khidmat_guzaar kh = _context.khidmat_guzaar.Where(x => x.itsId == u.itsId).FirstOrDefault();
                    bru = new branch_user
                    {
                        emailId = kh.officialEmailAddress == null ? kh.emailAddress : kh.officialEmailAddress,
                        itsId = u.itsId,
                        password = password,
                    };

                    kh.password = password;

                    _context.branch_user.Add(bru);
                }
                _context.SaveChanges();

            }

            foreach (var i in d)
            {

                dept_venue dqid = _context.dept_venue.Where(x => x.id == i.id && x.qismId == u.id).FirstOrDefault();

                if (dqid != null && i.right && (!bru.deptVenue.Contains(dqid)))
                {
                    bru.deptVenue.Add(dqid);
                }
                else if (dqid != null && (!i.right) && bru.deptVenue.Contains(dqid))
                {
                    bru.deptVenue.Remove(dqid);
                }
                _context.SaveChanges();

            }

            foreach (var i in r)
            {

                registrationform_dropdown_set rqid = _context.registrationform_dropdown_set.Where(x => x.id == i.rId && x.qismId == u.id).FirstOrDefault();
                if (rqid != null && i.right && (!bru.pset.Contains(rqid)))
                {
                    bru.pset.Add(rqid);
                }
                else if (rqid != null && (!i.right) && bru.pset.Contains(rqid))
                {
                    bru.pset.Remove(rqid);
                }
                _context.SaveChanges();
            }

            ModulePageModels.ForEach(y =>
            {
                y.modules.ForEach(x =>
                {
                    platform_user_module ur = _context.platform_user_module.Where(z => z.qismId == u.id && z.userId == u.itsId && z.moduleId == x.moduleId).FirstOrDefault();
                    if (x.isChecked == true && ur == null)
                    {
                        _context.platform_user_module.Add(new platform_user_module { qismId = u.id, moduleId = x.moduleId, userId = u.itsId });
                    }
                    else if (x.isChecked == false && ur != null)
                    {
                        _context.platform_user_module.Remove(ur);
                    }
                });
            });
            _context.SaveChanges();

            return Ok("User is successfully updated");


        }

        [Route("delete/{userId}")]
        [HttpDelete]
        public async Task<ActionResult<string>> populateQism(int userId)
        {
            string api = "delete/user/{userId}";
            string token = _tokenService.ExtractTokenFromRequest(HttpContext); AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);



            branch_user qu = _context.branch_user.Where(x => x.itsId == userId).FirstOrDefault();
            if (qu == null)
            {
                return BadRequest(new { message = "User does not exist" });
            }

            qu.deptVenue.Where(x => x.qismId == authUser.qismId).ToList().ForEach(x => qu.deptVenue.Remove(x));
            qu.pset.Where(x => x.qismId == authUser.qismId).ToList().ForEach(x => qu.pset.Remove(x));
            _context.branch_user.Remove(qu);
            _context.SaveChanges();


            return Ok("User is successfully deleted");


        }

        [Route("getDropdown/{id}")]
        [HttpGet]
        public async Task<ActionResult> getDropdown([FromRoute] int id)
        {
            string api = "api/user/getDropdown/{id}";

            try
            {
                List<dropdown_dataset_options> options = _context.dropdown_dataset_options.Where(x => x.headerId == id).ToList();
                List<dd> opt = new List<dd>();
                options.ForEach(x => opt.Add(new dd
                {
                    name = x.name,
                    headerId = x.id,
                    id = x.id
                }));
                return Ok(new { data = opt });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("itsuser/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getItsUser(int itsId)
        {
            ItsUser? user = new ItsUser();
            JHSAcademicData? jhsUser = new JHSAcademicData();

            //try
            //{
                user = await _itsService.GetItsUser(itsId);

                if (user == null)
                {
                    return BadRequest(new { message = "User not found" });
                }
            jhsUser = await _jhsService.GetJHSAcademicData(itsId);


            if (jhsUser == null)
            {
                jhsUser = new JHSAcademicData();
            }

            user.farighDarajah = jhsUser.farighDarajah;
            user.farighYear = jhsUser.farighYear;
            user.jameaDegree = jhsUser.jameaDegree;

                return Ok(user);
            //}
            //catch (Exception e)
            //{
            //      return BadRequest(e.Message);
            //}
        }

        [Route("getItsData")]
        [HttpPost]
        public async Task<ActionResult> getItsData(FeesAllotmentModel model)
        {
            string api = "api/query/getItsData";
            // Add_ApiLogs(api);

            string itsIdCSV = model.itsIdCSV;

            List<ItsUser> users = new List<ItsUser>();
            try
            {
                List<int> itsIds = _helperService.parseItsId(itsIdCSV);

                if (itsIds.Count > 40)
                {
                    return BadRequest(new { message = "ITS Ids cannot be more than 50" });
                }

                int c = 1;
                foreach (var i in itsIds)
                {
                    ItsUser user = new ItsUser();

                    try
                    {
                        user = await _itsService.GetItsUser(i);
                        JHSAcademicData jhSAcademicData = await _jhsService.GetJHSAcademicData(i);
                        if (jhSAcademicData != null)
                        {
                            user.farighDarajah = jhSAcademicData.farighDarajah;
                            user.farighYear = jhSAcademicData.farighYear;
                            user.jameaDegree = jhSAcademicData.jameaDegree;
                        }
                        if (user == null)
                        {
                            continue;
                        }
                        users.Add(user);
                        user.photo2 = Convert.ToBase64String(user.Photo, 0, user.Photo.Length);
                        user.srNo = c;

                        c = c + 1;
                        await _helperService.SaveITSImage(user.Photo, user.ItsId);
                    }
                    catch (Exception)
                    {

                    }

                }

                return Ok(users);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [Route("getItsData/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> getItsData(int itsId)
        {
            string api = "api/query/getItsData";
            // Add_ApiLogs(api);
            try
            {
                int c = 1;
                ItsUser? user = new ItsUser();

                user = await _itsService.GetItsUser(itsId);
                JHSAcademicData? jhSAcademicData = await _jhsService.GetJHSAcademicData(itsId);

                if (user == null)
                {
                    return BadRequest(new { message = "User not found" });
                }
                if (jhSAcademicData != null)
                {
                    user.farighDarajah = jhSAcademicData.farighDarajah;
                    user.farighYear = jhSAcademicData.farighYear;
                    user.jameaDegree = jhSAcademicData.jameaDegree;
                }

                if (user.Photo != null)
                {
                    user.photo2 = Convert.ToBase64String(user.Photo, 0, user.Photo.Length);
                    await _helperService.SaveITSImage(user.Photo, user.ItsId);
                }
                user.srNo = c;

                c = c + 1;

                return Ok(user);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [Route("getAllUserDept")]
        [HttpGet]
        public async Task<ActionResult> getAllUserDept()
        {
            var usersData = _context.user.Include(x => x.role).ToList();
            var deptVenues = _context.dept_venue.OrderBy(x => x.venueName).ThenBy(x => x.deptId).ToList();
            List<venue> venues = _context.venue.OrderBy(x => x.CampVenue).ToList();
            var classes = _context.registrationform_subprograms.Where(x => x.id < 18).ToList();

            //foreach (var depts in deptVenues)
            //{
            //    System.Diagnostics.Debug.WriteLine($"This is deptVenues: {depts.dept}");
            //}

            var dept = new List<object>();
            foreach (var venue in venues)
            {
                dept.Add(new {
                    Id = venue.Id,
                    VenueName = venue.CampVenue
                });
            }

            var cls = new List<object>();
            foreach (var classs in classes)
            {
                cls.Add(new
                {
                    Id = classs.id,
                    ClassName = classs.name
                });
            }


            List<users> userData = new List<users> { };
            foreach (var data in usersData)
            {
                userData.Add(new users
                {
                    Id = data.Id,
                    Username = data.Username,
                    ItsId = data.ItsId,
                    email = data.EmailId,
                    mobile = data.Mobile,
                    roleId = data.roleId ?? 0,
                    roleName = data.role.roleName
                }
                );           
            }

            List<object> AllData = new List<object>();

            AllData.Add(new
            {
                instituteData = dept,
                classData = cls,
                usersData = userData
            });

            return Ok(AllData);
        }

        [Route("getUserDeptClass")]
        [HttpGet]
        public async Task<ActionResult> getUserDeptClass()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext); 
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            var udvp = _context.user_deptvenue.Where(x => x.itsId == authUser.ItsId).ToList();
            var depts = _context.dept_venue.ToList();
            var rds = _context.registrationform_dropdown_set.ToList();
            var sps = _context.registrationform_subprograms.ToList();


            List<object> data = new List<object>();

            foreach (var item in udvp)
            {
                var dept = depts.Where(x => x.id == item.deptVenueId).FirstOrDefault();
                
                if(dept != null)
                {
                    var rd = rds.Where(x => x.deptVenueId == dept.id).ToList();

                    if(rd != null)
                    {
                        foreach (var item1 in rd)
                        {
                            var sp = sps.Where(x => x.id == item1.subprogramId).FirstOrDefault();

                            if (sp != null)
                            {
                                data.Add(new
                                {
                                    deptId = dept.id,
                                    schoolId = dept.venueId,
                                    name = dept.venueName + "_" + dept.deptName + "_" + sp.name,
                                    classId = sp.id,
                                    psetId = item1.id
                                });
                            }
                        }
                        
                    }
                }
            }
            return Ok(data);
        }

        [Route("getUserSchools")]
        [HttpGet]
        public async Task<IActionResult> getUserSchools()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            var udvp = _context.user_deptvenue.Where(x => x.itsId == authUser.ItsId).ToList();
            var depts = _context.dept_venue.ToList();
            var groupedDept = depts.GroupBy(x => x.venueId).ToList();

            List<data> data = new List<data>();

            foreach (var item in udvp)
            {
                var deptGroup = groupedDept.FirstOrDefault(g => g.Any(x => x.id == item.deptVenueId));

                if (deptGroup != null)
                {
                    var dept = deptGroup.First(x => x.id == item.deptVenueId);
                    
                    if(!data.Any(x => x.schoolId == dept.venueId))
                    {
                        data.Add(new data
                        {
                            schoolId = dept.venueId,
                            schoolName = dept.venueName
                        });

                    }
                                           
                }
            }
            return Ok(data);
        }

        [Route("getClasses")]
        [HttpGet]
        public async Task<IActionResult> getClasses()
        {
            //string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            //AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<registrationform_subprograms> rs = _context.registrationform_subprograms.Where(x => x.id < 18).ToList();

            List<classes> cls = rs.Select(x => new classes{
                id = x.id,
                name = x.name
            }).ToList();


            return Ok(cls);
        }

        [Route("assignUserDept")]
        [HttpPost]
        public async Task<IActionResult> assignUserDept()
        {

            return Ok();
        }
    }

    public class pswRest
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }

    public class data
    {
        public int? schoolId { get; set; }
        public string? schoolName { get; set; }
    }

    public class dd
    {
        public int id { get; set; }
        public int? headerId { get; set; }
        public string name { get; set; }
    }

    public class rightsTobeAssigned
    {
        public int itsId { get; set; }
        public List<int> venue { get; set; }
        public List<int> deptVenue { get; set; }
        public List<int>? programs { get; set; }
        public List<int> modules { get; set; }
        public int qismId { get; set; }
    }

    public class users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int ItsId { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public int roleId { get; set; }
        public string roleName { get; set; }
    }

    public class classes
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
