using Linn.Production.Domain.LinnApps;

namespace Linn.Production.IoC
{
    using System.Collections.Generic;

    using Autofac;

    using Common.Facade;
    using Common.Reporting.Models;

    using Domain.LinnApps;
    using Domain.LinnApps.ATE;
    using Domain.LinnApps.Measures;
    using Domain.LinnApps.SerialNumberReissue;

    using Facade.ResourceBuilders;

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
            builder.RegisterType<DepartmentResourceBuilder>().As<IResourceBuilder<Department>>();
            builder.RegisterType<DepartmentsResourceBuilder>().As<IResourceBuilder<IEnumerable<Department>>>();
            builder.RegisterType<ProductionMeasuresResourceBuilder>().As<IResourceBuilder<ProductionMeasures>>();
            builder.RegisterType<ProductionMeasuresListResourceBuilder>().As<IResourceBuilder<IEnumerable<ProductionMeasures>>>();
            builder.RegisterType<ManufacturingSkillResourceBuilder>().As<IResourceBuilder<ManufacturingSkill>>();
            builder.RegisterType<ManufacturingSkillsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ManufacturingSkill>>>();
            builder.RegisterType<ManufacturingRouteResourceBuilder>().As<IResourceBuilder<ManufacturingRoute>>();
            builder.RegisterType<ManufacturingRoutesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ManufacturingRoute>>>();
            builder.RegisterType<ManufacturingOperationResourceBuilder>().As<IResourceBuilder<ManufacturingOperation>>();
            builder.RegisterType<ManufacturingOperationsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ManufacturingOperation>>>();
            builder.RegisterType<BoardFailTypeResourceBuilder>().As<IResourceBuilder<BoardFailType>>();
            builder.RegisterType<BoardFailTypesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<BoardFailType>>>();
        }
    }
}
