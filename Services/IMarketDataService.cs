using System.Collections.Generic;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;

namespace InvestmentPortfolioAPI.Services
{
    /// <summary>
    /// Defines methods for retrieving market data such as top gainers and top losers.
    /// </summary>
    public interface IMarketDataService
    {
        /// <summary>
        /// Retrieves a list of stocks with the highest gains in the market.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a list of <see cref="StockInfo"/> representing the top gainers in the market.
        /// </returns>
        Task<List<StockInfo>> GetTopGainersAsync();

        /// <summary>
        /// Retrieves a list of stocks with the highest losses in the market.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a list of <see cref="StockInfo"/> representing the top losers in the market.
        /// </returns>
        Task<List<StockInfo>> GetTopLosersAsync();
    }
}
