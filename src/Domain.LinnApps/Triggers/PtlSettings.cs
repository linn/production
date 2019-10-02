namespace Linn.Production.Domain.LinnApps.Triggers
{
    public class PtlSettings
    {
        public string Key { get; set; }

        public int DaysToLookAhead { get; set; }

        public int? BuildToMonthEndFromDays { get; set; }

        public int FinalAssemblyDaysToLookAhead { get; set; }

        public int SubAssemblyDaysToLookAhead { get; set; }

        public int PriorityCutOffDays { get; set; }

        public int PriorityStrategy { get; set; }
    }
}
