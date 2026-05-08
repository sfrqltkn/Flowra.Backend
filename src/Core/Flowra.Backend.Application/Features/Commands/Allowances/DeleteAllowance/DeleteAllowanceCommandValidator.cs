using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Allowances.DeleteAllowance
{
    internal class DeleteAllowanceCommandValidator : AbstractValidator<DeleteAllowanceCommandRequest>
    {
        public DeleteAllowanceCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Geçerli bir kimlik (ID) girilmelidir.");
        }
    }
}
