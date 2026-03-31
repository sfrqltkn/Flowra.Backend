using Flowra.Backend.Application.DTOs.Allowence;
using Flowra.Backend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Allowances")]
    public class AllowancesController : ControllerBase
    {
        private readonly IAllowanceService _service;
        public AllowancesController(IAllowanceService service) => _service = service;
        [HttpGet] public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());
        [HttpPost] public async Task<IActionResult> Post([FromBody] CreateAllowanceDto dto) 
        { 
            await _service.AddAsync(dto); 
            return Ok(); 
        }
        [HttpPut("{id}")] public async Task<IActionResult> Put(int id, [FromBody] UpdateAllowanceDto dto) 
        { 
            if (id != dto.Id) 
            return BadRequest(); 
            await _service.UpdateAsync(dto);
            return Ok(); 
        }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { 
            await _service.DeleteAsync(id); 
            return Ok(); 
        }
    }
}
