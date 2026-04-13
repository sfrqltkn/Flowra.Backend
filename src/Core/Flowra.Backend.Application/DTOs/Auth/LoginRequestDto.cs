namespace Flowra.Backend.Application.DTOs.Auth
{
    public class LoginRequestDto
    {
        public string EmailOrUsername { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
// Kullanıcı giriş isteği.