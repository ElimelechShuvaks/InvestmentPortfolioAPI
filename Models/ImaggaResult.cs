
namespace InvestmentPortfolioAPI.Models
{
    /// <summary>
    /// Contains the result section of the Imagga API response.
    /// </summary>
    public class ImaggaResult
    {
        /// <summary>
        /// List of tags associated with the image.
        /// </summary>
        public List<ImaggaTag> Tags { get; set; }
    }
}
