using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Income;
using Flowra.Backend.Application.Persistence.Repositories;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Incomes.GetAllIncomes
{
    public class GetAllIncomesQueryHandler : IRequestHandler<GetAllIncomesQueryRequest, SuccessDetails<IEnumerable<IncomeDto>>>
    {
        private readonly IGenericRepository<Income> _repository;

        public GetAllIncomesQueryHandler(IGenericRepository<Income> repository)
        {
            _repository = repository;
        }

        public async Task<SuccessDetails<IEnumerable<IncomeDto>>> Handle(GetAllIncomesQueryRequest request, CancellationToken cancellationToken)
        {
            var incomes = await _repository.GetAllAsync();

            var dtos = incomes.Select(income => new IncomeDto
            {
                Id = income.Id,
                Name = income.Name,
                Amount = income.Amount,
                Date = income.Date,
                IsRecurring = income.IsRecurring
            });

            return ResultResponse.Success(dtos, "Gelirler başarıyla listelendi.");
        }
    }
}
