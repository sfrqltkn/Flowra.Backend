using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Roles.DeleteRole
{
    public class DeleteRoleCommandRequest : IRequest<SuccessDetails>
    {
        public int Id { get; set; }
    }
}
