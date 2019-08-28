namespace Linn.Production.IoC
{
    using Autofac;

    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.ViewModels;
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
            builder.RegisterType<DepartmentsRepository>().As<IRepository<Department, string>>();
            builder.RegisterType<AteFaultCodeRepository>().As<IRepository<AteFaultCode, string>>();
            builder.RegisterType<SerialNumberReissueRepository>().As<IRepository<SerialNumberReissue, int>>();
            builder.RegisterType<ProductionMeasuresRepository>().As<IRepository<ProductionMeasures, string>>();
            builder.RegisterType<ManufacturingSkillsRepository>().As<IRepository<ManufacturingSkill, string>>();
            builder.RegisterType<ManufacturingResourceRepository>().As<IRepository<ManufacturingResource, string>>();
            builder.RegisterType<BoardFailTypeRepository>().As<IRepository<BoardFailType, int>>();
            builder.RegisterType<AssemblyFailRepository>().As<IRepository<AssemblyFail, int>>();
            builder.RegisterType<PtlMasterRepository>().As<IMasterRepository<PtlMaster>>();
            builder.RegisterType<OsrRunMasterRepository>().As<IMasterRepository<OsrRunMaster>>();
            builder.RegisterType<CitRepository>().As<IRepository<Cit, string>>();

            // linnapps views
            builder.RegisterType<WhoBuiltWhatRepository>().As<IRepository<WhoBuiltWhat, string>>();
            builder.RegisterType<ProductionTriggerQueryRepository>().As<IQueryRepository<ProductionTrigger>>();
        }
    }
}
