using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.CashRecords.CreateCashRecord
{
    public class CreateCashRecordCommandRequest : IRequest<SuccessDetails<int>>
    {
        public string MonthYear { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
