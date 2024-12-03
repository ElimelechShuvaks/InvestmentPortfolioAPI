using InvestmentPortfolioAPI.Models;
using InvestmentPortfolioAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockSearchController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockSearchController(IStockService stockService)
    {
        _stockService = stockService;
    }

    // GET: api/stocksearch/{symbol}
    [HttpGet("{symbol}")]
    public async Task<IActionResult> GetStockInfo(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            return BadRequest("Symbol is required.");

        var stockInfo = await _stockService.GetStockInfoAsync(symbol);
        if (stockInfo == null)
            return NotFound("Stock information not found.");

        return Ok(stockInfo);
    }
}
