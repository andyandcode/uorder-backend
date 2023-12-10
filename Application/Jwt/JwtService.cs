using Data.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Accounts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UOrderDbContext _context;

        public JwtService(UOrderDbContext dbContext, IConfiguration configuration)
        {
            _context = dbContext;
            _configuration = configuration;
        }

        public string GenerateToken(AccountVm modal)
        {
            var expires = 0;
            switch (modal.RoleName)
            {
                case "admin":
                    {
                        expires = 15 * 60000;
                        break;
                    }
                case "creator":
                    {
                        expires = 60 * 60000;
                        break;
                    }
                case "staff":
                    {
                        expires = 600 * 60000;
                        break;
                    }
                default:
                    break;
            }

            var claims = new List<Claim>
            {
                new Claim("id", modal.Id),
                new Claim("username", modal.Username),
                new Claim("role", modal.RoleName),
                new Claim("maxa", expires.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddSeconds(expires),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
                return principal;
            }
            catch (SecurityTokenException)
            {
                // Xác thực không thành công, trả về null hoặc xử lý lỗi khác tùy vào yêu cầu của bạn
                return null;
            }
        }
    }
}