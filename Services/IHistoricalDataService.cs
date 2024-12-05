using System.Collections.Generic;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;

namespace InvestmentPortfolioAPI.Services
{
    /// <summary>
    /// Defines methods for retrieving historical stock data.
    /// </summary>
    public interface IHistoricalDataService
    {
        /// <summary>
        /// Retrieves historical stock data for the specified symbol.
        /// </summary>
        /// <param name="symbol">The stock symbol to retrieve historical data for (e.g., AAPL, MSFT).</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a list of <see cref="HistoricalDataPoint"/> representing the historical stock data.
        /// </returns>
        Task<List<HistoricalDataPoint>> GetHistoricalDataAsync(string symbol);
    }
}
