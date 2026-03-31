using Flowra.Backend.Application.DTOs.CashRecord;
using Flowra.Backend.Application.Interfaces.Repositories;
using Flowra.Backend.Application.Interfaces.Services;
using Flowra.Backend.Domain.Entities;

namespace Flowra.Backend.Application.Services
{
    public class CashRecordService : ICashRecordService
    {
        private readonly IGenericRepository<CashRecord> _repo;
        public CashRecordService(IGenericRepository<CashRecord> repo) => _repo = repo;
        public async Task<IEnumerable<CashRecord>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task AddAsync(CreateCashRecordDto dto) => await _repo.AddAsync(new CashRecord { MonthYear = dto.MonthYear, Balance = dto.Balance });
        public async Task UpdateAsync(UpdateCashRecordDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id) ?? throw new Exception("Bulunamadı");
            entity.MonthYear = dto.MonthYear; entity.Balance = dto.Balance;
            await _repo.UpdateAsync(entity);
        }
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
