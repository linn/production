namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ManufacturingRouteResponseProcessor : JsonResponseProcessor<ManufacturingRoute>
    {
        public ManufacturingRouteResponseProcessor(IResourceBuilder<ManufacturingRoute> resourceBuilder)
            : base(resourceBuilder, "manufacturing-route", 1)
        {
        }
    }
}
