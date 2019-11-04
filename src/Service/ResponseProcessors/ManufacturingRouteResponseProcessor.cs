namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ManufacturingRouteResponseProcessor : JsonResponseProcessor<ResponseModel<ManufacturingRoute>>
    {
        public ManufacturingRouteResponseProcessor(IResourceBuilder<ResponseModel<ManufacturingRoute>> resourceBuilder)
            : base(resourceBuilder, "manufacturing-route", 1)
        {
        }
    }
}
