using FluentValidation;
using PollBack.Core.PollAggregate.Commands;

namespace PollBack.Web.FluentValidation.Validators
{
    public class VotePollCommandValidator : AbstractValidator<VotePollCommand>
    {
        public VotePollCommandValidator()
        {
            RuleFor(x => x.PollId)
                .NotEmpty()
                .WithMessage("Te pole nie może być puste.");

            RuleFor(x => x.PollOptionIds as List<int>)
                .Must(x => x.Count > 1)
                .WithMessage("Musisz wybrać przynajmniej jedną opcję.");
        }
    }
}
