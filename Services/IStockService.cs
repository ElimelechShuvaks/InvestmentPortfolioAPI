using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;

namespace InvestmentPortfolioAPI.Services;

public interface IStockService
{
    Task<StockInfo> GetStockInfoAsync(string symbol);
}
