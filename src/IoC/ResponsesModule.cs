namespace Linn.Production.IoC
{
    using System.Collections.Generic;

    using Autofac;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Facade.ResourceBuilders;

    public class ResponsesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // resource builders
            builder.RegisterType<AteFaultCodeResourceBuilder>().As<IResourceBuilder<AteFaultCode>>();
            builder.RegisterType<AteFaultCodesResourceBuilder>().As<IResourceBuilder<IEnumerable<AteFaultCode>>>();
        }
    }
}
