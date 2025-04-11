using BusinessLogic.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesService _quotesService;

        public QuotesController(IQuotesService quotesService)
        {
            _quotesService = quotesService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var response = await _quotesService.GetQuotes();
            return Ok(response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _quotesService.GetQuote(id);
            return Ok(response);
        }
    }
}
