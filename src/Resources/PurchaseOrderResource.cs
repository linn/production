namespace Linn.Production.Resources
{
    using System.Collections.Generic;


    public class PurchaseOrderResource
    {
        public int OrderNumber { get; set; }

        public string DateOfOrder { get; set; }

        public string Addressee { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Address4 { get; set; }

        public string PostCode { get; set; }

        public string Country { get; set; }

        public string Remarks { get; set; }

        public List<string> Parts { get; set; }

        public List<PurchaseOrderDetailResource> Details { get; set; }
    }
}