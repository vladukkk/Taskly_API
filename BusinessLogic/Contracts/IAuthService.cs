using BusinessLogic.DTOs.User.Auth;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Contracts
{
    public interface IAuthService
    {
        Task<UserEntity?> GetUserByUserName(string userName);
        Task<SignInResult> LoginAsync(LoginDTO model);
        Task LogoutAsync();
        Task<IdentityResult> RegisterAsync(RegisterDTO model);
    }
}