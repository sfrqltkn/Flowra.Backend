using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.CashRecords.UpdateCashRecord
{
    public class UpdateCashRecordCommandHandler : IRequestHandler<UpdateCashRecordCommandRequest, SuccessDetails>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCashRecordCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails> Handle(UpdateCashRecordCommandRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<CashRecord, int>();
            var writeRepository = _unitOfWork.WriteRepository<CashRecord, int>();

            var entity = await readRepository.GetByIdAsync(request.Id, cancellationToken);

            entity.ThrowIfNull("Güncellenmek istenen kasa kaydı bulunamadı.");

            entity.MonthYear = request.MonthYear;
            entity.Balance = request.Balance;

            writeRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success(entity.Id, "Kasa kaydı başarıyla güncellendi.");
        }
    }
}
