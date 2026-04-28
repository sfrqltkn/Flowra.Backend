using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Abstractions.Infrastructure.Services.Mail;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.ResendConfirmationEmail
{
    public sealed class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommandRequest, SuccessDetails>
    {
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public ResendConfirmationEmailCommandHandler(IUserService userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        public async Task<SuccessDetails> Handle(ResendConfirmationEmailCommandRequest request, CancellationToken cancellationToken)
        {
            var isEmail = request.EmailOrUsername.Contains("@");
            var user = isEmail
                ? await _userService.FindByEmailAsync(request.EmailOrUsername)
                : await _userService.FindByNameAsync(request.EmailOrUsername);

            user.ThrowIfNull(ResponseMessages.Auth.ResendEmail_UserNotFound);
            user.EmailConfirmed.ThrowIfTrue(ResponseMessages.Auth.ResendEmail_AlreadyConfirmed);
            user.IsActive.ThrowIfFalse(ResponseMessages.Auth.ResendEmail_Inactive);

            var rawToken = await _userService.GenerateEmailConfirmationTokenAsync(user);
            var safeToken = TokenExtensions.EncodeToken(rawToken);
            var fullName = $"{user.FirstName} {user.LastName}".Trim();

            await _mailService.SendResendConfirmationMailAsync(user.Email!, user.Id, fullName, safeToken);

            return ResultResponse.Success(ResponseMessages.Auth.ResendEmail_Sent);
        }
    }
}
