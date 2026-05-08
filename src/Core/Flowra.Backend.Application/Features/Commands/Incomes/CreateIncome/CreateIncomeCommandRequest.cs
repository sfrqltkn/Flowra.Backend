using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Incomes.CreateIncome
{
    public class CreateIncomeCommandRequest : IRequest<SuccessDetails<int>>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; }
    }
}
