using Flowra.Backend.Application.DTOs.Expense;
using Flowra.Backend.Domain.Entities;

namespace Flowra.Backend.Application.Interfaces.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<Expense?> GetExpenseByIdAsync(int id);
        Task AddExpenseAsync(CreateExpenseDto dto);
        Task UpdateExpenseAsync(UpdateExpenseDto dto);
        Task DeleteExpenseAsync(int id);
    }
}
