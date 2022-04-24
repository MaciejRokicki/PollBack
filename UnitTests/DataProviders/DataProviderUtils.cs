using PollBack.Core.PollAggregate;
using PollAggregate = PollBack.Core.PollAggregate;
using System.Collections.Generic;

namespace UnitTests.DataProviders
{
    internal static class DataProviderUtils
    {
        internal static PollOption CreatePollOption(int id, int pollId, string? option)
        {
            return new PollOption()
            {
                Id = id,
                PollId = pollId,
                Option = option
            };
        }

        internal static PollOption CreatePollOption(int id, int pollId, string? option, int votes)
        {
            return new PollOption()
            {
                Id = id,
                PollId = pollId,
                Option = option,
                Votes = votes
            };
        }

        internal static PollAggregate.Poll CreatePoll(
            int id,
            int? userId,
            string? question,
            bool isDraft,
            string? endDate,
            ICollection<PollOption> options)
        {
            PollAggregate.Poll poll = new()
            {
                Id = id,
                UserId = userId,
                Question = question,
                IsDraft = isDraft,
                Options = options
            };

            poll.End = endDate == null ? null : poll.Created.AddMinutes(EndDateSetter.EndDates[endDate]);

            return poll;
        }

        internal static (PollAggregate.Poll testPoll, PollAggregate.Poll responsePoll) CreatePollTestModels(
            int id,
            int? userId,
            string? question,
            bool isDraft,
            string? endDate,
            ICollection<PollOption> options)
        {
            PollAggregate.Poll testPoll = CreatePoll(id, userId, question, isDraft, endDate, options);
            PollAggregate.Poll responsePoll = CreatePoll(id, userId, question, isDraft, endDate, options);

            responsePoll.End = endDate == null ? null : responsePoll.Created.AddMinutes(EndDateSetter.EndDates[endDate]);

            return (testPoll, responsePoll);
        }
    }
}
