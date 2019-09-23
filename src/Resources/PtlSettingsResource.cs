namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class PtlSettingsResource : HypermediaResource
    {
        public int DaysToLookAhead { get; set; }

        public int? BuildToMonthEndFromDays { get; set; }

        public int FinalAssemblyDaysToLookAhead { get; set; }

        public int SubAssemblyDaysToLookAhead { get; set; }

        public int PriorityCutOffDays { get; set; }

        public int PriorityStrategy { get; set; }
    }
}
