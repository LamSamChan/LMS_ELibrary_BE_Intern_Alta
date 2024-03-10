using LMS_Library_API.Helpers;
using LMS_Library_API.Models;
using LMS_Library_API.Services.AuthService;
using LMS_Library_API.Services.RoleAccess.RoleService;
using LMS_Library_API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthSvc _authService;
        private readonly IConfiguration _configuration;

        public AuthsController(IAuthSvc authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("user/login")]
        public async Task<ActionResult> UserLogin(LoginVM loginVM)
        {
            string token = null;
            var loggerResult = await _authService.IsUserLogin(loginVM);

            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                User user = (User)loggerResult.data;
                token = CreateToken(user.Email, user.Role.Name);
                return Ok(token);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        [HttpPost("student/login")]
        public async Task<ActionResult> StudentLogin(LoginVM loginVM)
        {
            string token = null;
            var loggerResult = await _authService.IsStudentLogin(loginVM);

            if (loggerResult.status == TaskStatus.RanToCompletion)
            {
                Student student = (Student)loggerResult.data;
                token = CreateToken(student.Email, "Student");
                return Ok(token);
            }
            else
            {
                return BadRequest(loggerResult);
            }
        }

        private string CreateToken(string email, string role)
        {
            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["AppSettings:Token"]!));

            var creads = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: creads
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
