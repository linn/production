﻿namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class ProductionTriggerLevelResource : HypermediaResource
    {
        public string PartNumber { get; set; }

        public string Description { get; set; }

        public int? TriggerLevel { get; set; }

        public int KanbanSize { get; set; }

        public int MaximumKanbans { get; set; }

        public string CitCode { get; set; }

        public CitResource Cit { get; set; }

        public int? BomLevel { get; set; }

        public string WorkStationName { get; set; }

        public string FaZoneType { get; set; }

        public int? VariableTriggerLevel { get; set; }

        public int? OverrideTriggerLevel { get; set; }

        public string Temporary { get; set; }

        public int? EngineerId { get; set; }

        public string Story { get; set; }

        public string RouteCode { get; set; }
    }
}
