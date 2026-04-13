using Flowra.Backend.Application.DTOs.Allowence;
using Flowra.Backend.Domain.Entities;

namespace Flowra.Backend.Application.Services
{
    public interface IAllowanceService
    {
        Task<IEnumerable<Allowance>> GetAllAsync(); 
        Task AddAsync(CreateAllowanceDto dto); 
        Task UpdateAsync(UpdateAllowanceDto dto); 
        Task DeleteAsync(int id);
    }
}
