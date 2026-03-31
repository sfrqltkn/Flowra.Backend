using Flowra.Backend.Application.DTOs.CashRecord;
using Flowra.Backend.Domain.Entities;

namespace Flowra.Backend.Application.Interfaces.Services
{
    public interface ICashRecordService { 
        Task<IEnumerable<CashRecord>> GetAllAsync(); 
        Task AddAsync(CreateCashRecordDto dto); 
        Task UpdateAsync(UpdateCashRecordDto dto); 
        Task DeleteAsync(int id); 
    }
}
