
using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, SuccessDetails>
    {
        private readonly IUserService _userService;

        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<SuccessDetails> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.Id.ToString());
            user.ThrowIfNull(ResponseMessages.User.Update_NotFound);

            if (!string.IsNullOrWhiteSpace(request.Email) && request.Email != user.Email)
            {
                var owner = await _userService.FindByEmailAsync(request.Email);
                owner.ThrowIfExists(ResponseMessages.User.Create_EmailAlreadyExists);
                user.Email = request.Email;
            }

            user.UserName = request.UserName;
            user.FirstName = request.FirstName?.Trim() ?? user.FirstName;
            user.LastName = request.LastName?.Trim() ?? user.LastName;
            user.PhoneNumber = request.PhoneNumber?.Trim() ?? user.PhoneNumber;

            var result = await _userService.UpdateAsync(user);
            result.ThrowIfFailed(ResponseMessages.User.Update_IdentityFailed);

            return ResultResponse.Success(ResponseMessages.Common.OperationSuccess);
        }
    }
}
