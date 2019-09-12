namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingResourceResourceBuilder : IResourceBuilder<ManufacturingResource>
    {
        public ManufacturingResourceResource Build(ManufacturingResource manufacturingResource)
        {
            return new ManufacturingResourceResource
            {
                ResourceCode = manufacturingResource.ResourceCode,
                Description = manufacturingResource.Description,
                Cost = manufacturingResource.Cost,
                Links = this.BuildLinks(manufacturingResource).ToArray()
            };
        }

        public string GetLocation(ManufacturingResource manufacturingResource)
        {
            return $"/production/resources/manufacturing-resources/{Uri.EscapeDataString(manufacturingResource.ResourceCode)}";
        }

        object IResourceBuilder<ManufacturingResource>.Build(ManufacturingResource manufacturingResource) => this.Build(manufacturingResource);

        private IEnumerable<LinkResource> BuildLinks(ManufacturingResource manufacturingResource)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(manufacturingResource) };
        }
    }
}
