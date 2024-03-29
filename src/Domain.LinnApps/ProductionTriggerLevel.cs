﻿namespace Linn.Production.Domain.LinnApps
{
    using Linn.Production.Domain.LinnApps.Measures;

    public class ProductionTriggerLevel
    {
        public string PartNumber { get; set; }

        public string Description { get; set; }

        public int? TriggerLevel { get; set; }

        public int? VariableTriggerLevel { get; set; }

        public int? OverrideTriggerLevel { get; set; }

        public int KanbanSize { get; set; }

        public int MaximumKanbans { get; set; }

        public string CitCode { get; set; }

        public Cit Cit { get; set; }

        public int? BomLevel { get; set; }

        public string WorkStationName { get; set; }

        public string FaZoneType { get; set; }

        public string RouteCode { get; set; }

        public string Temporary { get; set; }

        public int? EngineerId { get; set; }

        public string Story { get; set; }
    }
}
