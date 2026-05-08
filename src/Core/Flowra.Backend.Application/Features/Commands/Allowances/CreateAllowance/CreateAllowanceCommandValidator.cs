using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Allowances.CreateAllowance
{
    public class CreateAllowanceCommandValidator : AbstractValidator<CreateAllowanceCommandRequest>
    {
        public CreateAllowanceCommandValidator()
        {
            RuleFor(x => x.PersonName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Kişi adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kişi adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");
        }
    }
}
