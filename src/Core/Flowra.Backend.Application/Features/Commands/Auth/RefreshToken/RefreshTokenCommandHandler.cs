using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Abstractions.Infrastructure.Token;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, SuccessDetails<RefreshTokenCommandDto>>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        public async Task<SuccessDetails<RefreshTokenCommandDto>> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var existingToken = await _tokenService.ValidateRefreshTokenAsync(request.RefreshToken);
            existingToken.ThrowIfNull(ResponseMessages.Auth.Refresh_InvalidToken);

            existingToken.UserId.ThrowIfNullUnauthorized(ResponseMessages.Auth.Refresh_UserNotFound);

            var user = await _userService.FindByIdAsync(existingToken.UserId.Value.ToString());
            user.ThrowIfNull(ResponseMessages.Auth.Refresh_UserNotFound);

            var newRefreshToken = await _tokenService.RotateRefreshTokenAsync(user!, request.RefreshToken);
            newRefreshToken.ThrowIfNull(ResponseMessages.Auth.Refresh_Failed);

            var access = await _tokenService.GenerateAccessTokenAsync(user!);

            var dto = new RefreshTokenCommandDto
            {
                AccessToken = access.Token,
                AccessTokenExpiresAtUtc = access.ExpiresAtUtc,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiresAtUtc = newRefreshToken.ExpiresAtUtc
            };

            return ResultResponse.Success(dto, ResponseMessages.Auth.Refresh_Success);
        }
    }
}
