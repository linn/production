namespace Linn.Production.Facade.ResourceBuilders
{
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
                           Address1 = purchaseOrder.OrderAddress?.Line1,
                           Address2 = purchaseOrder.OrderAddress?.Line2,
                           Address3 = purchaseOrder.OrderAddress?.Line3,
                           Address4 = purchaseOrder.OrderAddress?.Line4,
                           PostCode = purchaseOrder.OrderAddress?.PostCode,
                           Remarks = purchaseOrder.Remarks,
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
                                        }).ToList(),
                           Links = this.BuildLinks(purchaseOrder).ToArray(),
                       };
        }

        public string GetLocation(PurchaseOrder purchaseOrder)
        {
            return $"/production/resources/purchase-orders/{purchaseOrder.OrderNumber}";
        }

        object IResourceBuilder<PurchaseOrder>.Build(PurchaseOrder purchaseOrder)
        {
            return this.Build(purchaseOrder);
        }

        private IEnumerable<LinkResource> BuildLinks(PurchaseOrder purchaseOrder)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(purchaseOrder) };
        }
    }
}
