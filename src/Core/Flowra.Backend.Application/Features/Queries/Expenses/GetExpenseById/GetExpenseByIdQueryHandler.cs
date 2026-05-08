using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Expense;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Expenses.GetExpenseById
{
    public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQueryRequest, SuccessDetails<ExpenseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetExpenseByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<ExpenseDto>> Handle(GetExpenseByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Expense, int>();

            var expense = await readRepository.GetByIdAsync(request.Id, cancellationToken);

            expense.ThrowIfNull("Getirilmek istenen gider kaydı bulunamadı.");

            var dto = new ExpenseDto
            {
                Id = expense.Id,
                Name = expense.Name,
                TotalAmount = expense.TotalAmount,
                Date = expense.Date,
                IsRecurring = expense.IsRecurring,
                IsCreditCard = expense.IsCreditCard,
                MinimumPaymentAmount = expense.MinimumPaymentAmount,
                IsPaid = expense.IsPaid
            };

            return ResultResponse.Success(dto, "Gider başarıyla getirildi.");
        }
    }
}
