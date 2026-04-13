using Flowra.Backend.Application.DTOs.Asset;
using Flowra.Backend.Application.Persistence.Repositories;
using Flowra.Backend.Domain.Entities;

namespace Flowra.Backend.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IGenericRepository<Asset> _repo;
        public AssetService(IGenericRepository<Asset> repo) => _repo = repo;

        public async Task<IEnumerable<Asset>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task AddAsync(CreateAssetDto dto)
        {
            await _repo.AddAsync(
                new Asset
                {
                    MonthYear = dto.MonthYear,
                    Name = dto.Name,
                    Type = dto.Type,
                    Amount = dto.Amount,
                    EstimatedUnitValue = dto.EstimatedUnitValue
                });
        }

        public async Task UpdateAsync(UpdateAssetDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id) ?? throw new Exception("Bulunamadı");

            entity.MonthYear = dto.MonthYear; 
            entity.Name = dto.Name;
            entity.Type = dto.Type;
            entity.Amount = dto.Amount;
            entity.EstimatedUnitValue = dto.EstimatedUnitValue;

            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
