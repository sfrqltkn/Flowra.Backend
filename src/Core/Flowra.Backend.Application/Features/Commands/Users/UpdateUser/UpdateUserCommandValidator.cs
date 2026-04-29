
using FluentValidation;
using System.Text.RegularExpressions;

namespace Flowra.Backend.Application.Features.Commands.Users.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommandRequest>
    {
        private const int MaxEmailLength = 254;
        private const int MinNameLength = 3;
        private const int MaxNameLength = 50;
        private const int MaxPhoneLength = 20;
        private const int MinPhoneLength = 10;

        // Ad/Soyad için sadece harflere, boşluğa ve Türkçe karakterlere izin veren Regex
        private static readonly Regex NameRegex =
            new(@"^[a-zA-ZğĞüÜşŞıİöÖçÇ\s]+$", RegexOptions.Compiled);

        // Telefon Regex: Rakam, boşluk, +, - ve parantez
        private static readonly Regex PhoneRegex =
            new(@"^[\d\+\-\(\)\s]+$", RegexOptions.Compiled);

        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Kullanıcı ID zorunludur.")
                .GreaterThan(0).WithMessage("Kullanıcı ID geçerli (sıfırdan büyük) olmalıdır.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(MaxEmailLength).WithMessage($"E-posta adresi en fazla {MaxEmailLength} karakter olmalıdır.")
                .EmailAddress().WithMessage("Geçersiz e-posta formatı.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(MinNameLength).WithMessage($"Ad en az {MinNameLength} karakter olmalıdır.")
                .MaximumLength(MaxNameLength).WithMessage($"Ad en fazla {MaxNameLength} karakter olmalıdır.")
                .Matches(NameRegex).WithMessage("Ad yalnızca harf, boşluk ve Türkçe karakterler içerebilir.")
                .When(x => !string.IsNullOrEmpty(x.FirstName));

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(MinNameLength).WithMessage($"Soyad en az {MinNameLength} karakter olmalıdır.")
                .MaximumLength(MaxNameLength).WithMessage($"Soyad en fazla {MaxNameLength} karakter olmalıdır.")
                .Matches(NameRegex).WithMessage("Soyad yalnızca harf, boşluk ve Türkçe karakterler içerebilir.")
                .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(MinPhoneLength).WithMessage($"Telefon numarası en az {MinPhoneLength} karakter olmalıdır.")
                .MaximumLength(MaxPhoneLength).WithMessage($"Telefon numarası en fazla {MaxPhoneLength} karakter olmalıdır.")
                .Matches(PhoneRegex).WithMessage("Telefon numarası yalnızca rakam ve geçerli işaretler (+, -, boşluk, parantez) içerebilir.")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
}
