using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.Logout
{
    public class LogoutCommandRequest : IRequest<SuccessDetails>
    {
    }
}
