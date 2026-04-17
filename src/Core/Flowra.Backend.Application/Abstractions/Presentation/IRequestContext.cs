using System.Security.Claims;

namespace Flowra.Backend.Application.Abstractions.Presentation
{
    public interface IRequestContext
    {
        string CorrelationId { get; }
        int? UserId { get; }
        string? UserName { get; }
        string? Email { get; }
        string? IpAddress { get; }
        string? UserAgent { get; }
        string Culture { get; }
        ClaimsPrincipal? User { get; }
    }
}
