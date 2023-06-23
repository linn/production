namespace Linn.Production.Domain.LinnApps.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShortageSummary
    {
        public string CitName { get; set; }

        public int OnesTwos { get; set; } = 0;

        public int NumShortages()
        {
            return this.Shortages.Count();
        } 

        public int BAT( )
        {
            return this.Shortages.Count(l => l.BoardShortage);
        }

        public int Metalwork()
        {
            return this.Shortages.Count(l => l.MetalworkShortage);
        }

        public int Procurement()
        {
            return this.Shortages.Count(l => l.ProcurementShortage);
        }

        public decimal ShortagePerc() => PercOfOnesTwos(this.NumShortages());

        public decimal BATPerc() => PercOfOnesTwos(this.BAT());

        public decimal MetalworkPerc() => PercOfOnesTwos(this.Metalwork());

        public decimal ProcurementPerc() => PercOfOnesTwos(this.Procurement());

        public IEnumerable<ShortageResult> Shortages { get; set; } = new List<ShortageResult>();

        private decimal PercOfOnesTwos(int fraction)
        {
            if (this.OnesTwos == 0)
            {
                return 0;
            }

            return decimal.Round((100 * fraction) / this.OnesTwos);
        }
    }
}