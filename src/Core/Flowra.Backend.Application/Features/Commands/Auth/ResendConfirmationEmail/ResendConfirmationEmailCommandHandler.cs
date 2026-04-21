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
            var user = await _userService.FindByEmailAsync(request.Email);

            if (user is not null && user.IsActive && !user.EmailConfirmed)
            {
                var rawToken = await _userService.GenerateEmailConfirmationTokenAsync(user);
                var safeToken = TokenExtensions.EncodeToken(rawToken);
                var fullName = $"{user.FirstName} {user.LastName}".Trim();

                var displayName = string.IsNullOrWhiteSpace(user.UserName)
                    ? $"{user.FirstName} {user.LastName}".Trim()
                    : user.UserName;

                await _mailService.SendResendConfirmationMailAsync(
                    request.Email,
                    user.Id,
                    fullName, 
                    safeToken);
            }

            return ResultResponse.Success(ResponseMessages.Auth.ResendEmail_Sent);
        }
    }
}
