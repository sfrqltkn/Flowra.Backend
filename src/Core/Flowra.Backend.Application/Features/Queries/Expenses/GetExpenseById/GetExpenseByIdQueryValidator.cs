using FluentValidation;

namespace Flowra.Backend.Application.Features.Queries.Expenses.GetExpenseById
{
    public class GetExpenseByIdQueryValidator : AbstractValidator<GetExpenseByIdQueryRequest>
    {
        public GetExpenseByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Gider ID zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir gider ID (sıfırdan büyük) giriniz.");
        }
    }
}
