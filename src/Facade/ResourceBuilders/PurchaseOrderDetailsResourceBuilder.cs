namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class PurchaseOrderDetailsResourceBuilder : IResourceBuilder<IEnumerable<PurchaseOrderDetail>>
    {
        private readonly PurchaseOrderDetailResourceBuilder purchaseOrderDetailResourceBuilder = new PurchaseOrderDetailResourceBuilder();

        public IEnumerable<PurchaseOrderDetailResource> Build(IEnumerable<PurchaseOrderDetail> purchaseOrders)
        {
            return purchaseOrders
                .Select(a => this.purchaseOrderDetailResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<PurchaseOrderDetail>>.Build(IEnumerable<PurchaseOrderDetail> purchaseOrders) => this.Build(purchaseOrders);

        public string GetLocation(IEnumerable<PurchaseOrderDetail> purchaseOrders)
        {
            throw new NotImplementedException();
        }
    }
}