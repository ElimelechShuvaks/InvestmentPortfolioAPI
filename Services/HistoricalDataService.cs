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
    public class HistoricalDataService : IHistoricalDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public HistoricalDataService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["AlphaVantage:ApiKey"];
        }

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
