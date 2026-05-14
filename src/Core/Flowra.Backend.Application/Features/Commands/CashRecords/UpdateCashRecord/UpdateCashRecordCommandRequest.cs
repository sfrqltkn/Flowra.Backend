using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.CashRecords.UpdateCashRecord
{
    public class UpdateCashRecordCommandRequest : IRequest<SuccessDetails>
    {
        public int Id { get; set; }
        public string MonthYear { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
