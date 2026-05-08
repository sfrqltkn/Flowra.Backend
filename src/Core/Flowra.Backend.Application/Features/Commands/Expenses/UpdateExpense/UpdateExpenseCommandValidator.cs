using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Expenses.UpdateExpense
{
    public class UpdateExpenseCommandValidator : AbstractValidator<UpdateExpenseCommandRequest>
    {
        public UpdateExpenseCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Geçerli bir kimlik (ID) girilmelidir.");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Gider adı boş olamaz.")
                .MaximumLength(100).WithMessage("Gider adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("Gider miktarı 0'dan büyük olmalıdır.");
        }
    }
}
