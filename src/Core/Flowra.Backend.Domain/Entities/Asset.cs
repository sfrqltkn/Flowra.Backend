namespace Flowra.Backend.Domain.Entities
{
    public class Asset : BaseEntity
    {
        public string MonthYear { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal EstimatedUnitValue { get; set; }
    }
}
