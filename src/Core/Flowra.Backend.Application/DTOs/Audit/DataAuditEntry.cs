namespace Flowra.Backend.Application.DTOs.Audit
{
    public class DataAuditEntry
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }

        public string ActionType { get; set; } = null!; 
        public string EntityName { get; set; } = null!;
        public string? EntityId { get; set; }

        public object? OldValues { get; set; }
        public object? NewValues { get; set; }

        public string? IpAddress { get; set; }
        public string CorrelationId { get; set; } = null!;

        public DateTime OccurredAtUtc { get; set; }
    }
}
