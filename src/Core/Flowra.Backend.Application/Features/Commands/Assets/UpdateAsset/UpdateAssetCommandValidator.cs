using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Assets.UpdateAsset
{
    public class UpdateAssetCommandValidator : AbstractValidator<UpdateAssetCommandRequest>
    {
        public UpdateAssetCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir varlık ID giriniz.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Varlık adı boş olamaz.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Varlık tipi boş olamaz.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");
        }
    }
}
