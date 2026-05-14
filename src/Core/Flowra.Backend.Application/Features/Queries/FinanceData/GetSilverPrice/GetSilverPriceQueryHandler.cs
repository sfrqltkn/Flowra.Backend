using Flowra.Backend.Application.Common.FinanceData;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.FinanceData;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Flowra.Backend.Application.Features.Queries.FinanceData.GetSilverPrice
{
    public class GetSilverPriceQueryHandler : IRequestHandler<GetSilverPriceQueryRequest, SuccessDetails<LivePriceDto>>
    {
        private readonly HttpClient _httpClient;

        public GetSilverPriceQueryHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("CollectApi");

            var apiKey = configuration["CollectApi:ApiKey"] ?? throw new ArgumentNullException("CollectApi Key eksik!");
            if (!_httpClient.DefaultRequestHeaders.Contains("authorization"))
            {
                _httpClient.DefaultRequestHeaders.Add("authorization", apiKey);
            }
        }

        public async Task<SuccessDetails<LivePriceDto>> Handle(GetSilverPriceQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<CollectApiResponse<SilverResult>>("economy/silverPrice", cancellationToken);

            var dto = new LivePriceDto { Name = "Gümüş", Price = response?.Result.Selling ?? 0 };

            return ResultResponse.Success(dto, "Gümüş fiyatı başarıyla getirildi.");
        }
    }
}
