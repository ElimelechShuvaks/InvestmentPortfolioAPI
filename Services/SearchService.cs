using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace InvestmentPortfolioAPI.Services
{
    /// <summary>
    /// Service responsible for searching companies using keywords via the AlphaVantage API.
    /// </summary>
    public class SearchService : ISearchService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchService"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration for accessing API keys and other settings.</param>
        public SearchService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["AlphaVantage:ApiKey"];
        }

        /// <summary>
        /// Searches for companies using the specified keywords.
        /// </summary>
        /// <param name="company">The keyword(s) to search for companies (e.g., company name or stock symbol).</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a list of <see cref="CompanySearchResult"/> representing the search results.
        /// </returns>
        public async Task<List<CompanySearchResult>> SearchCompanyAsync(string company)
        {
            var url = $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={company}&apikey={_apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            var data = JObject.Parse(response);

            var results = new List<CompanySearchResult>();

            if (data["bestMatches"] != null)
            {
                foreach (var item in data["bestMatches"])
                {
                    results.Add(new CompanySearchResult
                    {
                        Symbol = item["1. symbol"]?.ToString(),
                        Name = item["2. name"]?.ToString(),
                        Type = item["3. type"]?.ToString(),
                        Region = item["4. region"]?.ToString(),
                        MarketOpen = item["5. marketOpen"]?.ToString(),
                        MarketClose = item["6. marketClose"]?.ToString(),
                        Timezone = item["7. timezone"]?.ToString(),
                        Currency = item["8. currency"]?.ToString(),
                        MatchScore = item["9. matchScore"]?.ToString()
                    });
                }
            }

            return results;
        }
    }
}
