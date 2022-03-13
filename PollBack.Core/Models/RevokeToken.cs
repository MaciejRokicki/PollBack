namespace PollBack.Core.Models
{
    public class RevokeToken
    {
        public string Token { get; set; }

        public RevokeToken(string token)
        {
            Token = token;
        }
    }
}
