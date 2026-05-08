using FluentValidation;

namespace Flowra.Backend.Application.Features.Queries.Assets.GetAssetById
{
    public class GetAssetByIdQueryValidator : AbstractValidator<GetAssetByIdQueryRequest>
    {
        public GetAssetByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Varlık ID zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir varlık ID (sıfırdan büyük) giriniz.");
        }
    }
}
