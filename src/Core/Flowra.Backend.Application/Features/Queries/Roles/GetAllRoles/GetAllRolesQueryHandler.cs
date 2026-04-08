using Flowra.Backend.Application.Abstractions.Persistence.Repositories.Identity;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Roles;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Roles.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQueryRequest, SuccessDetails<List<RoleDto>>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetAllRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<SuccessDetails<List<RoleDto>>> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllRolesAsync(cancellationToken);

            var roleDtos = roles.Select(role => new RoleDto
            {
                Id = role.Id,
                Name = role.Name!
            }).ToList();

            return ResultResponse.Success(roleDtos, Response.Common.OperationSuccess);

        }
    }
}
