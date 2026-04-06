using Flowra.Backend.Application.DTOs.FinanceData;
using Flowra.Backend.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Flowra.Backend.Application.Services
{
    public class FinanceDataService : IFinanceDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public FinanceDataService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["CollectApi:ApiKey"] ?? throw new ArgumentNullException("CollectApi Key eksik!");

            // Eğer Program.cs'de eklemediysen burada ekle:
            if (!_httpClient.DefaultRequestHeaders.Contains("authorization"))
            {
                _httpClient.DefaultRequestHeaders.Add("authorization", _apiKey);
            }
        }

        public async Task<IEnumerable<LivePriceDto>> GetGoldPricesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CollectApiResponse<List<GoldResult>>>("economy/goldPrice");
            return response?.Result.Select(x => new LivePriceDto { Name = x.Name, Price = x.Selling }) ?? Enumerable.Empty<LivePriceDto>();
        }

        public async Task<LivePriceDto> GetSilverPriceAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CollectApiResponse<SilverResult>>("economy/silverPrice");
            return new LivePriceDto { Name = "Gümüş", Price = response?.Result.Selling ?? 0 };
        }

        public async Task<IEnumerable<LivePriceDto>> GetCurrencyPricesAsync()
        {
            // CollectAPI'de bu endpoint genellikle direkt dizi döner
            var response = await _httpClient.GetFromJsonAsync<CollectApiResponse<List<CurrencyItem>>>("economy/allCurrency");

            return response?.Result.Select(x => new LivePriceDto
            {
                Name = x.Name,
                Price = x.Selling
            }) ?? Enumerable.Empty<LivePriceDto>();
        }

        private record CollectApiResponse<T>(
             [property: JsonPropertyName("success")] bool Success,
             [property: JsonPropertyName("result")] T Result
      );

        private record GoldResult(
            [property: JsonPropertyName("name")] string Name,
            [property: JsonPropertyName("selling")] decimal Selling
        );

        private record SilverResult(
            [property: JsonPropertyName("selling")] decimal Selling
        );

        private record CurrencyResult(
            [property: JsonPropertyName("result")] List<CurrencyItem> Result
        );

        private record CurrencyItem(
           [property: JsonPropertyName("name")] string Name,
           [property: JsonPropertyName("selling")] decimal Selling
         );
    }
}
