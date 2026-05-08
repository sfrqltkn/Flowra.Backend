using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Allowances.DeleteAllowance
{
    public class DeleteAllowanceCommandHandler : IRequestHandler<DeleteAllowanceCommandRequest, SuccessDetails>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAllowanceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails> Handle(DeleteAllowanceCommandRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Allowance, int>();
            var writeRepository = _unitOfWork.WriteRepository<Allowance, int>();

            var entity = await readRepository.GetByIdAsync(request.Id, cancellationToken);

            entity.ThrowIfNull("Silinmek istenen ödenek kaydı bulunamadı.");
            entity.MarkAsDeleted();

            writeRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success("Ödenek başarıyla silindi.");
        }
    }
}
