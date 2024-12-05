namespace InvestmentPortfolioAPI.Models
{
    /// <summary>
    /// Contains status information returned by the Imagga API.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Text description of the status.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Type of status (e.g., "success", "error").
        /// </summary>
        public string Type { get; set; }
    }
}
