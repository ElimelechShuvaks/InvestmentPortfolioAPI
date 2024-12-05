using System;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;
using InvestmentPortfolioAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InvestmentPortfolioAPI.Controllers
{
    /// <summary>
    /// Controller for searching and retrieving stock information.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StockSearchController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly ILogger<StockSearchController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockSearchController"/> class.
        /// </summary>
        /// <param name="stockService">Service to retrieve stock information.</param>
        /// <param name="logger">Logger instance for logging information and errors.</param>
        public StockSearchController(IStockService stockService, ILogger<StockSearchController> logger)
        {
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves stock information based on the provided symbol.
        /// </summary>
        /// <param name="symbol">The stock symbol (e.g., AAPL, MSFT).</param>
        /// <returns>Stock information for the specified symbol.</returns>
        /// <response code="200">Returns the stock information.</response>
        /// <response code="400">If the stock symbol is null or empty.</response>
        /// <response code="404">If no stock information is found for the specified symbol.</response>
        /// <response code="503">If the stock service is unavailable.</response>
        /// <response code="500">If an unexpected error occurs while retrieving stock information.</response>
        [HttpGet("{symbol}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStockInfo(string symbol)
        {
            _logger.LogInformation("GetStockInfo called with symbol: {Symbol}", symbol);

            if (string.IsNullOrWhiteSpace(symbol))
            {
                _logger.LogWarning("GetStockInfo called with null or empty symbol.");
                return BadRequest("Symbol is required.");
            }

            try
            {
                var stockInfo = await _stockService.GetStockInfoAsync(symbol);

                if (stockInfo == null)
                {
                    _logger.LogInformation("No stock information found for symbol: {Symbol}", symbol);
                    return NotFound($"Stock information not found for symbol: {symbol}");
                }

                _logger.LogInformation("Successfully retrieved stock information for symbol: {Symbol}", symbol);
                return Ok(stockInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving stock information for symbol: {Symbol}", symbol);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while retrieving stock information.");
            }
        }
    }
}
