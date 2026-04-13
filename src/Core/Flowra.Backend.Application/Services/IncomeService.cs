using Flowra.Backend.Application.DTOs.Income;
using Flowra.Backend.Application.Persistence.Repositories;
using Flowra.Backend.Domain.Entities;

namespace Flowra.Backend.Application.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IGenericRepository<Income> _repo;
        public IncomeService(IGenericRepository<Income> repo) => _repo = repo;

        public async Task<IEnumerable<Income>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task AddAsync(CreateIncomeDto dto)
        {
            await _repo.AddAsync(new Income { Name = dto.Name, Amount = dto.Amount, Date = dto.Date, IsRecurring = dto.IsRecurring });
        }

        public async Task UpdateAsync(UpdateIncomeDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id) ?? throw new Exception("Bulunamadı");
            entity.Name = dto.Name; entity.Amount = dto.Amount; entity.Date = dto.Date; entity.IsRecurring = dto.IsRecurring;
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
