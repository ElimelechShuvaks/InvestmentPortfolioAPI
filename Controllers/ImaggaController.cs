using InvestmentPortfolioAPI.Models;
using InvestmentPortfolioAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImaggaController : ControllerBase
    {
        private readonly IImaggaService _imaggaService;

        public ImaggaController(IImaggaService imaggaService)
        {
            _imaggaService = imaggaService;
        }

        // POST: api/imagga/analyze
        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeImage([FromBody] ImageRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.ImageUrl))
                return BadRequest("Image URL is required.");

            var result = await _imaggaService.AnalyzeImageAsync(request.ImageUrl);
            if (result == null)
                return NotFound("Image analysis failed.");

            return Ok(result);
        }
    }
}
