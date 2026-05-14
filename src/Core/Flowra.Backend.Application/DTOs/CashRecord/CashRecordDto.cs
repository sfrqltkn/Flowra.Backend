namespace Flowra.Backend.Application.DTOs.CashRecord
{
    public class CashRecordDto
    {
        public int Id { get; set; }
        public string MonthYear { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
