namespace InvestmentPortfolioAPI.Models
{
    public class ImageAnalysisResult
    {
        public string Symbol { get; set; }
        public string StockName { get; set; }
        public double Confidence { get; set; }
    }
}
