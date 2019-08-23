namespace Linn.Production.Resources
{
    public class OsrInfoResource
    {
        public string LastOSRRunDateTime { get; set; }

        public string LastPtlRunDateTime { get; set; }

        public string LastPtlJobref { get; set; }

        public int LastDaysToLookAhead { get; set; }
    }
}