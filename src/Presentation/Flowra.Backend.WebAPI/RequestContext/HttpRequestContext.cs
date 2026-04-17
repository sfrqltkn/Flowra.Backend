using Flowra.Backend.Application.Abstractions.Presentation;
using System.Security.Claims;
using System.Globalization;

namespace Flowra.Backend.WebAPI.RequestContext
{
    public sealed class HttpRequestContext : IRequestContext
    {
        private readonly IHttpContextAccessor _http;

        public HttpRequestContext(IHttpContextAccessor http)
        {
            _http = http;
        }

        private HttpContext? HttpContext => _http.HttpContext;

        public string CorrelationId =>
            HttpContext?.Items["CorrelationId"]?.ToString()
            ?? HttpContext?.TraceIdentifier
            ?? "unknown";

        public int? UserId =>
            int.TryParse(HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id)
                ? id
                : null;

        public string? UserName =>
           HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

        public string? Email =>
             HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value
             ?? HttpContext?.User?.FindFirst("email")?.Value;

        public string? IpAddress =>
            HttpContext?.Connection?.RemoteIpAddress?.ToString();

        public ClaimsPrincipal? User =>
            _http.HttpContext?.User;

        public string? UserAgent
        {
            get
            {
                var value = HttpContext?.Request?.Headers["User-Agent"].ToString();
                return string.IsNullOrWhiteSpace(value) ? null : value;
            }
        }

        public string Culture =>
            CultureInfo.CurrentUICulture.Name;
    }
}
