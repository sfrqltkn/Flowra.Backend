using Flowra.Backend.Application.DTOs.Asset;
using Flowra.Backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Assets")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _service;
        public AssetsController(IAssetService service) => _service = service;
        [HttpGet] public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());
        [HttpPost] public async Task<IActionResult> Post([FromBody] CreateAssetDto dto) 
        { 
            await _service.AddAsync(dto); return Ok(); 
        }
        [HttpPut("{id}")] public async Task<IActionResult> Put(int id, [FromBody] UpdateAssetDto dto) 
        { 
            if (id != dto.Id) 
            return BadRequest(); 
            await _service.UpdateAsync(dto); 
            return Ok(); 
        }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) 
        { 
            await _service.DeleteAsync(id); 
            return Ok(); 
        }
    }
}
