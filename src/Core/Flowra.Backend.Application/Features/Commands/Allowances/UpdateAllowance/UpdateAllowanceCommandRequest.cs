using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Allowances.UpdateAllowance
{
    public class UpdateAllowanceCommandRequest : IRequest<SuccessDetails>
    {
        public int Id { get; set; }
        public string PersonName { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
