namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ManufacturingOperationsResponseProcessor : JsonResponseProcessor<IEnumerable<ManufacturingOperation>>
    {
        public ManufacturingOperationsResponseProcessor(IResourceBuilder<IEnumerable<ManufacturingOperation>> resourceBuilder)
            : base(resourceBuilder, "manufacturing-operations", 1)
        {
        }
    }
}
