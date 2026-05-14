using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.CashRecords.CreateCashRecord
{
    public class CreateCashRecordCommandHandler : IRequestHandler<CreateCashRecordCommandRequest, SuccessDetails<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCashRecordCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<int>> Handle(CreateCashRecordCommandRequest request, CancellationToken cancellationToken)
        {
            var cashRecord = new CashRecord
            {
                MonthYear = request.MonthYear,
                Balance = request.Balance
            };

            var writeRepository = _unitOfWork.WriteRepository<CashRecord, int>();

            await writeRepository.AddAsync(cashRecord, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success(cashRecord.Id, "Kasa kaydı başarıyla eklendi.");
        }
    }
}
