using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Common.Exceptions;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommandRequest, SuccessDetails>
    {
        private readonly IUserService _userService;

        public ConfirmEmailCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<SuccessDetails> Handle(ConfirmEmailCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId.ToString());

            if (user is null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            if (!user.IsActive)
                throw new BusinessRuleException("Pasif kullanıcılar için e-posta doğrulama işlemi yapılamaz.");

            if (user.EmailConfirmed)
                throw new ConflictException("Bu e-posta adresi zaten doğrulanmış.", nameof(request.UserId));

            string decodedToken;
            try
            {
                decodedToken = TokenExtensions.DecodeToken(request.Token);
            }
            catch
            {
                throw new BadRequestException("Geçersiz doğrulama token'ı.");
            }

            var result = await _userService.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                throw new OperationFailedException($"E-posta doğrulama işlemi başarısız oldu: {errors}");
            }

            return ResultResponse.Success(ResponseMessages.Auth.ConfirmEmail_Success);
        }
    }
}
