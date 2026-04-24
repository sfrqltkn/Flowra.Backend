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
            user.ThrowIfNull(ResponseMessages.Auth.ConfirmEmail_UserNotFound);

            user!.IsActive.ThrowIfFalse(ResponseMessages.Auth.ConfirmEmail_Inactive);
            user.EmailConfirmed.ThrowIfTrue(ResponseMessages.Auth.ConfirmEmail_AlreadyConfirmed);

            string decodedToken = TokenExtensions.DecodeToken(request.Token);

            var confirmResult = await _userService.ConfirmEmailAsync(user, decodedToken);
            confirmResult.ThrowIfFailed(ResponseMessages.Auth.ConfirmEmail_Failed);

            return ResultResponse.Success(ResponseMessages.Auth.ConfirmEmail_Success);
        }
    }
}
