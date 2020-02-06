namespace Linn.Production.Domain.LinnApps.ViewModels
{
    using System;

    public class FailedParts
    {
        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public int Qty { get; set; }

        public string BatchRef { get; set; }

        public DateTime DateBooked { get; set; }

        public DateTime? StockRotationDate { get; set; }

        public decimal TotalValue { get; set; }

        public string StockLocatorRemarks { get; set; }

        public string CreatedBy { get; set; }

        public string CitCode { get; set; }

        public string CitName { get; set; }

        public string StoragePlace { get; set; }

        public int? PreferredSupplierId { get; set; }

        public string SupplierName { get; set; }
    }
}
