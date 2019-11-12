namespace Linn.Production.IoC
{
    using System.Data;
    using Autofac;

    using Linn.Common.Authorisation;
    using Linn.Common.Configuration;
    using Linn.Common.Facade;
    using Linn.Common.Proxy;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.BoardTests;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.Reports.Smt;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Proxy;
    using Linn.Production.Resources;
    using Oracle.ManagedDataAccess.Client;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // domain services
            builder.RegisterType<BuildsSummaryReportService>().As<IBuildsSummaryReportService>();
            builder.RegisterType<WhoBuiltWhatReport>().As<IWhoBuiltWhatReport>();
            builder.RegisterType<OutstandingWorksOrdersReportService>().As<IOutstandingWorksOrdersReportService>();
            builder.RegisterType<BuildsDetailReportService>().As<IBuildsDetailReportService>();
            builder.RegisterType<WorksOrderFactory>().As<IWorksOrderFactory>();
            builder.RegisterType<AssemblyFailsReportService>().As<IAssemblyFailsReportService>();
            builder.RegisterType<LinnWeekService>().As<ILinnWeekService>();
            builder.RegisterType<SmtReports>().As<ISmtReports>();
            builder.RegisterType<WorksOrderUtilities>().As<IWorksOrderUtilities>();
            builder.RegisterType<PartFailService>().As<IPartFailService>();
            builder.RegisterType<OverdueOrdersReportService>().As<IOverdueOrdersService>();
            builder.RegisterType<ManufacturingCommitDateReport>().As<IManufacturingCommitDateReport>();
            builder.RegisterType<PartsReportService>().As<IPartsReportService>();
            builder.RegisterType<LabelService>().As<ILabelService>();
            builder.RegisterType<BoardTestReports>().As<IBoardTestReports>();

            // facade services
            builder.RegisterType<AteFaultCodeService>().As<IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource>>();
            builder.RegisterType<BoardFailTypesService>()
                .As<IFacadeService<BoardFailType, int, BoardFailTypeResource, BoardFailTypeResource>>();
            builder.RegisterType<DepartmentService>().As<IFacadeService<Department, string, DepartmentResource, DepartmentResource>>();
            builder.RegisterType<OutstandingWorksOrdersReportFacade>().As<IOutstandingWorksOrdersReportFacade>();
            builder.RegisterType<ProductionMeasuresReportFacade>().As<IProductionMeasuresReportFacade>();
            builder.RegisterType<BuildsByDepartmentReportFacadeService>().As<IBuildsByDepartmentReportFacadeService>();
            builder.RegisterType<WhoBuiltWhatReportFacadeService>().As<IWhoBuiltWhatReportFacadeService>();
            builder.RegisterType<CitService>().As<IFacadeService<Cit, string, CitResource, CitResource>>();
            builder.RegisterType<AssemblyFailsReportsFacadeService>()
                .As<IAssemblyFailsReportsFacadeService>();
            builder.RegisterType<AteFaultCodeService>().As<IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource>>();
            builder.RegisterType<ManufacturingSkillService>()
                .As<IFacadeService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource>>();
            builder.RegisterType<OutstandingWorksOrdersReportFacade>().As<IOutstandingWorksOrdersReportFacade>();
            builder.RegisterType<AssemblyFailsService>().As<IFacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource>>();
            builder.RegisterType<WorksOrdersService>().As<IWorksOrdersService>();
            builder.RegisterType<ProductionTriggerLevelService>()
                .As<IFacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource>>();
            builder.RegisterType<PcasRevisionService>()
                .As<IFacadeService<PcasRevision, string, PcasRevisionResource, PcasRevisionResource>>();
            builder.RegisterType<EmployeeService>()
                .As<IFacadeService<Employee, int, EmployeeResource, EmployeeResource>>();
            builder.RegisterType<AssemblyFailFaultCodesService>()
                .As<IFacadeService<AssemblyFailFaultCode, string, AssemblyFailFaultCodeResource, AssemblyFailFaultCodeResource>>();
            builder.RegisterType<AteFaultCodeService>().As<IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource>>();
            builder.RegisterType<ManufacturingSkillService>()
                .As<IFacadeService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource>>();
            builder.RegisterType<OutstandingWorksOrdersReportFacade>().As<IOutstandingWorksOrdersReportFacade>();
            builder.RegisterType<ManufacturingResourceService>()
                .As<IFacadeService<ManufacturingResource, string, ManufacturingResourceResource, ManufacturingResourceResource>>();
            builder.RegisterType<ManufacturingRouteService>()
                .As<IFacadeService<ManufacturingRoute, string, ManufacturingRouteResource, ManufacturingRouteResource>>();
            builder.RegisterType<ManufacturingOperationsService>()
                .As<IFacadeService<ManufacturingOperation, int, ManufacturingOperationResource, ManufacturingOperationResource>>();
            builder.RegisterType<SmtReportsFacadeService>().As<ISmtReportsFacadeService>();
            builder.RegisterType<PartsFacadeService>().As<IFacadeService<Part, string, PartResource, PartResource>>();
            builder.RegisterType<SmtShiftsService>()
                .As<IFacadeService<SmtShift, string, SmtShiftResource, SmtShiftResource>>();
            builder.RegisterType<PtlSettingsFacadeService>().As<ISingleRecordFacadeService<PtlSettings, PtlSettingsResource>>();
            builder.RegisterType<PtlSettingsFacadeService>().As<ISingleRecordFacadeService<PtlSettings, PtlSettingsResource>>();
            builder.RegisterType<PartFailFacadeService>()
                .As<IFacadeService<PartFail, int, PartFailResource, PartFailResource>>();
            builder.RegisterType<PartFailErrorTypeService>()
                .As<IFacadeService<PartFailErrorType, string, PartFailErrorTypeResource, PartFailErrorTypeResource>>();
            builder.RegisterType<StoragePlaceService>().As<IStoragePlaceService>();
            builder.RegisterType<PartFailFaultCodeService>()
                .As<IFacadeService<PartFailFaultCode, string, PartFailFaultCodeResource, PartFailFaultCodeResource>>();
            builder.RegisterType<PurchaseOrderService>()
                .As<IFacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource>>();
            builder.RegisterType<OrdersReportsFacadeService>().As<IOrdersReportsFacadeService>();
            builder.RegisterType<PartsReportFacadeService>().As<IPartsReportFacadeService>();
            builder.RegisterType<BoardTestsService>()
                .As<IFacadeService<BoardTest, BoardTestKey, BoardTestResource, BoardTestResource>>();
            builder.RegisterType<BoardTestReportFacadeService>().As<IBoardTestReportFacadeService>();
            builder.RegisterType<PartFailSupplierService>().As<IPartFailSupplierService>();

            // oracle proxies
            builder.RegisterType<DatabaseService>().As<IDatabaseService>();
            builder.RegisterType<BuildsSummaryReportProxy>().As<IBuildsSummaryReportDatabaseService>();
            builder.RegisterType<OutstandingWorksOrdersReportProxy>()
                .As<IOutstandingWorksOrdersReportDatabaseService>();
            builder.RegisterType<SerialNumberReissueService>().As<ISerialNumberReissueService>();
            builder.RegisterType<SernosRenumPack>().As<ISernosRenumPack>();
            builder.RegisterType<BuildsDetailReportProxy>().As<IBuildsDetailReportDatabaseService>();
            builder.RegisterType<OutstandingWorksOrdersReportService>().As<IOutstandingWorksOrdersReportService>();
            builder.RegisterType<LinnWeekPack>().As<ILinnWeekPack>();
            builder.RegisterType<WorksOrderProxy>().As<IWorksOrderProxyService>();
            builder.RegisterType<SernosPack>().As<ISernosPack>();
            builder.RegisterType<ProductionTriggersFacadeService>().As<IProductionTriggersFacadeService>();
            builder.RegisterType<TriggerRunPack>().As<ITriggerRunPack>();
            builder.RegisterType<BartenderLabelPack>().As<IBartenderLabelPack>();
            builder.RegisterType<LabelPack>().As<ILabelPack>();

            // rest client proxies
            builder.RegisterType<RestClient>().As<IRestClient>();
            builder.RegisterType<SalesArticleProxy>().As<ISalesArticleService>().WithParameter("rootUri", ConfigurationManager.Configuration["PROXY_ROOT"]);

            // services
            builder.RegisterType<ReportingHelper>().As<IReportingHelper>();
            builder.RegisterType<AuthorisationService>().As<IAuthorisationService>();

            // Oracle connection
            builder.RegisterType<OracleConnection>().As<IDbConnection>().WithParameter(
                "connectionString",
                ConnectionStrings.ManagedConnectionString());
            builder.RegisterType<OracleCommand>().As<IDbCommand>();
            builder.RegisterType<OracleDataAdapter>().As<IDataAdapter>();
        }
    }
}
