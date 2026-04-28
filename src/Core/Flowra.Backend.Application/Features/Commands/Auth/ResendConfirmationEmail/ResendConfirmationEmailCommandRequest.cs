using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.ResendConfirmationEmail
{
    public class ResendConfirmationEmailCommandRequest : IRequest<SuccessDetails>
    {
        public string EmailOrUsername { get; set; } = string.Empty;

    }
}
