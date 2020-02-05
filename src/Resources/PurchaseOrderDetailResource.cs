namespace Linn.Production.Resources
{
    public class PurchaseOrderDetailResource
    {
        public int OrderLine { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public int OrderQuantity { get; set; }

        public string OurUnitOfMeasure { get; set; }

        public string IssuedSerialNumbers { get; set; }
    }
}