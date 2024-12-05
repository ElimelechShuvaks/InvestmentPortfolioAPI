using System;

namespace InvestmentPortfolioAPI.Models
{
    /// <summary>
    /// Represents a news article related to investment portfolios.
    /// </summary>
    public class NewsArticle
    {
        /// <summary>
        /// Gets or sets the title of the news article.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL link to the full news article.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a brief summary or excerpt of the news article.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the publication date and time of the news article.
        /// </summary>
        public DateTime PublishedAt { get; set; }
    }
}
