namespace PollBack.Web.ViewModels
{
    public class SignInViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public SignInViewModel(string email, string token)
        {
            Email = email;
            Token = token;
        }
    }
}
