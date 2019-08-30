namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ManufacturingRoutesResponseProcessor : JsonResponseProcessor<IEnumerable<ManufacturingRoute>>
    {
        public ManufacturingRoutesResponseProcessor(IResourceBuilder<IEnumerable<ManufacturingRoute>> resourceBuilder)
            : base(resourceBuilder, "manufacturing-routes", 1)
        {
        }
    }
}
