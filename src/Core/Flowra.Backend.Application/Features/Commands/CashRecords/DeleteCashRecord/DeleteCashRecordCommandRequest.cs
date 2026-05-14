using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.CashRecords.DeleteCashRecord
{
    public class DeleteCashRecordCommandRequest : IRequest<SuccessDetails>
    {
        public int Id { get; set; }
    }
}
