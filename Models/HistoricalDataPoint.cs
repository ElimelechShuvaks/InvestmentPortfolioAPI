using System;

namespace InvestmentPortfolioAPI.Models
{
    /// <summary>
    /// Represents a single data point in the historical stock data.
    /// </summary>
    public class HistoricalDataPoint
    {
        /// <summary>
        /// Gets or sets the timestamp indicating when the data point was recorded.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the opening price of the stock at the start of the trading period.
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// Gets or sets the highest price of the stock during the trading period.
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// Gets or sets the lowest price of the stock during the trading period.
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// Gets or sets the closing price of the stock at the end of the trading period.
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// Gets or sets the total volume of shares traded during the trading period.
        /// </summary>
        public decimal Volume { get; set; }
    }
}
