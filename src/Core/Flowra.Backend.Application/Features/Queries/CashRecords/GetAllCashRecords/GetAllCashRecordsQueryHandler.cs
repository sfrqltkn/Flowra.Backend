using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.CashRecord;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.CashRecords.GetAllCashRecords
{
    public class GetAllCashRecordsQueryHandler : IRequestHandler<GetAllCashRecordsQueryRequest, SuccessDetails<IEnumerable<CashRecordDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCashRecordsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<IEnumerable<CashRecordDto>>> Handle(GetAllCashRecordsQueryRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<CashRecord, int>();

            var cashRecords = await readRepository.GetListAsync(x => true, cancellationToken);

            var dtos = cashRecords.Select(c => new CashRecordDto
            {
                Id = c.Id,
                MonthYear = c.MonthYear,
                Balance = c.Balance
            });

            return ResultResponse.Success(dtos, "Kasa kayıtları başarıyla listelendi.");
        }
    }
}
