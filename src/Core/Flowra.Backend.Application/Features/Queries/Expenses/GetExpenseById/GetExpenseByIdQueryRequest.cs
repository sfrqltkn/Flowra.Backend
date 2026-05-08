using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Expense;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Expenses.GetExpenseById
{
    public class GetExpenseByIdQueryRequest : IRequest<SuccessDetails<ExpenseDto>>
    {
        public int Id { get; set; }
    }
}
