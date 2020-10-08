namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class SupplierResponseProcessor : JsonResponseProcessor<Supplier>
    {
        public SupplierResponseProcessor(IResourceBuilder<Supplier> resourceBuilder)
            : base(resourceBuilder, "supplier", 1)
        {
        }
    }
}