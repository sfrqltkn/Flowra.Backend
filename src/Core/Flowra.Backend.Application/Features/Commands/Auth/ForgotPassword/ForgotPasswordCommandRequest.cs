using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.ForgotPassword
{
    public class ForgotPasswordCommandRequest : IRequest<SuccessDetails>
    {
        public string Email { get; set; } = string.Empty;
    }
}
