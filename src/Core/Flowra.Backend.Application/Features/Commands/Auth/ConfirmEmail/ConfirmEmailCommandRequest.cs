using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.ConfirmEmail
{
    public class ConfirmEmailCommandRequest : IRequest<SuccessDetails>
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;

    }
}
