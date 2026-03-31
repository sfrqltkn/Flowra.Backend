namespace Flowra.Backend.Application.DTOs.Income
{
    public class CreateIncomeDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; }
    }
}
