namespace Flowra.Backend.Domain.Entities
{
    public class Allowance : BaseEntity
    {
        public string PersonName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
