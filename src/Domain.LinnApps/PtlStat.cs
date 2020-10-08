namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class PtlStat
    {
        public int TriggerId { get; set; }

        public DateTime? DateCompleted { get; set; }

        public int PtlPriority { get; set; }

        public string BuildGroup { get; set; }

        public string CitName { get; set; }

        public int SortOrder { get; set; }

        public string CitCode { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public DateTime? TriggerDate { get; set; }

        public decimal WorkingDays { get; set; }

        public int TargetDays()
        {
            if (this.BuildGroup == "EP")
            {
                return 3;
            }

            return this.BuildGroup == "CP" ? 3 : 5;
        }
    }
}
