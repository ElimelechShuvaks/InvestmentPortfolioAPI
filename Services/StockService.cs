using System.Net.Http;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace InvestmentPortfolioAPI.Services;

public class StockService : IStockService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public StockService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _apiKey = configuration["AlphaVantage:ApiKey"];
    }

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
