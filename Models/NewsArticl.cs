using System;

namespace InvestmentPortfolioAPI.Models;

public class NewsArticle
{
    public string Title { get; set; }
    public string Url { get; set; }
    public string Summary { get; set; }
    public DateTime PublishedAt { get; set; }
}
