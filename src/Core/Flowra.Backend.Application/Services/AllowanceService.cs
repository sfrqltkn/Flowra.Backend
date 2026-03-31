using Flowra.Backend.Application.DTOs.Allowence;
using Flowra.Backend.Application.Interfaces.Repositories;
using Flowra.Backend.Application.Interfaces.Services;
using Flowra.Backend.Domain.Entities;

namespace Flowra.Backend.Application.Services
{
    public class AllowanceService : IAllowanceService
    {
        private readonly IGenericRepository<Allowance> _repo;
        public AllowanceService(IGenericRepository<Allowance> repo) => _repo = repo;
        public async Task<IEnumerable<Allowance>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task AddAsync(CreateAllowanceDto dto) => await _repo.AddAsync(new Allowance { PersonName = dto.PersonName, Amount = dto.Amount });
        public async Task UpdateAsync(UpdateAllowanceDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id) ?? throw new Exception("Bulunamadı");
            entity.PersonName = dto.PersonName; entity.Amount = dto.Amount;
            await _repo.UpdateAsync(entity);
        }
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
