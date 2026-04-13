namespace Flowra.Backend.Application.DTOs.Auth
{
    public class ResetPasswordRequestDto
    {
        public int UserId { get; set; } 
        public string ResetToken { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmNewPassword { get; set; } = null!;
    }
}
// Reset token + yeni şifre işlemi.