using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace mahadalzahrawebapi.Api.v1
{
    public class UserModel
    {
        public int? id { get; set; }
        public string? userName { get; set; }
        public string? name { get; set; }
        public int? itsId { get; set; }
        public string? password { get; set; }
        public string? emailId { get; set; }
        public string? mobile { get; set; }
        public string? role { get; set; }
        public string? description { get; set; }
        public string? status { get; set; }
        public int? roleId { get; set; }
        public List<QismModel>? qism { get; set; }
        public List<int>? modules { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TokenService _tokenService;

        private readonly List<String> _secrets = new List<String>()
        {
            "68940fa8-053c-4395-9a13-ca1879f97def", //admin
            "718437b2-4d08-4c07-9e8e-ef168bf3a354", //student
            "9768372a-0141-45e4-88c3-682dc0bf69e4", //branch
            "a824e952-2205-4296-8915-9a131cae3af6", //testing
        };

        public AuthenticationController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(AuthUser authUser, [FromQuery] string secret)
        {

            // Your authentication logic here (e.g., validating credentials)

            if (!_secrets.Any(x => x == secret))
            {
                return BadRequest(new { message = "UnAuthorized User" });
            }

            authUser.loginName = "HR_Login_Angular";

            // If authentication is successful, generate a JWT token
            var token = _tokenService.GenerateJwtToken(authUser);

            return Ok(new { Token = token });
        }
    }

}
