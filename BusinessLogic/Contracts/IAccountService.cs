using BusinessLogic.DTOs;
using BusinessLogic.DTOs.User;

namespace BusinessLogic.Contracts
{
    public interface IAccountService
    {
        Task<UserDTO?> GetCurrentUser(string userId);
        Task<UserStatsDTO?> GetUserStats(string userId);
    }
}