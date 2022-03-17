using FluentValidation;
using PollBack.Core.Models;

namespace PollBack.Web.FluentValidation.Validators
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidator()
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
