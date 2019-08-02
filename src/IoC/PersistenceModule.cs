﻿using Linn.Common.Persistence;
using Linn.Common.Persistence.EntityFramework;
using Linn.Production.Domain;
using Linn.Production.Domain.LinnApps.SerialNumberReissue;

namespace Linn.Production.IoC
{
    using Autofac;

    using Domain.LinnApps;
    using Domain.LinnApps.Repositories;
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
            builder.RegisterType<BuildsSummariesRepository>().As<IBuildsSummariesRepository>();
            builder.RegisterType<DepartmentsRepository>().As<IRepository<Department, string>>();
            builder.RegisterType<AteFaultCodeRepository>().As<IRepository<AteFaultCode, string>>();
            builder.RegisterType<SerialNumberReissueRepository>().As<IRepository<SerialNumberReissue, int>>();
            builder.RegisterType<ProductionMeasuresRepository>().As<IRepository<ProductionMeasures, string>>();
        }
    }
}