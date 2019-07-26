using System;
using System.Collections.Generic;
using System.Text;

namespace Linn.Production.Domain
{
    public class Cit
    {
        public string Code { get; set;}

        public string Name { get; set; }
    }

    public class ProductionMeasures
    {
        public Cit Cit { get; set; }

        public int? Ones { get; set; }

        public int? Twos { get; set; }

        public int? Threes { get; set; }

        public int? Fours { get; set; }

        public int? Fives { get; set; }

        public double? BuiltThisWeekValue { get; set; }

        public double? BackOrderValue { get; set; }

        public double? StockValue { get; set; }

        public double? OvertStockValue { get; set; }

        public int? NumberOfBackOrders { get; set; }

        public DateTime? OldestBackOrder { get; set; }
    }
}
