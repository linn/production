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

        public int SernosBuilt { get; set; }

        public int SernosIssued { get; set; }

        public int FirstSernos { get; set; }

        public int LastSernos { get; set; }
    }
}