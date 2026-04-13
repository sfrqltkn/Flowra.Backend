using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.ChangePassword
{
    public class ChangePasswordCommandRequest : IRequest<SuccessDetails>
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmNewPassword { get; set; } = null!;

    }
}
