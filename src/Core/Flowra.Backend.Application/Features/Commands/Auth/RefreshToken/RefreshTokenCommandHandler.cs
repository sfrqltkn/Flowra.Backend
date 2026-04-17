using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Abstractions.Infrastructure.Token;
using Flowra.Backend.Application.Common.Exceptions;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, SuccessDetails<AuthResultDto>>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        public async Task<SuccessDetails<AuthResultDto>> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var existingToken = await _tokenService.ValidateRefreshTokenAsync(request.RefreshToken);

            if (existingToken is null)
                throw new UnauthorizedException("Geçersiz veya süresi dolmuş refresh token.");

            if (existingToken.UserId is null)
                throw new UnauthorizedException("Refresh token ile ilişkili kullanıcı bulunamadı.");

            var user = await _userService.FindByIdAsync(existingToken.UserId.Value.ToString());

            if (user is null)
                throw new UnauthorizedException("Kullanıcı bulunamadı.");

            var newRefreshToken = await _tokenService.RotateRefreshTokenAsync(user, request.RefreshToken, request.IpAddress);

            if (newRefreshToken is null)
                throw new OperationFailedException("Refresh token yenilenemedi.");

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user);

            var dto = new AuthResultDto
            {
                AccessToken = accessToken.Token,
                RefreshToken = newRefreshToken.Token,
            };

            return ResultResponse.Success(dto, ResponseMessages.Auth.Refresh_Success);
        }
    }
}
