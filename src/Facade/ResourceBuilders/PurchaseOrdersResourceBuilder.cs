namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class PurchaseOrdersResourceBuilder : IResourceBuilder<IEnumerable<PurchaseOrder>>
    {
        private readonly PurchaseOrderResourceBuilder purchaseOrderResourceBuilder = new PurchaseOrderResourceBuilder();

        public IEnumerable<PurchaseOrderResource> Build(IEnumerable<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders
                .Select(a => this.purchaseOrderResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<PurchaseOrder>>.Build(IEnumerable<PurchaseOrder> purchaseOrders) => this.Build(purchaseOrders);

        public string GetLocation(IEnumerable<PurchaseOrder> purchaseOrders)
        {
            throw new NotImplementedException();
        }
    }
}