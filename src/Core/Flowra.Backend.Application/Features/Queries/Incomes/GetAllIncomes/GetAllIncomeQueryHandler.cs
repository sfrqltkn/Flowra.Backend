using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Income;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Incomes.GetAllIncomes
{
    public class GetAllIncomesQueryHandler : IRequestHandler<GetAllIncomesQueryRequest, SuccessDetails<IEnumerable<IncomeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllIncomesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<IEnumerable<IncomeDto>>> Handle(GetAllIncomesQueryRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Income, int>();
            var incomes = await readRepository.GetListAsync(x => true, cancellationToken);

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
