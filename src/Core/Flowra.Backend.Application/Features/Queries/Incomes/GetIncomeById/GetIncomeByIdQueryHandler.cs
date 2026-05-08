using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Income;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Incomes.GetIncomeById
{
    public class GetIncomeByIdQueryHandler : IRequestHandler<GetIncomeByIdQueryRequest, SuccessDetails<IncomeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetIncomeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<IncomeDto>> Handle(GetIncomeByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Income, int>();

            var income = await readRepository.GetByIdAsync(request.Id, cancellationToken);

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
