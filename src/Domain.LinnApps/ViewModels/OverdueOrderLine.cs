namespace Linn.Production.Domain.LinnApps.ViewModels
{
    using System;

    public class OverdueOrderLine
    {
        public int JobId { get; set; }

        public int AccountId { get; set; }

        public int OutletNumber { get; set; }

        public string OutletName { get; set; }

        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public string OrderRef { get; set; }

        public string ArticleNumber { get; set; }

        public string InvoiceDescription { get; set; }

        public DateTime? RequestedDeliveryDate { get; set; }

        public DateTime? FirstAdvisedDespatchDate { get; set; }

        public int NoStockQuantity { get; set; }

        public int? AllocVal { get; set; }

        public int? RequiredNowStockDespatchableValue { get; set; }

        public int? RequiredNowStockNotDespatchableValue { get; set; }

        public int? RequiredNowNoStockValue { get; set; }

        public int? RequiredThisMonthStockValue { get; set; }

        public int? RequiredThisMonthNoStockValue { get; set; }

        public int? RequiredNextMonthStockValue { get; set; }

        public int? RequiredNextMonthNoStockValue { get; set; }

        public string Reasons { get; set; }

        public int Quantity { get; set; }

        public int DaysLate { get; set; }

        public int DaysLateFa { get; set; }

        public int WorkingDaysLate { get; set; }

        public int WorkingDaysLateFa { get; set; }

        public decimal BaseValue { get; set; }

        public decimal OrderValue { get; set; }
    }
}
