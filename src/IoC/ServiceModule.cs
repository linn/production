using Linn.Production.Domain.LinnApps.SerialNumberReissue;

namespace Linn.Production.IoC
{
    using System.Data;

    using Autofac;

    using Linn.Common.Facade;
    using Domain.LinnApps.ATE;
    using Domain.LinnApps.RemoteServices;
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
            builder.RegisterType<OutstandingWorksOrdersReportService>().As<IOutstandingWorksOrdersReportService>();

            // facade services
            builder.RegisterType<AteFaultCodeService>().As<IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource>>();
            builder.RegisterType<SerialNumberReissueService>()
                .As<IFacadeService<SerialNumberReissue, int, SerialNumberReissueResource, SerialNumberReissueResource>>();
            builder.RegisterType<OutstandingWorksOrdersReportFacade>().As<IOutstandingWorksOrdersReportFacade>();

            // Oracle proxies
            builder.RegisterType<DatabaseProxy>().As<IDatabaseService>();
            builder.RegisterType<OutstandingWorksOrdersReportProxy>()
                .As<IOutstandindWorksOrdersReportDatabaseService>();

            builder.RegisterType<OracleConnection>().As<IDbConnection>().WithParameter("connectionString", ConnectionStrings.ManagedConnectionString());
            builder.RegisterType<OracleCommand>().As<IDbCommand>();
            builder.RegisterType<OracleDataAdapter>().As<IDataAdapter>();
        }
    }
}