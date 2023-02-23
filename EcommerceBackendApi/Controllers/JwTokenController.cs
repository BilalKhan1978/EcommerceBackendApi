using EcommerceBackendApi.Data;
using EcommerceBackendApi.Models;
using EcommerceBackendApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwTokenController : Controller
    {
        private IConfiguration _config;
        public readonly EcommerceDbContext _context;

        public JwTokenController(IConfiguration config, EcommerceDbContext context)
        {
            _config = config;
            _context = context;
        }
        //[AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] JwtUserRequestDto userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        [NonAction]
        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [NonAction]
        private User Authenticate(JwtUserRequestDto userLogin)
        {
            var currentUser = _context.Users.FirstOrDefault(o => o.Email.ToLower() == userLogin.Email.ToLower() && o.Password == userLogin.Password);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
