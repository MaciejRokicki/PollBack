using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate.CommandHandlers;
using PollBack.Core.PollAggregate.Commands;
using Shouldly;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
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
            Mock<IHttpContextAccessor> httpContextAccessor = new();
            DefaultHttpContext? context = new();

            context.User.Identities.First().AddClaim(new Claim("UserId", "5"));

            httpContextAccessor
                .Setup(x => x.HttpContext)
                .Returns(context);

            string? userId = httpContextAccessor.Object.HttpContext?.User.FindFirst("UserId")?.Value;

            ActivatePollCommandHandler? handler = new(pollRepository.Object);

            DateTime dateTime = DateTime.UtcNow;

            PollAggregate.Poll poll = new()
            {
                Id = 1,
                End = null,
                IsDraft = true,
                UserId = userId != null ? int.Parse(userId) : null
            };

            pollRepository
                .Setup(x => x.GetAsync(x => x.Id == 1 && x.UserId == int.Parse(userId) && x.IsDraft))
                .Returns(Task.FromResult(It.IsAny<PollAggregate.Poll?>()));

            poll.IsDraft = false;

            pollRepository
                .Setup(x => x.UpdateAsync(poll))
                .Returns(Task.FromResult(poll));

            pollRepository
                .Setup(x => x.GetAsync(x => x.Id == 1))
                .Returns(Task.FromResult(It.IsAny<PollAggregate.Poll?>()));

            Task<Unit> response = handler.Handle(new ActivatePollCommand() { UserId = int.Parse(userId), PollId = 1 }, CancellationToken.None);

            poll.IsDraft.ShouldBeFalse();
            poll.End.ShouldBeNull();
            poll.UserId.ShouldBe(5);
        }

        [Fact]
        public void ActivatePollWithWrongUser()
        {
            Mock<IHttpContextAccessor> httpContextAccessor = new();
            DefaultHttpContext? context = new();

            context.User.Identities.First().AddClaim(new Claim("UserId", "5"));

            httpContextAccessor
                .Setup(x => x.HttpContext)
                .Returns(context);

            string? userId = httpContextAccessor.Object.HttpContext?.User.FindFirst("UserId")?.Value;

            ActivatePollCommandHandler? handler = new(pollRepository.Object);

            DateTime dateTime = DateTime.UtcNow;

            PollAggregate.Poll poll = new()
            {
                Id = 1,
                End = null,
                IsDraft = true,
                UserId = userId != null ? int.Parse(userId) : null
            };

            pollRepository
                .Setup(x => x.GetAsync(x => x.Id == 1 && x.UserId == int.Parse(userId) && x.IsDraft))
                .Returns(Task.FromResult(It.IsAny<PollAggregate.Poll?>()));

            pollRepository
                .Setup(x => x.UpdateAsync(poll))
                .Returns(Task.FromResult(poll));

            pollRepository
                .Setup(x => x.GetAsync(x => x.Id == 1))
                .Returns(Task.FromResult(It.IsAny<PollAggregate.Poll?>()));

            Task<Unit> response = handler.Handle(new ActivatePollCommand() { UserId = 2, PollId = 1 }, CancellationToken.None);

            poll.IsDraft.ShouldBeTrue();
            poll.End.ShouldBeNull();
            poll.UserId.ShouldBe(5);
        }

        [Fact]
        public void ActivatePollWithEndDate()
        {
            Mock<IHttpContextAccessor> httpContextAccessor = new();
            DefaultHttpContext? context = new();

            context.User.Identities.First().AddClaim(new Claim("UserId", "5"));

            httpContextAccessor
                .Setup(x => x.HttpContext)
                .Returns(context);

            string? userId = httpContextAccessor.Object.HttpContext?.User.FindFirst("UserId")?.Value;

            ActivatePollCommandHandler? handler = new(pollRepository.Object);

            DateTime dateTime = DateTime.UtcNow;

            PollAggregate.Poll poll = new()
            {
                Id = 1,
                Created = dateTime,
                End = dateTime.AddMinutes(10),
                IsDraft = true,
                UserId = userId != null ? int.Parse(userId) : null
            };

            pollRepository
                .Setup(x => x.CreateAsync(It.IsAny<PollAggregate.Poll>()))
                .Returns(Task.FromResult(poll));

            if (poll.End != null)
            {
                TimeSpan? x = poll.End - poll.Created;

                poll.Created = DateTime.UtcNow;
                poll.End = poll.Created.Add(x.Value);
            }

            poll.IsDraft = false;

            pollRepository
                .Setup(x => x.UpdateAsync(poll))
                .Returns(Task.FromResult(poll));

            pollRepository
                .Setup(x => x.GetAsync(x => x.Id == 1))
                .Returns(Task.FromResult(It.IsAny<PollAggregate.Poll?>()));

            Task<Unit> response = handler.Handle(new ActivatePollCommand() { UserId = int.Parse(userId), PollId = 1 }, CancellationToken.None);

            poll.IsDraft.ShouldBeFalse();
            poll.End.ShouldBe(poll.Created.AddMinutes(10));
            poll.UserId.ShouldBe(5);
        }
    }
}
