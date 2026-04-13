using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.ResendConfirmationEmail
{
    public class ResendConfirmationEmailCommandRequest : IRequest<SuccessDetails>
    {
        public string Email { get; set; } = string.Empty;

    }
}
