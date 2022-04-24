namespace PollBack.Core.Exceptions
{
    public class PollEndDateExpiredException : Exception
    {
        public PollEndDateExpiredException() : base("End date expired.")
        {
        }
    }
}
