using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace InvestmentPortfolioAPI.Services
{
    public class ImaggaService : IImaggaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiSecret;

        public ImaggaService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["Imagga:ApiKey"];
            _apiSecret = configuration["Imagga:ApiSecret"];

            var byteArray = Encoding.ASCII.GetBytes($"{_apiKey}:{_apiSecret}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<ImageAnalysisResult> AnalyzeImageAsync(string imageUrl)
        {
            var url = $"https://api.imagga.com/v2/tags?image_url={Uri.EscapeDataString(imageUrl)}";
            var response = await _httpClient.GetStringAsync(url);
            var data = JObject.Parse(response)["result"]["tags"];

            if (data == null)
                return null;

            // לדוגמה: מחזיר את התגית הראשונה עם הביטחון הגבוה ביותר
            var topTag = data[0];
            return new ImageAnalysisResult
            {
                Symbol = MapTagToSymbol(topTag["tag"]["en"]?.ToString()),
                StockName = MapSymbolToStockName(MapTagToSymbol(topTag["tag"]["en"]?.ToString())),
                Confidence = (double)topTag["confidence"]
            };
        }

        private string MapTagToSymbol(string tag)
        {
            // מפה ידנית בין תגיות למניית סימול
            // לדוגמה:
            var mapping = new Dictionary<string, string>
            {
                { "apple", "AAPL" },
                { "google", "GOOGL" },
                // הוסף לפי הצורך
            };

            return mapping.ContainsKey(tag.ToLower()) ? mapping[tag.ToLower()] : null;
        }

        private string MapSymbolToStockName(string symbol)
        {
            // מפה ידנית בין סימול למידע על מניה
            var mapping = new Dictionary<string, string>
            {
                { "AAPL", "Apple Inc." },
                { "GOOGL", "Alphabet Inc." },
                // הוסף לפי הצורך
            };

            return mapping.ContainsKey(symbol) ? mapping[symbol] : null;
        }
    }
}
