using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Asset;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Assets.GetAllAssets
{
    public class GetAllAssetsQueryRequest : IRequest<SuccessDetails<IEnumerable<AssetDto>>>
    {
    }
}
