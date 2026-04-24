using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.RefreshToken
{
    public class RefreshTokenCommandRequest : IRequest<SuccessDetails<RefreshTokenCommandDto>>
    {
        public string RefreshToken { get; set; } = null!;

    }
}
