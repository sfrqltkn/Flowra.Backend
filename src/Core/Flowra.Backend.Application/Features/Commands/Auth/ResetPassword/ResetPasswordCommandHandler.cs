using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Common.Exceptions;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, SuccessDetails>
    {
        private readonly IUserService _userService;

        public ResetPasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<SuccessDetails> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId.ToString());

            if (user is null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            if (!user.IsActive)
                throw new BusinessRuleException("Pasif kullanıcılar için şifre sıfırlama işlemi yapılamaz.");

            if (!user.EmailConfirmed)
                throw new ForbiddenException("E-posta adresi doğrulanmamış kullanıcılar şifre sıfırlayamaz.");

            string decodedToken;
            try
            {
                decodedToken = TokenExtensions.DecodeToken(request.ResetToken);
            }
            catch
            {
                throw new BadRequestException("Geçersiz şifre sıfırlama token'ı.");
            }

            var result = await _userService.ResetPasswordAsync(user, decodedToken, request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                throw new OperationFailedException($"Şifre sıfırlama işlemi başarısız oldu: {errors}");
            }

            user.NeedPasswordReset = false;
            await _userService.UpdateAsync(user);
            await _userService.UpdateSecurityStampAsync(user);

            return ResultResponse.Success(ResponseMessages.Auth.ResetPass_Success);
        }
    }
}
