namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.Linq;

    public class PurchaseOrder
    {
        public int OrderNumber { get; set; }

        public IEnumerable<PurchaseOrderDetail> Details { get; set; }

        public bool ContainsPart(string partNumber)
        {
            return this.Details.Any(d => d.PartNumber == partNumber);
        }
    }
}