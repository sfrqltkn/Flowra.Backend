using Flowra.Backend.Application.DTOs.AiAdvisor;

namespace Flowra.Backend.Application.Interfaces.Services
{
    public interface IAiAdvisorService
    {
        Task<AiStrategyResponseDto> GetOptimizationStrategiesAsync(AiRequestDto request);
    }
}
