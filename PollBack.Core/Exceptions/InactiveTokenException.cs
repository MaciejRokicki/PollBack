namespace PollBack.Core.Exceptions
{
    public class InactiveTokenException : Exception
    {
        public InactiveTokenException() : base("Inactive token.")
        {
        }
    }
}
