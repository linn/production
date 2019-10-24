namespace Linn.Production.Facade.ResourceBuilders
{ 
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class PurchaseOrderResourceBuilder : IResourceBuilder<PurchaseOrder>
    {
        public PurchaseOrderResource Build(PurchaseOrder purchaseOrder)
        {
            return new PurchaseOrderResource
                       {
                           OrderNumber = purchaseOrder.OrderNumber,
                           Parts = purchaseOrder.Details.Select(d => d.PartNumber).ToList()
                       };
        }

        public string GetLocation(PurchaseOrder purchaseOrder)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PurchaseOrder>.Build(PurchaseOrder purchaseOrder) => this.Build(purchaseOrder);

        private IEnumerable<LinkResource> BuildLinks(PurchaseOrder purchaseOrder)
        {
            throw new NotImplementedException();
        }
    }
}