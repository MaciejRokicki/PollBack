namespace PollBack.Core.Models
{
    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public SignUpRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
