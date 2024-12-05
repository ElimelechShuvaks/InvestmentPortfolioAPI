using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioAPI.Models
{
    /// <summary>
    /// Represents an item in the investment portfolio.
    /// </summary>
    public class PortfolioItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the portfolio item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the stock.
        /// </summary>
        [Required(ErrorMessage = "Stock name is required.")]
        [StringLength(100, ErrorMessage = "Stock name cannot exceed 100 characters.")]
        public string StockName { get; set; }    // שם המניה

        /// <summary>
        /// Gets or sets the stock symbol (e.g., AAPL, MSFT).
        /// </summary>
        [Required(ErrorMessage = "Stock symbol is required.")]
        [StringLength(10, ErrorMessage = "Stock symbol cannot exceed 10 characters.")]
        public string Symbol { get; set; }       // סימול המניה

        /// <summary>
        /// Gets or sets the quantity of shares held.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }        // כמות

        /// <summary>
        /// Gets or sets the purchase price per share.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Purchase price must be a positive value.")]
        public decimal PurchasePrice { get; set; } // מחיר רכישה

        /// <summary>
        /// Gets or sets the current price per share.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Current price must be a positive value.")]
        public decimal CurrentPrice { get; set; }  // מחיר נוכחי

        /// <summary>
        /// Gets or sets the total value of the portfolio item.
        /// </summary>
        [Range(0.00, double.MaxValue, ErrorMessage = "Total value cannot be negative.")]
        public decimal TotalValue { get; set; }    // ערך כולל
    }
}
