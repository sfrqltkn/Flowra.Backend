using Flowra.Backend.Application.DTOs.Expense;
using Flowra.Backend.Application.Interfaces.Repositories;
using Flowra.Backend.Application.Interfaces.Services;
using Flowra.Backend.Domain.Entities;

namespace Flowra.Backend.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IGenericRepository<Expense> _repository;

        public ExpenseService(IGenericRepository<Expense> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Expense?> GetExpenseByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddExpenseAsync(CreateExpenseDto dto)
        {
            var newExpense = new Expense
            {
                Name = dto.Name,
                TotalAmount = dto.TotalAmount,
                Date = dto.Date,
                IsRecurring = dto.IsRecurring,
                IsCreditCard = dto.IsCreditCard,
                MinimumPaymentAmount = dto.MinimumPaymentAmount,
                IsPaid = dto.IsPaid
            };

            await _repository.AddAsync(newExpense);
        }

        public async Task UpdateExpenseAsync(UpdateExpenseDto dto)
        {
            var existingExpense = await _repository.GetByIdAsync(dto.Id);
            if (existingExpense == null)
                throw new Exception("Güncellenmek istenen gider bulunamadı.");

            existingExpense.Name = dto.Name;
            existingExpense.TotalAmount = dto.TotalAmount;
            existingExpense.Date = dto.Date;
            existingExpense.IsRecurring = dto.IsRecurring;
            existingExpense.IsCreditCard = dto.IsCreditCard;
            existingExpense.MinimumPaymentAmount = dto.MinimumPaymentAmount;
            existingExpense.IsPaid = dto.IsPaid;

            await _repository.UpdateAsync(existingExpense);
        }

        public async Task DeleteExpenseAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
