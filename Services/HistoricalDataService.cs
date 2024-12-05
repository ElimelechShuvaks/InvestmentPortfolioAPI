using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace InvestmentPortfolioAPI.Services
{
    /// <summary>
    /// Service responsible for fetching historical stock data from the AlphaVantage API.
    /// </summary>
    public class HistoricalDataService : IHistoricalDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalDataService"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration for accessing API keys and other settings.</param>
        public HistoricalDataService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["AlphaVantage:ApiKey"];
        }

        /// <summary>
        /// Retrieves historical stock data for the specified symbol.
        /// </summary>
        /// <param name="symbol">The stock symbol (e.g., AAPL, MSFT).</param>
        /// <returns>A list of historical data points ordered by timestamp in descending order.</returns>
        public async Task<List<HistoricalDataPoint>> GetHistoricalDataAsync(string symbol)
        {
            var url = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            var data = JObject.Parse(response);

            var historicalData = new List<HistoricalDataPoint>();

            // בדיקת קיום מידע רלוונטי
            if (data["Time Series (Daily)"] is JObject timeSeries)
            {
                foreach (var entry in timeSeries)
                {
                    var date = DateTime.Parse(entry.Key);
                    var values = entry.Value as JObject;

                    if (values != null)
                    {
                        historicalData.Add(new HistoricalDataPoint
                        {
                            Timestamp = date,
                            Open = decimal.Parse(values["1. open"].ToString()),
                            High = decimal.Parse(values["2. high"].ToString()),
                            Low = decimal.Parse(values["3. low"].ToString()),
                            Close = decimal.Parse(values["4. close"].ToString()),
                            Volume = decimal.Parse(values["5. volume"].ToString())
                        });
                    }
                }
            }

            return historicalData.OrderByDescending(d => d.Timestamp).ToList();
        }
    }
}
