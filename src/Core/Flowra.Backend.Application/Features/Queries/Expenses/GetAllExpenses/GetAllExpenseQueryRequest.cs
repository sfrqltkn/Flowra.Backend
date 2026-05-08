using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Expense;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Expenses.GetAllExpenses
{
    public class GetAllExpensesQueryRequest : IRequest<SuccessDetails<IEnumerable<ExpenseDto>>>
    {
    }
}
