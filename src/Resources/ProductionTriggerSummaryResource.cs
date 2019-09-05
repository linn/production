namespace Linn.Production.Resources
{
    using System;

    public class ProductionTriggerSummaryResource
    {
        public string PartNumber { get; set; }

        public string Description { get; set; }

        public int? TriggerLevel { get; set; }

        public int KanbanSize { get; set; }

        public int? EffectiveKanbanSize { get; set; }

        public int MaximumKanbans { get; set; }
        
        public int? RemainingBuild { get; set; }

        public int? QtyBeingBuilt { get; set; }
        
        public int? FixedBuild { get; set; }

        public string Priority { get; set; }
        
        public string ReasonStarted { get; set; }

        public int? SortOrder { get; set; }

        public string EarliestRequestedDate { get; set; }

        public int? CanBuild { get; set; }

        public int? ReqtForInternalAndTriggerLevelBT { get; set; }

        public string Story { get; set; }
    }
}