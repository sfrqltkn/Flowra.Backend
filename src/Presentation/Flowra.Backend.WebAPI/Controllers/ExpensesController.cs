using Flowra.Backend.Application.Features.Commands.Expenses.CreateExpense;
using Flowra.Backend.Application.Features.Commands.Expenses.DeleteExpense;
using Flowra.Backend.Application.Features.Commands.Expenses.UpdateExpense;
using Flowra.Backend.Application.Features.Queries.Expenses.GetAllExpenses;
using Flowra.Backend.Application.Features.Queries.Expenses.GetExpenseById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Expenses")]
    public class ExpensesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllExpensesQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetExpenseByIdQueryRequest { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateExpenseCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateExpenseCommandRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                return BadRequest("URL'deki ID ile gönderilen veri ID'si uyuşmuyor.");

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteExpenseCommandRequest { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}