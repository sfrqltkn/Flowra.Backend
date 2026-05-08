using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Assets.CreateAsset
{
    public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommandRequest>
    {
        public CreateAssetCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Varlık adı boş olamaz.")
                .MaximumLength(150).WithMessage("Varlık adı en fazla 150 karakter olabilir.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Varlık tipi boş olamaz.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");

            RuleFor(x => x.EstimatedUnitValue)
                .GreaterThanOrEqualTo(0).WithMessage("Tahmini birim değer 0'dan küçük olamaz.");
        }
    }
}
