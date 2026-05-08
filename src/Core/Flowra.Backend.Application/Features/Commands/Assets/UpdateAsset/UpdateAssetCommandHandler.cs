using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Assets.UpdateAsset
{
    public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommandRequest, SuccessDetails>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAssetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails> Handle(UpdateAssetCommandRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Asset, int>();
            var writeRepository = _unitOfWork.WriteRepository<Asset, int>();

            var entity = await readRepository.GetByIdAsync(request.Id, cancellationToken);

            entity.ThrowIfNull("Güncellenmek istenen varlık kaydı bulunamadı.");

            entity.MonthYear = request.MonthYear;
            entity.Name = request.Name;
            entity.Type = request.Type;
            entity.Amount = request.Amount;
            entity.EstimatedUnitValue = request.EstimatedUnitValue;

            writeRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success("Varlık başarıyla güncellendi.");
        }
    }
}
