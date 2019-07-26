namespace Linn.Production.Domain
{
    using System;

    public class ProductionMeasures
    {
        public Cit Cit { get; set; }

        public int? Ones { get; set; }

        public int? Twos { get; set; }

        public int? Threes { get; set; }

        public int? Fours { get; set; }

        public int? Fives { get; set; }

        public double? BuiltThisWeekValue { get; set; }

        public int? BuiltThisWeekQty { get; set; }

        public double? BackOrderValue { get; set; }

        public double? FFlaggedValue { get; set; }

        public double? FFlaggedQty { get; set; }

        public double? StockValue { get; set; }

        public double? OverStockValue { get; set; }

        public int? NumberOfBackOrders { get; set; }

        public int? NumberOfPartsOrders { get; set; }

        public DateTime? OldestBackOrder { get; set; }

        public string PtlJobref { get; set; }

        public string PboJobref { get; set; }

        public double? DaysRequired { get; set; }

        public double? DaysRequired3 { get; set; }

        public double? DaysRequiredCanDo12 { get; set; }

        public double? DaysRequiredCanDo3 { get; set; }

        public string PboJobId { get; set; }

        public double? UsageValue { get; set; }

        public double? UsageForTotalValue { get; set; }

        public double? AvgStockValue { get; set; }

        public int? ShortBat { get; set; }

        public int? ShortMetalwork { get; set; }

        public int? ShortProc { get; set; }

        public int? ShortAny{ get; set; }

        public double? DeliveryPerformance1s { get; set; }

        public double? DeliveryPerformance2s { get; set; }
    }
}