namespace Linn.Production.IoC
{
    using Autofac;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Facade.ResourceBuilders;

    public class ResponsesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // resource builders
            builder.RegisterType<ResultsModelResourceBuilder>().As<IResourceBuilder<ResultsModel>>();
        }
    }
}
