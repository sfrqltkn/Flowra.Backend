using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.ResetPassword
{
    public class ResetPasswordCommandRequest : IRequest<SuccessDetails<LogoutCommandDto>>
    {
        public int UserId { get; set; }
        public string ResetToken { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;

    }
}
