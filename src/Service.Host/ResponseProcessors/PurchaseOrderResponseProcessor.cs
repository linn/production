namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class PurchaseOrderResponseProcessor : JsonResponseProcessor<PurchaseOrder>
    {
        public PurchaseOrderResponseProcessor(IResourceBuilder<PurchaseOrder> resourceBuilder)
            : base(resourceBuilder, "purchase-order", 1)
        {
        }
    }
}