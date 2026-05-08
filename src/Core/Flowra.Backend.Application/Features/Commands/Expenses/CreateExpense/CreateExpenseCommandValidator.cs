using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Expenses.CreateExpense
{
    public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommandRequest>
    {
        public CreateExpenseCommandValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Gider adı boş olamaz.")
                .MaximumLength(100).WithMessage("Gider adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("Gider miktarı 0'dan büyük olmalıdır.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Tarih alanı zorunludur.");
        }
    }
}
