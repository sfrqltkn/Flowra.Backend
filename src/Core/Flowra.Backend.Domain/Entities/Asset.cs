using Flowra.Backend.Domain.Common;

namespace Flowra.Backend.Domain.Entities
{
    public class Asset : BaseEntity<int>
    {
        public DateTime MonthYear { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal EstimatedUnitValue { get; set; }
    }
}
