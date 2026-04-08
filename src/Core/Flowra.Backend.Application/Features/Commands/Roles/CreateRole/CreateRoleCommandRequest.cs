using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Roles.CreateRole
{
    public class CreateRoleCommandRequest : IRequest<SuccessDetails<int>>
    {
        public string Name { get; set; } = null!;
    }
}
