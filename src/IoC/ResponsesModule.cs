namespace Linn.Production.IoC
{
    using System.Collections.Generic;

    using Autofac;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.BoardTests;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
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
            builder.RegisterType<ManufacturingRouteResourceBuilder>().As<IResourceBuilder<ResponseModel<ManufacturingRoute>>>();
            builder.RegisterType<ManufacturingRoutesResourceBuilder>()
                .As<IResourceBuilder<ResponseModel<IEnumerable<ManufacturingRoute>>>>();
            builder.RegisterType<ManufacturingOperationResourceBuilder>().As<IResourceBuilder<ManufacturingOperation>>();
            builder.RegisterType<ManufacturingOperationsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ManufacturingOperation>>>();
            builder.RegisterType<CitResourceBuilder>().As<IResourceBuilder<Cit>>();
            builder.RegisterType<CitsResourceBuilder>().As<IResourceBuilder<IEnumerable<Cit>>>();
            builder.RegisterType<BoardFailTypeResourceBuilder>().As<IResourceBuilder<BoardFailType>>();
            builder.RegisterType<BoardFailTypesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<BoardFailType>>>();
            builder.RegisterType<AssemblyFailResourceBuilder>().As<IResourceBuilder<AssemblyFail>>();
            builder.RegisterType<AssemblyFailsResourceBuilder>().As<IResourceBuilder<IEnumerable<AssemblyFail>>>();
            builder.RegisterType<WorksOrderResourceBuilder>().As<IResourceBuilder<WorksOrder>>();
            builder.RegisterType<WorksOrdersResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<WorksOrder>>>();
            builder.RegisterType<ProductionTriggerLevelResourceBuilder>().As<IResourceBuilder<ResponseModel<ProductionTriggerLevel>>>();
            builder.RegisterType<ProductionTriggerLevelsResourceBuilder>()
                .As<IResourceBuilder<ResponseModel<IEnumerable<ProductionTriggerLevel>>>>();
            builder.RegisterType<PcasRevisionResourceBuilder>()
                .As<IResourceBuilder<PcasRevision>>();
            builder.RegisterType<PcasRevisionsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<PcasRevision>>>();
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
            builder.RegisterType<WorksOrderPartDetailsResourceBuilder>().As<IResourceBuilder<WorksOrderPartDetails>>();
            builder.RegisterType<ProductionTriggersReportResourceBuilder>().As<IResourceBuilder<ProductionTriggersReport>>();
            builder.RegisterType<ProductionTriggersFactsResourceBuilder>().As<IResourceBuilder<ProductionTriggerFacts>>();
            builder.RegisterType<PartResourceBuilder>().As<IResourceBuilder<Part>>();
            builder.RegisterType<PartsResourceBuilder>().As<IResourceBuilder<IEnumerable<Part>>>();
            builder.RegisterType<PartFailSupplierResourceBuilder>().As<IResourceBuilder<PartFailSupplierView>>();
            builder.RegisterType<PartFailSuppliersResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<PartFailSupplierView>>>();
            builder.RegisterType<SmtShiftResourceBuilder>().As<IResourceBuilder<SmtShift>>();
            builder.RegisterType<SmtShiftsResourceBuilder>().As<IResourceBuilder<IEnumerable<SmtShift>>>();
            builder.RegisterType<PtlSettingsResourceBuilder>().As<IResourceBuilder<ResponseModel<PtlSettings>>>();
            builder.RegisterType<PtlSettingsResourceBuilder>().As<IResourceBuilder<ResponseModel<PtlSettings>>>();
            builder.RegisterType<ErrorResourceBuilder>().As<IResourceBuilder<Error>>();
            builder.RegisterType<PartFailResourceBuilder>().As<IResourceBuilder<PartFail>>();
            builder.RegisterType<PartFailsResourceBuilder>().As<IResourceBuilder<IEnumerable<PartFail>>>();
            builder.RegisterType<PartFailErrorTypeResourceBuilder>().As<IResourceBuilder<PartFailErrorType>>();
            builder.RegisterType<PartFailErrorTypesResourceBuilder>().As<IResourceBuilder<IEnumerable<PartFailErrorType>>>();
            builder.RegisterType<StoragePlaceResourceBuilder>().As<IResourceBuilder<StoragePlace>>();
            builder.RegisterType<StoragePlacesResourceBuilder>().As<IResourceBuilder<IEnumerable<StoragePlace>>>();
            builder.RegisterType<PartFailFaultCodeResourceBuilder>()
                .As<IResourceBuilder<PartFailFaultCode>>();
            builder.RegisterType<PartFailFaultCodesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<PartFailFaultCode>>>();
            builder.RegisterType<PurchaseOrderResourceBuilder>()
                .As<IResourceBuilder<PurchaseOrder>>();
            builder.RegisterType<PurchaseOrdersResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<PurchaseOrder>>>();
            builder.RegisterType<ManufacturingCommitDateResourceBuilder>().As<IResourceBuilder<ManufacturingCommitDateResults>>();
            builder.RegisterType<WwdResultResourceBuilder>().As<IResourceBuilder<WwdResult>>();
            builder.RegisterType<WorksOrderLabelResourceBuilder>().As<IResourceBuilder<WorksOrderLabel>>();
            builder.RegisterType<WorksOrderLabelsResourceBuilder>().As<IResourceBuilder<IEnumerable<WorksOrderLabel>>>();
            builder.RegisterType<LabelTypeResourceBuilder>().As<IResourceBuilder<LabelType>>();
            builder.RegisterType<LabelTypesResourceBuilder>().As<IResourceBuilder<IEnumerable<LabelType>>>();
            builder.RegisterType<WorkStationResourceBuilder>().As<IResourceBuilder<WorkStation>>();
            builder.RegisterType<WorkStationsResourceBuilder>().As<IResourceBuilder<IEnumerable<WorkStation>>>();
            builder.RegisterType<LabelReprintResourceBuilder>().As<IResourceBuilder<ResponseModel<LabelReprint>>>();
            builder.RegisterType<BuildPlanResourceBuilder>().As<IResourceBuilder<ResponseModel<BuildPlan>>>();
            builder.RegisterType<BuildPlansResourceBuilder>().As<IResourceBuilder<ResponseModel<IEnumerable<BuildPlan>>>>();
            builder.RegisterType<BuildPlanRuleResourceBuilder>().As<IResourceBuilder<ResponseModel<BuildPlanRule>>>();
            builder.RegisterType<BuildPlanRulesResourceBuilder>().As<IResourceBuilder<ResponseModel<IEnumerable<BuildPlanRule>>>>();
            builder.RegisterType<BuildPlanDetailResourceBuilder>().As<IResourceBuilder<ResponseModel<BuildPlanDetail>>>();
            builder.RegisterType<BuildPlanDetailsResourceBuilder>()
                .As<IResourceBuilder<ResponseModel<IEnumerable<BuildPlanDetail>>>>();
                            builder.RegisterType<AteTestResourceBuilder>().As<IResourceBuilder<AteTest>>();
            builder.RegisterType<AteTestsResourceBuilder>().As<IResourceBuilder<IEnumerable<AteTest>>>();
            builder.RegisterType<AteTestDetailResourceBuilder>().As<IResourceBuilder<AteTestDetail>>();
            builder.RegisterType<AteTestDetailsResourceBuilder>().As<IResourceBuilder<IEnumerable<AteTestDetail>>>();
            builder.RegisterType<LabelPrintResourceBuilder>().As<IResourceBuilder<LabelPrint>>();
            builder.RegisterType<IdAndNameResourceBuilder>().As<IResourceBuilder<IdAndName>>();
            builder.RegisterType<IdAndNameListResourceBuilder>().As<IResourceBuilder<IEnumerable<IdAndName>>>();
            builder.RegisterType<ComponentCountResourceBuilder>().As<IResourceBuilder<ComponentCount>>();

        }
    }
}
