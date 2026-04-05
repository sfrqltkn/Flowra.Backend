using Flowra.Backend.Application.DTOs.AiAdvisor;
using Flowra.Backend.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Flowra.Backend.Application.Services
{
    public class AiAdvisorService : IAiAdvisorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AiAdvisorService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Gemini:ApiKey"] ?? throw new ArgumentNullException("Gemini ApiKey eksik!");
        }

        public async Task<AiStrategyResponseDto> GetOptimizationStrategiesAsync(AiRequestDto request)
        {
            // PROMPT ENGINEERING: 3 Farklı Senaryo İstiyoruz
            var prompt = $@"Sen üst düzey bir Finansal CFO yapay zekasısın. 
                Kullanıcının {request.MonthYear} dönemi verileri:
                - Mevcut Kasa: {request.CurrentCash} ₺
                - Toplam Gelir: {request.TotalIncome} ₺
                - Tüm Borçlar: {request.TotalExpense} ₺
                - Sadece Asgari Ödenirse Borç: {request.MinimumExpense} ₺
                - Varlıklar (Altın/Döviz vb): {request.TotalAssets} ₺

                Görev: Kullanıcıya 3 farklı Kriz Yönetim Senaryosu (A, B, C) sunacaksın. Her senaryo, adım adım nakit akışını (Steps) göstermelidir.

                Kurallar:
                1. Senaryo A (Agresif Koruma): Varlıklara asla dokunma, sadece asgari ödeme yap.
                2. Senaryo B (Tam Likidite): Kredi notunu korumak için (asgari değil tamamını ödemek için) ne kadar varlık varsa boz.
                3. Senaryo C (Optimum Denge): Sadece kasayı 0 ₺'nin üzerine (artıya) atacak kadar spesifik miktarda varlık boz ve asgari ödeme yap. En zekice olan budur.

                KESİNLİKLE SADECE AŞAĞIDAKİ JSON FORMATINDA YANIT VER. Markdown (```json) KULLANMA:
                {{
                  ""summary"": ""Durum özeti ve kriz seviyesi tespiti."",
                  ""scenarios"": [
                    {{
                      ""id"": ""A"",
                      ""title"": ""A Planı: Agresif Koruma"",
                      ""description"": ""Varlıklara dokunulmaz, sadece asgari ödemeler yapılır."",
                      ""finalBalance"": -26786,
                      ""isOptimistic"": false,
                      ""steps"": [
                        {{ ""stepName"": ""Başlangıç Kasası"", ""amount"": -25000, ""runningBalance"": -25000, ""isPositive"": false }},
                        {{ ""stepName"": ""Aylık Gelirler"", ""amount"": 104000, ""runningBalance"": 79000, ""isPositive"": true }},
                        {{ ""stepName"": ""Asgari Borç Çıkışı"", ""amount"": -105786, ""runningBalance"": -26786, ""isPositive"": false }}
                      ]
                    }}
                  ]
                }}";

            var requestBody = new
            {
                contents = new[] { new { parts = new[] { new { text = prompt } } } },
                generationConfig = new
                {
                    temperature = 0.2,
                    responseMimeType = "application/json"
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var cleanApiKey = _apiKey.Trim();

            // 2. YENİ NESİL MODELİ (2.5 Flash) KULLANIYORUZ
            var requestUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent";

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            requestMessage.Headers.Add("x-goog-api-key", cleanApiKey);
            requestMessage.Content = content;

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gemini API Hatası: {error}");
            }

            var responseData = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(responseData);
            var jsonText = document.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text").GetString();

            var result = JsonSerializer.Deserialize<AiStrategyResponseDto>(jsonText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new AiStrategyResponseDto();
        }
    }
}