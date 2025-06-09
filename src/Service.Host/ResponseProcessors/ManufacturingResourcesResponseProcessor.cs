namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ManufacturingResourcesResponseProcessor : JsonResponseProcessor<IEnumerable<ManufacturingResource>>
    {
        public ManufacturingResourcesResponseProcessor(IResourceBuilder<IEnumerable<ManufacturingResource>> resourceBuilder)
            : base(resourceBuilder, "manufacturing-resources", 1)
        {
        }
    }
}
