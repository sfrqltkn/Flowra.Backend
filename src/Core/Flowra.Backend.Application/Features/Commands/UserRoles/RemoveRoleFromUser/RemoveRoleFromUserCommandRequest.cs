using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.UserRoles.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCommandRequest : IRequest<SuccessDetails>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
