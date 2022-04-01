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
    public class ActivatePoll
    {
        private Mock<IPollRepository> pollRepository = new();

        [Fact]
        public void ActivateDraftPollWithoutEndDate()
        {
            ActivatePollCommandHandler? handler = new(pollRepository.Object);

            pollRepository
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<PollAggregate.Poll, bool>>>()))
                .Returns(Task.FromResult((PollAggregate.Poll?)ActivatePollDataProvider.polls["ActivateDraftPollWithoutEndDate"].testPoll));

            pollRepository
                .Setup(x => x.UpdateAsync(ActivatePollDataProvider.polls["ActivateDraftPollWithoutEndDate"].testPoll))
                .Returns(Task.FromResult(ActivatePollDataProvider.polls["ActivateDraftPollWithoutEndDate"].responsePoll));

            Task<PollAggregate.Poll> response = handler.Handle(new ActivatePollCommand() { UserId = 5, PollId = 1 }, CancellationToken.None);

            response.Result
                .IsDraft
                .ShouldBeFalse();

            response.Result
                .End
                .ShouldBeNull();

            response.Result
                .UserId
                .ShouldBe(5);
        }

        [Fact]
        public void ActivatePollWithWrongUser()
        {
            ActivatePollCommandHandler? handler = new(pollRepository.Object);

            pollRepository
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<PollAggregate.Poll, bool>>>()))
                .Returns(Task.FromResult<PollAggregate.Poll?>(null));

            Task<PollAggregate.Poll> response = handler.Handle(new ActivatePollCommand() { UserId = 2, PollId = 1 }, CancellationToken.None);

            response
                .Status
                .ShouldBe(TaskStatus.Faulted);
        }

        [Fact]
        public void ActivatePollWithEndDate()
        {
            ActivatePollCommandHandler? handler = new(pollRepository.Object);

            pollRepository
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<PollAggregate.Poll, bool>>>()))
                .Returns(Task.FromResult((PollAggregate.Poll?)ActivatePollDataProvider.polls["ActivatePollWithEndDate"].testPoll));

            pollRepository
                .Setup(x => x.UpdateAsync(ActivatePollDataProvider.polls["ActivatePollWithEndDate"].testPoll))
                .Returns(Task.FromResult(ActivatePollDataProvider.polls["ActivatePollWithEndDate"].responsePoll));

            Task<PollAggregate.Poll> response = handler.Handle(new ActivatePollCommand() { UserId = 7, PollId = 1 }, CancellationToken.None);

            response.Result
                .IsDraft
                .ShouldBeFalse();

            response.Result
                .End
                .ShouldBe(response.Result.Created.AddMinutes(10));

            response.Result
                .UserId
                .ShouldBe(7);
        }
    }
}
