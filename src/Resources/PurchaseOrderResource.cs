namespace Linn.Production.Resources
{
    using System.Collections.Generic;

    public class PurchaseOrderResource
    {
        public int OrderNumber { get; set; }

        public List<string> Parts { get; set; }
    }
}