using Autofac;
using Nuget.Database.ContinuousDelivery.Infrastructure;

namespace Nuget.Database.ContinuousDelivery
{
    public class IocModuleInfrastructure : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PublisherDacPac>().AsImplementedInterfaces();
            builder.RegisterType<ControladorFerramentas>().AsImplementedInterfaces();
            builder.RegisterType<ControladorProcessos>().AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}