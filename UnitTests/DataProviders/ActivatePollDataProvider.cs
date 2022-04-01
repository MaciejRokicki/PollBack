using PollBack.Core.PollAggregate;
using System.Collections.Generic;
using PollAggregate = PollBack.Core.PollAggregate;

namespace UnitTests.DataProviders
{
    internal static class ActivatePollDataProvider
    {
        internal static Dictionary<string, (PollAggregate.Poll testPoll, PollAggregate.Poll responsePoll)> polls = new()
        {
            {
                "ActivateDraftPollWithoutEndDate",
                (
                    DataProviderUtils.CreatePoll(1, 5, null, true, null, new PollOption[] { }),
                    DataProviderUtils.CreatePoll(1, 5, null, false, null, new PollOption[] { })
                )
            },
            {
                "ActivatePollWithWrongUser",
                (
                    DataProviderUtils.CreatePoll(1, 2, null, true, null, new PollOption[] { }),
                    DataProviderUtils.CreatePoll(1, 5, null, false, null, new PollOption[] { })
                )
            },
            {
                "ActivatePollWithEndDate",
                (
                    DataProviderUtils.CreatePoll(1, 7, null, true, "1", new PollOption[] { }),
                    DataProviderUtils.CreatePoll(1, 7, null, false, "1", new PollOption[] { })
                )
            }
        };
    }
}
