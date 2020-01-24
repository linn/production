namespace Linn.Production.Resources
{
    using System.Collections.Generic;

    public class PurchaseOrderWithSernosInfoResource : PurchaseOrderResource
    {
        public List<PurchaseOrderDetailSWithernosInfoResource> DetailSernosInfos { get; set; }
    }
}