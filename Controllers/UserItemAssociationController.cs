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
    public class UserItemAssociationController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        public UserItemAssociationController(mzdbContext context, IMapper mapper, TokenService tokenService)
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

        [Route("GetUser/{itsId}")]
        [HttpGet]
        public async Task<ActionResult> GetUserDetails(int itsid)
        {
            string api = "api/GetUser/{itsId}";
            //// Add_ApiLogs(api);

            List<UserDeptAssociationModel> departments = new List<UserDeptAssociationModel>();

            //List<user> user = _context.user.Where(x => x.ItsId == itsid).ToList();
            user us = _context.user.FirstOrDefault(x => x.ItsId == itsid);
            string userName = us.Username;

            if (us == null)
            {
                return BadRequest(new { message = "No user with this itsId found" });
            }
            List<userdeptassociation> userdeptassociation = _context.userdeptassociation.Where(x => x.DID == us.DID).ToList();
            if (userdeptassociation == null) { throw new Exception("No Entity Found"); }

            else if (userdeptassociation != null)
            {
                departments = _mapper.Map<List<UserDeptAssociationModel>>(userdeptassociation);
            }
            return Ok(new { departments, userName });


            //return Ok(department);
        }


        [Route("UserItemDetails/{ItsId}/{DID}")]
        [HttpPost]
        public async Task<ActionResult> PostItemForUser(int ItsId, int DID, List<UserItemAssociation> selectedBaseItem)
        {
            string api = "api/UserItemDetails/{ItsId}/{DID}";
            //// Add_ApiLogs(api);

            //user user = _context.user.FirstOrDefault(x => x.ItsId == selectedUser.ItsId && x.DID == selectedUser.DID);
            user user = _context.user.FirstOrDefault(x => x.ItsId == ItsId && x.DID == DID);
            if (user == null) throw new Exception("No User Found with this ItsId");

            foreach (var item in selectedBaseItem)
            {
                useritemassociation useritem = _context.useritemassociation.FirstOrDefault(x => x.UserId == user.Id && x.BaseItemId == item.BaseItemId);
                if (useritem == null)
                {
                    _context.useritemassociation.Add(new useritemassociation()
                    {
                        UserId = user.Id,
                        BaseItemId = item.BaseItemId,
                        DID = DID
                    });
                }
                else continue;
            }
            _context.SaveChanges();
            return Ok();


        }


        [Route("delete/{ItsId}/{DID}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteItemForUser(int ItsId, int DID, List<UserItemAssociation> selectedBaseItem)
        {
            string api = "api/delete/{ItsId}/{DID}";
            //// Add_ApiLogs(api);

            user user = _context.user.FirstOrDefault(x => x.ItsId == ItsId && x.DID == DID);
            if (user == null) throw new Exception("No User Found with this ItsId");

            List<useritemassociation> useritem = _context.useritemassociation.Where(x => x.UserId == user.Id).ToList();
            foreach (var u in useritem)
            {
                _context.useritemassociation.Remove(u);
            }
            _context.SaveChanges();
            return Ok();

        }



        [Route("GetDepartmentForUser")]
        [HttpGet]
        public async Task<ActionResult> GetUserDepartments()
        {
            string api = "api/GetDepartmentForUser";
            //// Add_ApiLogs(api);

            List<UserDeptAssociationModel> departments = new List<UserDeptAssociationModel>();

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            int itsid = authUser.ItsId;
            List<user> user = _context.user.Where(x => x.ItsId == itsid).ToList();
            if (user == null || user.Count <= 0)
            {
                return BadRequest(new { message = "some error occured, Please try again after refreshing the page" });
            }
            foreach (var u in user)
            {
                List<userdeptassociation> userdeptassociation = _context.userdeptassociation.Where(x => x.DID == u.DID).ToList();
                userdeptassociation.ForEach(x => departments.Add(_mapper.Map<UserDeptAssociationModel>(x)));
            }
            return Ok(departments);
        }


    }
}
