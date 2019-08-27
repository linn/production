namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ManufacturingResourceResponseProcessor : JsonResponseProcessor<ManufacturingResource>
    {
        public ManufacturingResourceResponseProcessor(IResourceBuilder<ManufacturingResource> resourceBuilder)
            : base(resourceBuilder, "manufacturing-resource", 1)
        {
        }
    }
}
