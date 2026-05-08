using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Income;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Application.Persistence.Repositories;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Incomes.GetIncomeById
{
    public class GetIncomeByIdQueryHandler : IRequestHandler<GetIncomeByIdQueryRequest, SuccessDetails<IncomeDto>>
    {
        private readonly IGenericRepository<Income> _repository;

        public GetIncomeByIdQueryHandler(IGenericRepository<Income> repository)
        {
            _repository = repository;
        }

        public async Task<SuccessDetails<IncomeDto>> Handle(GetIncomeByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var income = await _repository.GetByIdAsync(request.Id);

            income.ThrowIfNull("Getirilmek istenen gelir kaydı bulunamadı.");

            var incomeDto = new IncomeDto
            {
                Id = income.Id,
                Name = income.Name,
                Amount = income.Amount,
                Date = income.Date,
                IsRecurring = income.IsRecurring
            };

            return ResultResponse.Success(incomeDto, "Gelir kaydı başarıyla getirildi.");
        }
    }
}
