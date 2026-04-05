namespace Flowra.Backend.Application.DTOs.AiAdvisor
{
    public class AiScenarioStepDto
    {
        public string StepName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal RunningBalance { get; set; }
        public bool IsPositive { get; set; }
    }
}
