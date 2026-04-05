using Flowra.Backend.Application.DTOs.AiAdvisor;
using Flowra.Backend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Flowra.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("AI Advisor")]
    public class AiAdvisorController : ControllerBase
    {
        private readonly IAiAdvisorService _aiService;

        public AiAdvisorController(IAiAdvisorService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze([FromBody] AiRequestDto request)
        {
            try
            {
                var result = await _aiService.GetOptimizationStrategiesAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Yapay Zeka Analiz Hatası", Details = ex.Message });
            }
        }
    }
}
