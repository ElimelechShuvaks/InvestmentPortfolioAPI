using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using InvestmentPortfolioAPI.Models;
using InvestmentPortfolioAPI.Services;

namespace YourNamespace.Services
{
    /// <summary>
    /// Service responsible for communicating with the Imagga API.
    /// </summary>
    public class ImaggaService : IImaggaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiSecret;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImaggaService"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The application configuration for accessing API keys and other settings.
        /// </param>
        public ImaggaService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["Imagga:ApiKey"];
            _apiSecret = configuration["Imagga:ApiSecret"];

            // Set up basic authentication headers
            var byteArray = Encoding.ASCII.GetBytes($"{_apiKey}:{_apiSecret}");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        /// <summary>
        /// Retrieves the highest confidence level for tags related to symbols in the given image.
        /// </summary>
        /// <param name="imageUrl">The URL of the image to analyze.</param>
        /// <returns>
        /// The confidence level of the symbol tag if found; otherwise, null.
        /// </returns>
        public async Task<double?> GetSymbolConfidenceAsync(string imageUrl)
        {
            // Build the request URL
            var url = $"https://api.imagga.com/v2/tags?image_url={Uri.EscapeDataString(imageUrl)}";

            // Send the GET request to Imagga API
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                // Handle error response
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var imaggaResponse = JsonConvert.DeserializeObject<ImaggaResponse>(content);

            // List of keywords to identify a symbol
            var symbolKeywords = new List<string> { "symbol", "icon", "logo", "sign", "emblem" };

            // Find the tag with the highest confidence related to symbols
            var symbolTag = imaggaResponse.Result.Tags
                .Where(tag => symbolKeywords.Contains(tag.Tag["en"].ToLower()))
                .OrderByDescending(tag => tag.Confidence)
                .FirstOrDefault();

            return symbolTag?.Confidence;
        }
    }
}
