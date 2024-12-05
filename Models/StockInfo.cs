namespace InvestmentPortfolioAPI.Models
{
    /// <summary>
    /// Represents detailed information about a specific stock.
    /// </summary>
    public class StockInfo
    {
        /// <summary>
        /// Gets or sets the stock symbol (e.g., AAPL, MSFT).
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the opening price of the stock at the start of the trading day.
        /// </summary>
        public string Open { get; set; }

        /// <summary>
        /// Gets or sets the highest price of the stock during the trading day.
        /// </summary>
        public string High { get; set; }

        /// <summary>
        /// Gets or sets the lowest price of the stock during the trading day.
        /// </summary>
        public string Low { get; set; }

        /// <summary>
        /// Gets or sets the current price of the stock.
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Gets or sets the total volume of shares traded during the trading day.
        /// </summary>
        public string Volume { get; set; }

        /// <summary>
        /// Gets or sets the date of the latest trading day.
        /// </summary>
        public string LatestTradingDay { get; set; }

        /// <summary>
        /// Gets or sets the closing price of the stock on the previous trading day.
        /// </summary>
        public string PreviousClose { get; set; }

        /// <summary>
        /// Gets or sets the absolute change in stock price since the previous trading day.
        /// </summary>
        public string Change { get; set; }

        /// <summary>
        /// Gets or sets the percentage change in stock price since the previous trading day.
        /// </summary>
        public string ChangePercent { get; set; }
    }
}
