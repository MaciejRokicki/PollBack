using Moq;
using PollBack.Core.Interfaces.Repositories;
using System.Threading.Tasks;
using Xunit;
using PollBack.Core.PollAggregate.Commands;
using PollBack.Core.PollAggregate.CommandHandlers;
using PollAggregate = PollBack.Core.PollAggregate;
using Shouldly;
using System.Threading;
using UnitTests.DataProviders;

namespace UnitTests.Poll
{
    public class CreatePoll
    {
        private Mock<IPollRepository> pollRepository = new();

        [Fact]
        public void CreatePollWithoutEndDateAndWithoutUser()
        {
            CreatePollCommandHandler? handler = new(pollRepository.Object);

            pollRepository
                .Setup(x => x.CreateAsync(It.IsAny<PollAggregate.Poll>()))
                .Returns(Task.FromResult(CreatePollDataProvider.polls["CreatePollWithoutEndDateAndWithoutUser"].responsePoll));

            Task<PollAggregate.Poll> response = handler.Handle(
                new CreatePollCommand(
                    CreatePollDataProvider.polls["CreatePollWithoutEndDateAndWithoutUser"].testPoll, 
                    null),
                CancellationToken.None);

            AssertPoll(CreatePollDataProvider.polls["CreatePollWithoutEndDateAndWithoutUser"].testPoll, response.Result);
        }

        [Fact]
        public void CreatePollWithoutEndDateAndWithUser()
        {
            CreatePollCommandHandler? handler = new(pollRepository.Object);

            pollRepository.Setup(x =>
                x.CreateAsync(It.IsAny<PollAggregate.Poll>()))
                    .Returns(Task.FromResult(CreatePollDataProvider.polls["CreatePollWithoutEndDateAndWithUser"].responsePoll));

            Task<PollAggregate.Poll> response = handler.Handle(
                new CreatePollCommand(
                    CreatePollDataProvider.polls["CreatePollWithoutEndDateAndWithUser"].testPoll, 
                    null), 
                CancellationToken.None);

            AssertPoll(CreatePollDataProvider.polls["CreatePollWithoutEndDateAndWithUser"].testPoll, response.Result);
        }

        [Fact]
        public void CreatePollWithEndDateAndWithoutUser()
        {
            CreatePollCommandHandler? handler = new(pollRepository.Object);

            pollRepository
                .Setup(x => x.CreateAsync(It.IsAny<PollAggregate.Poll>()))
                .Returns(Task.FromResult(CreatePollDataProvider.polls["CreatePollWithEndDateAndWithoutUser"].responsePoll));

            Task<PollAggregate.Poll> response = handler.Handle(
                new CreatePollCommand(
                    CreatePollDataProvider.polls["CreatePollWithEndDateAndWithoutUser"].testPoll, 
                    "1"), 
                CancellationToken.None);

            AssertPoll(CreatePollDataProvider.polls["CreatePollWithEndDateAndWithoutUser"].testPoll, response.Result);

            response.Result
                .End
                .ShouldNotBeNull()
                .ShouldBeGreaterThan(CreatePollDataProvider.polls["CreatePollWithEndDateAndWithoutUser"].testPoll.Created);
        }

        [Fact]
        public void CreatePollWithEndDateAndWithUser()
        {
            CreatePollCommandHandler? handler = new(pollRepository.Object);

            pollRepository.Setup(x =>
                x.CreateAsync(It.IsAny<PollAggregate.Poll>()))
                    .Returns(Task.FromResult(CreatePollDataProvider.polls["CreatePollWithEndDateAndWithUser"].responsePoll));

            Task<PollAggregate.Poll> response = handler.Handle(
                new CreatePollCommand(
                    CreatePollDataProvider.polls["CreatePollWithEndDateAndWithUser"].testPoll,
                    "4"),
                CancellationToken.None);

            AssertPoll(CreatePollDataProvider.polls["CreatePollWithEndDateAndWithUser"].testPoll, response.Result);

            response.Result
                .End
                .ShouldNotBeNull()
                .ShouldBeGreaterThan(CreatePollDataProvider.polls["CreatePollWithEndDateAndWithUser"].testPoll.Created);
        }

        private void AssertPoll(PollAggregate.Poll testPoll, PollAggregate.Poll responsePoll)
        {
            responsePoll
                .Id
                .ShouldBe(testPoll.Id);

            responsePoll
                .IsDraft
                .ShouldBe(testPoll.IsDraft);

            responsePoll
                .Question
                .ShouldBe(testPoll.Question);

            responsePoll
                .Options.Count
                .ShouldBe(testPoll.Options.Count);

            responsePoll
                .UserId
                .ShouldBe(testPoll.UserId);

            responsePoll
                .User?.Id.ToString()
                .ShouldBe(testPoll.User?.Id.ToString());

            responsePoll
                .User?.Email
                .ShouldBe(testPoll.User?.Email);
        }
    }
}
