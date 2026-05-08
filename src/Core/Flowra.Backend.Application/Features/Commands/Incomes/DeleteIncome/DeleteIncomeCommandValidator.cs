using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Incomes.DeleteIncome
{
    public class DeleteIncomeCommandValidator : AbstractValidator<DeleteIncomeCommandRequest>
    {
        public DeleteIncomeCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Geçerli bir kimlik (ID) girilmelidir.");
        }
    }
}
