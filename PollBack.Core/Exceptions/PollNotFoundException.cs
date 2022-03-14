namespace PollBack.Core.Exceptions
{
    public class PollNotFoundException : Exception
    {
        public PollNotFoundException() : base("Poll not found.")
        {
        }
    }
}
