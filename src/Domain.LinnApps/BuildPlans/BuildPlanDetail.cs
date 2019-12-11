namespace Linn.Production.Domain.LinnApps.BuildPlans
{
    public class BuildPlanDetail
    {
        public string BuildPlanName { get; set; }

        public string PartNumber { get; set; }

        public int FromLinnWeekNumber { get; set; }

        public int? ToLinnWeekNumber { get; set; }

        public string RuleCode { get; set; }

        public int? Quantity { get; set; }
    }
}