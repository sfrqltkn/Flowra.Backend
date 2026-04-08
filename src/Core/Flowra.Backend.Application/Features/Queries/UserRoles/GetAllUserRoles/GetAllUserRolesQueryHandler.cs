using Flowra.Backend.Application.Abstractions.Persistence.Repositories.Identity;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.UserRoles;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.UserRoles.GetAllUserRoles
{
    public class GetAllUserRolesQueryHandler : IRequestHandler<GetAllUserRolesQueryRequest, SuccessDetails<List<UserRolesDto>>>
    {
        private readonly IUserRolesRepository _userRolesRepository;

        public GetAllUserRolesQueryHandler(IUserRolesRepository userRolesRepository)
        {
            _userRolesRepository = userRolesRepository;
        }

        public async Task<SuccessDetails<List<UserRolesDto>>> Handle(GetAllUserRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var userRoles = await _userRolesRepository.GetAllUserRolesAsync(cancellationToken);

            var response = userRoles.ToList();
            return ResultResponse.Success(response, Response.Common.OperationSuccess);
        }
    }
}
