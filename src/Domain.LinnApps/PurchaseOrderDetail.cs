namespace Linn.Production.Domain.LinnApps
{
    public class PurchaseOrderDetail
    {
        public PurchaseOrder PurchaseOrder { get; set; }

        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public string PartNumber { get; set; }

        public Part Part { get; set; }

        public int OrderQuantity { get; set; }

        public string OurUnitOfMeasure { get; set; }

        public string IssuedSerialNumbers { get; set; }
    }
}