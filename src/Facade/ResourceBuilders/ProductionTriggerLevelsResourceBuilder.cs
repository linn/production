namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ProductionTriggerLevelsResourceBuilder : IResourceBuilder<IEnumerable<ProductionTriggerLevel>>
    {
        private readonly ProductionTriggerLevelResourceBuilder productionTriggerLevelResourceBuilder = new ProductionTriggerLevelResourceBuilder();

        public IEnumerable<ProductionTriggerLevelResource> Build(IEnumerable<ProductionTriggerLevel> productionTriggerLevels)
        {
            return productionTriggerLevels
                .OrderBy(b => b.PartNumber)
                .Select(a => this.productionTriggerLevelResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ProductionTriggerLevel>>.Build(IEnumerable<ProductionTriggerLevel> productionTriggerLevels) => this.Build(productionTriggerLevels);

        public string GetLocation(IEnumerable<ProductionTriggerLevel> productionTriggerLevels)
        {
            throw new NotImplementedException();
        }
    }
}