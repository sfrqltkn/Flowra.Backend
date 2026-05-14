using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.FinanceData;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.FinanceData.GetCurrencies
{
    public class GetCurrenciesQueryRequest : IRequest<SuccessDetails<IEnumerable<LivePriceDto>>>
    {
    }
}
