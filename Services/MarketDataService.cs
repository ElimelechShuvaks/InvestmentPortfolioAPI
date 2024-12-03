using System.Collections.Generic;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;
using Newtonsoft.Json.Linq;

namespace InvestmentPortfolioAPI.Services;

public class MarketDataService : IMarketDataService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public MarketDataService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _apiKey = configuration["AlphaVantage:ApiKey"];
    }

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
