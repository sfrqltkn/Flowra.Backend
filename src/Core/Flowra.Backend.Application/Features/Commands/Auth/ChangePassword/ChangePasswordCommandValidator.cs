using FluentValidation;
using System.Text.RegularExpressions;

namespace Flowra.Backend.Application.Features.Commands.Auth.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommandRequest>
    {
        private const int MinPasswordLength = 6;
        private const int MaxPasswordLength = 30;

        // şifre politikası regex'i: En az 1 büyük, 1 küçük, 1 rakam, 1 özel karakter
        private static readonly Regex StrongPasswordRegex =
            new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$",
                RegexOptions.Compiled);

        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.OldPassword)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty().WithMessage("Mevcut şifrenizi girmeniz zorunludur.")
                 .Must(p => p == null || p.Trim() == p).WithMessage("Mevcut şifrenizin başında veya sonunda boşluk olamaz.")
                 .Length(MinPasswordLength, MaxPasswordLength).WithMessage($"Mevcut şifreniz {MinPasswordLength} ile {MaxPasswordLength} karakter arasında olmalıdır.")
                 .Must(IsStrongPassword).WithMessage("Mevcut şifreniz; büyük harf, küçük harf, rakam ve özel karakter içermelidir.");

            RuleFor(x => x.NewPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Yeni şifre zorunludur.")
                .Must(p => p == null || p.Trim() == p).WithMessage("Yeni şifrenin başında veya sonunda boşluk olamaz.")
                .Length(MinPasswordLength, MaxPasswordLength).WithMessage($"Yeni şifre {MinPasswordLength} ile {MaxPasswordLength} karakter arasında olmalıdır.")
                .Must(IsStrongPassword).WithMessage("Yeni şifre; büyük harf, küçük harf, rakam ve özel karakter içermelidir.")
                .Must((req, newPassword) => newPassword != req.OldPassword).WithMessage("Yeni şifre, mevcut şifre ile aynı olamaz.");

            RuleFor(x => x.ConfirmNewPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Yeni şifre onayı zorunludur.")
                .Must(p => p == null || p.Trim() == p).WithMessage("Yeni şifre onayının başında veya sonunda boşluk olamaz.")
                .Length(MinPasswordLength, MaxPasswordLength).WithMessage($"Yeni şifre onayı {MinPasswordLength} ile {MaxPasswordLength} karakter arasında olmalıdır.")
                .Equal(x => x.NewPassword).WithMessage("Yeni şifre ile onay şifresi eşleşmiyor.");
        }

        private static bool IsStrongPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return StrongPasswordRegex.IsMatch(password);
        }
    }
}
