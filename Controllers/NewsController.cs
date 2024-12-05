using System;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InvestmentPortfolioAPI.Controllers
{
    /// <summary>
    /// Controller for handling news-related requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly ILogger<NewsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsController"/> class.
        /// </summary>
        /// <param name="newsService">Service to fetch news articles.</param>
        /// <param name="logger">Logger instance.</param>
        public NewsController(INewsService newsService, ILogger<NewsController> logger)
        {
            _newsService = newsService ?? throw new ArgumentNullException(nameof(newsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves the latest news articles.
        /// </summary>
        /// <returns>A list of news articles.</returns>
        /// <response code="200">Returns the list of news articles.</response>
        /// <response code="404">If no news articles are found.</response>
        /// <response code="503">If the news service is unavailable.</response>
        /// <response code="500">If an unexpected error occurs while fetching data.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNewsArticles()
        {
            _logger.LogInformation("GetNewsArticles called.");

            try
            {
                var articles = await _newsService.GetNewsArticlesAsync();

                if (articles == null || articles.Count == 0)
                {
                    _logger.LogWarning("No news articles found.");
                    return NotFound("No news articles found.");
                }

                _logger.LogInformation("News articles retrieved successfully.");
                return Ok(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching news articles.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while fetching news articles.");
            }
        }
    }
}
