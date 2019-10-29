namespace Linn.Production.Domain.LinnApps
{
    public class PurchaseOrderDetail
    {
        public PurchaseOrder PurchaseOrder { get; set; }

        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public string PartNumber { get; set; }
    }
}