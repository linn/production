namespace Linn.Production.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Resources;

    public class ProductionTriggerSummaryResourceBuilder : IResourceBuilder<ProductionTrigger>
    {
        public object Build(ProductionTrigger trigger)
        {
            return this.BuildSummary(trigger);
        }

        public ProductionTriggerSummaryResource BuildSummary(ProductionTrigger trigger)
        {
            return new ProductionTriggerSummaryResource
            {
                PartNumber = trigger.PartNumber,
                Description = trigger.Description,
                TriggerLevel = trigger.TriggerLevel,
                Priority = trigger.Priority,
                CanBuild = trigger.CanBuild,
                FixedBuild = trigger.FixedBuild,
                QtyBeingBuilt = trigger.QtyBeingBuilt,
                KanbanSize = trigger.KanbanSize,
                EffectiveKanbanSize = trigger.EffectiveKanbanSize,
                EarliestRequestedDate = trigger.EarliestRequestedDate?.ToString("o"),
                ReasonStarted = trigger.ReasonStarted,
                RemainingBuild = trigger.RemainingBuild,
                MaximumKanbans = trigger.MaximumKanbans,
                ReqtForInternalAndTriggerLevelBT = trigger.ReqtForInternalAndTriggerLevelBT,
                Story = trigger.Story,
                SortOrder = trigger.SortOrder,
                ReportFormat = trigger.ReportType
            };
        }

        public string GetLocation(ProductionTrigger model)
        {
            throw new System.NotImplementedException();
        }
    }
}