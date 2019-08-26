namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingResourcesResourceBuilder : IResourceBuilder<IEnumerable<ManufacturingResource>>
    {
        private readonly ManufacturingResourceResourceBuilder manufacturingResourceResourceBuilder = new ManufacturingResourceResourceBuilder();

        public IEnumerable<ManufacturingResourceResource> Build(IEnumerable<ManufacturingResource> manufacturingResources)
        {
            return manufacturingResources
                .OrderBy(b => b.ResourceCode)
                .Select(a => this.manufacturingResourceResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ManufacturingResource>>.Build(IEnumerable<ManufacturingResource> manufacturingResources) => this.Build(manufacturingResources);

        public string GetLocation(IEnumerable<ManufacturingResource> manufacturingResources)
        {
            throw new NotImplementedException();
        }
    }
}
