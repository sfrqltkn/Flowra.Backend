using Flowra.Backend.Domain.Common;

namespace Flowra.Backend.Domain.Entities
{
    public class Allowance : BaseEntity<int>
    {
        public string PersonName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
