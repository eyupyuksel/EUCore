using System;
using Autofac;
using EUCore.Audit;

namespace EUCore
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuditManager>().As<IAuditManager>().InstancePerLifetimeScope();

            //Lifetime Scope
            base.Load(builder);
        }
    }
}
