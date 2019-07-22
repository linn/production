namespace Linn.Production.IoC
{
    using Autofac;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Facade.Services;
    using Linn.Production.Proxy;
    using Linn.Production.Resources;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // domain services
            builder.RegisterType<BuildsByDepartmentReportService>().As<IBuildsByDepartmentReportService>();

            // facade services
            builder.RegisterType<BuildsByDepartmentReportFacadeService>().As<IBuildsByDepartmentReportFacadeService>();

            // Oracle proxies
            builder.RegisterType<DatabaseService>().As<IDatabaseService>();
            builder.RegisterType<LrpPack>().As<ILrpPack>();
            builder.RegisterType<LinnWeekPack>().As<ILinnWeekPack>();

            // facade services
            builder.RegisterType<AteFaultCodeService>().As<IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource>>();
        }
    }
}