using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Incomes.UpdateIncome
{
    public class UpdateIncomeCommandValidator : AbstractValidator<UpdateIncomeCommandRequest>
    {
        public UpdateIncomeCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Geçerli bir kimlik (ID) girilmelidir.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Gelir adı boş olamaz.")
                .MaximumLength(100).WithMessage("Gelir adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Gelir miktarı 0'dan büyük olmalıdır.");
        }
    }
}
