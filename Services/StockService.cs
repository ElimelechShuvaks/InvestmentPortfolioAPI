using System.Net.Http;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace InvestmentPortfolioAPI.Services
{
    /// <summary>
    /// Service responsible for retrieving detailed stock information from the AlphaVantage API.
    /// </summary>
    public class StockService : IStockService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockService"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration for accessing API keys and other settings.</param>
        public StockService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["AlphaVantage:ApiKey"];
        }

        /// <summary>
        /// Retrieves detailed stock information for the specified symbol.
        /// </summary>
        /// <param name="symbol">The stock symbol (e.g., AAPL, MSFT) to retrieve information for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a <see cref="StockInfo"/> object with the stock's details.
        /// Returns <c>null</c> if no data is found for the provided symbol.
        /// </returns>
        public async Task<StockInfo> GetStockInfoAsync(string symbol)
        {
            var url = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            var data = JObject.Parse(response)["Global Quote"];

            if (data == null)
                return null;

            return new StockInfo
            {
                Symbol = data["01. symbol"]?.ToString(),
                Open = data["02. open"]?.ToString(),
                High = data["03. high"]?.ToString(),
                Low = data["04. low"]?.ToString(),
                Price = data["05. price"]?.ToString(),
                Volume = data["06. volume"]?.ToString(),
                LatestTradingDay = data["07. latest trading day"]?.ToString(),
                PreviousClose = data["08. previous close"]?.ToString(),
                Change = data["09. change"]?.ToString(),
                ChangePercent = data["10. change percent"]?.ToString()
            };
        }
    }
}
