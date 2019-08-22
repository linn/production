namespace Linn.Production.IoC
{
    using Autofac;

    using Domain.LinnApps;
    using Domain.LinnApps.ATE;
    using Domain.LinnApps.Measures;
    using Domain.LinnApps.SerialNumberReissue;
    using Domain.LinnApps.ViewModels;

    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;

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
            builder.RegisterType<BoardFailTypeRepository>().As<IRepository<BoardFailType, int>>();
            builder.RegisterType<AssemblyFailRepository>().As<IRepository<AssemblyFail, int>>();
            builder.RegisterType<WorksOrderRepository>().As<IRepository<WorksOrder, int>>();
            builder.RegisterType<PartsRepository>().As<IRepository<Part, string>>();
            builder.RegisterType<ProductionTriggerLevelRepository>().As<IRepository<ProductionTriggerLevel, string>>();
            builder.RegisterType<PcasRevisionRepository>().As<IRepository<PcasRevision, string>>();
            builder.RegisterType<CitRepository>().As<IRepository<Cit, string>>();
            builder.RegisterType<EmployeeRepository>().As<IRepository<Employee, int>>();
            builder.RegisterType<AssemblyFailFaultCodeRepository>().As<IRepository<AssemblyFailFaultCode, string>>();

            // linnapps views
            builder.RegisterType<WhoBuiltWhatRepository>().As<IRepository<WhoBuiltWhat, string>>();
        }
    }
}