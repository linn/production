﻿namespace Linn.Production.Resources
{
    public class PurchaseOrderDetaiWithSernosInfoResource : PurchaseOrderDetailResource
    {
        public int? FirstSernos { get; set; }

        public int? LastSernos { get; set; }

        public int? SernosIssued { get; set; }

        public int? SernosBuilt { get; set; }

        public int? QuantityReceived { get; set; }

        public int? NumberOfSernos { get; set; }
    }
}