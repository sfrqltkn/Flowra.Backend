using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Auth.ResendConfirmationEmail
{
    public class ResendConfirmationEmailCommandValidator : AbstractValidator<ResendConfirmationEmailCommandRequest>
    {
        private const int MinEmailLength = 3;
        private const int MaxEmailLength = 256;

        public ResendConfirmationEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("E-posta adresi zorunludur.")
                .Must(e => e == null || e.Trim() == e).WithMessage("E-posta adresinin başında veya sonunda boşluk olamaz.")
                .Length(MinEmailLength, MaxEmailLength).WithMessage($"E-posta adresi {MinEmailLength}-{MaxEmailLength} karakter arasında olmalıdır.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
        }
    }
}
