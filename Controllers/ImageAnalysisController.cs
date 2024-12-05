using System.Threading.Tasks;
using InvestmentPortfolioAPI.Services;
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Services;

namespace YourNamespace.Controllers
{
    /// <summary>
    /// API controller for image analysis operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ImageAnalysisController : ControllerBase
    {
        private readonly IImaggaService _imaggaService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAnalysisController"/> class.
        /// </summary>
        /// <param name="imaggaService">The Imagga service for image analysis.</param>
        public ImageAnalysisController(IImaggaService imaggaService)
        {
            _imaggaService = imaggaService;
        }

        /// <summary>
        /// Analyzes an image from the provided URL and determines if it contains a symbol.
        /// </summary>
        /// <param name="imageUrl">The URL of the image to analyze.</param>
        /// <returns>
        /// An action result containing the detection result and confidence level.
        /// </returns>
        [HttpGet("analyze")]
        public async Task<IActionResult> AnalyzeImage([FromQuery] string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest("Please provide a valid image URL.");
            }

            var confidence = await _imaggaService.GetSymbolConfidenceAsync(imageUrl);

            if (confidence == null)
            {
                return StatusCode(500, "Error analyzing the image.");
            }

            return Ok(new { IsSymbolDetected = confidence > 0, Confidence = confidence });
        }
    }
}
