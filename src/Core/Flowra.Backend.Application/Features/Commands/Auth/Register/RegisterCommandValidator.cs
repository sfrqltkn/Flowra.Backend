using FluentValidation;
using System.Text.RegularExpressions;

namespace Flowra.Backend.Application.Features.Commands.Auth.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        private const int MinPasswordLength = 6;
        private const int MaxPasswordLength = 30;

        private const int MinUserNameLength = 3;
        private const int MaxUserNameLength = 256;

        private const int MinEmailLength = 3;
        private const int MaxEmailLength = 256;

        private const int MinNameLength = 3;
        private const int MaxNameLength = 100;

        private const int MinPhoneLength = 10;
        private const int MaxPhoneLength = 30;

        // şifre politikası regex'i: En az 1 büyük, 1 küçük, 1 rakam, 1 özel karakter
        private static readonly Regex StrongPasswordRegex =
            new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$",
                RegexOptions.Compiled);

        // Ad/Soyad için sadece harflere, boşluğa ve Türkçe karakterlere izin veren Regex 
        private static readonly Regex NameRegex =
            new(@"^[a-zA-ZğĞüÜşŞıİöÖçÇ\s]+$", RegexOptions.Compiled);

        // Telefon Regex: Rakam, boşluk, +, - ve parantez
        private static readonly Regex PhoneRegex =
            new(@"^[\d\+\-\(\)\s]+$", RegexOptions.Compiled);

        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
                .Must(e => e == null || e.Trim() == e).WithMessage("Kullanıcı adının başında veya sonunda boşluk olamaz.")
                .Length(MinUserNameLength, MaxUserNameLength).WithMessage($"Kullanıcı adı en az {MinUserNameLength}-{MaxUserNameLength} karakter arasında olmalıdır.")
                .Matches(@"^[a-zA-Z0-9_.]+$").WithMessage("Kullanıcı adı yalnızca harf, rakam, nokta ve alt çizgi içerebilir.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("E-posta adresi zorunludur.")
                .Must(e => e == null || e.Trim() == e).WithMessage("E-posta adresinin başında veya sonunda boşluk olamaz.")
                .Length(MinEmailLength, MaxEmailLength).WithMessage($"E-posta adresi {MinEmailLength}-{MaxEmailLength} karakter arasında olmalıdır.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ad zorunludur.")
                .Must(n => n == null || n.Trim() == n).WithMessage("Adın başında veya sonunda boşluk bırakamazsınız.")
                .Length(MinNameLength, MaxNameLength).WithMessage($"Ad {MinNameLength} ile {MaxNameLength} karakter arasında olmalıdır.")
                .Matches(NameRegex).WithMessage("Ad yalnızca harf ve boşluk içerebilir.");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Soyad zorunludur.")
                .Must(n => n == null || n.Trim() == n).WithMessage("Soyadın başında veya sonunda boşluk bırakamazsınız.")
                .Length(MinNameLength, MaxNameLength).WithMessage($"Soyad {MinNameLength} ile {MaxNameLength} karakter arasında olmalıdır.")
                .Matches(NameRegex).WithMessage("Soyad yalnızca harf ve boşluk içerebilir.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Şifre zorunludur.")
                .Must(p => p == null || p.Trim() == p).WithMessage("Şifrenin başında veya sonunda boşluk olamaz.")
                .Length(MinPasswordLength, MaxPasswordLength).WithMessage($"Şifre {MinPasswordLength} ile {MaxPasswordLength} karakter arasında olmalıdır.")
                .Must(IsStrongPassword).WithMessage("Şifre; büyük harf, küçük harf, rakam ve özel karakter içermelidir.");

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Telefon numarası zorunludur.")
                .Must(p => p == null || p.Trim() == p).WithMessage("Telefon numarasının başında veya sonunda boşluk olamaz.")
                .Length(MinPhoneLength, MaxPhoneLength).WithMessage($"Telefon numarası {MinPhoneLength} ile {MaxPhoneLength} karakter arasında olmalıdır.")
                .Matches(PhoneRegex).WithMessage("Telefon numarası yalnızca rakam ve geçerli işaretler (+, -, boşluk, parantez) içerebilir.");
        }

        private static bool IsStrongPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return StrongPasswordRegex.IsMatch(password);
        }
    }
}
