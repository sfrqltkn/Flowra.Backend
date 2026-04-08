using Flowra.Backend.Application.Common.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace Flowra.Backend.Application.Features.Commands.Users.SetActiveUser
{
    public class SetActiveUserCommandRequest : IRequest<SuccessDetails>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
