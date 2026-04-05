namespace Flowra.Backend.Application.DTOs.AiAdvisor
{
    public class AiStrategyResponseDto
    {
        public string Summary { get; set; } = string.Empty;
        public List<AiScenarioDto> Scenarios { get; set; } = new();
    }
}
