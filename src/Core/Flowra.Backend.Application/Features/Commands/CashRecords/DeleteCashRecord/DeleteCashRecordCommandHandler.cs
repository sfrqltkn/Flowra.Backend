using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.CashRecords.DeleteCashRecord
{
    public class DeleteCashRecordCommandHandler : IRequestHandler<DeleteCashRecordCommandRequest, SuccessDetails>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCashRecordCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails> Handle(DeleteCashRecordCommandRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<CashRecord, int>();
            var writeRepository = _unitOfWork.WriteRepository<CashRecord, int>();

            var entity = await readRepository.GetByIdAsync(request.Id, cancellationToken);

            entity.ThrowIfNull("Silinmek istenen kasa kaydı bulunamadı.");
            entity.MarkAsDeleted();

            writeRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success("Kasa kaydı başarıyla silindi.");
        }
    }
}
