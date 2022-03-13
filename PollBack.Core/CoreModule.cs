using Autofac;
using PollBack.Core.Interfaces.Services;
using PollBack.Core.Services;

namespace PollBack.Infrastructure
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder
                .RegisterType<EncryptionService>()
                .As<IEncryptionService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<JwtTokenService>()
                .As<IJwtTokenService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<AuthenticationService>()
                .As<IAuthenticationService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("Handler"))
                .AsImplementedInterfaces();
        }
    }
}
