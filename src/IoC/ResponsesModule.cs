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

    using Linn.Production.Domain.LinnApps.ViewModels;

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
            builder.RegisterType<BoardFailTypeResourceBuilder>().As<IResourceBuilder<BoardFailType>>();
            builder.RegisterType<BoardFailTypesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<BoardFailType>>>();
            builder.RegisterType<AssemblyFailResourceBuilder>().As<IResourceBuilder<AssemblyFail>>();
            builder.RegisterType<WorksOrderResourceBuilder>().As<IResourceBuilder<WorksOrder>>();
            builder.RegisterType<WorksOrdersResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<WorksOrder>>>();
            builder.RegisterType<PartResourceBuilder>().As<IResourceBuilder<Part>>();
            builder.RegisterType<PartsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<Part>>>();
            builder.RegisterType<ProductionTriggerLevelResourceBuilder>().As<IResourceBuilder<ProductionTriggerLevel>>();
            builder.RegisterType<ProductionTriggerLevelsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ProductionTriggerLevel>>>();
            builder.RegisterType<PcasRevisionResourceBuilder>()
                .As<IResourceBuilder<PcasRevision>>();
            builder.RegisterType<PcasRevisionsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<PcasRevision>>>();
        }
    }
}
