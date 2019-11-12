namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ManufacturingRoutesResponseProcessor : JsonResponseProcessor<ResponseModel<IEnumerable<ManufacturingRoute>>>
    {
        public ManufacturingRoutesResponseProcessor(IResourceBuilder<ResponseModel<IEnumerable<ManufacturingRoute>>> resourceBuilder)
            : base(resourceBuilder, "manufacturing-routes", 1)
        {
        }
    }
}
