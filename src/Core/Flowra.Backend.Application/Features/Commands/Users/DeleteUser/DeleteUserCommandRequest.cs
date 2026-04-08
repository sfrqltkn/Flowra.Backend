using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Users.DeleteUser
{
    public class DeleteUserCommandRequest : IRequest<SuccessDetails>
    {
        public int Id { get; set; }
    }
}
