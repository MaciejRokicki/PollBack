using PollBack.Core.Entities;
using PollBack.Shared;
using System.Text.Json.Serialization;

namespace PollBack.Core.PollAggregate
{
    public class Poll : BaseEntity
    {
        [JsonIgnore]
        public int? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public string? Question { get; set; }
        public bool IsDraft { get; set; }
        public DateTime Created { get; init; }
        public DateTime? End { get; set; }
        public ICollection<PollOption> Options { get; set; } = new List<PollOption>();

        public Poll()
        {
            Created = DateTime.UtcNow;
        }
    }
}
