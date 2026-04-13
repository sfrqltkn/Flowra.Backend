namespace Flowra.Backend.Application.DTOs.Audit
{
    public class ActivityAuditEntry
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }

        public string RequestName { get; set; } = null!;
        public string? RequestPayload { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        public string CorrelationId { get; set; } = null!;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
