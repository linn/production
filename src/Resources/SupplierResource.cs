﻿namespace Linn.Production.Resources
{
    using System;

    using Linn.Common.Resources;

    public class SupplierResource : HypermediaResource
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public string OrderAddressId { get; set; }

        public string InvoiceAddressId { get; set; }

        public string DateClosed { get; set; }
    }
}
