using System.Collections.Generic;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;

namespace InvestmentPortfolioAPI.Services
{
    public interface INewsService
    {
        Task<List<NewsArticle>> GetNewsArticlesAsync();
    }
}
