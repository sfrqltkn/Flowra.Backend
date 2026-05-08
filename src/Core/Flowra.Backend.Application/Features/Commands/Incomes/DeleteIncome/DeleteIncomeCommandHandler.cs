using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Incomes.DeleteIncome
{
    public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommandRequest, SuccessDetails>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteIncomeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails> Handle(DeleteIncomeCommandRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Income, int>();
            var writeRepository = _unitOfWork.WriteRepository<Income, int>();

            var entity = await readRepository.GetByIdAsync(request.Id, cancellationToken);

            entity.ThrowIfNull("Silinmek istenen gelir kaydı bulunamadı.");
            entity.MarkAsDeleted();

            writeRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success("Gelir başarıyla silindi.");
        }
    }
}
