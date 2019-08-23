namespace Linn.Production.Domain.LinnApps.Triggers
{
    using System;

    public class PtlMaster
    {
        public string LastFullRunJobref { get; set; }

        public DateTime LastFullRunDateTime { get; set; }

        public int LastDaysToLookAhead { get; set; }

        public string Status { get; set; }

        public string LastPtlShortageJobref { get; set; }
    }
}
