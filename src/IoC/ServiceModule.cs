﻿using Linn.Production.Domain.LinnApps;

namespace Linn.Production.IoC
{
    using System.Data;

    using Autofac;

    using Common.Reporting.Models;

    using Domain.LinnApps;
    using Domain.LinnApps.ATE;
    using Domain.LinnApps.RemoteServices;

    using Linn.Common.Configuration;
    using Linn.Common.Facade;
    using Linn.Common.Proxy;
    using Linn.Production.Domain.LinnApps.Reports;
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
            builder.RegisterType<AssemblyFailsWaitingListReportService>().As<IAssemblyFailsWaitingListReportService>();

            // facade services
            builder.RegisterType<AteFaultCodeService>().As<IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource>>();
            builder.RegisterType<BoardFailTypesService>()
                .As<IFacadeService<BoardFailType, int, BoardFailTypeResource, BoardFailTypeResource>>();
            builder.RegisterType<DepartmentService>().As<IFacadeService<Department, string, DepartmentResource, DepartmentResource>>();
            builder.RegisterType<OutstandingWorksOrdersReportFacade>().As<IOutstandingWorksOrdersReportFacade>();
            builder.RegisterType<ProductionMeasuresReportFacade>().As<IProductionMeasuresReportFacade>();
            builder.RegisterType<BuildsByDepartmentReportFacadeService>().As<IBuildsByDepartmentReportFacadeService>();
            builder.RegisterType<WhoBuiltWhatReportFacadeService>().As<IWhoBuiltWhatReportFacadeService>();
            builder.RegisterType<AssemblyFailsWaitingListReportFacadeService>()
                .As<IAssemblyFailsWaitingListReportFacadeService>();
            builder.RegisterType<AteFaultCodeService>().As<IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource>>();
            builder.RegisterType<ManufacturingSkillService>()
                .As<IFacadeService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource>>();
            builder.RegisterType<OutstandingWorksOrdersReportFacade>().As<IOutstandingWorksOrdersReportFacade>();
            builder.RegisterType<AssemblyFailsService>().As<IFacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource>>();
            builder.RegisterType<WorksOrdersService>()
                .As<IFacadeService<WorksOrder, int, WorksOrderResource, WorksOrderResource>>();
            builder.RegisterType<PartsService>()
                .As<IFacadeService<Part, string, PartResource, PartResource>>();
            builder.RegisterType<ProductionTriggerLevelService>()
                .As<IFacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource>>();

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

            // rest client proxies
            builder.RegisterType<RestClient>().As<IRestClient>();
            builder.RegisterType<SalesArticleProxy>().As<ISalesArticleService>().WithParameter("rootUri", ConfigurationManager.Configuration["PROXY_ROOT"]);

            // services
            builder.RegisterType<ReportingHelper>().As<IReportingHelper>();

            // Oracle connection
            builder.RegisterType<OracleConnection>().As<IDbConnection>().WithParameter(
                "connectionString",
                ConnectionStrings.ManagedConnectionString());
            builder.RegisterType<OracleCommand>().As<IDbCommand>();
            builder.RegisterType<OracleDataAdapter>().As<IDataAdapter>();
        }
    }
}