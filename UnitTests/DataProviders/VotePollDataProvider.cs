using PollBack.Core.PollAggregate;
using System;
using System.Collections.Generic;
using PollAggregate = PollBack.Core.PollAggregate;

namespace UnitTests.DataProviders
{
    internal class VotePollDataProvider
    {
        private static PollOption[] pollOptions = new PollOption[]
        {
            DataProviderUtils.CreatePollOption(1, 1, "Opcja 1"),
            DataProviderUtils.CreatePollOption(2, 1, "Opcja 2"),
            DataProviderUtils.CreatePollOption(3, 1, "Opcja 3"),
            DataProviderUtils.CreatePollOption(4, 1, "Opcja 4"),

            DataProviderUtils.CreatePollOption(1, 1, "Opcja 1", 1),
            DataProviderUtils.CreatePollOption(3, 1, "Opcja 3", 1),
        };

        internal static Dictionary<string, (PollAggregate.Poll testPoll, PollAggregate.Poll responsePoll)> polls = new()
        {
            {
                "VoteTwoPollOptions",
                (
                    DataProviderUtils.CreatePoll(1, null, "Pytanie - test", false, null, new PollOption[] { pollOptions[0], pollOptions[1], pollOptions[2], pollOptions[3] }),
                    DataProviderUtils.CreatePoll(1, null, "Pytanie - test", false, null, new PollOption[] { pollOptions[4], pollOptions[1], pollOptions[5], pollOptions[3] })
                )
            },
            {
                "VoteWrongPollOptions",
                (
                    DataProviderUtils.CreatePoll(1, null, "Pytanie - test", false, null, new PollOption[] { pollOptions[0], pollOptions[1], pollOptions[2], pollOptions[3] }),
                    DataProviderUtils.CreatePoll(1, null, "Pytanie - test", false, null, new PollOption[] { pollOptions[4], pollOptions[1], pollOptions[5], pollOptions[3] })
                )
            },
            {
                "VoteDraftPoll",
                (
                    DataProviderUtils.CreatePoll(1, null, "Pytanie - test", true, null, new PollOption[] { pollOptions[0], pollOptions[1], pollOptions[2], pollOptions[3] }),
                    DataProviderUtils.CreatePoll(1, null, "Pytanie - test", true, null, new PollOption[] { pollOptions[4], pollOptions[1], pollOptions[5], pollOptions[3] })
                )
            },
            {
                "VoteEndDateExpiredPoll",
                (
                    new PollAggregate.Poll()
                    {
                        Id = 1,
                        UserId = null,
                        Question = "Pytanie - test",
                        IsDraft = false,
                        End = DateTime.UtcNow.AddDays(-1),
                        Options = new PollOption[] { pollOptions[0], pollOptions[1], pollOptions[2], pollOptions[3] }
                    },
                    new PollAggregate.Poll()
                    {
                        Id = 1,
                        UserId = null,
                        Question = "Pytanie - test",
                        IsDraft = false,
                        End = DateTime.UtcNow.AddDays(-1),
                        Options = new PollOption[] { pollOptions[0], pollOptions[1], pollOptions[2], pollOptions[3] }
                    }
                )
            },
        };
    }
}
