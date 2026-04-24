using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Abstractions.Infrastructure.Services.Mail;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommandRequest, SuccessDetails>
    {
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public ForgotPasswordCommandHandler(IUserService userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        public async Task<SuccessDetails> Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByEmailAsync(request.Email);
            user.ThrowIfNull(ResponseMessages.Auth.ForgotPass_UserNotFound);

            user!.IsActive.ThrowIfFalse(ResponseMessages.Auth.ForgotPass_Inactive);
            user.EmailConfirmed.ThrowIfFalse(ResponseMessages.Auth.ForgotPass_EmailNotConfirmed);


            var rawToken = await _userService.GeneratePasswordResetTokenAsync(user);
            var safeToken = TokenExtensions.EncodeToken(rawToken);
            var fullName = $"{user.FirstName} {user.LastName}".Trim();

            await _mailService.SendPasswordResetMailAsync(user.Email!, user.Id, fullName, safeToken);

            return ResultResponse.Success(ResponseMessages.Auth.ForgotPass_Sent);
        }
    }
}
