using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Expenses.DeleteExpense
{
    public class DeleteExpenseCommandRequest : IRequest<SuccessDetails>
    {
        public int Id { get; set; }
    }
}
