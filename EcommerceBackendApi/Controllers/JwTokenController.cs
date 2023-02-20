using EcommerceBackendApi.Data;
using EcommerceBackendApi.Models;
using EcommerceBackendApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IConfiguration _configuration;
        public readonly EcommerceDbContext _context;

        public JwTokenController(IConfiguration configuration, EcommerceDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post(JwTokenRequestDto user)
        {
            if (user != null && user.Email != null && user.Password != null && user.Role !=null)
            {
                var userData = await GetUser(user.Email, user.Password, user.Role);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                if (userData != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName",user.Email),
                        new Claim("Password",user.Password),
                        new Claim("Role",user.Role)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(

                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn
                     );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }

            }
            else
            {
                return BadRequest("Invalid Credentials");
            }

        }

        [NonAction]
        public async Task<User> GetUser(string email, string password, string role)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.Role == role);
        }
    }
}
