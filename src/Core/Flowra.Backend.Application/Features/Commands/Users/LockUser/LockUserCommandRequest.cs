using Flowra.Backend.Application.Common.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace Flowra.Backend.Application.Features.Commands.Users.LockUser
{
    public class LockUserCommandRequest : IRequest<SuccessDetails>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int? DurationInHours { get; set; }
    }
}
