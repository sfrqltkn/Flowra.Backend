using Flowra.Backend.Application.DTOs.Income;
using Flowra.Backend.Domain.Entities;

namespace Flowra.Backend.Application.Services
{
    public interface IIncomeService
    {
        Task<IEnumerable<Income>> GetAllAsync();
        Task AddAsync(CreateIncomeDto dto);
        Task UpdateAsync(UpdateIncomeDto dto);
        Task DeleteAsync(int id);
    }
}
