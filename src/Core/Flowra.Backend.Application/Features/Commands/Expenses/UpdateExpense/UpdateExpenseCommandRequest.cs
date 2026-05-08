using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Expenses.UpdateExpense
{
    public class UpdateExpenseCommandRequest : IRequest<SuccessDetails>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; }
        public bool IsCreditCard { get; set; }
        public decimal? MinimumPaymentAmount { get; set; }
        public bool IsPaid { get; set; }
    }
}
