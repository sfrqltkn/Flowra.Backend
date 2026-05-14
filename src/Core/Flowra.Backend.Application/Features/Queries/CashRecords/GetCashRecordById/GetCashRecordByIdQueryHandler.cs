using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.CashRecord;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.CashRecords.GetCashRecordById
{
    public class GetCashRecordByIdQueryHandler : IRequestHandler<GetCashRecordByIdQueryRequest, SuccessDetails<CashRecordDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCashRecordByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<CashRecordDto>> Handle(GetCashRecordByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<CashRecord, int>();

            var cashRecord = await readRepository.GetByIdAsync(request.Id, cancellationToken);

            cashRecord.ThrowIfNull("Getirilmek istenen kasa kaydı bulunamadı.");

            var dto = new CashRecordDto
            {
                Id = cashRecord.Id,
                MonthYear = cashRecord.MonthYear,
                Balance = cashRecord.Balance
            };

            return ResultResponse.Success(dto, "Kasa kaydı başarıyla getirildi.");
        }
    }
}
