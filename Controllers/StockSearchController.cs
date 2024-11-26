using InvestmentPortfolioAPI.Models;
using InvestmentPortfolioAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockSearchController : ControllerBase
{
    private readonly IStockService _stockService;
    private readonly IImaggaService _imaggaService;

    public StockSearchController(IStockService stockService, IImaggaService imaggaService)
    {
        _stockService = stockService;
        _imaggaService = imaggaService;
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

    // POST: api/stocksearch/image
    [HttpPost("image")]
    public async Task<IActionResult> GetStockInfoFromImage([FromBody] ImageRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.ImageUrl))
            return BadRequest("Image URL is required.");

        var analysisResult = await _imaggaService.AnalyzeImageAsync(request.ImageUrl);
        if (analysisResult == null || string.IsNullOrWhiteSpace(analysisResult.Symbol))
            return NotFound("Could not determine stock from image.");

        var stockInfo = await _stockService.GetStockInfoAsync(analysisResult.Symbol);
        if (stockInfo == null)
            return NotFound("Stock information not found.");

        return Ok(new
        {
            analysisResult.Symbol,
            analysisResult.StockName,
            analysisResult.Confidence,
            stockInfo
        });
    }
}
