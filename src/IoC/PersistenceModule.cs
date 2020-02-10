namespace Linn.Production.IoC
{
    using Autofac;

    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Domain.LinnApps.BoardTests;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.PCAS;
    using Linn.Production.Domain.LinnApps.Products;
    using Linn.Production.Domain.LinnApps.Repositories;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
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
            builder.RegisterType<ManufacturingRoutesRepository>().As<IRepository<ManufacturingRoute, string>>();
            builder.RegisterType<ManufacturingOperationsRepository>().As<IRepository<ManufacturingOperation, int>>();
            builder.RegisterType<CitRepository>().As<IRepository<Cit, string>>();
            builder.RegisterType<ManufacturingResourceRepository>().As<IRepository<ManufacturingResource, string>>();
            builder.RegisterType<BoardFailTypeRepository>().As<IRepository<BoardFailType, int>>();
            builder.RegisterType<AssemblyFailRepository>().As<IRepository<AssemblyFail, int>>();
            builder.RegisterType<WorksOrderRepository>().As<IRepository<WorksOrder, int>>();
            builder.RegisterType<PartsRepository>().As<IRepository<Part, string>>();
            builder.RegisterType<ProductionTriggerLevelRepository>().As<IRepository<ProductionTriggerLevel, string>>();
            builder.RegisterType<PcasRevisionRepository>().As<IRepository<PcasRevision, string>>();
            builder.RegisterType<CitRepository>().As<IRepository<Cit, string>>();
            builder.RegisterType<EmployeeRepository>().As<IRepository<Employee, int>>();
            builder.RegisterType<AssemblyFailFaultCodeRepository>().As<IRepository<AssemblyFailFaultCode, string>>();
            builder.RegisterType<PtlMasterRepository>().As<ISingleRecordRepository<PtlMaster>>();
            builder.RegisterType<OsrRunMasterRepository>().As<ISingleRecordRepository<OsrRunMaster>>();
            builder.RegisterType<PartsRepository>().As<IRepository<Part, string>>();
            builder.RegisterType<WorkStationsRepository>().As<IRepository<WorkStation, string>>();
            builder.RegisterType<PcasBoardForAuditRepository>().As<IRepository<PcasBoardForAudit, string>>();
            builder.RegisterType<ProductionTriggerLevelsRepository>().As<IRepository<ProductionTriggerLevel, string>>();
            builder.RegisterType<CitRepository>().As<IRepository<Cit, string>>();
            builder.RegisterType<AccountingCompanyRepository>().As<IRepository<AccountingCompany, string>>();
            builder.RegisterType<LinnWeekRepository>().As<ILinnWeekRepository>();
            builder.RegisterType<SmtShiftsRepository>().As<IRepository<SmtShift, string>>();
            builder.RegisterType<StoragePlaceRepository>().As<IQueryRepository<StoragePlace>>();
            builder.RegisterType<PartFailFaultCodeRepository>().As<IRepository<PartFailFaultCode, string>>();
            builder.RegisterType<PartFailRepository>().As<IRepository<PartFail, int>>();
            builder.RegisterType<PartFailErrorTypeRepository>().As<IRepository<PartFailErrorType, string>>();
            builder.RegisterType<StorageLocationRepository>().As<IRepository<StorageLocation, int>>();
            builder.RegisterType<PurchaseOrderRepository>().As<IRepository<PurchaseOrder, int>>();
            builder.RegisterType<PtlSettingsRepository>().As<ISingleRecordRepository<PtlSettings>>();
            builder.RegisterType<MCDLineRepository>().As<IQueryRepository<MCDLine>>();
            builder.RegisterType<ProductDataRepository>().As<IRepository<ProductData, int>>();
            builder.RegisterType<OverdueOrderLineRepository>().As<IQueryRepository<OverdueOrderLine>>();
            builder.RegisterType<BoardTestRepository>().As<IRepository<BoardTest, BoardTestKey>>();
            builder.RegisterType<EmployeeDepartmentViewRepository>().As<IQueryRepository<EmployeeDepartmentView>>();
            builder.RegisterType<WorksOrderLabelsRepository>().As<IRepository<WorksOrderLabel, WorksOrderLabelKey>>();
            builder.RegisterType<PartFailSuppliersViewRepository>().As<IQueryRepository<PartFailSupplierView>>();
            builder.RegisterType<LabelTypeRepository>().As<IRepository<LabelType, string>>();
            builder.RegisterType<LabelReprintRepository>().As<IRepository<LabelReprint, int>>();
            builder.RegisterType<SerialNumberRepository>().As<IRepository<SerialNumber, int>>();
            builder.RegisterType<BuildPlanRepository>().As<IRepository<BuildPlan, string>>();
            builder.RegisterType<BuildPlanDetailsReportLineRepository>().As<IQueryRepository<BuildPlanDetailsReportLine>>();
            builder.RegisterType<BuildPlanDetailRepository>().As<IRepository<BuildPlanDetail, BuildPlanDetailKey>>();
            builder.RegisterType<BuildPlanRuleRepository>().As<IQueryRepository<BuildPlanRule>>();
            builder.RegisterType<AteTestRepository>().As<IRepository<AteTest, int>>();
            builder.RegisterType<AteTestDetailRepository>().As<IRepository<AteTestDetail, AteTestDetailKey>>();
            builder.RegisterType<AddressRepository>().As<IRepository<Address, int>>();
            builder.RegisterType<SupplierRepository>().As<IRepository<Supplier, int>>();

            // linnapps views
            builder.RegisterType<WhoBuiltWhatRepository>().As<IRepository<WhoBuiltWhat, string>>();
            builder.RegisterType<ProductionTriggerQueryRepository>().As<IQueryRepository<ProductionTrigger>>();
            builder.RegisterType<ProductionTriggerAssemblyRepository>().As<IQueryRepository<ProductionTriggerAssembly>>();
            builder.RegisterType<ProductionBackOrderQueryRepository>().As<IQueryRepository<ProductionBackOrder>>();
            builder.RegisterType<BomDetailExplodedPhantomPartViewRepository>().As<IRepository<BomDetailExplodedPhantomPartView, int>>();
            builder.RegisterType<WwdDetailQueryRepository>().As<IQueryRepository<WwdDetail>>();
            builder.RegisterType<ProductionBackOrdersViewRepository>().As<IQueryRepository<ProductionBackOrdersView>>();
            builder.RegisterType<BuiltThisWeekStatisticRepository>().As<IQueryRepository<BuiltThisWeekStatistic>>();
            builder.RegisterType<PtlStatRepository>().As<IQueryRepository<PtlStat>>();
            builder.RegisterType<SernosBuiltRepository>().As<IQueryRepository<SernosBuilt>>();
            builder.RegisterType<SernosIssuedRepository>().As<IQueryRepository<SernosIssued>>();
            builder.RegisterType<PurchaseOrdersReceivedRepository>().As<IQueryRepository<PurchaseOrdersReceived>>();
            builder.RegisterType<WswShortageRepository>().As<IQueryRepository<WswShortage>>();
            builder.RegisterType<WswShortageStoryRepository>().As<IQueryRepository<WswShortageStory>>();
            builder.RegisterType<FailedPartsRepository>().As<IQueryRepository<FailedParts>>();
            builder.RegisterType<ProductionDaysRequiredRepository>().As<IQueryRepository<ProductionDaysRequired>>();
        }
    }
}
