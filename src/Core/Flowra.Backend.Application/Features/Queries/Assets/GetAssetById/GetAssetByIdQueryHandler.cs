using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Asset;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flowra.Backend.Application.Features.Queries.Assets.GetAssetById
{
    public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQueryRequest, SuccessDetails<AssetDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAssetByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<AssetDto>> Handle(GetAssetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Asset, int>();

            var asset = await readRepository.GetByIdAsync(request.Id, cancellationToken);

            asset.ThrowIfNull("Getirilmek istenen varlık kaydı bulunamadı.");

            var dto = new AssetDto
            {
                Id = asset.Id,
                MonthYear = asset.MonthYear,
                Name = asset.Name,
                Type = asset.Type,
                Amount = asset.Amount,
                EstimatedUnitValue = asset.EstimatedUnitValue
            };

            return ResultResponse.Success(dto, "Varlık başarıyla getirildi.");
        }
    }
}
