using Linn.Production.Domain;

namespace Linn.Production.IoC
{
    using Autofac;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using System.Collections.Generic;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Facade.ResourceBuilders;

    public class ResponsesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // resource builders
            builder.RegisterType<ResultsModelResourceBuilder>().As<IResourceBuilder<ResultsModel>>();
            builder.RegisterType<ResultsModelsResourceBuilder>().As<IResourceBuilder<IEnumerable<ResultsModel>>>();
            builder.RegisterType<AteFaultCodeResourceBuilder>().As<IResourceBuilder<AteFaultCode>>();
            builder.RegisterType<AteFaultCodesResourceBuilder>().As<IResourceBuilder<IEnumerable<AteFaultCode>>>();
            builder.RegisterType<ProductionMeasuresResourceBuilder>().As<IResourceBuilder<ProductionMeasures>>();
            builder.RegisterType<ProductionMeasuresListResourceBuilder>().As<IResourceBuilder<IEnumerable<ProductionMeasures>>>();
        }
    }
}
