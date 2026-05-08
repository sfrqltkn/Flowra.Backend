using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Expenses.UpdateExpense
{
    public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommandRequest, SuccessDetails>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExpenseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails> Handle(UpdateExpenseCommandRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Expense, int>();
            var writeRepository = _unitOfWork.WriteRepository<Expense, int>();

            var entity = await readRepository.GetByIdAsync(request.Id, cancellationToken);

            entity.ThrowIfNull("Güncellenmek istenen gider kaydı bulunamadı.");

            entity.Name = request.Name;
            entity.TotalAmount = request.TotalAmount;
            entity.Date = request.Date;
            entity.IsRecurring = request.IsRecurring;
            entity.IsCreditCard = request.IsCreditCard;
            entity.MinimumPaymentAmount = request.MinimumPaymentAmount;
            entity.IsPaid = request.IsPaid;

            writeRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success(entity.Id,"Gider başarıyla güncellendi.");
        }
    }
}
