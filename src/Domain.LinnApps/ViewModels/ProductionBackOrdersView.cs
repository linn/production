namespace Linn.Production.Domain.LinnApps.ViewModels
{
    using System;

    public class ProductionBackOrdersView
    {
        public int JobId { get; set; }

        public string CitCode { get; set; }

        public string ArticleNumber { get; set; }

        public string InvoiceDescription { get; set; }

        public int OrderQuantity { get; set; }

        public decimal OrderValue { get; set; }

        public DateTime OldestDate { get; set; }

        public int CanBuildQuantity { get; set; }

        public decimal CanBuildValue { get; set; }
    }
}