namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Models;

    public class PurchaseOrderWithSernosInfoResponseProcessor : JsonResponseProcessor<PurchaseOrderWithSernosInfo>
    {
        public PurchaseOrderWithSernosInfoResponseProcessor(IResourceBuilder<PurchaseOrderWithSernosInfo> resourceBuilder)
            : base(resourceBuilder, "purchase-order-with-sernos-info", 1)
        {
        }
    }
}
