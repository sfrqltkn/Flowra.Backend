using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Abstractions.Infrastructure.Token;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, SuccessDetails<LogoutCommandDto>>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public ResetPasswordCommandHandler(IUserService userService, ITokenService tokenService = null)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public async Task<SuccessDetails<LogoutCommandDto>> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId.ToString());
            user.ThrowIfNull(ResponseMessages.Auth.ResetPass_UserNotFound);
            
            user!.IsActive.ThrowIfFalse(ResponseMessages.Auth.ResetPass_Inactive);
            user.EmailConfirmed.ThrowIfFalse(ResponseMessages.Auth.ResetPass_EmailNotConfirmed);

            string decodedToken = TokenExtensions.DecodeToken(request.ResetToken);
            var result = await _userService.ResetPasswordAsync(user, decodedToken, request.NewPassword);

            if (!result.Succeeded)
            {
                result.ThrowIfFailed(ResponseMessages.Auth.ResetPass_Failed);
            }

            user.NeedPasswordReset = false;
            await _userService.UpdateAsync(user);
            await _userService.UpdateSecurityStampAsync(user);

            await _tokenService.RevokeAllAsync(user.Id);

            var dto = new LogoutCommandDto
            {
                ClearAccessTokenCookie = true,
                ClearRefreshTokenCookie = true
            };

            return ResultResponse.Success(dto,ResponseMessages.Auth.ResetPass_Success);
        }
    }
}
