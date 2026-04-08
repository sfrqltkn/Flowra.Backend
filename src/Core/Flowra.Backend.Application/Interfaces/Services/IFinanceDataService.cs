using Flowra.Backend.Application.DTOs.FinanceData;

namespace Flowra.Backend.Application.Interfaces.Services
{
    public interface IFinanceDataService
    {
        Task<IEnumerable<LivePriceDto>> GetGoldPricesAsync();
        Task<LivePriceDto> GetSilverPriceAsync();
        Task<IEnumerable<LivePriceDto>> GetCurrencyPricesAsync();
    }
}
