namespace Flowra.Backend.Application.DTOs.Auth
{
    public class RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}

// Eski token  yeni token üretme isteği.