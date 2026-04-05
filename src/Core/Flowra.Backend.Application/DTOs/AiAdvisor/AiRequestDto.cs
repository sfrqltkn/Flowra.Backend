namespace Flowra.Backend.Application.DTOs.AiAdvisor
{
    public class AiRequestDto
    {
        public string MonthYear { get; set; } = string.Empty;
        public decimal CurrentCash { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal MinimumExpense { get; set; }
        public decimal TotalAssets { get; set; }
    }
}
