using Flowra.Backend.Domain.Common;

namespace Flowra.Backend.Domain.Entities
{
    public class CashRecord : BaseEntity<int>
    {
        public string MonthYear { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
