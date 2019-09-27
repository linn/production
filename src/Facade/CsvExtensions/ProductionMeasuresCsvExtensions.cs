namespace Linn.Production.Facade.CsvExtensions
{
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.Measures;

    public static class ProductionMeasuresCsvExtensions
    {
        public static IEnumerable<string> ToCsvLine(this ProductionMeasures measures)
        {
            return new List<string>
            {
                measures.Cit.Name,
                measures.Ones.ToString(),
                measures.Twos.ToString(),
                measures.Threes.ToString(),
                measures.DaysRequired.ToString(),
                measures.DaysRequired3.ToString(),
                measures.DaysRequiredCanDo12.ToString(),
                measures.DaysRequiredCanDo3.ToString(),
                measures.DeliveryPerformance1s.ToString(),
                measures.DeliveryPerformance2s.ToString(),
                measures.ShortAny.ToString(),
                measures.ShortBat.ToString(),
                measures.ShortMetalwork.ToString(),
                measures.ShortProc.ToString(),
                measures.NumberOfPartsBackOrdered.ToString(),
                measures.BackOrderValue.ToString(),
                measures.BuiltThisWeekValue.ToString(),
                measures.BuiltThisWeekQty.ToString(),
                measures.FFlaggedQty.ToString(),
                measures.Fours.ToString(),
                measures.Fives.ToString(),
                measures.StockValue.ToString(),
                measures.OverStockValue.ToString()
            };
        }

        public static IEnumerable<string> CsvHeaderLine()
        {
            return new List<string>()
            {
                "CIT",
                "1s",
                "2s",
                "3s",
                "Days 1/2s",
                "Days 3s",
                "Can Do 1/2s",
                "Can Do 3s",
                "Del Perf 1s%",
                "Del Perf 2s%",
                "Shortages",
                "Short BAT",
                "Short Metal",
                "Short Proc",
                "Back Orders",
                "Back Order Value",
                "Back Order Oldest",
                "Built This Week Value",
                "Built This Week Qty",
                "F-Flag Qty",
                "4s",
                "5s",
                "Stock",
                "Over-stock"
            };
        }
    }
}
