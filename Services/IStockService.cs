using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;

namespace InvestmentPortfolioAPI.Services
{
    /// <summary>
    /// Defines methods for retrieving stock information.
    /// </summary>
    public interface IStockService
    {
        /// <summary>
        /// Retrieves detailed stock information for the specified symbol.
        /// </summary>
        /// <param name="symbol">The stock symbol (e.g., AAPL, MSFT) to retrieve information for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a <see cref="StockInfo"/> object with the stock's details.
        /// </returns>
        Task<StockInfo> GetStockInfoAsync(string symbol);
    }
}
