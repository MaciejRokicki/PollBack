using FluentValidation;
using PollBack.Core.Models;

namespace PollBack.Core.Validators
{
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Te pole jest wymagane.")
                .EmailAddress()
                .WithMessage("Niepoprawny format adresu email.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Te pole jest wymagane.")
                .MinimumLength(6)
                .WithMessage("Minimum 6 znaków.");
        }
    }
}
