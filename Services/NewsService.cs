using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace InvestmentPortfolioAPI.Services;

public class NewsService : INewsService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public NewsService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _apiKey = configuration["AlphaVantage:ApiKey"];
    }

    public async Task<List<NewsArticle>> GetNewsArticlesAsync()
    {
        var url = $"https://www.alphavantage.co/query?function=NEWS_SENTIMENT&apikey={_apiKey}";
        var response = await _httpClient.GetStringAsync(url);
        var data = JObject.Parse(response);

        var articles = new List<NewsArticle>();

        if (data["feed"] != null)
        {
            foreach (var item in data["feed"])
            {
                articles.Add(new NewsArticle
                {
                    Title = item["title"]?.ToString(),
                    Url = item["url"]?.ToString(),
                    Summary = item["summary"]?.ToString(),
                    PublishedAt = DateTime.ParseExact(item["time_published"]?.ToString(), "yyyyMMdd'T'HHmmss", null)
                });
            }
        }

        return articles;
    }
}
