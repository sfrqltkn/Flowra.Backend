using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.UserRoles;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.UserRoles.GetAllUserRoles
{
    public class GetAllUserRolesQueryRequest : IRequest<SuccessDetails<List<UserRolesDto>>>
    {
    }
}
