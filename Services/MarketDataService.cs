using System.Collections.Generic;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace InvestmentPortfolioAPI.Services
{
    /// <summary>
    /// Service responsible for retrieving market data, including top gainers and top losers, from the AlphaVantage API.
    /// </summary>
    public class MarketDataService : IMarketDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketDataService"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration for accessing API keys and other settings.</param>
        public MarketDataService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["AlphaVantage:ApiKey"];
        }

        /// <summary>
        /// Retrieves a list of top gaining stocks from the AlphaVantage API.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a list of <see cref="StockInfo"/> representing the top gainers in the market.
        /// </returns>
        public async Task<List<StockInfo>> GetTopGainersAsync()
        {
            var url = $"https://www.alphavantage.co/query?function=TOP_GAINERS_LOSERS&apikey={_apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            var data = JObject.Parse(response);

            var gainers = data["top_gainers"] as JArray;
            if (gainers == null)
                return new List<StockInfo>();

            var stocks = new List<StockInfo>();

            foreach (var item in gainers)
            {
                stocks.Add(new StockInfo
                {
                    Symbol = item["ticker"]?.ToString(),
                    Price = item["price"]?.ToString(),
                    Change = item["change_amount"]?.ToString(),
                    ChangePercent = item["change_percentage"]?.ToString(),
                    Volume = item["volume"]?.ToString()
                });
            }

            return stocks;
        }

        /// <summary>
        /// Retrieves a list of top losing stocks from the AlphaVantage API.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a list of <see cref="StockInfo"/> representing the top losers in the market.
        /// </returns>
        public async Task<List<StockInfo>> GetTopLosersAsync()
        {
            var url = $"https://www.alphavantage.co/query?function=TOP_GAINERS_LOSERS&apikey={_apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            var data = JObject.Parse(response);

            var losers = data["top_losers"] as JArray;
            if (losers == null)
                return new List<StockInfo>();

            var stocks = new List<StockInfo>();

            foreach (var item in losers)
            {
                stocks.Add(new StockInfo
                {
                    Symbol = item["ticker"]?.ToString(),
                    Price = item["price"]?.ToString(),
                    Change = item["change_amount"]?.ToString(),
                    ChangePercent = item["change_percentage"]?.ToString(),
                    Volume = item["volume"]?.ToString()
                });
            }

            return stocks;
        }
    }
}
