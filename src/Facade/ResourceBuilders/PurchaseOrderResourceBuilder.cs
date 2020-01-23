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
                           DateOfOrder = purchaseOrder.DateOfOrder.ToString("o"),
                           Addressee = purchaseOrder.OrderAddress?.Addressee,
                           Address1 = purchaseOrder.OrderAddress?.Address1,
                           Address2 = purchaseOrder.OrderAddress?.Address2,
                           Address3 = purchaseOrder.OrderAddress?.Address3,
                           Address4 = purchaseOrder.OrderAddress?.Address4,
                           PostCode = purchaseOrder.OrderAddress?.PostCode,
                           Parts = purchaseOrder.Details.Select(d => d.PartNumber).ToList(),
                           Details = purchaseOrder.Details.Select(
                               d => new PurchaseOrderDetailResource
                                        {
                                            OrderLine = d.OrderLine,
                                            PartNumber = d.PartNumber,
                                            PartDescription = d.Part?.Description,
                                            OrderQuantity = d.OrderQuantity,
                                            OurUnitOfMeasure = d.OurUnitOfMeasure,
                                            IssuedSerialNumbers = d.IssuedSerialNumbers
                                        }).ToList()
                       };
        }

        public string GetLocation(PurchaseOrder purchaseOrder)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PurchaseOrder>.Build(PurchaseOrder purchaseOrder)
        {
            return this.Build(purchaseOrder);
        }

        private IEnumerable<LinkResource> BuildLinks(PurchaseOrder purchaseOrder)
        {
            throw new NotImplementedException();
        }
    }
}
