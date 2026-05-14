using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.FinanceData;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.FinanceData.GetSilverPrice
{
    public class GetSilverPriceQueryRequest : IRequest<SuccessDetails<LivePriceDto>>
    {
    }
}
