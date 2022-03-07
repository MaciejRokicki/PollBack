using PollBack.Shared;

namespace PollBack.Core.PollAggregate
{
    public class Poll : BaseEntity
    {
        public string Question { get; set; }
        public bool IsDraft { get; set; }
        public DateTime Created { get; init; }
        public DateTime End { get; set; }    
        public ICollection<PollOption> Options { get; set; } = Array.Empty<PollOption>();

        public Poll(string question, bool isDraft, DateTime end)
        {
            Question = question;
            IsDraft = isDraft;
            Created = DateTime.UtcNow;
            End = end;
        }
    }
}
