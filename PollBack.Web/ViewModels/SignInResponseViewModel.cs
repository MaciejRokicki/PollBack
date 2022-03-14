namespace PollBack.Web.ViewModels
{
    public class SignInResponseViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }

        public SignInResponseViewModel(string email, string token)
        {
            Email = email;
            Token = token;
        }
    }
}
