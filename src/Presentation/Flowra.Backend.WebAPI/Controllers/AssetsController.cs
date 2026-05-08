using Flowra.Backend.Application.Features.Commands.Assets.CreateAsset;
using Flowra.Backend.Application.Features.Commands.Assets.DeleteAsset;
using Flowra.Backend.Application.Features.Commands.Assets.UpdateAsset;
using Flowra.Backend.Application.Features.Queries.Assets.GetAllAssets;
using Flowra.Backend.Application.Features.Queries.Assets.GetAssetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Assets")]
    public class AssetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAssetsQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetAssetByIdQueryRequest { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAssetCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAssetCommandRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                return BadRequest("URL'deki ID ile gönderilen veri ID'si uyuşmuyor.");

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteAssetCommandRequest { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}