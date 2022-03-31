using Moq;
using PollBack.Core.Interfaces.Repositories;
using System.Threading.Tasks;
using Xunit;
using System;
using PollBack.Core.PollAggregate.Commands;
using PollBack.Core.PollAggregate.CommandHandlers;
using PollAggregate = PollBack.Core.PollAggregate;
using System.Collections.Generic;
using Shouldly;
using PollBack.Core.PollAggregate;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;
using System.Threading;

namespace UnitTests.Poll
{
    public class CreatePoll
    {
        private Mock<IPollRepository> pollRepository = new();

        [Fact]
        public void CreatePollWithoutEndDateAndWithoutUser()
        {
            CreatePollCommandHandler? handler = new(pollRepository.Object);

            DateTime dateTime = DateTime.UtcNow;

            PollAggregate.Poll poll = new()
            {
                Id = 1,
                Created = dateTime,
                End = null,
                IsDraft = false,
                Question = " Pytanie - test",
                Options = new List<PollOption>()
                {
                    new()
                    {
                        Id = 1,
                        Option = " Opcja1",
                        PollId = 1
                    },
                    new()
                    {
                        Id = 2,
                        Option = " Opcja2",
                        PollId = 1
                    }
                },
                User = null,
                UserId = null
            };

            pollRepository
                .Setup(x => x.CreateAsync(It.IsAny<PollAggregate.Poll>()))
                .Returns(Task.FromResult(poll));

            Task<PollAggregate.Poll> response = handler.Handle(new CreatePollCommand(poll, null), CancellationToken.None);

            AssertPoll(response, poll);
        }

        [Fact]
        public void CreatePollWithoutEndDateAndWithUser()
        {
            Mock<IHttpContextAccessor> httpContextAccessor = new();
            DefaultHttpContext? context = new();

            context.User.Identities.First().AddClaim(new Claim("UserId", "10"));

            httpContextAccessor
                .Setup(x => x.HttpContext)
                .Returns(context);

            CreatePollCommandHandler? handler = new(pollRepository.Object);

            DateTime dateTime = DateTime.UtcNow;
            string? userId = httpContextAccessor.Object.HttpContext?.User.FindFirst("UserId")?.Value;

            PollAggregate.Poll poll = new()
            {
                Id = 1,
                Created = dateTime,
                End = null,
                IsDraft = false,
                Question = " Pytanie - test",
                Options = new List<PollOption>()
                {
                    new()
                    {
                        Id = 1,
                        Option = " Opcja1",
                        PollId = 1
                    },
                    new()
                    {
                        Id = 2,
                        Option = " Opcja2",
                        PollId = 1
                    }
                },
                User = null,
                UserId = userId != null ? int.Parse(userId) : null
            };

            pollRepository.Setup(x =>
                x.CreateAsync(It.IsAny<PollAggregate.Poll>()))
                    .Returns(Task.FromResult(poll));

            Task<PollAggregate.Poll> response = handler.Handle(new CreatePollCommand(poll, null), CancellationToken.None);

            AssertPoll(response, poll);
        }

        [Fact]
        public void CreatePollWithEndDateAndWithoutUser()
        {
            CreatePollCommandHandler? handler = new(pollRepository.Object);

            DateTime dateTime = DateTime.UtcNow;

            PollAggregate.Poll poll = new()
            {
                Id = 1,
                Created = dateTime,
                End = null,
                IsDraft = false,
                Question = " Pytanie - test",
                Options = new List<PollOption>()
                {
                    new()
                    {
                        Id = 1,
                        Option = " Opcja1",
                        PollId = 1
                    },
                    new()
                    {
                        Id = 2,
                        Option = " Opcja2",
                        PollId = 1
                    }
                },
                User = null,
                UserId = null
            };

            pollRepository
                .Setup(x => x.CreateAsync(It.IsAny<PollAggregate.Poll>()))
                .Returns(Task.FromResult(poll));

            Task<PollAggregate.Poll> response = handler.Handle(new CreatePollCommand(poll, "1"), CancellationToken.None);

            AssertPoll(response, poll);
        }

        [Fact]
        public void CreatePollWithEndDateAndWithUser()
        {
            Mock<IHttpContextAccessor> httpContextAccessor = new();
            DefaultHttpContext? context = new();

            context.User.Identities.First().AddClaim(new Claim("UserId", "10"));

            httpContextAccessor
                .Setup(x => x.HttpContext)
                .Returns(context);

            CreatePollCommandHandler? handler = new(pollRepository.Object);

            DateTime dateTime = DateTime.UtcNow;
            string? userId = httpContextAccessor.Object.HttpContext?.User.FindFirst("UserId")?.Value;

            PollAggregate.Poll poll = new()
            {
                Id = 1,
                Created = dateTime,
                End = null,
                IsDraft = false,
                Question = " Pytanie - test",
                Options = new List<PollOption>()
                {
                    new()
                    {
                        Id = 1,
                        Option = " Opcja1",
                        PollId = 1
                    },
                    new()
                    {
                        Id = 2,
                        Option = " Opcja2",
                        PollId = 1
                    }
                },
                User = null,
                UserId = userId != null ? int.Parse(userId) : null
            };

            pollRepository.Setup(x =>
                x.CreateAsync(It.IsAny<PollAggregate.Poll>()))
                    .Returns(Task.FromResult(poll));

            Task<PollAggregate.Poll> response = handler.Handle(new CreatePollCommand(poll, "2"), CancellationToken.None);

            AssertPoll(response, poll);
        }

        private void AssertPoll(Task<PollAggregate.Poll> response, PollAggregate.Poll actual)
        {
            response
                .Result.Id
                .ShouldBe(actual.Id);

            response
                .Result.Created
                .ShouldBe(actual.Created);

            response
                .Result.End
                .ShouldBe(actual.End);

            response
                .Result.IsDraft
                .ShouldBe(actual.IsDraft);

            response
                .Result.Question
                .ShouldBe(actual.Question);

            response
                .Result.Options.Count
                .ShouldBe(actual.Options.Count);

            response
                .Result.UserId
                .ShouldBe(actual.UserId);

            response
                .Result.User?.Id.ToString()
                .ShouldBe(actual.User?.Id.ToString());

            response
                .Result.User?.Email
                .ShouldBe(actual.User?.Email);
        }
    }
}
