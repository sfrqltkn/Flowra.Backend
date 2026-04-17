using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Abstractions.Infrastructure.Services.Mail;
using Flowra.Backend.Application.Common.Exceptions;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.SystemMessages;
using Flowra.Backend.Domain.Identity;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, SuccessDetails<int>>
    {
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public RegisterCommandHandler(IUserService userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        public async Task<SuccessDetails<int>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var existingEmail = await _userService.FindByEmailAsync(request.Email);
            if (existingEmail is not null)
                throw new ConflictException("Bu e-posta adresi sistemde zaten kayıtlı.", nameof(request.Email));

            var existingUserName = await _userService.FindByNameAsync(request.UserName);
            if (existingUserName is not null)
                throw new ConflictException("Bu kullanıcı adı sistemde zaten kayıtlı.", nameof(request.UserName));

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                IsActive = true,
                EmailConfirmed = false,
                NeedPasswordReset = false
            };

            var result = await _userService.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                throw new OperationFailedException($"Kullanıcı kayıt işlemi sırasında hata meydana geldi: {errors}");
            }

            try
            {
                var token = await _userService.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = TokenExtensions.EncodeToken(token);
                await _mailService.SendEmailConfirmationMailAsync(user.Email!, user.Id, $"{user.FirstName} {user.LastName}", encodedToken);
            }
            catch
            {
                await _userService.DeleteAsync(user);
                throw new OperationFailedException("Doğrulama e-postası gönderilemediği için kayıt işlemi geri alındı.");
            }

            return ResultResponse.Created(user.Id, ResponseMessages.Auth.Register_Success);
        }
    }
}
