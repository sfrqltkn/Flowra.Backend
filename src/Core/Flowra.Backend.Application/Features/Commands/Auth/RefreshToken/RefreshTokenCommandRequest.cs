using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using MediatR;
using System.Text.Json.Serialization;

namespace Flowra.Backend.Application.Features.Commands.Auth.RefreshToken
{
    public class RefreshTokenCommandRequest : IRequest<SuccessDetails<AuthResultDto>>
    {
        public string RefreshToken { get; set; } = null!;

        [JsonIgnore]
        public string IpAddress { get; set; } = "N/A";
    }
}
