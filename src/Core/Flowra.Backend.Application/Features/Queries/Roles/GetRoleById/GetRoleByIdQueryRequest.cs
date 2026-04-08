using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Roles;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Roles.GetRoleById
{
    public class GetRoleByIdQueryRequest : IRequest<SuccessDetails<RoleDto>>
    {
        public int Id { get; set; }
    }
}
