using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.Logout
{
    public sealed class LogoutCommandRequest : IRequest<SuccessDetails<LogoutCommandDto>>
    {
    }
}
