using Moq;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate.CommandHandlers;
using PollBack.Core.PollAggregate.Commands;
using Shouldly;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using UnitTests.DataProviders;
using Xunit;
using PollAggregate = PollBack.Core.PollAggregate;

namespace UnitTests.Poll
{
    public class VotePoll
    {
        private Mock<IPollRepository> pollRepository = new();
        private Mock<IPollOptionVoteRepository> pollOptionVoteRepository = new();

        [Fact]
        public void VoteTwoPollOptions()
        {
            VotePollCommandHandler? handler = new(pollRepository.Object, pollOptionVoteRepository.Object);

            pollRepository
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<PollAggregate.Poll, bool>>>()))
                .Returns(Task.FromResult((PollAggregate.Poll?)VotePollDataProvider.polls["VoteTwoPollOptions"].testPoll));

            Task response = handler.Handle(new VotePollCommand() { UserId = null, PollId = 1, PollOptionIds = new int[] { 1, 3 } }, CancellationToken.None);

            response.Status
                .ShouldBe(TaskStatus.RanToCompletion);
        }

        [Fact]
        public void VoteWrongPollOptions()
        {
            VotePollCommandHandler? handler = new(pollRepository.Object, pollOptionVoteRepository.Object);

            pollRepository
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<PollAggregate.Poll, bool>>>()))
                .Returns(Task.FromResult((PollAggregate.Poll?)VotePollDataProvider.polls["VoteWrongPollOptions"].testPoll));

            Task response = handler.Handle(new VotePollCommand() { UserId = null, PollId = 1, PollOptionIds = new int[] { 1, 6 } }, CancellationToken.None);

            response.Status
                .ShouldBe(TaskStatus.Faulted);
        }

        [Fact]
        public void VoteNonExistsPoll()
        {
            VotePollCommandHandler? handler = new(pollRepository.Object, pollOptionVoteRepository.Object);

            pollRepository
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<PollAggregate.Poll, bool>>>()))
                .Returns(Task.FromResult(It.IsAny<PollAggregate.Poll?>()));

            Task response = handler.Handle(new VotePollCommand() { UserId = null, PollId = 2, PollOptionIds = new int[] { 1, 3 } }, CancellationToken.None);

            response.Status
                .ShouldBe(TaskStatus.Faulted);
        }

        [Fact]
        public void VoteDraftPoll()
        {
            VotePollCommandHandler? handler = new(pollRepository.Object, pollOptionVoteRepository.Object);

            pollRepository
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<PollAggregate.Poll, bool>>>()))
                .Returns(Task.FromResult((PollAggregate.Poll?)VotePollDataProvider.polls["VoteDraftPoll"].testPoll));

            Task response = handler.Handle(new VotePollCommand() { UserId = null, PollId = 1, PollOptionIds = new int[] { 1, 3 } }, CancellationToken.None);

            response.Status
                .ShouldBe(TaskStatus.Faulted);
        }

        [Fact]
        public void VoteEndDateExpiredPoll()
        {
            VotePollCommandHandler? handler = new(pollRepository.Object, pollOptionVoteRepository.Object);

            pollRepository
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<PollAggregate.Poll, bool>>>()))
                .Returns(Task.FromResult((PollAggregate.Poll?)VotePollDataProvider.polls["VoteEndDateExpiredPoll"].testPoll));

            Task response = handler.Handle(new VotePollCommand() { UserId = null, PollId = 1, PollOptionIds = new int[] { 1, 2 } }, CancellationToken.None);

            response.Status
                .ShouldBe(TaskStatus.Faulted);
        }
    }
}
