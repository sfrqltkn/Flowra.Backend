namespace Flowra.Backend.Domain.Entities
{
    public class CashRecord : BaseEntity
    {
        public string MonthYear { get; set; } = string.Empty; // Örn: "2026-03"
        public decimal Balance { get; set; }
    }
}
