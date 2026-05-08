using FluentValidation;

namespace Flowra.Backend.Application.Features.Queries.Incomes.GetIncomeById
{
    public class GetIncomeByIdQueryValidator : AbstractValidator<GetIncomeByIdQueryRequest>
    {
        public GetIncomeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Gelir ID zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir gelir ID (sıfırdan büyük) giriniz.");
        }
    }
}
