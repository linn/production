namespace Linn.Production.Domain.LinnApps.ViewModels
{
    using System;

    public class ProductionDaysRequired
    {
        public string Priority { get; set; }

        public string JobRef { get; set; }

        public string CitCode { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public int QtyBeingBuilt { get; set; }

        public int BuildQty { get; set; }

        public int CanBuild { get; set; }

        public int CanBuildExcludingSubAssemblies { get; set; }

        public int EffectiveKanbanSize { get; set; }

        public double QtyBeingBuiltDays { get; set; }

        public double CanBuildDays { get; set; }

        public double BuildExcludingSubAssembliesDays { get; set; }

        public DateTime? EarliestRequestedDate { get; set; }

        public int SortOrder { get; set; }

        public string MfgRouteCode { get; set; }

        public double DaysToBuildKanban { get; set; }

        public double DaysToSetUpKanban { get; set; }

        public int RecommendedBuildQty { get; set; }

        public double RecommendedBuildQtyDays { get; set; }

        public int? FixedBuild { get; set; }

        public double? FixedBuildDays { get; set; }

        public double BuildDays { get; set; }
    }
}
