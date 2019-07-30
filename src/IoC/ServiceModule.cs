namespace Linn.Production.IoC
{
    using System.Data;

    using Autofac;
    using Linn.Common.Facade;
    using Domain.LinnApps.ATE;
    using Domain.LinnApps.RemoteServices;
    using Domain.LinnApps.Services;
    using Domain.LinnApps.Reports;
    using Facade.Services;
    using Proxy;
    using Resources;

    using Oracle.ManagedDataAccess.Client;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // domain services
            builder.RegisterType<BuildsSummaryReportService>().As<IBuildsSummaryReportService>();

            // facade services
            builder.RegisterType<BuildsByDepartmentReportFacadeService>().As<IBuildsByDepartmentReportFacadeService>();

            // Oracle proxies
            builder.RegisterType<DatabaseService>().As<IDatabaseService>();
            builder.RegisterType<LrpPack>().As<ILrpPack>();
            builder.RegisterType<LinnWeekPack>().As<ILinnWeekPack>();
            builder.RegisterType<OutstandingWorksOrdersReportService>().As<IOutstandingWorksOrdersReportService>();

            // facade services
            builder.RegisterType<AteFaultCodeService>().As<IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource>>();
            builder.RegisterType<OutstandingWorksOrdersReportFacade>().As<IOutstandingWorksOrdersReportFacade>();
            builder.RegisterType<ProductionMeasuresReportFacade>().As<IProductionMeasuresReportFacade>();

            // Oracle proxies
            builder.RegisterType<DatabaseService>().As<IDatabaseService>();
            builder.RegisterType<OutstandingWorksOrdersReportProxy>()
                .As<IOutstandindWorksOrdersReportDatabaseService>();
            builder.RegisterType<BuildsSummaryReportProxy>().As<IBuildsSummaryReportDatabaseService>();

            builder.RegisterType<OracleConnection>().As<IDbConnection>().WithParameter("connectionString", ConnectionStrings.ManagedConnectionString());
            builder.RegisterType<OracleCommand>().As<IDbCommand>();
            builder.RegisterType<OracleDataAdapter>().As<IDataAdapter>();
        }
    }
}