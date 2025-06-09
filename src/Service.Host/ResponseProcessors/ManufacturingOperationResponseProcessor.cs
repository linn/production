namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ManufacturingOperationResponseProcessor : JsonResponseProcessor<ManufacturingOperation>
    {
        public ManufacturingOperationResponseProcessor(IResourceBuilder<ManufacturingOperation> resourceBuilder)
            : base(resourceBuilder, "manufacturing-operation", 1)
        {
        }
    }
}
