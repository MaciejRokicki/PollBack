using Moq;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate.CommandHandlers;
using PollBack.Core.PollAggregate.Commands;
using Shouldly;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using UnitTests.DataProviders;
using Xunit;
using PollAggregate = PollBack.Core.PollAggregate;

namespace UnitTests.Poll
{
    public class UpdatePoll
    {
        private Mock<IPollRepository> pollRepository = new();

        [Fact]
        public void UpdateQuestionPoll()
        {
            UpdatePollCommandHandler? handler = new(pollRepository.Object);

            PollAggregate.Poll testPoll = new();

            pollRepository
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<PollAggregate.Poll, bool>>>()))
                .Returns(Task.FromResult((PollAggregate.Poll?)UpdatePollDataProvider.polls["UpdateQuestionPoll"].testPoll));

            pollRepository
                .Setup(x => x.UpdateAsync(It.IsAny<PollAggregate.Poll>()))
                .Returns(Task.FromResult(UpdatePollDataProvider.polls["UpdateQuestionPoll"].responsePoll));

            testPoll.Id = UpdatePollDataProvider.polls["UpdateQuestionPoll"].testPoll.Id;
            testPoll.Question = "Pytanie - test2";

            Task<PollAggregate.Poll> response = handler.Handle(
                new UpdatePollCommand(
                    testPoll,
                    null),
                CancellationToken.None);

            response.Result
                .Id
                .ShouldBe(UpdatePollDataProvider.polls["UpdateQuestionPoll"].testPoll.Id);

            response.Result
                .UserId
                .ShouldBe(UpdatePollDataProvider.polls["UpdateQuestionPoll"].testPoll.UserId);

            response.Result
                .Question
                .ShouldBe(UpdatePollDataProvider.polls["UpdateQuestionPoll"].testPoll.Question);
        }

        [Fact]
        public void UpdateEndDatePoll()
        {
            UpdatePollCommandHandler? handler = new(pollRepository.Object);

            pollRepository
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<PollAggregate.Poll, bool>>>()))
                .Returns(Task.FromResult((PollAggregate.Poll?)UpdatePollDataProvider.polls["UpdateEndDatePoll"].testPoll));

            pollRepository
                .Setup(x => x.UpdateAsync(It.IsAny<PollAggregate.Poll>()))
                .Returns(Task.FromResult(UpdatePollDataProvider.polls["UpdateEndDatePoll"].responsePoll));

            Task<PollAggregate.Poll> response = handler.Handle(
                new UpdatePollCommand(
                    UpdatePollDataProvider.polls["UpdateEndDatePoll"].testPoll,
                    "2"),
                CancellationToken.None);

            response.Result
                .Id
                .ShouldBe(UpdatePollDataProvider.polls["UpdateEndDatePoll"].testPoll.Id);

            response.Result
                .End
                .ShouldNotBeNull();

            if(UpdatePollDataProvider.polls["UpdateEndDatePoll"].responsePoll.End != null)
            {
                response.Result
                    .End
                    .ShouldBe(UpdatePollDataProvider.polls["UpdateEndDatePoll"].responsePoll.End);
            }
        }

        [Fact]
        public void UpdateNonDraftPoll()
        {
            UpdatePollCommandHandler? handler = new(pollRepository.Object);

            pollRepository
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<PollAggregate.Poll, bool>>>()))
                .Returns(Task.FromResult((PollAggregate.Poll?)UpdatePollDataProvider.polls["UpdateNonDraftPoll"].testPoll));

            pollRepository
                .Setup(x => x.UpdateAsync(It.IsAny<PollAggregate.Poll>()))
                .Returns(Task.FromResult(UpdatePollDataProvider.polls["UpdateNonDraftPoll"].responsePoll));

            Task<PollAggregate.Poll> response = handler.Handle(
                new UpdatePollCommand(
                    UpdatePollDataProvider.polls["UpdateNonDraftPoll"].testPoll,
                    null),
                CancellationToken.None);

            response
                .Status
                .ShouldBe(TaskStatus.Faulted);
        }

        [Fact]
        public void UpdatePollOptions()
        {
            UpdatePollCommandHandler? handler = new(pollRepository.Object);

            pollRepository
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<PollAggregate.Poll, bool>>>()))
                .Returns(Task.FromResult((PollAggregate.Poll?)UpdatePollDataProvider.polls["UpdatePollOptions"].testPoll));

            pollRepository
                .Setup(x => x.UpdateAsync(It.IsAny<PollAggregate.Poll>()))
                .Returns(Task.FromResult(UpdatePollDataProvider.polls["UpdatePollOptions"].responsePoll));

            PollAggregate.Poll testPoll = new();
            testPoll.Id = UpdatePollDataProvider.polls["UpdatePollOptions"].testPoll.Id;
            testPoll.Question = UpdatePollDataProvider.polls["UpdatePollOptions"].testPoll.Question;
            testPoll.Options = new PollAggregate.PollOption[] 
            {
                new PollAggregate.PollOption()
                {
                    Id = 1,
                    PollId = 1,
                    Option = "Opcja 1"
                },
                new PollAggregate.PollOption()
                {
                    Id = 2,
                    PollId = 1,
                    Option = "Opcja 2"
                },
                new PollAggregate.PollOption()
                {
                    Id = 4,
                    PollId = 1,
                    Option = "Opcja 4"
                },
            };

            Task<PollAggregate.Poll> response = handler.Handle(
                new UpdatePollCommand(
                    testPoll,
                    null),
                CancellationToken.None);

            response.Result
                .Id
                .ShouldBe(UpdatePollDataProvider.polls["UpdatePollOptions"].responsePoll.Id);

            for(int i = 0; i < response.Result.Options.Count; i++)
            {
                response.Result.Options.ElementAt(i)
                    .Id
                    .ShouldBe(UpdatePollDataProvider.polls["UpdatePollOptions"].responsePoll.Options.ElementAt(i).Id);

                response.Result.Options.ElementAt(i)
                    .PollId
                    .ShouldBe(UpdatePollDataProvider.polls["UpdatePollOptions"].responsePoll.Options.ElementAt(i).PollId);

                response.Result.Options.ElementAt(i)
                    .Option
                    .ShouldBe(UpdatePollDataProvider.polls["UpdatePollOptions"].responsePoll.Options.ElementAt(i).Option);
            }
        }
    }
}
