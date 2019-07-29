using Linn.Production.Domain;

namespace Linn.Production.IoC
{
    using Autofac;

    using Domain.LinnApps;
    using Domain.LinnApps.Repositories;

    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Domain.LinnApps.ATE;
    using Persistence.LinnApps;
    using Persistence.LinnApps.Repositories;

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

            builder.RegisterType<ProductionMeasuresRepository>().As<IRepository<ProductionMeasures, string>>();
        }
    }
}