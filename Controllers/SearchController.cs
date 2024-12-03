using System;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("{company}")]
    public async Task<IActionResult> SearchCompany(string company)
    {
        if (string.IsNullOrWhiteSpace(company))
            return BadRequest("Keywords are required.");

        try
        {
            var results = await _searchService.SearchCompanyAsync(company);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error searching for company: {ex.Message}");
        }
    }
}
