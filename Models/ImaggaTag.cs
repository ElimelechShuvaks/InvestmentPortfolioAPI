namespace InvestmentPortfolioAPI.Models
{
    /// <summary>
    /// Represents a tag returned by the Imagga API.
    /// </summary>
    public class ImaggaTag
    {
        /// <summary>
        /// Confidence level of the tag (percentage).
        /// </summary>
        public double Confidence { get; set; }

        /// <summary>
        /// Dictionary containing tag names in different languages.
        /// </summary>
        public Dictionary<string, string> Tag { get; set; }
    }
}
