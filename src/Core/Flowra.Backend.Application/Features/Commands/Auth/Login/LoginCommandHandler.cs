using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Abstractions.Infrastructure.Token;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.SystemMessages;
using Flowra.Backend.Application.DTOs.Auth;
using Flowra.Backend.Application.Extensions;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, SuccessDetails<LoginCommandDto>>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        public async Task<SuccessDetails<LoginCommandDto>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var isEmail = request.EmailOrUsername.Contains("@");
            var user = isEmail
                ? await _userService.FindByEmailAsync(request.EmailOrUsername)
                : await _userService.FindByNameAsync(request.EmailOrUsername);

            user.ThrowIfNull(ResponseMessages.Auth.Login_UserNotFound);

            user!.IsActive.ThrowIfFalse(ResponseMessages.Auth.Login_Inactive);
            user.EmailConfirmed.ThrowIfFalse(ResponseMessages.Auth.Login_EmailNotConfirmed);

            var signIn = await _userService.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

            signIn.IsLockedOut.ThrowIfTrue(ResponseMessages.Auth.Login_Locked);
            signIn.Succeeded.ThrowIfFalse(ResponseMessages.Auth.Login_InvalidCredentials);

            if (user.NeedPasswordReset)
            {
                var rawResetToken = await _userService.GeneratePasswordResetTokenAsync(user);
                var safeResetToken = TokenExtensions.EncodeToken(rawResetToken);

                var loginResponse = new LoginResponseDto
                {
                    UserId = user.Id,
                    Email = user.Email ?? "",
                    FirstName = user.FirstName ?? "",
                    LastName = user.LastName ?? "",
                    Roles = new List<string>(),
                    RequiresPasswordReset = true,
                    ResetPasswordToken = safeResetToken
                };

                return new SuccessDetails<LoginCommandDto>
                {
                    Status = 200,
                    Detail = ResponseMessages.Auth.Login_PasswordResetRequired,
                    Data = new LoginCommandDto
                    {
                        Response = loginResponse,
                    }
                };
            }

            var access = await _tokenService.GenerateAccessTokenAsync(user);
            var refreshToken = await _tokenService.CreateRefreshTokenAsync(user);
            var roles = await _userService.GetRolesAsync(user);

            var dto = new LoginResponseDto
            {
                UserId = user.Id,
                UserName = user.UserName!,
                FirstName = user.FirstName!,
                LastName = user.LastName,
                Email = user.Email!,
                Roles = roles.ToList(),
                RequiresPasswordReset = false,
                ResetPasswordToken = ""
            };

            var loginCommandDto = new LoginCommandDto
            {
                Response = dto,
                AccessToken = access.Token,
                AccessTokenExpiresAtUtc = access.ExpiresAtUtc,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiresAtUtc = refreshToken.ExpiresAtUtc
            };

            return ResultResponse.Success(loginCommandDto, ResponseMessages.Auth.Login_Success);
        }
    }
}
