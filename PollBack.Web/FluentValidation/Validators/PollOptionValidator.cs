using FluentValidation;
using PollBack.Core.PollAggregate;

namespace PollBack.Web.FluentValidation.Validators
{
    public class PollOptionValidator : AbstractValidator<PollOption>
    {
        public PollOptionValidator()
        {
            RuleFor(x => x.Option)
                .NotEmpty()
                .WithMessage("Te pole nie może być puste.");
        }
    }
}
