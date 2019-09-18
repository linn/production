namespace Linn.Production.Resources
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

        public int? BomLevel { get; set; }

        public string WsName { get; set; }

        public string FaZoneType { get; set; }

        public int? VariableTriggerLevel { get; set; }

        public int? OverrideTriggerLevel { get; set; }
    }
}