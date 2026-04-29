using Flowra.Backend.Application.Abstractions.Persistence.Repositories.Identity;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Users;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Users.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, SuccessDetails<List<UserDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<SuccessDetails<List<UserDto>>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync(cancellationToken);
           
            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName!,
                Email = u.Email!,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IsActive = u.IsActive,
                PhoneNumber =u.PhoneNumber!,
                Roles = u.UserRoles.Select(ur => ur.Role.Name!).ToList()
            }).ToList();

            return ResultResponse.Success(userDtos, ResponseMessages.User.Listed);
        }
    }
}
