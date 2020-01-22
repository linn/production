namespace Linn.Production.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    public class Address
    {
        public int Id { get; set; }

        public string Addressee { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Address4 { get; set; }

        public string PostCode { get; set; }

        public List<PurchaseOrder> PurchaseOrders { get; set; }
    }
}