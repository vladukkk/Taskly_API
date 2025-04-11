using BusinessLogic.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Users()
        {
            var users = await _usersService.GetUsers();

            return Ok(users);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> Roles()
        {
            var roles = await _usersService.GetRoles();
            return Ok(roles);
        }

        [HttpGet("user-roles")]
        public async Task<IActionResult> UserRoles()
        {
            var userRoles = await _usersService.GetUserRoles();
            return Ok(userRoles);
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            await _usersService.CreateRole(roleName);
            return Ok(new { Message = "Role has created successfully" });
        }

        [HttpPut("add-role-to-user{userId}")]
        public async Task<IActionResult> AddRoleToUser(string userId, string roleName)
        {
            var result = await _usersService.AddRoleToUser(roleName: roleName, userId: userId);
            if (result.Succeeded)
                return Ok(new { Message = "role has added successfully" });

            return BadRequest(result.Errors);
        }
    }
}
