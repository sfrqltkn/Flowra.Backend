using Flowra.Backend.Application.Features.Commands.Roles.CreateRole;
using Flowra.Backend.Application.Features.Commands.Roles.DeleteRole;
using Flowra.Backend.Application.Features.Commands.Roles.UpdateRole;
using Flowra.Backend.Application.Features.Queries.Roles.GetAllRoles;
using Flowra.Backend.Application.Features.Queries.Roles.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Roles")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllRolesQueryRequest());
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetRoleByIdQueryRequest { Id = id });
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateRoleCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRoleCommandRequest request)
        {
            request.Id = id;
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteRoleCommandRequest { Id = id });
            return Ok(response);
        }
    }
}
