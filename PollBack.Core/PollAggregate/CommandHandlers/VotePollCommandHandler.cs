using MediatR;
using PollBack.Core.Exceptions;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate.Commands;

namespace PollBack.Core.PollAggregate.CommandHandlers
{
    public class VotePollCommandHandler : IRequestHandler<VotePollCommand, Unit>
    {
        private readonly IPollRepository pollRepository;
        private readonly IPollOptionVoteRepository pollOptionVoteRepository;

        public VotePollCommandHandler(IPollRepository pollRepository, IPollOptionVoteRepository pollOptionVoteRepository)
        {
            this.pollRepository = pollRepository;
            this.pollOptionVoteRepository = pollOptionVoteRepository;
        }

        public async Task<Unit> Handle(VotePollCommand request, CancellationToken cancellationToken)
        {
            Poll? poll = await pollRepository.GetAsync(x => x.Id == request.PollId);

            if(poll != null)
            {
                if(!poll.IsDraft)
                {
                    List<PollOptionVote> pollOptionsVotes = new();
                    List<int> pollOptionIds = poll.Options.Select(x => x.Id).ToList();

                    foreach (int id in request.PollOptionIds)
                    {
                        if (!pollOptionIds.Contains(id))
                            throw new WrongPollOptionException();

                        pollOptionsVotes.Add(new PollOptionVote()
                        {
                            UserId = request.UserId,
                            PollOptionId = id
                        });
                    }

                    await pollOptionVoteRepository.CreateRangeAsync(pollOptionsVotes);
                }
                else
                {
                    throw new DraftStatePollException();
                }
            }
            else
            {
                throw new PollNotFoundException();
            }

            return Unit.Value;
        }
    }
}
