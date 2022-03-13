namespace PollBack.Core.Exceptions
{
    public class EmailExistsException : Exception
    {
        public EmailExistsException() : base("Email is in use.")
        {
        }
    }
}
