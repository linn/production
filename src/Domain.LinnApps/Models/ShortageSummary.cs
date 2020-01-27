namespace Linn.Production.Domain.LinnApps.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Production.Domain.LinnApps.Triggers;

    public class ShortageSummary
    {
        public int OnesTwos { get; set; } = 0;

        public int NumShortages { get; set; } = 0;

        public int BAT { get; set; } = 0;

        public int Metalwork { get; set; } = 0;

        public int Procurement { get; set; } = 0;

        public decimal BATPerc() => PercOfShortages(this.BAT);

        public decimal MetalworkPerc() => PercOfShortages(this.Metalwork);

        public decimal ProcurementPerc() => PercOfShortages(this.Procurement);

        public IEnumerable<ShortageResult> Shortages { get; set; } = new List<ShortageResult>();

        private decimal PercOfShortages(int fraction)
        {
            if (this.NumShortages == 0)
            {
                return 0;
            }
            return Decimal.Round(fraction / this.NumShortages * 100);
        }
    }
}