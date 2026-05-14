using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.CashRecord;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.CashRecords.GetCashRecordById
{
    public class GetCashRecordByIdQueryRequest : IRequest<SuccessDetails<CashRecordDto>>
    {
        public int Id { get; set; }
    }
}
