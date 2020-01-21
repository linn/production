namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Resources;

    public class ProductionTriggersFactsResourceBuilder : IResourceBuilder<ProductionTriggerFacts>
    {
        private readonly WorksOrderResourceBuilder worksOrderResourceBuilder = new WorksOrderResourceBuilder();

        private readonly ProductionBackOrderResourceBuilder productionBackOrderResourceBuilder = new ProductionBackOrderResourceBuilder();

        private readonly ProductionTriggerAssemblyResourceBuilder productionTriggerAssemblyResourceBuilder = new ProductionTriggerAssemblyResourceBuilder();

        public object Build(ProductionTriggerFacts facts)
        {
            return new ProductionTriggerFactsResultsResource
            {
                ReportResults = new ProductionTriggerFactsResource
                {
                    PartNumber = facts.Trigger.PartNumber,
                    QtyBeingBuilt = facts.Trigger.QtyBeingBuilt,
                    EarliestRequestedDate = facts.Trigger.EarliestRequestedDate.ToString(),
                    FixedBuild = facts.Trigger.FixedBuild,
                    Jobref = facts.Trigger.Jobref,
                    RemainingBuild = facts.Trigger.RemainingBuild,
                    MaximumKanbans = facts.Trigger.MaximumKanbans,
                    CanBuild = facts.Trigger.CanBuild,
                    ReasonStarted = facts.Trigger.ReasonStarted,
                    EffectiveKanbanSize = facts.Trigger.EffectiveKanbanSize,
                    KanbanSize = facts.Trigger.KanbanSize,
                    TriggerLevel = facts.Trigger.TriggerLevel,
                    OverrideTriggerLevel = facts.Trigger.OverrideTriggerLevel,
                    VariableTriggerLevel = facts.Trigger.VariableTriggerLevel,
                    EffectiveTriggerLevel = facts.Trigger.EffectiveTriggerLevel,
                    TriggerLevelText = facts.Trigger.TriggerLevelText,
                    Description = facts.Trigger.Description,
                    Priority = facts.Trigger.Priority,
                    CanBuildExSubAssemblies = facts.Trigger.CanBuildExSubAssemblies,
                    Citcode = facts.Trigger.Citcode,
                    CitName = facts.Trigger.CitName,
                    DaysToBuildKanban = facts.Trigger.DaysToBuildKanban,
                    DaysTriggerLasts = facts.Trigger.DaysTriggerLasts,
                    FixedBuildDays = facts.Trigger.FixedBuildDays,
                    MWPriority = facts.Trigger.MWPriority,
                    MfgRouteCode = facts.Trigger.MfgRouteCode,
                    NettSalesOrders = facts.Trigger.NettSalesOrders,
                    OnHold = facts.Trigger.OnHold,
                    QtyBeingBuiltDays = facts.Trigger.QtyBeingBuiltDays,
                    QtyFFlagged = facts.Trigger.QtyFFlagged,
                    QtyFree = facts.Trigger.QtyFree,
                    QtyManualWo = facts.Trigger.QtyManualWo,
                    QtyNFlagged = facts.Trigger.QtyNFlagged,
                    QtyYFlagged = facts.Trigger.QtyYFlagged,
                    ReqtForInternalAndTriggerLevelBT = facts.Trigger.ReqtForInternalAndTriggerLevelBT,
                    ReqtForInternalCustomersBI = facts.Trigger.ReqtForInternalCustomersBI,
                    ReqtForInternalCustomersBIDays = facts.Trigger.ReqtForInternalCustomersBIDays,
                    ReqtForInternalCustomersGBI = facts.Trigger.ReqtForInternalCustomersGBI,
                    ReqtForInternalTriggerBTDays = facts.Trigger.ReqtForInternalTriggerBTDays,
                    ReqtForSalesOrdersBE = facts.Trigger.ReqtForSalesOrdersBE,
                    ReqtForSalesOrdersBEDays = facts.Trigger.ReqtForSalesOrdersBEDays,
                    ReqtForSalesOrdersGBE = facts.Trigger.ReqtForSalesOrdersGBE,
                    ReqtFromFixedBuild = facts.Trigger.ReqtFromFixedBuild,
                    ShortNowBackOrdered = facts.Trigger.ShortNowBackOrdered,
                    ShortNowMonthEnd = facts.Trigger.ShortNowMonthEnd,
                    StockAvailableShortNowBackOrdered = facts.Trigger.StockAvailableShortNowBackOrdered,
                    StockReqtPercNt = facts.Trigger.StockReqtPercNt,
                    Story = facts.Trigger.Story,
                    OutstandingWorksOrders = facts.OutstandingWorksOrders.Select(w => this.worksOrderResourceBuilder.Build(w)),
                    ProductionBackOrders = facts.OutstandingSalesOrders.Select(o => this.productionBackOrderResourceBuilder.Build(o)),
                    WhereUsedAssemblies = facts.WhereUsedAssemblies.Select(a => this.productionTriggerAssemblyResourceBuilder.BuildAssembly(a))
                }
            };
        }

        public string GetLocation(ProductionTriggerFacts model)
        {
            throw new System.NotImplementedException();
        }
    }
}