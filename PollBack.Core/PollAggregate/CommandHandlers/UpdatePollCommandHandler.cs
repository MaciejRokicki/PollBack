using MediatR;
using PollBack.Core.Exceptions;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate.Commands;

namespace PollBack.Core.PollAggregate.CommandHandlers
{
    public class UpdatePollCommandHandler : IRequestHandler<UpdatePollCommand, Poll>
    {
        private readonly IPollRepository pollRepository;

        public UpdatePollCommandHandler(IPollRepository pollRepository)
        {
            this.pollRepository = pollRepository;
        }

        public async Task<Poll> Handle(UpdatePollCommand request, CancellationToken cancellationToken)
        {
            Poll? poll = await pollRepository.GetAsync(x => x.Id == request.Model.Id && x.UserId == request.UserId);

            if (poll == null)
                throw new PollNotFoundException();

            poll.Question = request.Model.Question;
            poll.Options = request.Model.Options;
            poll.End = request.EndOption == null ? null : poll.Created.AddMinutes(EndDateSetter.EndDates[request.EndOption]);

            Poll? data = await pollRepository.UpdateAsync(poll);

            return data;
        }
    }
}
