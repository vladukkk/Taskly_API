
using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.DTOs.User.Auth;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(UserManager<UserEntity> userManager
            , SignInManager<UserEntity> signInManager
            , IMapper mapper
            , IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = configuration;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDTO model)
        {
            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return IdentityResult.Failed(new IdentityError { Description = "User with such Name is already Registered" });

            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return IdentityResult.Failed(new IdentityError { Description = "User with such Email is already Registered" });

            var user = _mapper.Map<UserEntity>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return SignInResult.Failed;

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> GenerateJwtToken(string email, string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Унікальний ID токена
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserEntity?> GetUserByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
    }
}
