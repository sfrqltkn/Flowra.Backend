using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Abstractions.Infrastructure.Token;
using Flowra.Backend.Application.Abstractions.Presentation;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommandRequest, SuccessDetails>
    {
        private readonly IUserService _userService;
        private readonly IRequestContext _requestContext;
        private readonly ITokenService _tokenService;

        public ChangePasswordCommandHandler(IUserService userService, IRequestContext requestContext, ITokenService tokenService)
        {
            _userService = userService;
            _requestContext = requestContext;
            _tokenService = tokenService;
        }

        public async Task<SuccessDetails> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _requestContext.UserId;
            userId.ThrowIfNullUnauthorized(ResponseMessages.Auth.Unauthorized);

            var user = await _userService.FindByIdAsync(userId.Value.ToString());
            user.ThrowIfNull(ResponseMessages.Auth.ChangePass_UserNotFound);

            var check = await _userService.CheckPasswordSignInAsync(user!, request.OldPassword, lockoutOnFailure: false);
            check.Succeeded.ThrowIfFalse(ResponseMessages.Auth.ChangePass_WrongOldPassword);

            var result = await _userService.ChangePasswordAsync(user!, request.OldPassword, request.NewPassword);
            result.ThrowIfFailed(ResponseMessages.Auth.ChangePass_Failed);

            await _userService.UpdateSecurityStampAsync(user!);
            await _tokenService.RevokeAllAsync(_requestContext.UserId!.Value);

            var dto = new LogoutCommandDto
            {
                ClearAccessTokenCookie = true,
                ClearRefreshTokenCookie = true
            };

            return ResultResponse.Success(dto, ResponseMessages.Auth.ChangePass_Success);
        }
    }
}
