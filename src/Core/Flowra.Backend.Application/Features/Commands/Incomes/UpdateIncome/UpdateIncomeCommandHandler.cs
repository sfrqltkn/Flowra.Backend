using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Incomes.UpdateIncome
{
    public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommandRequest, SuccessDetails>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateIncomeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails> Handle(UpdateIncomeCommandRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Income, int>();
            var writeRepository = _unitOfWork.WriteRepository<Income, int>();

            var entity = await readRepository.GetByIdAsync(request.Id, cancellationToken);

            entity.ThrowIfNull("Güncellenmek istenen gelir kaydı bulunamadı.");

            entity.Name = request.Name;
            entity.Amount = request.Amount;
            entity.Date = request.Date;
            entity.IsRecurring = request.IsRecurring;

            writeRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success(entity.Id,"Gelir başarıyla güncellendi.");
        }
    }
}
