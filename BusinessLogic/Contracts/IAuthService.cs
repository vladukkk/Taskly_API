using BusinessLogic.DTOs.User.Auth;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Contracts
{
    public interface IAuthService
    {
        Task<SignInResult> LoginAsync(LoginDTO model);
        Task LogoutAsync();
        Task<IdentityResult> RegisterAsync(RegisterDTO model);
    }
}