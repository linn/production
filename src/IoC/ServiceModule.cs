namespace Linn.Production.IoC
{


    using Autofac;

    using Domain.LinnApps.RemoteServices;
    using Domain.LinnApps.Services;

    using Linn.Production.Facade.Services;
    using Linn.Production.Proxy;

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
        }
    }
}