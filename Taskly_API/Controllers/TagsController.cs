using BusinessLogic.Contracts;
using BusinessLogic.DTOs.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> Tags()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId is null)
                return Unauthorized(new { message = "User not authenticated" });

            var request = await _tagService.GetTags(userId);
            return Ok(request);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TagById(Guid id)
        {
            var tag = await _tagService.GetById(id);
            return Ok(tag);
        }

        [HttpPost("add-personality")]
        public async Task<IActionResult> AddTag([FromBody]TagAddDTO tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized(new { message = "User not authenticated" });

            await _tagService.AddPersonalityTag(tag, userId);
            return Ok();
        }
    }
}
