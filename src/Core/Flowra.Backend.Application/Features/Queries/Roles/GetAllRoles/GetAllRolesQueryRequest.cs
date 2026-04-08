using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Roles;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Roles.GetAllRoles
{
    public class GetAllRolesQueryRequest : IRequest<SuccessDetails<List<RoleDto>>>
    {
    }
}
