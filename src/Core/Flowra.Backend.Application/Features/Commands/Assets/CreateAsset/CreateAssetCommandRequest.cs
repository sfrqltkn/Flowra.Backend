using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Assets.CreateAsset
{
    public class CreateAssetCommandRequest : IRequest<SuccessDetails<int>>
    {
        public DateTime MonthYear { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal EstimatedUnitValue { get; set; }
    }
}
