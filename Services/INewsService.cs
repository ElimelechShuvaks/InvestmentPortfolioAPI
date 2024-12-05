using System.Collections.Generic;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;

namespace InvestmentPortfolioAPI.Services
{
    /// <summary>
    /// Defines methods for retrieving news articles related to the market or stocks.
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// Retrieves a list of news articles.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a list of <see cref="NewsArticle"/> representing the retrieved news articles.
        /// </returns>
        Task<List<NewsArticle>> GetNewsArticlesAsync();
    }
}
