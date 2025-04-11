using BusinessLogic.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
    }
}
