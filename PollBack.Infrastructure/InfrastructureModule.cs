using Autofac;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Infrastructure.Data.Repositories;

namespace PollBack.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<PollRepository>()
                .As<IPollRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
