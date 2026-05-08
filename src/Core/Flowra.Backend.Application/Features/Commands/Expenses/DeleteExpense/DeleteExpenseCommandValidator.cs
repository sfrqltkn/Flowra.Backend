using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Expenses.DeleteExpense
{
    public class DeleteExpenseCommandValidator : AbstractValidator<DeleteExpenseCommandRequest>
    {
        public DeleteExpenseCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Geçerli bir kimlik (ID) girilmelidir.");
        }
    }
}
