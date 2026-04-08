using Flowra.Backend.Application.Common.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace Flowra.Backend.Application.Features.Commands.Roles.UpdateRole
{
    public class UpdateRoleCommandRequest : IRequest<SuccessDetails>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
