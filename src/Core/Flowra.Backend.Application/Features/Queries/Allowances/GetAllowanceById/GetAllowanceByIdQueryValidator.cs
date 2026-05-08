using FluentValidation;

namespace Flowra.Backend.Application.Features.Queries.Allowances.GetAllowanceById
{
    public class GetAllowanceByIdQueryValidator : AbstractValidator<GetAllowanceByIdQueryRequest>
    {
        public GetAllowanceByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ödenek ID zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir ödenek ID (sıfırdan büyük) giriniz.");
        }
    }
}
