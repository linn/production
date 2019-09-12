namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

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
                           Description = productionTriggerLevel.Description
                       };
        }

        public string GetLocation(ProductionTriggerLevel productionTriggerLevel)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<ProductionTriggerLevel>.Build(ProductionTriggerLevel productionTriggerLevel) => this.Build(productionTriggerLevel);

        private IEnumerable<LinkResource> BuildLinks(ProductionTriggerLevel productionTriggerLevel)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(productionTriggerLevel) };
        }
    }
}