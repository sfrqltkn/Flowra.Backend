using FluentValidation;

namespace Flowra.Backend.Application.Features.Commands.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi giriniz.")
                .MaximumLength(256).WithMessage("E-posta adresi en fazla 256 karakter olabilir.");

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ad alanı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Ad alanı en fazla 100 karakter olabilir.");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Soyad alanı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Soyad alanı en fazla 100 karakter olabilir.");
        }
    }
}
