using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Incomes.CreateIncome
{
    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommandRequest, SuccessDetails<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateIncomeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            var writeRepository = _unitOfWork.WriteRepository<Income, int>();
            await writeRepository.AddAsync(income, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success(income.Id, "Gelir başarıyla eklendi.");
        }
    }
}
