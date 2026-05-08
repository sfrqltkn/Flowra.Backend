using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.Persistence.Repositories;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Incomes.CreateIncome
{
    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommandRequest, SuccessDetails<int>>
    {
        private readonly IGenericRepository<Income> _repository;

        public CreateIncomeCommandHandler(IGenericRepository<Income> repository)
        {
            _repository = repository;
        }

        public async Task<SuccessDetails<int>> Handle(CreateIncomeCommandRequest request, CancellationToken cancellationToken)
        {
            var income = new Income
            {
                Name = request.Name,
                Amount = request.Amount,
                Date = request.Date,
                IsRecurring = request.IsRecurring
            };

            await _repository.AddAsync(income);

            return ResultResponse.Success(income.Id, "Gelir başarıyla eklendi.");
        }
    }
}
