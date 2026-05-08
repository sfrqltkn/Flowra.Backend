using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Expenses.DeleteExpense
{
    public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommandRequest, SuccessDetails>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExpenseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails> Handle(DeleteExpenseCommandRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Expense, int>();
            var writeRepository = _unitOfWork.WriteRepository<Expense, int>();

            var entity = await readRepository.GetByIdAsync(request.Id, cancellationToken);

            entity.ThrowIfNull("Silinmek istenen gider kaydı bulunamadı.");
            entity.MarkAsDeleted();

            writeRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success("Gider başarıyla silindi.");
        }
    }
}
