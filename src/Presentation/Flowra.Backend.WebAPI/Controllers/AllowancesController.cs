using Flowra.Backend.Application.Features.Commands.Allowances.CreateAllowance;
using Flowra.Backend.Application.Features.Commands.Allowances.DeleteAllowance;
using Flowra.Backend.Application.Features.Commands.Allowances.UpdateAllowance;
using Flowra.Backend.Application.Features.Queries.Allowances.GetAllAllowances;
using Flowra.Backend.Application.Features.Queries.Allowances.GetAllowanceById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Allowances")]
    public class AllowancesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AllowancesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAllowancesQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetAllowanceByIdQueryRequest { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAllowanceCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAllowanceCommandRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                return BadRequest("URL'deki ID ile gönderilen veri ID'si uyuşmuyor.");

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteAllowanceCommandRequest { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}