using Autofac;
using PollBack.Core.PollAggregate.Handlers;

namespace PollBack.Infrastructure
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<GetAllPollsQueryHandler>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder
                .RegisterType<CreatePollCommandHandler>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
