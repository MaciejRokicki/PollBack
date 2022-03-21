namespace PollBack.Core.PollAggregate
{
    public static class EndDateSetter
    {
        public static Dictionary<string, int> EndDates = new Dictionary<string, int>()
        {
            { "1", 10 },
            { "2", 30 },
            { "3", 60 },
            { "4", 360 },
            { "5", 720 },
            { "6", 1440 },
        };
    }
}
