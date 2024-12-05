using System.Collections.Generic;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;

namespace InvestmentPortfolioAPI.Services
{
    /// <summary>
    /// Defines methods for searching companies based on specific keywords.
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Searches for companies using the specified keywords.
        /// </summary>
        /// <param name="keywords">The keywords to use for the company search (e.g., company name or stock symbol).</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a list of <see cref="CompanySearchResult"/> representing the search results.
        /// </returns>
        Task<List<CompanySearchResult>> SearchCompanyAsync(string keywords);
    }
}
