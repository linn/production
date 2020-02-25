namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class Supplier
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public int? OrderAddressId { get; set; }

        public int? InvoiceAddressId { get; set; }

        public DateTime? DateClosed { get; set; }
    }
}
