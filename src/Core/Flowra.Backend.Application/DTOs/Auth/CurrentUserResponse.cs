namespace Flowra.Backend.Application.DTOs.Auth
{
    public sealed class CurrentUserResponse
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string> Roles { get; set; } = new();
        public List<string> Permissions { get; set; } = new();
    }
}