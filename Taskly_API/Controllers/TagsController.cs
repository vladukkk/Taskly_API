using BusinessLogic.Contracts;
using BusinessLogic.DTOs.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var request = await _tagService.GetTags();
            return Ok(request);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TagById(Guid id)
        {
            var tag = await _tagService.GetById(id);
            return Ok(tag);
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTag([FromBody]TagAddDTO tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _tagService.AddTag(tag);
            return Ok();
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTag([FromBody] TagUpdateDTO tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _tagService.UpdateTag(tag);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            await _tagService.DeleteTag(id);
            return NoContent();
        }
    }
}
