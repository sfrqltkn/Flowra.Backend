using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Users;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Users.GetAllUsers
{
    public class GetAllUsersQueryRequest : IRequest<SuccessDetails<List<UserDto>>>
    {
    }
}
