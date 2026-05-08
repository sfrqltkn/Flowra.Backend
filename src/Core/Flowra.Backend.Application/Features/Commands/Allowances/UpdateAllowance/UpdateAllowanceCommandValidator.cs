using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Allowances.UpdateAllowance
{
    public class UpdateAllowanceCommandValidator : AbstractValidator<UpdateAllowanceCommandRequest>
    {
        public UpdateAllowanceCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ödenek ID giriniz.");

            RuleFor(x => x.PersonName)
                .NotEmpty().WithMessage("Kişi adı boş olamaz.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");
        }
    }
}
