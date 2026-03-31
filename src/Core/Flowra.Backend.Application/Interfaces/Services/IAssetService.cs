using Flowra.Backend.Application.DTOs.Asset;
using Flowra.Backend.Domain.Entities;

namespace Flowra.Backend.Application.Interfaces.Services
{
    public interface IAssetService {
        Task<IEnumerable<Asset>> GetAllAsync(); 
        Task AddAsync(CreateAssetDto dto); 
        Task UpdateAsync(UpdateAssetDto dto); 
        Task DeleteAsync(int id); 
    }
}
