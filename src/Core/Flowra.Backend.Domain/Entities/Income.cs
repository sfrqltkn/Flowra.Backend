namespace Flowra.Backend.Domain.Entities
{
    public class Income : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; }
    }
}
