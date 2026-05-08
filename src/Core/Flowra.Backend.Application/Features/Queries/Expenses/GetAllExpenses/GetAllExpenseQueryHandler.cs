using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Expense;
using Flowra.Backend.Domain.Entities;
using MediatR;


namespace Flowra.Backend.Application.Features.Queries.Expenses.GetAllExpenses
{
    public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQueryRequest, SuccessDetails<IEnumerable<ExpenseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllExpensesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<IEnumerable<ExpenseDto>>> Handle(GetAllExpensesQueryRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Expense, int>();

            var expenses = await readRepository.GetListAsync(x => true, cancellationToken);

            var dtos = expenses.Select(e => new ExpenseDto
            {
                Id = e.Id,
                Name = e.Name,
                TotalAmount = e.TotalAmount,
                Date = e.Date,
                IsRecurring = e.IsRecurring,
                IsCreditCard = e.IsCreditCard,
                MinimumPaymentAmount = e.MinimumPaymentAmount,
                IsPaid = e.IsPaid
            });

            return ResultResponse.Success(dtos, "Giderler başarıyla listelendi.");
        }
    }
}
