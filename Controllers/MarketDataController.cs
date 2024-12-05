using System;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InvestmentPortfolioAPI.Controllers
{
    /// <summary>
    /// Controller for handling market data requests such as top gainers and top losers.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MarketDataController : ControllerBase
    {
        private readonly IMarketDataService _marketDataService;
        private readonly ILogger<MarketDataController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketDataController"/> class.
        /// </summary>
        /// <param name="marketDataService">Service to fetch market data.</param>
        /// <param name="logger">Logger instance.</param>
        public MarketDataController(IMarketDataService marketDataService, ILogger<MarketDataController> logger)
        {
            _marketDataService = marketDataService ?? throw new ArgumentNullException(nameof(marketDataService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves the list of top gaining stocks.
        /// </summary>
        /// <returns>A list of top gainers in the market.</returns>
        /// <response code="200">Returns the list of top gainers.</response>
        /// <response code="500">If an unexpected error occurs while fetching data.</response>
        [HttpGet("topgainers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTopGainers()
        {
            _logger.LogInformation("GetTopGainers called.");

            try
            {
                var data = await _marketDataService.GetTopGainersAsync();

                if (data == null || data.Count == 0)
                {
                    _logger.LogWarning("No top gainers data found.");
                    return NotFound("No top gainers data found.");
                }

                _logger.LogInformation("Top gainers data retrieved successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching top gainers.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while fetching top gainers.");
            }
        }

        /// <summary>
        /// Retrieves the list of top losing stocks.
        /// </summary>
        /// <returns>A list of top losers in the market.</returns>
        /// <response code="200">Returns the list of top losers.</response>
        /// <response code="500">If an unexpected error occurs while fetching data.</response>
        [HttpGet("toplosers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTopLosers()
        {
            _logger.LogInformation("GetTopLosers called.");

            try
            {
                var data = await _marketDataService.GetTopLosersAsync();

                if (data == null || data.Count == 0)
                {
                    _logger.LogWarning("No top losers data found.");
                    return NotFound("No top losers data found.");
                }

                _logger.LogInformation("Top losers data retrieved successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching top losers.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while fetching top losers.");
            }
        }
    }
}
