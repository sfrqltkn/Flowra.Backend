namespace Flowra.Backend.Application.DTOs.Auth
{
    public class ChangePasswordRequestDto
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmNewPassword { get; set; } = null!;
    }
}
// Kullanıcı giriş yapmışken şifre değiştirme.