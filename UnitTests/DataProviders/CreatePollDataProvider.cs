using PollBack.Core.PollAggregate;
using System.Collections.Generic;
using PollAggregate = PollBack.Core.PollAggregate;

namespace UnitTests.DataProviders
{
    internal static class CreatePollDataProvider
    {
        private static PollOption[] pollOptions = new PollOption[]
        {
            DataProviderUtils.CreatePollOption(1, 1, "Opcja 1"),
            DataProviderUtils.CreatePollOption(2, 1, "Opcja 2"),
        };

        internal static Dictionary<string, (PollAggregate.Poll testPoll, PollAggregate.Poll responsePoll)> polls = new()
        {
            {
                "CreatePollWithoutEndDateAndWithoutUser",
                DataProviderUtils.CreatePollTestModels(1, null, "Pytanie - test", false, null, new PollOption[] { pollOptions[0], pollOptions[1] })
            },
            {
                "CreatePollWithoutEndDateAndWithUser",
                DataProviderUtils.CreatePollTestModels(1, 10, "Pytanie - test", false, null, new PollOption[] { pollOptions[0], pollOptions[1] })
            },
            {
                "CreatePollWithEndDateAndWithoutUser",
                DataProviderUtils.CreatePollTestModels(1, null, "Pytanie - test", false, "1", new PollOption[] { pollOptions[0], pollOptions[1] })
            },
            {
                "CreatePollWithEndDateAndWithUser",
                DataProviderUtils.CreatePollTestModels(1, 5, "Pytanie - test", false, "4", new PollOption[] { pollOptions[0], pollOptions[1] })
            }
        };
    }
}
