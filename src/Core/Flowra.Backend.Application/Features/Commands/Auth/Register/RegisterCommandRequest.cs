using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.Register
{
    public class RegisterCommandRequest : IRequest<SuccessDetails<int>>
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
