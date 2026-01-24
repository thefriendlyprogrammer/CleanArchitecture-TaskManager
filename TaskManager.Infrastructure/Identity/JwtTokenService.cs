using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaskManager.Infrastructure.Identity
{
    public class JwtTokenService(IConfiguration configuration) : IJwtTokenService
    {
        public string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(configuration["Jwt:Duration"]!)
                ),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
