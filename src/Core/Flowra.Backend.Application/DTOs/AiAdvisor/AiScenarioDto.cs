namespace Flowra.Backend.Application.DTOs.AiAdvisor
{
    public class AiScenarioDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal FinalBalance { get; set; }
        public bool IsOptimistic { get; set; }
        public List<AiScenarioStepDto> Steps { get; set; } = new();
    }
}
