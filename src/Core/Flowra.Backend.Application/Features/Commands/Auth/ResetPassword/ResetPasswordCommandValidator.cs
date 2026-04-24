using FluentValidation;
using System.Text.RegularExpressions;

namespace Flowra.Backend.Application.Features.Commands.Auth.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommandRequest>
    {
        private const int MinPasswordLength = 6;
        private const int MaxPasswordLength = 30;

        // şifre politikası regex'i: En az 1 büyük, 1 küçük, 1 rakam, 1 özel karakter
        private static readonly Regex StrongPasswordRegex =
            new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$",
                RegexOptions.Compiled);

        // URL-safe token (Base64 / Identity token uyumlu) format kontrolü
        private static readonly Regex ResetTokenFormatRegex =
            new(@"^[A-Za-z0-9\-_+=/%]+$",
                RegexOptions.Compiled);

        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty().WithMessage("Kullanıcı ID zorunludur.")
                 .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID giriniz.");

            RuleFor(x => x.ResetToken)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Reset token'ı zorunludur.")
                .Matches(ResetTokenFormatRegex).WithMessage("Geçersiz reset token formatı.");

            RuleFor(x => x.NewPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Şifre zorunludur.")
                .Must(p => p == null || p.Trim() == p).WithMessage("Şifrenin başında veya sonunda boşluk olamaz.")
                .Length(MinPasswordLength, MaxPasswordLength).WithMessage($"Şifre {MinPasswordLength} ile {MaxPasswordLength} karakter arasında olmalıdır.")
                .Must(IsStrongPassword).WithMessage("Şifre; büyük harf, küçük harf, rakam ve özel karakter içermelidir.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Yeni şifre onayı zorunludur.")
                .Must(p => p == null || p.Trim() == p).WithMessage("Şifrenin başında veya sonunda boşluk olamaz.")
                .Equal(x => x.NewPassword)
                .WithMessage("Yeni şifre ile onay şifresi eşleşmiyor.");
        }

        private static bool IsStrongPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return StrongPasswordRegex.IsMatch(password);
        }
    }
}
