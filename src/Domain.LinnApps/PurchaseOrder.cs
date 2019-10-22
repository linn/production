namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.Linq;

    public class PurchaseOrder
    {
        public int OrderNumber { get; set; }

        public List<PurchaseOrderDetail> Details { get; set; }

        public bool ContainsPart(string partNumber)
        {
            return this.Details.Any(d => d.PartNumber == partNumber);
        }
    }
}