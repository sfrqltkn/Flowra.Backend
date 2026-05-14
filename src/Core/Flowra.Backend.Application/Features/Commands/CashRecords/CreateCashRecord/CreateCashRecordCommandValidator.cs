using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.CashRecords.CreateCashRecord
{
    public class CreateCashRecordCommandValidator : AbstractValidator<CreateCashRecordCommandRequest>
    {
        public CreateCashRecordCommandValidator()
        {
            RuleFor(x => x.MonthYear)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ay/Yıl bilgisi boş olamaz.")
                .MaximumLength(50).WithMessage("Ay/Yıl bilgisi en fazla 50 karakter olabilir.");

            RuleFor(x => x.Balance)
                .NotNull().WithMessage("Bakiye alanı zorunludur.");
        }
    }
}
