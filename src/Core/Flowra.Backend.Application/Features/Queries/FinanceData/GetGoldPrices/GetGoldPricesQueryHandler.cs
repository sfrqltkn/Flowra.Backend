using Flowra.Backend.Application.Common.FinanceData;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.FinanceData;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Flowra.Backend.Application.Features.Queries.FinanceData.GetGoldPrices
{
    public class GetGoldPricesQueryHandler : IRequestHandler<GetGoldPricesQueryRequest, SuccessDetails<IEnumerable<LivePriceDto>>>
    {
        private readonly HttpClient _httpClient;

        public GetGoldPricesQueryHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            // "CollectApi" ismini Program.cs'de tanımlayacağız
            _httpClient = httpClientFactory.CreateClient("CollectApi");

            var apiKey = configuration["CollectApi:ApiKey"] ?? throw new ArgumentNullException("CollectApi Key eksik!");
            if (!_httpClient.DefaultRequestHeaders.Contains("authorization"))
            {
                _httpClient.DefaultRequestHeaders.Add("authorization", apiKey);
            }
        }

        public async Task<SuccessDetails<IEnumerable<LivePriceDto>>> Handle(GetGoldPricesQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<CollectApiResponse<List<GoldResult>>>("economy/goldPrice", cancellationToken);

            var dtos = response?.Result.Select(x => new LivePriceDto { Name = x.Name, Price = x.Selling }) ?? Enumerable.Empty<LivePriceDto>();

            return ResultResponse.Success(dtos, "Altın fiyatları başarıyla getirildi.");
        }
    }
}
