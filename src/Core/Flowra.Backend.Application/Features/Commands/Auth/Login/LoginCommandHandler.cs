using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Abstractions.Infrastructure.Token;
using Flowra.Backend.Application.Abstractions.Presentation;
using Flowra.Backend.Application.Common.Exceptions;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.SystemMessages;
using Flowra.Backend.Application.DTOs.Auth;
using Flowra.Backend.Application.Extensions;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, SuccessDetails<AuthResultDto>>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IRequestContext _requestContext;

        public LoginCommandHandler(IUserService userService, ITokenService tokenService, IRequestContext requestContext)
        {
            _userService = userService;
            _tokenService = tokenService;
            _requestContext = requestContext;
        }

        public async Task<SuccessDetails<AuthResultDto>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var isEmail = request.EmailOrUsername.Contains("@");

            var user = isEmail
                ? await _userService.FindByEmailAsync(request.EmailOrUsername)
                : await _userService.FindByNameAsync(request.EmailOrUsername);

            if (user is null)
                throw new UnauthorizedException("Kullanıcı adı/e-posta veya şifre hatalı.");

            var signInResult = await _userService.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

            if (signInResult.IsLockedOut)
                throw new UnauthorizedException("Hesabınız geçici olarak kilitlenmiştir.");

            if (!signInResult.Succeeded)
                throw new UnauthorizedException("Kullanıcı adı/e-posta veya şifre hatalı.");

            if (!user.IsActive)
                throw new BusinessRuleException("Pasif kullanıcılar giriş yapamaz.");

            if (!user.EmailConfirmed)
                throw new UnauthorizedException("E-posta adresiniz doğrulanmamış.");

            if (user.NeedPasswordReset)
            {
                var rawResetToken = await _userService.GeneratePasswordResetTokenAsync(user);
                var safeResetToken = TokenExtensions.EncodeToken(rawResetToken);
                return new SuccessDetails<AuthResultDto>
                {
                    Status = 200,
                    Detail = ResponseMessages.Auth.Login_PasswordResetRequired,
                    Data = new AuthResultDto
                    {
                        UserId = user.Id,
                        Email = user.Email ?? "",
                        Username = user.UserName ?? "",
                        FullName = $"{user.FirstName} {user.LastName}".Trim(),
                        Roles = new List<string>(),
                        RequiresPasswordReset = true,
                        ResetPasswordToken = safeResetToken
                    }
                };
            }

            var access = await _tokenService.GenerateAccessTokenAsync(user);
            var ipAddress = string.IsNullOrWhiteSpace(_requestContext.IpAddress) ? "unknown" : _requestContext.IpAddress;
            var refreshToken = await _tokenService.CreateRefreshTokenAsync(user, ipAddress);
            var roles = await _userService.GetRolesAsync(user);

            var dto = new AuthResultDto
            {
                UserId = user.Id,
                Email = user.Email!,
                Username = user.UserName!,
                FullName = $"{user.FirstName} {user.LastName}".Trim(),
                Roles = roles.ToList(),
                AccessToken = access.Token,
                AccessTokenExpiresAtUtc = access.ExpiresAtUtc,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiresAtUtc = refreshToken.ExpiresAtUtc,
                RequiresPasswordReset = false
            };

            return ResultResponse.Success(dto, ResponseMessages.Auth.Login_Success);
        }
    }
}
