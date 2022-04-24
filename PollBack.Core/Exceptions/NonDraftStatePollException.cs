namespace PollBack.Core.Exceptions
{
    public class NonDraftStatePollException : Exception
    {
        public NonDraftStatePollException() : base("Poll is not in draft state.")
        {
        }
    }
}
