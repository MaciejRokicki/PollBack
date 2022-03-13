namespace PollBack.Core.Models
{
    public class SignInResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public SignInResponse(int id, string email, string token, string refreshToken)
        {
            Id = id;
            Email = email;
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}
