using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.CashRecord;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.CashRecords.GetAllCashRecords
{
    public class GetAllCashRecordsQueryRequest : IRequest<SuccessDetails<IEnumerable<CashRecordDto>>>
    {
    }
}
