
namespace InvestmentPortfolioAPI.Models
{
    /// <summary>
    /// Represents the full response from the Imagga API.
    /// </summary>
    public class ImaggaResponse
    {
        /// <summary>
        /// Result section containing tags and other information.
        /// </summary>
        public ImaggaResult Result { get; set; }

        /// <summary>
        /// Status information of the API response.
        /// </summary>
        public Status Status { get; set; }
    }
}
