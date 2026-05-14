using FluentValidation;

namespace Flowra.Backend.Application.Features.Queries.CashRecords.GetCashRecordById
{
    public class GetCashRecordByIdQueryValidator : AbstractValidator<GetCashRecordByIdQueryRequest>
    {
        public GetCashRecordByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Kasa Kayıt ID zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir Kasa Kayıt ID (sıfırdan büyük) giriniz.");
        }
    }
}
