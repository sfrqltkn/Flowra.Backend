using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Users;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Users.GetUserById
{
    public class GetUserByIdQueryRequest : IRequest<SuccessDetails<UserDto>>
    {
        public int Id { get; set; }
    }
}
