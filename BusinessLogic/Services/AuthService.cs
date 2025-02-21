
using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.DTOs.User.Auth;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<UserEntity> userManager
            , SignInManager<UserEntity> signInManager
            , IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
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
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //return IdentityResult.Success;
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return SignInResult.Failed;

            /*if (!user.EmailConfirmed)
                return SignInResult.NotAllowed;*/

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
