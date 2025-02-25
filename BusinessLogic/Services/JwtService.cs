using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.Services
{
    public class JwtService
    {
        private readonly IOptions<AuthSettings> _options;

        public JwtService(IOptions<AuthSettings> options)
        {
            _options = options;
        }
        public string GeterateToken(string email, string userId)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", email),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var jwtToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.Add(_options.Value.Expires),
                claims: claims,
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecretKey)),
                    SecurityAlgorithms.HmacSha256));

            Console.WriteLine("Key Length: " + Encoding.UTF8.GetBytes(_options.Value.SecretKey).Length);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
