namespace Linn.Production.IoC
{
    using Autofac;

    using Domain.LinnApps;
    using Domain.LinnApps.Repositories;

    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Persistence.LinnApps;
    using Linn.Production.Persistence.LinnApps.Repositories;

    using Microsoft.EntityFrameworkCore;

    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceDbContext>().AsSelf().As<DbContext>().InstancePerRequest();
            builder.RegisterType<TransactionManager>().As<ITransactionManager>();

            // linnapps repositories
            builder.RegisterType<BuildsRepository>().As<IBuildsRepository>();
            builder.RegisterType<DepartmentsRepository>().As<IRepository<Department, string>>();

            builder.RegisterType<AteFaultCodeRepository>().As<IRepository<AteFaultCode, string>>();
        }
    }
}