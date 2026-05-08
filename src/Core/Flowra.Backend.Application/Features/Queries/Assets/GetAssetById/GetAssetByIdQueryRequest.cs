using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Asset;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Assets.GetAssetById
{
    public class GetAssetByIdQueryRequest : IRequest<SuccessDetails<AssetDto>>
    {
        public int Id { get; set; }
    }
}
