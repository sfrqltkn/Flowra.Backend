using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Allowances.DeleteAllowance
{
    public class DeleteAllowanceCommandRequest : IRequest<SuccessDetails>
    {
        public int Id { get; set; }
    }
}
