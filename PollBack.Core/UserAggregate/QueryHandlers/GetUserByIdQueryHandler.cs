using MediatR;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.UserAggregate.Queries;

namespace PollBack.Core.UserAggregate.QueryHandlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User?>
    {
        private readonly IUserRepository userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.GetAsync(x => x.Id == request.Id);

            return user;
        }
    }
}
