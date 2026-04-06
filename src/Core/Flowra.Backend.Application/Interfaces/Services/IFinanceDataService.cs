using Flowra.Backend.Application.DTOs.FinanceData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flowra.Backend.Application.Interfaces.Services
{
    public interface IFinanceDataService
    {
        Task<IEnumerable<LivePriceDto>> GetGoldPricesAsync();
        Task<LivePriceDto> GetSilverPriceAsync();
        Task<IEnumerable<LivePriceDto>> GetCurrencyPricesAsync();
    }
}
