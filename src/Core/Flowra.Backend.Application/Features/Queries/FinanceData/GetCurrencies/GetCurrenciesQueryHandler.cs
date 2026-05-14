using Flowra.Backend.Application.Common.FinanceData;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.FinanceData;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Flowra.Backend.Application.Features.Queries.FinanceData.GetCurrencies
{
    public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQueryRequest, SuccessDetails<IEnumerable<LivePriceDto>>>
    {
        private readonly HttpClient _httpClient;

        public GetCurrenciesQueryHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("CollectApi");

            var apiKey = configuration["CollectApi:ApiKey"] ?? throw new ArgumentNullException("CollectApi Key eksik!");
            if (!_httpClient.DefaultRequestHeaders.Contains("authorization"))
            {
                _httpClient.DefaultRequestHeaders.Add("authorization", apiKey);
            }
        }

        public async Task<SuccessDetails<IEnumerable<LivePriceDto>>> Handle(GetCurrenciesQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<CollectApiResponse<List<CurrencyItem>>>("economy/allCurrency", cancellationToken);

            var dtos = response?.Result.Select(x => new LivePriceDto
            {
                Name = x.Name,
                Price = x.Selling
            }) ?? Enumerable.Empty<LivePriceDto>();

            return ResultResponse.Success(dtos, "Döviz kurları başarıyla getirildi.");
        }
    }
}
