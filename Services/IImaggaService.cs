using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;

namespace InvestmentPortfolioAPI.Services;

public interface IImaggaService
{
    Task<ImageAnalysisResult> AnalyzeImageAsync(string imageUrl);
}
