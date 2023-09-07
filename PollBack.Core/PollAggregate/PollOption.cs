using PollBack.Shared;
using System.Text.Json.Serialization;

namespace PollBack.Core.PollAggregate
{
    public class PollOption : BaseEntity
    {
        [JsonIgnore]
        public int PollId { get; set; }
        [JsonIgnore]
        public Poll? Poll { get; set; }
        public string Option { get; init; } = string.Empty;
        [JsonIgnore]
        public int Votes { get; set; }

        [JsonIgnore]
        public ICollection<PollOptionVote> PollOptionVotes { get; set; } = new List<PollOptionVote>();
    }
}
