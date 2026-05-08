using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Allowances.CreateAllowance
{
    public class CreateAllowanceCommandRequest : IRequest<SuccessDetails<int>>
    {
        public string PersonName { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
