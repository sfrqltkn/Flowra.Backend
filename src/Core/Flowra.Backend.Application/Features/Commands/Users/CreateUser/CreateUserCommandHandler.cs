using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Abstractions.Infrastructure.Services.Mail;
using Flowra.Backend.Application.Common.Exceptions;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.SystemMessages;
using Flowra.Backend.Domain.Identity;
using Flowra.BackendApplication.Extensions;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, SuccessDetails<int>>
    {
        private readonly IUserService _userService;
        private readonly IMailService _mailService;


        public CreateUserCommandHandler(IUserService userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        public async Task<SuccessDetails<int>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var existingEmail = await _userService.FindByEmailAsync(request.Email);
            if (existingEmail != null)
                throw new ConflictException("Bu e-posta adresi sistemde zaten kayıtlı",nameof(request.Email));

            string UserName = UserNameExtensions.ToUserName(request.FirstName, request.LastName);

            var existingUserName = await _userService.FindByNameAsync(UserName);
            if (existingUserName is not null)
                throw new ConflictException("Bu kullanıcı adı sistemde zaten kayıtlı.", nameof(UserName));

            string generatedPassword = PasswordExtensions.GenerateSecurePassword();

            var user = new User
            {
                UserName = UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true,
                IsActive = true,
                NeedPasswordReset = true
            };

            var result = await _userService.CreateAsync(user, generatedPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new OperationFailedException($"Kullanıcı oluşturulurken hata meydana geldi: {errors}");
            }

            var fullName = $"{user.FirstName} {user.LastName}".Trim();
            await _mailService.SendInitialPasswordMailAsync(user.Email!, fullName, user.UserName!, generatedPassword);

            return ResultResponse.Created(user.Id, ResponseMessages.Common.OperationSuccess);
        }
    }
}
