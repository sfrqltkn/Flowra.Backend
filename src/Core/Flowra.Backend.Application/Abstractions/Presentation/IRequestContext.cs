namespace Flowra.Backend.Application.Abstractions.Presentation
{
    public interface IRequestContext
    {
        string CorrelationId { get; }
        int? UserId { get; }
        string? UserName { get; }
        string? IpAddress { get; }
        string? UserAgent { get; }
        string Culture { get; }
    }
}
