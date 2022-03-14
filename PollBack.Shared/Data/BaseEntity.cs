using System.Text.Json.Serialization;

namespace PollBack.Shared
{
    public abstract class BaseEntity
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
