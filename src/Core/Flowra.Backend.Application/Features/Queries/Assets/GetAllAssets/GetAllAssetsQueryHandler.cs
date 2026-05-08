using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Asset;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Assets.GetAllAssets
{
    public class GetAllAssetsQueryHandler : IRequestHandler<GetAllAssetsQueryRequest, SuccessDetails<IEnumerable<AssetDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAssetsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<IEnumerable<AssetDto>>> Handle(GetAllAssetsQueryRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Asset, int>();

            var assets = await readRepository.GetListAsync(x => true, cancellationToken);

            var dtos = assets.Select(a => new AssetDto
            {
                Id = a.Id,
                MonthYear = a.MonthYear,
                Name = a.Name,
                Type = a.Type,
                Amount = a.Amount,
                EstimatedUnitValue = a.EstimatedUnitValue
            });

            return ResultResponse.Success(dtos, "Varlıklar başarıyla listelendi.");
        }
    }
}
