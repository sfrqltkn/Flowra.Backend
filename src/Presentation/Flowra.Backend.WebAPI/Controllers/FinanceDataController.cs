using Flowra.Backend.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Finance Data")]
    public class FinanceDataController : ControllerBase
    {
        private readonly IFinanceDataService _financeService;

        public FinanceDataController(IFinanceDataService financeService)
        {
            _financeService = financeService;
        }

        [HttpGet("gold")]
        public async Task<IActionResult> GetGold() => Ok(await _financeService.GetGoldPricesAsync());

        [HttpGet("silver")]
        public async Task<IActionResult> GetSilver() => Ok(await _financeService.GetSilverPriceAsync());

        [HttpGet("currencies")]
        public async Task<IActionResult> GetCurrencies() => Ok(await _financeService.GetCurrencyPricesAsync());
    }
}
