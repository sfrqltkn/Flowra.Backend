using Flowra.Backend.Application.Features.Commands.Incomes.CreateIncome;
using Flowra.Backend.Application.Features.Commands.Incomes.DeleteIncome;
using Flowra.Backend.Application.Features.Commands.Incomes.UpdateIncome;
using Flowra.Backend.Application.Features.Queries.Incomes.GetAllIncomes;
using Flowra.Backend.Application.Features.Queries.Incomes.GetIncomeById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Incomes")]
    public class IncomesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncomesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllIncomesQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateIncomeCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateIncomeCommandRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteIncomeCommandRequest { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetIncomeByIdQueryRequest { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}