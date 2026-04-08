using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.UserRoles.AddRoleToUser
{
    public class AddRoleToUserCommandRequest : IRequest<SuccessDetails>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
