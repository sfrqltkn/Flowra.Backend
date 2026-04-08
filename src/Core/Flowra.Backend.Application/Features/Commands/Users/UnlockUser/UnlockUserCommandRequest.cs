
using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Users.UnlockUser
{
    public class UnlockUserCommandRequest : IRequest<SuccessDetails>
    {
        public int Id { get; set; }
    }
}
