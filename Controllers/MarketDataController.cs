using System;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketDataController : ControllerBase
    {
        private readonly IMarketDataService _marketDataService;

        public MarketDataController(IMarketDataService marketDataService)
        {
            _marketDataService = marketDataService;
        }

        [HttpGet("topgainers")]
        public async Task<IActionResult> GetTopGainers()
        {
            try
            {
                var data = await _marketDataService.GetTopGainersAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching top gainers: {ex.Message}");
            }
        }

        [HttpGet("toplosers")]
        public async Task<IActionResult> GetTopLosers()
        {
            try
            {
                var data = await _marketDataService.GetTopLosersAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching top losers: {ex.Message}");
            }
        }
    }
}
