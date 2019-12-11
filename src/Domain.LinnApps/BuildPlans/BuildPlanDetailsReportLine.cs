namespace Linn.Production.Domain.LinnApps.BuildPlans
{
    public class BuildPlanDetailsReportLine
    {
        public int SortOrder { get; set; }

        public string CitName { get; set; }

        public string PartNumber { get; set; }

        public int LinnWeekNumber { get; set; }

        public string LinnWeek { get; set; }

        public string DDMon { get; set; }

        public int? FixedBuild { get; set; }

        public string BuildPlanName { get; set; }
    }
}