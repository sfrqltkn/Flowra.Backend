using Flowra.Backend.Application.Features.Commands.CashRecords.CreateCashRecord;
using Flowra.Backend.Application.Features.Commands.CashRecords.DeleteCashRecord;
using Flowra.Backend.Application.Features.Commands.CashRecords.UpdateCashRecord;
using Flowra.Backend.Application.Features.Queries.CashRecords.GetAllCashRecords;
using Flowra.Backend.Application.Features.Queries.CashRecords.GetCashRecordById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Cash Records")]
    public class CashRecordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CashRecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCashRecordsQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetCashRecordByIdQueryRequest { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCashRecordCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCashRecordCommandRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                return BadRequest("URL'deki ID ile gönderilen veri ID'si uyuşmuyor.");

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteCashRecordCommandRequest { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}