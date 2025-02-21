
using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.DTOs.User;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsersService(UserManager<UserEntity> userManager
            , RoleManager<IdentityRole> roleManager
            , IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<List<RoleDTO>?> GetRoles()
        {
            var roles = _roleManager.Roles;
            return _mapper.Map<List<RoleDTO>>(await roles.ToListAsync());
        }

        public async Task<List<UserEntity>?> GetUsers()
        {
            var users = _userManager.Users;
            return await users.ToListAsync();
        }

        public async Task<List<UserRolesDTO>?> GetUserRoles()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRoles = new List<UserRolesDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles.Add(new UserRolesDTO
                {
                    UserName = user.UserName,
                    UserRoles = roles.ToList()
                });
            }

            return userRoles;
        }

        public async Task CreateRole(string roleName)
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task<IdentityResult> AddRoleToUser(string roleName, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return IdentityResult.Failed(new IdentityError { Description = $"user with id: {userId} doesn't exist" });

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
                return IdentityResult.Failed(new IdentityError { Description = $"role {roleName} doesn't exist" });

            return await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
