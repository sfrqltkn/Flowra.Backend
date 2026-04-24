namespace Flowra.Backend.Application.DTOs.Auth
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public List<string> Roles { get; set; } = new();
        public bool RequiresPasswordReset { get; set; }
        public string? ResetPasswordToken { get; set; }
    }
}
