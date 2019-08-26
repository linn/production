namespace Linn.Production.Domain.LinnApps.Measures
{
    using System;

    public class OsrRunMaster
    {
        public DateTime RunDateTime { get; set; }

        public string LastTriggerJobref { get; set; }

        public DateTime LastTriggerRunDateTime { get; set; }
    }
}