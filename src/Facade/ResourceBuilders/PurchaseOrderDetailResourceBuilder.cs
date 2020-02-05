namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class PurchaseOrderDetailResourceBuilder : IResourceBuilder<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailResource Build(PurchaseOrderDetail purchaseOrderDetail)
        {
            return new PurchaseOrderDetailResource
                       {
                           OrderLine = purchaseOrderDetail.OrderLine,
                           PartNumber = purchaseOrderDetail.PartNumber,
                           OrderQuantity = purchaseOrderDetail.OrderQuantity,
                           OurUnitOfMeasure = purchaseOrderDetail.OurUnitOfMeasure,
                           IssuedSerialNumbers = purchaseOrderDetail.IssuedSerialNumbers
                       };
        }

        public string GetLocation(PurchaseOrderDetail purchaseOrderDetail)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PurchaseOrderDetail>.Build(PurchaseOrderDetail purchaseOrderDetail) => this.Build(purchaseOrderDetail);

        private IEnumerable<LinkResource> BuildLinks(PurchaseOrderDetail purchaseOrderDetail)
        {
            throw new NotImplementedException();
        }
    }
}