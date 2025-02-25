using BusinessLogic.Contracts;
using BusinessLogic.DTOs.Quotes;
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

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddQuote([FromBody]QuoteAddDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _quotesService.AddQuote(model);
            return Ok();
        }

        [HttpDelete("delete{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteQuote(Guid id)
        {
            await _quotesService.DeleteQuote(id);
            return NoContent();
        }
    }
}
