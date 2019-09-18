namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ProductionTriggerLevelResourceBuilder : IResourceBuilder<ProductionTriggerLevel>
    {
        public ProductionTriggerLevelResource Build(ProductionTriggerLevel productionTriggerLevel)
        {
            return new ProductionTriggerLevelResource
                       {
                           PartNumber = productionTriggerLevel.PartNumber,
                           Description = productionTriggerLevel.Description,
                           CitCode = productionTriggerLevel.CitCode,
                           BomLevel = productionTriggerLevel.BomLevel,
                           FaZoneType = productionTriggerLevel.FaZoneType,
                           KanbanSize = productionTriggerLevel.KanbanSize,
                           MaximumKanbans = productionTriggerLevel.MaximumKanbans,
                           OverrideTriggerLevel = productionTriggerLevel.OverrideTriggerLevel,
                           TriggerLevel = productionTriggerLevel.TriggerLevel,
                           VariableTriggerLevel = productionTriggerLevel.VariableTriggerLevel,
                           WsName = productionTriggerLevel.WsName,
                           Links = this.BuildLinks(productionTriggerLevel).ToArray()
                       };
        }

        public string GetLocation(ProductionTriggerLevel productionTriggerLevel)
        {
            return $"production/maintenance/production-trigger-levels/{productionTriggerLevel.PartNumber}";
        }

        object IResourceBuilder<ProductionTriggerLevel>.Build(ProductionTriggerLevel productionTriggerLevel) => this.Build(productionTriggerLevel);

        private IEnumerable<LinkResource> BuildLinks(ProductionTriggerLevel productionTriggerLevel)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(productionTriggerLevel) };
        }
    }
}