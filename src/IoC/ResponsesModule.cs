namespace Linn.Production.IoC
{
    using System.Collections.Generic;

    using Autofac;

    using Domain.LinnApps.SerialNumberReissue;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
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
            builder.RegisterType<CitResourceBuilder>().As<IResourceBuilder<Cit>>();
            builder.RegisterType<CitsResourceBuilder>().As<IResourceBuilder<IEnumerable<Cit>>>();
            builder.RegisterType<BoardFailTypeResourceBuilder>().As<IResourceBuilder<BoardFailType>>();
            builder.RegisterType<BoardFailTypesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<BoardFailType>>>();
            builder.RegisterType<AssemblyFailResourceBuilder>().As<IResourceBuilder<AssemblyFail>>();
            builder.RegisterType<WorksOrderResourceBuilder>().As<IResourceBuilder<WorksOrder>>();
            builder.RegisterType<WorksOrdersResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<WorksOrder>>>();
            builder.RegisterType<ProductionTriggerLevelResourceBuilder>().As<IResourceBuilder<ProductionTriggerLevel>>();
            builder.RegisterType<ProductionTriggerLevelsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ProductionTriggerLevel>>>();
            builder.RegisterType<PcasRevisionResourceBuilder>()
                .As<IResourceBuilder<PcasRevision>>();
            builder.RegisterType<PcasRevisionsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<PcasRevision>>>();
            builder.RegisterType<CitResourceBuilder>()
                .As<IResourceBuilder<Cit>>();
            builder.RegisterType<CitsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<Cit>>>();
            builder.RegisterType<EmployeeResourceBuilder>()
                .As<IResourceBuilder<Employee>>();
            builder.RegisterType<EmployeesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<Employee>>>();
            builder.RegisterType<AssemblyFailFaultCodeResourceBuilder>()
                .As<IResourceBuilder<AssemblyFailFaultCode>>();
            builder.RegisterType<AssemblyFailFaultCodesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<AssemblyFailFaultCode>>>();
            builder.RegisterType<ManufacturingResourceResourceBuilder>().As<IResourceBuilder<ManufacturingResource>>();
            builder.RegisterType<ManufacturingResourcesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ManufacturingResource>>>();
            builder.RegisterType<OsrInfoResourceBuilder>().As<IResourceBuilder<OsrInfo>>();
            builder.RegisterType<WorksOrderResourceBuilder>().As<IResourceBuilder<WorksOrder>>();
            builder.RegisterType<WorksOrdersResourceBuilder>().As<IResourceBuilder<IEnumerable<WorksOrder>>>();
            builder.RegisterType<WorksOrderDetailsResourceBuilder>().As<IResourceBuilder<WorksOrderDetails>>();
            builder.RegisterType<ProductionTriggersReportResourceBuilder>().As<IResourceBuilder<ProductionTriggersReport>>();
            builder.RegisterType<ProductionTriggersFactsResourceBuilder>().As<IResourceBuilder<ProductionTriggerFacts>>();
            builder.RegisterType<PartResourceBuilder>().As<IResourceBuilder<Part>>();
            builder.RegisterType<PartsResourceBuilder>().As<IResourceBuilder<IEnumerable<Part>>>();
            builder.RegisterType<PtlSettingsResourceBuilder>().As<IResourceBuilder<ResponseModel<PtlSettings>>>();
        }
    }
}
