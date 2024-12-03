using System.Collections.Generic;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;

namespace InvestmentPortfolioAPI.Services;

public interface IHistoricalDataService
{
    Task<List<HistoricalDataPoint>> GetHistoricalDataAsync(string symbol);
}
