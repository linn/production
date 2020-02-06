namespace Linn.Production.Domain.LinnApps.Models
{
    using System.Collections.Generic;

    public class PurchaseOrderWithSernosInfo : PurchaseOrder
    {
        public IEnumerable<PurchaseOrderDetailWithSernosInfo> DetaisWithSernosInfo { get; set; }
    }
}