using FluentValidation;
using System.Text.RegularExpressions;

namespace Flowra.Backend.Application.Features.Commands.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
    {
        private const int MinUsernameLength = 3;
        private const int MaxUsernameLength = 256;
        private const int MinEmailLength = 3;
        private const int MaxEmailLength = 256;
        private const int MinPasswordLength = 6;
        private const int MaxPasswordLength = 30;


        private static readonly Regex UserNameRegex =
            new(@"^[a-zA-Z0-9._@-]+$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public LoginCommandValidator()
        {
            RuleFor(x => x.EmailOrUsername)
                     .NotEmpty().WithMessage("E-posta veya Kullanıcı adı zorunludur.");
            When(x => !string.IsNullOrEmpty(x.EmailOrUsername) && x.EmailOrUsername.Contains("@"), () =>
            {
                RuleFor(x => x.EmailOrUsername)
                    .Cascade(CascadeMode.Stop)
                    .Must(e => e == null || e.Trim() == e).WithMessage("E-posta adresinin başında veya sonunda boşluk olamaz.")
                    .Length(MinEmailLength, MaxEmailLength).WithMessage($"E-posta adresi {MinEmailLength} ile {MaxEmailLength} karakter arasında olmalıdır.")
                    .EmailAddress().WithMessage("Lütfen geçerli bir e-posta formatı giriniz.");
            })
            .Otherwise(() =>
            {
                RuleFor(x => x.EmailOrUsername)
                    .Cascade(CascadeMode.Stop)
                    .Must(e => e == null || e.Trim() == e).WithMessage("Kullanıcı adının başında veya sonunda boşluk olamaz.")
                    .Length(MinUsernameLength, MaxUsernameLength).WithMessage($"Kullanıcı adı {MinUsernameLength} ile {MaxUsernameLength} karakter arasında olmalıdır.")
                    .Matches(UserNameRegex).WithMessage("Geçersiz format. Kullanıcı adı yalnızca harf, rakam, nokta ve alt çizgi içerebilir.");
            });

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("Şifre zorunludur.")
                .Length(MinPasswordLength, MaxPasswordLength).WithMessage($"Şifre {MinPasswordLength} ile {MaxPasswordLength} karakter arasında olmalıdır.");
        }
    }
}
