using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.Persistence.Repositories;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Incomes.UpdateIncome
{
    public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommandRequest, SuccessDetails>
    {
        private readonly IGenericRepository<Income> _repository;

        public UpdateIncomeCommandHandler(IGenericRepository<Income> repository)
        {
            _repository = repository;
        }

        public async Task<SuccessDetails> Handle(UpdateIncomeCommandRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            entity.ThrowIfNull("Güncellenmek istenen gelir kaydı bulunamadı.");

            entity.Name = request.Name;
            entity.Amount = request.Amount;
            entity.Date = request.Date;
            entity.IsRecurring = request.IsRecurring;

            await _repository.UpdateAsync(entity);

            return ResultResponse.Success(Unit.Value, "Gelir başarıyla güncellendi.");
        }
    }
}
