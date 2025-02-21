using BusinessLogic.Contracts;
using BusinessLogic.DTOs.User.Auth;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO model)
        {
            var result = await _authService.RegisterAsync(model);
            if (result.Succeeded)
                return Ok(new { Message = "User registered successfully" });

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO model)
        {
            var result = await _authService.LoginAsync(model);
            if(result.Succeeded)
                return Ok(new {Message = "Login successfully"});

            if (result.IsNotAllowed)
                return BadRequest(new { Message = "Email is not confirmed" });

            return Unauthorized(new { Message = "Invalid credentials" });

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok(new {Message = "Logout successfully"});
        }
    }
}
