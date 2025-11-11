using CricUpdate.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CricUpdate.API.Services
{
    public class TokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name,user.Username),
                new Claim(JwtRegisteredClaimNames.Email,user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role,user.Role ?? "User")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(configuration["Jwt:ExpireMinutes"] ?? "10")),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
