using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.CashRecords.UpdateCashRecord
{
    public class UpdateCashRecordCommandValidator : AbstractValidator<UpdateCashRecordCommandRequest>
    {
        public UpdateCashRecordCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Geçerli bir kimlik (ID) girilmelidir.");

            RuleFor(x => x.MonthYear)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ay/Yıl bilgisi boş olamaz.")
                .MaximumLength(50).WithMessage("Ay/Yıl bilgisi en fazla 50 karakter olabilir.");

            RuleFor(x => x.Balance)
                .NotNull().WithMessage("Bakiye alanı zorunludur.");
        }
    }
}
