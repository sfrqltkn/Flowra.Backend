using Flowra.Backend.Application.Features.Queries.FinanceData.GetCurrencies;
using Flowra.Backend.Application.Features.Queries.FinanceData.GetGoldPrices;
using Flowra.Backend.Application.Features.Queries.FinanceData.GetSilverPrice;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [Authorize] // Eğer diğerleri gibi yetki gerektiriyorsa ekleyebilirsin
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Finance Data")]
    public class FinanceDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinanceDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("gold")]
        public async Task<IActionResult> GetGold(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetGoldPricesQueryRequest(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("silver")]
        public async Task<IActionResult> GetSilver(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSilverPriceQueryRequest(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("currencies")]
        public async Task<IActionResult> GetCurrencies(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCurrenciesQueryRequest(), cancellationToken);
            return Ok(result);
        }
    }
}