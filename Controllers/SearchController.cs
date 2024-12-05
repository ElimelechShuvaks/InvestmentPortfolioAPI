using System;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InvestmentPortfolioAPI.Controllers
{
    /// <summary>
    /// Controller for handling search-related requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ILogger<SearchController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController"/> class.
        /// </summary>
        /// <param name="searchService">Service to perform search operations.</param>
        /// <param name="logger">Logger instance for logging information and errors.</param>
        public SearchController(ISearchService searchService, ILogger<SearchController> logger)
        {
            _searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Searches for companies based on the provided keyword.
        /// </summary>
        /// <param name="company">The company name or keyword to search for.</param>
        /// <returns>A list of companies matching the search criteria.</returns>
        /// <response code="200">Returns the list of matching companies.</response>
        /// <response code="400">If the search keyword is null or empty.</response>
        /// <response code="404">If no matching companies are found.</response>
        /// <response code="503">If the search service is unavailable.</response>
        /// <response code="500">If an unexpected error occurs while performing the search.</response>
        [HttpGet("{company}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchCompany(string company)
        {
            _logger.LogInformation("SearchCompany called with keyword: {Company}", company);

            if (string.IsNullOrWhiteSpace(company))
            {
                _logger.LogWarning("SearchCompany called with null or empty keyword.");
                return BadRequest("Keywords are required.");
            }

            try
            {
                var results = await _searchService.SearchCompanyAsync(company);

                if (results == null || results.Count == 0)
                {
                    _logger.LogInformation("No companies found matching the keyword: {Company}", company);
                    return NotFound($"No companies found matching the keyword: {company}");
                }

                _logger.LogInformation("SearchCompany successfully retrieved {Count} companies.", results.Count);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while searching for companies with keyword: {Company}", company);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while searching for companies.");
            }
        }
    }
}
