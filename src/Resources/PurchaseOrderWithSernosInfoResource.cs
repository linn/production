namespace Linn.Production.Resources
{
    using System.Collections.Generic;

    public class PurchaseOrderWithSernosInfoResource : PurchaseOrderResource
    {
        public List<PurchaseOrderDetailSernosInfoResource> DetailSernosInfos { get; set; }
    }
}