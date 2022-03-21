using PollBack.Core.Entities;
using PollBack.Shared;
using System.Text.Json.Serialization;

namespace PollBack.Core.PollAggregate
{
    public class Poll : BaseEntity
    {
        public int? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public string? Question { get; set; }
        public bool IsDraft { get; set; }
        [JsonIgnore]
        public DateTime Created { get; set; }
        public DateTime? End { get; set; }
        public ICollection<PollOption> Options { get; set; } = new List<PollOption>();

        public Poll()
        {
            Created = DateTime.UtcNow;
        }
    }
}
