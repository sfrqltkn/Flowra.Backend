namespace Flowra.Backend.Application.DTOs.CashRecord
{
    public class CreateCashRecordDto {
        public string MonthYear { get; set; } = string.Empty;
        public decimal Balance { get; set; } 
    }
}
