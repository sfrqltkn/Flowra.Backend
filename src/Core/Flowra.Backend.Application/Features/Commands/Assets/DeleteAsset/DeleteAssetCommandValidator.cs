using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Assets.DeleteAsset
{
    internal class DeleteAssetCommandValidator : AbstractValidator<DeleteAssetCommandRequest>
    {
        public DeleteAssetCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Geçerli bir kimlik (ID) girilmelidir.");
        }
    }
}
