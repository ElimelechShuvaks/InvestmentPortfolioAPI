using System;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsArticles()
        {
            try
            {
                var articles = await _newsService.GetNewsArticlesAsync();
                return Ok(articles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching news articles: {ex.Message}");
            }
        }
    }
}
