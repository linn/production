namespace Linn.Production.Resources
{
    using System.Collections.Generic;

    public class ShortageSummaryResource
    {
        public int OnesTwos { get; set; } = 0;

        public int NumShortages { get; set; } = 0;

        public int BAT { get; set; } = 0;

        public int Metalwork { get; set; } = 0;

        public int Procurement { get; set; } = 0;

        public IEnumerable<ShortageResultResource> Shortages { get; set; }
    }
}