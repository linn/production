namespace Linn.Production.Resources
{
    using System.Collections.Generic;

    public class ShortageSummaryResource
    {
        public string CitName { get; set; }

        public int OnesTwos { get; set; } = 0;

        public int NumShortages { get; set; } = 0;

        public int BAT { get; set; } = 0;

        public int Metalwork { get; set; } = 0;

        public int Procurement { get; set; } = 0;

        public decimal PercShortages { get; set; } = 0;

        public decimal PercBAT { get; set; } = 0;

        public decimal PercMetalwork { get; set; } = 0;

        public decimal PercProcurement { get; set; } = 0;

        public IEnumerable<ShortageResultResource> Shortages { get; set; }
    }
}