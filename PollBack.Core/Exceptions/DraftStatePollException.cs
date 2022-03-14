namespace PollBack.Core.Exceptions
{
    public class DraftStatePollException : Exception
    {
        public DraftStatePollException() : base("Poll is in draft state.")
        {
        }
    }
}
