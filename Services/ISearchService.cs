using System.Collections.Generic;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;

namespace InvestmentPortfolioAPI.Services
{
    public interface ISearchService
    {
        Task<List<CompanySearchResult>> SearchCompanyAsync(string keywords);
    }
}
