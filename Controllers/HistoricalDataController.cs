using System;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InvestmentPortfolioAPI.Controllers
{
    /// <summary>
    /// Controller for handling historical data requests for investment symbols.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HistoricalDataController : ControllerBase
    {
        private readonly IHistoricalDataService _historicalDataService;
        private readonly ILogger<HistoricalDataController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalDataController"/> class.
        /// </summary>
        /// <param name="historicalDataService">Service to fetch historical data.</param>
        /// <param name="logger">Logger instance.</param>
        public HistoricalDataController(IHistoricalDataService historicalDataService, ILogger<HistoricalDataController> logger)
        {
            _historicalDataService = historicalDataService ?? throw new ArgumentNullException(nameof(historicalDataService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves historical data for the specified investment symbol.
        /// </summary>
        /// <param name="symbol">The investment symbol (e.g., AAPL, MSFT).</param>
        /// <returns>A list of historical data points for the given symbol.</returns>
        /// <response code="200">Returns the historical data.</response>
        /// <response code="400">If the symbol is null or empty.</response>
        /// <response code="404">If no data is found for the specified symbol.</response>
        /// <response code="500">If an unexpected error occurs while fetching data.</response>
        [HttpGet("{symbol}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHistoricalData(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                _logger.LogWarning("GetHistoricalData called with null or empty symbol.");
                return BadRequest("Symbol is required.");
            }

            try
            {
                var data = await _historicalDataService.GetHistoricalDataAsync(symbol);

                if (data == null || !data.Any())
                {
                    _logger.LogInformation($"No historical data found for symbol: {symbol}");
                    return NotFound($"No historical data found for symbol: {symbol}");
                }

                return Ok(data);
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Invalid argument provided for symbol: {Symbol}", symbol);
                return BadRequest(argEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching historical data for symbol: {Symbol}", symbol);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while fetching historical data.");
            }
        }
    }
}
