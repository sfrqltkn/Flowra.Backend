using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Users.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommandRequest>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir kullanıcı kimliği (Id) girmelisiniz.");
        }
    }
}
