namespace Linn.Production.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    public class Supplier
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public string OrderAddressId { get; set; }

        public string InvoiceAddressId { get; set; }
        
        public DateTime? DateClosed { get; set; }
    }
}
