
namespace Flowra.Backend.Application.Common.Responses
{
    public class ProblemDetails
    {
        public string Type { get; set; } = "";
        public string Title { get; set; } = "";
        public int Status { get; set; }
        public string Detail { get; set; } = "";
        public string Instance { get; set; } = "";
        public string CorrelationId { get; set; } = "";
        public List<string>? Errors { get; set; }
    }
}
