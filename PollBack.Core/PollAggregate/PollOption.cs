using PollBack.Shared;

namespace PollBack.Core.PollAggregate
{
    public class PollOption : BaseEntity
    {
        public int PollId { get; set; }
        public string Option { get; init; }
        public int Votes { get; set; }

        public PollOption(string option)
        {
            Option = option;
        }
    }
}
