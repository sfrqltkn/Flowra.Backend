using Flowra.Backend.Application.DTOs.Income;
using Flowra.Backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Incomes")]
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeService _service;
        public IncomesController(IIncomeService service) => _service = service;

        [HttpGet] public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());
        [HttpPost] public async Task<IActionResult> Post([FromBody] CreateIncomeDto dto) {
            await _service.AddAsync(dto); 
            return Ok(); 
        }
        [HttpPut("{id}")] public async Task<IActionResult> Put(int id, [FromBody] UpdateIncomeDto dto) { 
            if (id != dto.Id) 
            return BadRequest(); 
            await _service.UpdateAsync(dto); return Ok(); 
        }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { 
            await _service.DeleteAsync(id); 
            return Ok(); 
        }
    }
}
