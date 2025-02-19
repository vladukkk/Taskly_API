using BusinessLogic.Contracts;
using BusinessLogic.DTOs.Priority;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityService _service;

        public PrioritiesController(IPriorityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var request = await _service.GetPriorities();
            return Ok(request);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPriority(Guid id)
        {
            var priority = await _service.GetById(id);
            return Ok(priority);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPriority([FromBody] PriorityAddDTO priority)
        {
            if (priority == null)
                return BadRequest("can't be null");

            await _service.AddPriority(priority);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePriority([FromBody] PriorityUpdateDTO priority)
        {
            if (priority == null)
                return BadRequest("can't be null");

            await _service.UpdatePriority(priority);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriority(Guid id)
        {
            await _service.DeletePriority(id);
            return NoContent();
        }
    }
}
