using mahadalzahrawebapi.GenericModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mahadalzahrawebapi.Services
{

    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public string? GenerateJwtToken(AuthUser user)
        {
            //var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            //var deviceDetails = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString()),
                new Claim("qismId", user.qismId.ToString()),
                new Claim("its_id", user.ItsId.ToString()),
                new Claim("department", user.Department),
                new Claim("display_name", user.DisplayName),
                new Claim("department_code", user.DepartmentCode),
                new Claim("did", user.DID),
                new Claim("dept_venue_id", user.DeptVenueId.ToString()),
                new Claim("dept_venue_name", user.DeptVenueName),
                new Claim("ip_address", user.ipAddress),
                new Claim("device_details", user.deviceDetails),
                new Claim("log_id", user.logId.ToString()),
                new Claim("login_name", user.loginName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:ValidIssuer"],
                audience: _configuration["JwtSettings:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24), // This sets the token to expire in 24 hour
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _configuration["JwtSettings:ValidIssuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JwtSettings:ValidAudience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(15) // This sets the maximum allowable clock skew - i.e. the time difference between the server and client
            };

            try
            {
                SecurityToken validatedToken;
                return tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
            }
            catch (Exception)
            {
                // Token validation failed
                return null;
            }
        }

        public string ExtractTokenFromRequest(HttpContext httpContext)
        {
            var token = string.Empty;
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.Contains("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorizationHeader.Substring("Bearer ".Length).Trim();
            }

            return token;
        }

        public AuthUser GetAuthUserFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var authUser = new AuthUser
            {
                Id = long.Parse(jwtToken.Claims.First(x => x.Type == "id").Value),
                qismId = int.Parse(jwtToken.Claims.First(x => x.Type == "qismId").Value),
                Name = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value,
                ItsId = int.Parse(jwtToken.Claims.First(x => x.Type == "its_id").Value),
                Department = jwtToken.Claims.First(x => x.Type == "department").Value,
                DisplayName = jwtToken.Claims.First(x => x.Type == "display_name").Value,
                DepartmentCode = jwtToken.Claims.First(x => x.Type == "department_code").Value,
                DID = jwtToken.Claims.First(x => x.Type == "did").Value,
                DeptVenueId = int.Parse(jwtToken.Claims.First(x => x.Type == "dept_venue_id").Value),
                DeptVenueName = jwtToken.Claims.First(x => x.Type == "dept_venue_name").Value,
                ipAddress = jwtToken.Claims.First(x => x.Type == "ip_address").Value,
                deviceDetails = jwtToken.Claims.First(x => x.Type == "device_details").Value,
                logId = long.Parse(jwtToken.Claims.First(x => x.Type == "log_id").Value),
                loginName = jwtToken.Claims.First(x => x.Type == "login_name").Value
            };

            return authUser;
        }

    }
}
