using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace InvestmentPortfolioAPI.Services
{
    /// <summary>
    /// Service responsible for retrieving news articles related to the market or stocks from the AlphaVantage API.
    /// </summary>
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsService"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration for accessing API keys and other settings.</param>
        public NewsService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["AlphaVantage:ApiKey"];
        }

        /// <summary>
        /// Retrieves a list of news articles from the AlphaVantage API.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a list of <see cref="NewsArticle"/> representing the retrieved news articles.
        /// </returns>
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
}
