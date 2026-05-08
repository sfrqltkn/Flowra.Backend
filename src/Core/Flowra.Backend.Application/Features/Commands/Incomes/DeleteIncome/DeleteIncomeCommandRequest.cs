using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Incomes.DeleteIncome
{
    public class DeleteIncomeCommandRequest : IRequest<SuccessDetails<Unit>>
    {
        public int Id { get; set; }
    }
}
