using Domain.Entities;
using Infrastructure.InterfaceServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class JwtServices : IJwtToken
    {
        private readonly IConfiguration _configure;
        public JwtServices(IConfiguration configure)
        {
            _configure = configure;
        }

        public string GetJwt(User user, IList<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configure.GetSection("Jwt:securityKey").Value));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configure.GetSection("Jwt:issuer").Value,
                audience: _configure.GetSection("Jwt:audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.AddMonths(2),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
