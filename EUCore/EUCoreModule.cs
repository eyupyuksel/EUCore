using System;
using System.Reflection;
using Autofac;
using EUCore.Audit;
using EUCore.Configuration;
using EUCore.Repositories;
using EUCore.Repositories.Dapper;

namespace EUCore
{
    public class EUCoreModule : Autofac.Module
    {
        private readonly AppSettings appSettings;
        public EUCoreModule(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuditManager>().As<IAuditManager>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes().Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

                builder.RegisterGeneric(typeof(DapperRepository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
                builder.RegisterGeneric(typeof(DapperRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //Lifetime Scope
            base.Load(builder);
        }
    }
}
