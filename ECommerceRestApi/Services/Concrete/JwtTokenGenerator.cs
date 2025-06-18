using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerceRestApi.Models;

namespace ECommerceRestApi.Services.Concrete
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _config;

        public JwtTokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            // Kullanıcıya ait claimler
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Kullanıcı Id'si
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Subject
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty), // Email
                new Claim("name", user.Name ?? string.Empty), // İsim
                new Claim("surname", user.Surname ?? string.Empty), // Soyisim
                new Claim("userType", user.UserType.ToString()), // UserType (enum veya int olabilir)
                new Claim("userName", user.UserName ?? string.Empty) // Kullanıcı adı
            };

            // Gizli anahtar
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"] ?? throw new InvalidOperationException("JwtSettings:SecretKey is missing")));

            // İmzalama için credential
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token oluşturma
            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            // Token string olarak dönüyor
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
