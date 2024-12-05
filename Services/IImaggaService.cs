using System.Threading.Tasks;

namespace InvestmentPortfolioAPI.Services
{
    /// <summary>
    /// Defines methods for interacting with the Imagga image recognition API.
    /// </summary>
    public interface IImaggaService
    {
        /// <summary>
        /// Retrieves the highest confidence level for tags related to symbols in the given image.
        /// </summary>
        /// <param name="imageUrl">The URL of the image to analyze.</param>
        /// <returns>
        /// The confidence level of the symbol tag if found; otherwise, null.
        /// </returns>
        Task<double?> GetSymbolConfidenceAsync(string imageUrl);
    }
}
