using FluentValidation;
using PollBack.Core.PollAggregate;
using PollBack.Core.PollAggregate.Commands;

namespace PollBack.Core.Validators
{
    public class PollValidator : AbstractValidator<CreatePollCommand>
    {
        public PollValidator()
        {
            RuleFor(x => x.Model.Question)
                .NotEmpty()
                .WithMessage("Te pole nie może być puste.");

            RuleFor(x => x.Model.Options as List<PollOption>)
                .NotNull()
                .WithMessage("Te pole nie może być puste.")
                .Must(x => x.Count >= 2)
                .WithMessage("Musisz podać minimum 2 opcje do wyboru.");

            RuleFor(x => x.EndOption)
                .Must(x => !string.IsNullOrEmpty(x) && (x == "0" || EndDateSetter.EndDates.ContainsKey(x)))
                .WithMessage("Te pole nie może być puste lub podano nieprawidłow");

            RuleForEach(x => x.Model.Options)
                .SetValidator(new PollOptionValidator());
        }
    }
}
