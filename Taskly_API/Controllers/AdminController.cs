using BusinessLogic.Contracts;
using BusinessLogic.DTOs.Priority;
using BusinessLogic.DTOs.Quotes;
using BusinessLogic.DTOs.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IPriorityService _priorityService;
        private readonly IQuotesService _quotesService;
        private readonly ITaskService _taskService;

        public AdminController(ITagService tagService
            , IPriorityService priorityService
            , IQuotesService quotesService
            , ITaskService taskService)
        {
            _tagService = tagService;
            _priorityService = priorityService;
            _quotesService = quotesService;
            _taskService = taskService;
        }

        [HttpGet("tags")]
        public async Task<IActionResult> Tags()
        {
            var tags = await _tagService.GetTags();
            return Ok(tags);
        }

        [HttpPost("tag-add-global")]
        public async Task<IActionResult> AddTag([FromBody] TagAddDTO tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _tagService.AddGlobalTag(tag);
            return Ok();
        }

        [HttpPut("tag-update")]
        public async Task<IActionResult> UpdateTag([FromBody] TagUpdateDTO tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _tagService.UpdateTag(tag);
            return Ok();
        }

        [HttpDelete("tag{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            await _tagService.DeleteTag(id);
            return NoContent();
        }

        [HttpGet("priorities")]
        public async Task<IActionResult> Priorities()
        {
            var priorities = await _priorityService.GetPriorities();
            return Ok(priorities);
        }

        [HttpPost("priority-add")]
        public async Task<IActionResult> AddPriority([FromBody] PriorityAddDTO priority)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _priorityService.AddPriority(priority);
            return Ok();
        }

        [HttpPut("priority-update")]
        public async Task<IActionResult> UpdatePriority([FromBody] PriorityUpdateDTO priority)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _priorityService.UpdatePriority(priority);
            return Ok();
        }

        [HttpDelete("priority{id}")]
        public async Task<IActionResult> DeletePriority(Guid id)
        {
            await _priorityService.DeletePriority(id);
            return NoContent();
        }

        [HttpGet("quotes")]
        public async Task<IActionResult> Quotes()
        {
            var quotes = await _quotesService.GetQuotes();
            return Ok(quotes);
        }

        [HttpPost("quote-add")]
        public async Task<IActionResult> AddQuote([FromBody] QuoteAddDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _quotesService.AddQuote(model);
            return Ok();
        }

        [HttpDelete("quote{id}")]
        public async Task<IActionResult> DeleteQuote(Guid id)
        {
            await _quotesService.DeleteQuote(id);
            return NoContent();
        }

        [HttpGet("tasks")]
        public async Task<IActionResult> Tasks()
        {
            var request = await _taskService.GetTasks();
            return Ok(request);
        }
    }
}
