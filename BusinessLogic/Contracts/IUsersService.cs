using BusinessLogic.DTOs.User;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Contracts
{
    public interface IUsersService
    {
        Task<IdentityResult> AddRoleToUser(string roleName, string userId);
        Task CreateRole(string roleName);
        Task<List<RoleDTO>?> GetRoles();
        Task<List<UserRolesDTO>?> GetUserRoles();
        Task<List<UserEntity>?> GetUsers();
    }
}