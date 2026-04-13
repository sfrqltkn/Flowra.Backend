namespace Flowra.Backend.Application.DTOs.Auth
{
    public class ForgotPasswordRequestDto
    {
        public string Email { get; set; } = string.Empty;
    }
}
// Reset linki/token üretmek için gönderilen email.