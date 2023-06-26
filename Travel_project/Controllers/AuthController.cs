using Application.DTOs.AuthDto;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly UserManager<User> _userManager;
        readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            User user = new()
            {
                Name = register.Firstname,
                Surname = register.Lastname,
                Email = register.Email,
                UserName = register.Username,
                BirthDate = register.BirthDate,
            };
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new
            {
                user.Name,
                user.Email
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user == null)
            {
                return BadRequest(new { Message = "Username or password incorrect!!!" });
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!passwordValid)
            {
                return BadRequest(new { Message = "Username or password incorrect!!!" });
            }

            var jwtSettings = _configuration.GetSection("JWT");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecurityKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
            };

            IList<string> roles =await  _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(name => new Claim(ClaimTypes.Role, name)));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedToken = tokenHandler.WriteToken(token);

            return Ok(new { Token = encodedToken });
        }

    }
}


