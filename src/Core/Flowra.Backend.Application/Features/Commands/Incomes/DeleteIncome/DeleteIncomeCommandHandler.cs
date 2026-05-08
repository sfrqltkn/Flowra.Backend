using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.Persistence.Repositories;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Incomes.DeleteIncome
{
    public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommandRequest, SuccessDetails<Unit>>
    {
        private readonly IGenericRepository<Income> _repository;

        public DeleteIncomeCommandHandler(IGenericRepository<Income> repository)
        {
            _repository = repository;
        }

        public async Task<SuccessDetails<Unit>> Handle(DeleteIncomeCommandRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            entity.ThrowIfNull("Silinmek istenen gelir kaydı bulunamadı.");

            await _repository.DeleteAsync(request.Id);

            return ResultResponse.Success(Unit.Value, "Gelir başarıyla silindi.");
        }
    }
}
