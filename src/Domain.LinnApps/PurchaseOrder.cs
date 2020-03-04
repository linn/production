namespace Linn.Production.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PurchaseOrder
    {
        public int OrderNumber { get; set; }

        public string DocumentType { get; set; }

        public int OrderAddressId { get; set; }

        public DateTime DateOfOrder { get; set; }

        public Address OrderAddress { get; set; }

        public string Remarks { get; set; }

        public IEnumerable<PurchaseOrderDetail> Details { get; set; }

        public int SupplierId { get; set; }

        public bool ContainsPart(string partNumber)
        {
            return this.Details.Any(d => d.PartNumber == partNumber);
        }
    }
}