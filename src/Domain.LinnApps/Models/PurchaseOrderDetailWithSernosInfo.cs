namespace Linn.Production.Domain.LinnApps
{
    public class PurchaseOrderDetailWithSernosInfo : PurchaseOrderDetail
    {
        public PurchaseOrderDetailWithSernosInfo(PurchaseOrderDetail detail)
        {
            this.PartNumber = detail.PartNumber;
            this.OrderLine = detail.OrderLine;
            this.OrderNumber = detail.OrderNumber;
            this.OrderQuantity = detail.OrderQuantity;
            this.OurUnitOfMeasure = detail.OurUnitOfMeasure;
            this.Part = detail.Part;
            this.IssuedSerialNumbers = detail.IssuedSerialNumbers;
            this.PurchaseOrder = detail.PurchaseOrder;
        }

        public int SernosBuilt { get; set; }

        public int SernosIssued { get; set; }

        public int FirstSernos { get; set; }

        public int LastSernos { get; set; }

        public int QuantityReceived { get; set; }
    }
}