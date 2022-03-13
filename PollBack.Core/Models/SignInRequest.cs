using System.Text.Json.Serialization;

namespace PollBack.Core.Models
{
    public class SignInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public string IpAddress { get; set; } = String.Empty;

        public SignInRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
