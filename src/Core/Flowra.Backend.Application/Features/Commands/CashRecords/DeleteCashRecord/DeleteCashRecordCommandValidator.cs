using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.CashRecords.DeleteCashRecord
{
    public class DeleteCashRecordCommandValidator : AbstractValidator<DeleteCashRecordCommandRequest>
    {
        public DeleteCashRecordCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Geçerli bir kimlik (ID) girilmelidir.");
        }
    }
}
