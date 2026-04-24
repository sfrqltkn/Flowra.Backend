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
            (await _userService.FindByEmailAsync(request.Email))
                  .ThrowIfExists(ResponseMessages.Auth.Register_EmailConflict);

            (await _userService.FindByNameAsync(request.UserName))
                 .ThrowIfExists(ResponseMessages.Auth.Register_UsernameConflict);

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                IsActive = true,
                EmailConfirmed = false,
                NeedPasswordReset = false,
            };

            var createResult = await _userService.CreateAsync(user, request.Password);
            createResult.ThrowIfFailed(ResponseMessages.Auth.Register_IdentityFailed);

            try
            {
                var rawToken = await _userService.GenerateEmailConfirmationTokenAsync(user);
                var safeToken = TokenExtensions.EncodeToken(rawToken);
                await _mailService.SendEmailConfirmationMailAsync(user.Email!, user.Id,$"{user.FirstName} {user.LastName}", safeToken);
            }
            catch (Exception)
            {
                await _userService.DeleteAsync(user);
                throw new BusinessRuleException(ResponseMessages.Auth.Register_MailSendFailed);
            }

            return ResultResponse.Created(user.Id, ResponseMessages.Auth.Register_Success);
        }
    }
}
