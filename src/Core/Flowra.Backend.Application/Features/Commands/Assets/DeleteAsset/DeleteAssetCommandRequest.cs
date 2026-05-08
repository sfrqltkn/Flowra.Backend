using Flowra.Backend.Application.Common.Responses;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Assets.DeleteAsset
{
    public class DeleteAssetCommandRequest : IRequest<SuccessDetails>
    {
        public int Id { get; set; }
    }
}
