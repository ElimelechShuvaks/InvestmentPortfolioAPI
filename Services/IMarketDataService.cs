using System.Collections.Generic;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;

namespace InvestmentPortfolioAPI.Services
{
    public interface IMarketDataService
    {
        Task<List<StockInfo>> GetTopGainersAsync();
        Task<List<StockInfo>> GetTopLosersAsync();
    }
}
