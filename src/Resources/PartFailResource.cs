﻿namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class PartFailResource : HypermediaResource
    {
        public int Id { get; set; }

        public string DateCreated { get; set; }

        public int EnteredBy { get; set; }

        public string EnteredByName { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public int? Quantity { get; set; }

        public string Batch { get; set; }

        public string FaultCode { get; set; }

        public string FaultDescription { get; set; }

        public string ErrorType { get; set; }

        public string Story { get; set; }

        public int? WorksOrderNumber { get; set; }

        public int? PurchaseOrderNumber { get; set; }

        public string StoragePlace { get; set; }

        public string StoragePlaceDescription { get; set; }

        public int? MinutesWasted { get; set; }

        public int? SerialNumber { get; set; }

        public string Comments { get; set; }

        public int? Owner { get; set; }

        public string OwnerName { get; set; }
    }
}