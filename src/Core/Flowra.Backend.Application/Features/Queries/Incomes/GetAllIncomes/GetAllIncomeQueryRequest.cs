using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Income;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Incomes.GetAllIncomes
{
    public class GetAllIncomesQueryRequest : IRequest<SuccessDetails<IEnumerable<IncomeDto>>>
    {
    }
}
