using PollBack.Core.PollAggregate;
using System.Collections.Generic;
using PollAggregate = PollBack.Core.PollAggregate;

namespace UnitTests.DataProviders
{
    internal static class UpdatePollDataProvider
    {
        private static PollOption[] pollOptions = new PollOption[]
        {
            DataProviderUtils.CreatePollOption(1, 1, "Opcja 1"),
            DataProviderUtils.CreatePollOption(2, 1, "Opcja 2"),
            DataProviderUtils.CreatePollOption(3, 1, "Opcja 3"),
            DataProviderUtils.CreatePollOption(4, 1, "Opcja 4"),
        };

        internal static Dictionary<string, (PollAggregate.Poll testPoll, PollAggregate.Poll responsePoll)> polls = new()
        {
            {
                "UpdateQuestionPoll",
                (
                    DataProviderUtils.CreatePoll(1, 5, "Pytanie - test", true, null, new PollOption[] { pollOptions[0], pollOptions[1] }),
                    DataProviderUtils.CreatePoll(1, 5, "Pytanie - test2", true, null, new PollOption[] { pollOptions[0], pollOptions[1] })
                )
            },
            {
                "UpdateEndDatePoll",
                DataProviderUtils.CreatePollTestModels(1, 2, "Pytanie - test", true, "2", new PollOption[] { pollOptions[0], pollOptions[1] })
            },
            {
                "UpdateNonDraftPoll",
                DataProviderUtils.CreatePollTestModels(1, 2, "Pytanie - test", false, null, new PollOption[] { pollOptions[0], pollOptions[1] })
            },
            {
                "UpdatePollOptions",
                (
                    DataProviderUtils.CreatePoll(1, 5, "Pytanie - test", true, null, new PollOption[] { pollOptions[0], pollOptions[1] }),
                    DataProviderUtils.CreatePoll(1, 5, "Pytanie - test", true, null, new PollOption[] { pollOptions[0], pollOptions[2], pollOptions[3] })
                )
            },
        };
    }
}
