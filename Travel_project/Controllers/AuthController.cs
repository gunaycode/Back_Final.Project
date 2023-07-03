using Application.Abstract;
using Application.DTOs.AuthDto;
using Application.DTOs.MailSender;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using Persistance.DataContext;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
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
        readonly RoleManager<Role> _roleManager;
        readonly IEmailServices _emailService;
        public AuthController(UserManager<User> userManager, IConfiguration configuration,RoleManager<Role> roleManager, IEmailServices services)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _emailService = services;
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
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string? link = Url.Action("ConfirmUser", "Auth", new { email = user.Email, token = token }, HttpContext.Request.Scheme);
            _emailService.SendMessage(token, "Confirm", user.Email);
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

        [HttpPost("ConfirmUser")]
        public async Task<IActionResult> ConfirmUser(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Invalid email.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest("Email confirmation failed.");
            }

            return Ok("Email confirmed successfully.");
        }

        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgotPassword([FromForm]ForgetPasswordDto forgotPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user is null)
            {
                return BadRequest("User not found.");
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action("ResetPassword", "Auth", new { UserId = user.Id, token });
            string message = $"Please reset your password by clicking the following link: {link}";
            string subject = "Password Reset";
            _emailService.SendMessage(message, subject, user.Email);
            return Ok(link);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto resetPasswordDto, string UserId, string token)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }
            var resetPasswordResul = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordResul,resetPasswordDto.NewPassword );
            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }


    }
}


