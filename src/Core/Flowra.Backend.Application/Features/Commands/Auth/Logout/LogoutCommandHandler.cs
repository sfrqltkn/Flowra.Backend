using Flowra.Backend.Application.Abstractions.Infrastructure.Token;
using Flowra.Backend.Application.Abstractions.Presentation;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.Logout
{
    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommandRequest, SuccessDetails<LogoutCommandDto>>
    {
        private readonly ITokenService _tokenService;
        private readonly IRequestContext _requestContext;

        public LogoutCommandHandler(ITokenService tokenService, IRequestContext requestContext)
        {
            _tokenService = tokenService;
            _requestContext = requestContext;
        }

        public async Task<SuccessDetails<LogoutCommandDto>> Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _requestContext.UserId;
            userId.ThrowIfNullUnauthorized(ResponseMessages.Auth.Unauthorized);

            await _tokenService.RevokeAllAsync(_requestContext.UserId!.Value);

            var dto = new LogoutCommandDto
            {
                ClearAccessTokenCookie = true,
                ClearRefreshTokenCookie = true
            };

            return ResultResponse.Success(dto,ResponseMessages.Auth.Logout_Success);
        }
    }
}
