using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Assets.CreateAsset
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommandRequest, SuccessDetails<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAssetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<int>> Handle(CreateAssetCommandRequest request, CancellationToken cancellationToken)
        {
            var asset = new Asset
            {
                MonthYear = request.MonthYear,
                Name = request.Name,
                Type = request.Type,
                Amount = request.Amount,
                EstimatedUnitValue = request.EstimatedUnitValue
            };

            var writeRepository = _unitOfWork.WriteRepository<Asset, int>();

            await writeRepository.AddAsync(asset, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success(asset.Id, "Varlık başarıyla eklendi.");
        }
    }
}
