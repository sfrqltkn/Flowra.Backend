using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Allowence;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Allowances.GetAllAllowances
{
    public class GetAllAllowancesQueryRequest : IRequest<SuccessDetails<IEnumerable<AllowanceDto>>>
    {
    }
}
