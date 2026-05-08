using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Income;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Incomes.GetIncomeById
{
    public class GetIncomeByIdQueryRequest : IRequest<SuccessDetails<IncomeDto>>
    {
        public int Id { get; set; }
    }
}
