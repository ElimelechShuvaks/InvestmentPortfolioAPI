namespace InvestmentPortfolioAPI.Models
{
    /// <summary>
    /// Represents the result of a company search query.
    /// </summary>
    public class CompanySearchResult
    {
        /// <summary>
        /// Gets or sets the stock symbol of the company (e.g., AAPL, MSFT).
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the full name of the company.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the company (e.g., Equity, ETF).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the region where the company is headquartered.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the local market opening time.
        /// </summary>
        public string MarketOpen { get; set; }

        /// <summary>
        /// Gets or sets the local market closing time.
        /// </summary>
        public string MarketClose { get; set; }

        /// <summary>
        /// Gets or sets the timezone of the market.
        /// </summary>
        public string Timezone { get; set; }

        /// <summary>
        /// Gets or sets the currency in which the company's stock is traded.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the match score indicating the relevance of the search result.
        /// </summary>
        public string MatchScore { get; set; }
    }
}
