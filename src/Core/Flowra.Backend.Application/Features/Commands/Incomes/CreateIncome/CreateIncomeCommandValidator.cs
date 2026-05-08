using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Incomes.CreateIncome
{
    public class CreateIncomeCommandValidator : AbstractValidator<CreateIncomeCommandRequest>
    {
        public CreateIncomeCommandValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Gelir adı boş olamaz.")
                .MaximumLength(100).WithMessage("Gelir adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Gelir miktarı 0'dan büyük olmalıdır.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Tarih alanı zorunludur.");
        }
    }
}
