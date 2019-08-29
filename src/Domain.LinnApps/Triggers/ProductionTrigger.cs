namespace Linn.Production.Domain.LinnApps.Triggers
{
    using System;

    public class ProductionTrigger
    {
        public string Jobref { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public string Citcode { get; set; }

        public string CitName { get; set; }

        public int? TriggerLevel { get; set; }

        public int KanbanSize { get; set; }

        public int? EffectiveKanbanSize { get; set; }

        public int MaximumKanbans { get; set; }

        public string MfgRouteCode { get; set; }

        public int? DaysToBuildKanban { get; set; }

        public decimal? NettSalesOrders { get; set; }

        public int? QtyFree { get; set; }

        public int? RemainingBuild { get; set; }

        public int? QtyBeingBuilt { get; set; }

        public int? BE { get; set; }

        public int? BI { get; set; }

        public int? BT { get; set; }

        public int? GBE { get; set; }

        public int? GBI { get; set; }

        public int? FixedBuild { get; set; }

        public string Priority { get; set; }

        public string ReqtFromFixedBuild { get; set; }

        public decimal? Ldays { get; set; }

        public string Story { get; set; }

        public string OnHold { get; set; }

        public string ReasonStarted { get; set; }

        public int? SortOrder { get; set; }

        public int? Snbo { get; set; }

        public int? Snme { get; set; }

        // weird EF Core/Oracle bug doesn't allow these Days fields to be cast to decimal?
        public double? QtyBeingBuiltDays { get; set; }

        public double? BEDays { get; set; }

        public double? BIDays { get; set; }

        public double? BTDays { get; set; }

        public decimal? FixedBuildDays { get; set; }

        public int? QtyNFlagged { get; set; }

        public int? QtyFFlagged { get; set; }

        public int? QtyYFlagged { get; set; }

        public DateTime? EarliestRequestedDate { get; set; }

        public int? StockReqtPcnt { get; set; }

        public int? CanBuild { get; set; }

        public int? QtyManualWo { get; set; }

        public int? SASnbo { get; set; }

        public int? MWPriority { get; set; }

        public int? CanBuildExSubAssemblies { get; set; }

        public string ReportType { get; set; }
    }
}

