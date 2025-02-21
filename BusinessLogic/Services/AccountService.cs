using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.DTOs.User;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<UserEntity> userManager
            , IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDTO?> GetCurrentUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
