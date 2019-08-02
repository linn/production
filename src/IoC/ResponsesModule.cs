namespace Linn.Production.IoC
{
    using Linn.Common.Facade;
    using Common.Reporting.Models;
    using Domain;
    using Domain.LinnApps.ATE;
    using Domain.LinnApps.SerialNumberReissue;
    using Facade.ResourceBuilders;

    using Autofac;
    using System.Collections.Generic;

    public class ResponsesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // resource builders
            builder.RegisterType<ResultsModelResourceBuilder>().As<IResourceBuilder<ResultsModel>>();
            builder.RegisterType<ResultsModelsResourceBuilder>().As<IResourceBuilder<IEnumerable<ResultsModel>>>();
            builder.RegisterType<AteFaultCodeResourceBuilder>().As<IResourceBuilder<AteFaultCode>>();
            builder.RegisterType<AteFaultCodesResourceBuilder>().As<IResourceBuilder<IEnumerable<AteFaultCode>>>();
            builder.RegisterType<SerialNumberReissueResourceBuilder>().As<IResourceBuilder<SerialNumberReissue>>();
            builder.RegisterType<ProductionMeasuresResourceBuilder>().As<IResourceBuilder<ProductionMeasures>>();
            builder.RegisterType<ProductionMeasuresListResourceBuilder>().As<IResourceBuilder<IEnumerable<ProductionMeasures>>>();
        }
    }
}
