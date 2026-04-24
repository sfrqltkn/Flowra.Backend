using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.Login
{
    public class LoginCommandRequest : IRequest<SuccessDetails<LoginCommandDto>>
    {
        public string EmailOrUsername { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
