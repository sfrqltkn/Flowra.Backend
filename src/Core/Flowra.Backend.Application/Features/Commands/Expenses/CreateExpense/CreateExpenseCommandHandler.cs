using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Expenses.CreateExpense
{
    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommandRequest, SuccessDetails<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateExpenseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<int>> Handle(CreateExpenseCommandRequest request, CancellationToken cancellationToken)
        {
            var expense = new Expense
            {
                Name = request.Name,
                TotalAmount = request.TotalAmount,
                Date = request.Date,
                IsRecurring = request.IsRecurring,
                IsCreditCard = request.IsCreditCard,
                MinimumPaymentAmount = request.MinimumPaymentAmount,
                IsPaid = request.IsPaid
            };

            var writeRepository = _unitOfWork.WriteRepository<Expense, int>();

            await writeRepository.AddAsync(expense, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success(expense.Id, "Gider başarıyla eklendi.");
        }
    }
}
