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

        public int? OverrideTriggerLevel { get; set; }

        public int? VariableTriggerLevel { get; set; }

        public int? EffectiveTriggerLevel { get; set; }

        public string TriggerLevelText { get; set; }

        public int KanbanSize { get; set; }

        public int? EffectiveKanbanSize { get; set; }

        public int MaximumKanbans { get; set; }

        public string MfgRouteCode { get; set; }

        public int? DaysToBuildKanban { get; set; }

        public decimal? NettSalesOrders { get; set; }

        public int? QtyFree { get; set; }

        public int? RemainingBuild { get; set; }

        public int? QtyBeingBuilt { get; set; }

        public int? ReqtForSalesOrdersBE { get; set; }

        public int? ReqtForInternalCustomersBI { get; set; }

        // ReqtForInternalAndTriggerLevelBT = what's needed for internal customers + trigger level
        public int? ReqtForInternalAndTriggerLevelBT { get; set; }

        public int? ReqtForSalesOrdersGBE { get; set; }

        public int? ReqtForInternalCustomersGBI { get; set; }

        public int? FixedBuild { get; set; }

        public string Priority { get; set; }

        public string ReqtFromFixedBuild { get; set; }

        public decimal? DaysTriggerLasts { get; set; }

        public string Story { get; set; }

        public string OnHold { get; set; }

        public string ReasonStarted { get; set; }

        public int? SortOrder { get; set; }

        public int? ShortNowBackOrdered { get; set; }

        public int? ShortNowMonthEnd { get; set; }

        // weird EF Core/Oracle bug doesn't allow these Days fields to be cast to decimal?
        public double? QtyBeingBuiltDays { get; set; }

        public double? ReqtForSalesOrdersBEDays { get; set; }

        public double? ReqtForInternalCustomersBIDays { get; set; }

        public double? ReqtForInternalTriggerBTDays { get; set; }

        public decimal? FixedBuildDays { get; set; }

        public int? QtyNFlagged { get; set; }

        public int? QtyFFlagged { get; set; }

        // qty_free-qty_n_flagged qty_y_flagged
        public int? QtyYFlagged { get; set; }

        public DateTime? EarliestRequestedDate { get; set; }

        // appears to be perc of reqt for internal customers is actually available in stock
        // v_pw.stock_reqt_pcnt := (v_pw.qty_free-v_pw.qty_n_flagged)/v_pw.gbi*100;
        public int? StockReqtPercNt { get; set; }

        public int? CanBuild { get; set; }

        public int? QtyManualWo { get; set; }

        public int? StockAvailableShortNowBackOrdered { get; set; }

        public int? MWPriority { get; set; }

        public int? CanBuildExSubAssemblies { get; set; }

        public string ReportType { get; set; }

        public bool IsShortage()
        {
            var build = (this.RemainingBuild ?? this.ReqtForInternalAndTriggerLevelBT) + (this.QtyBeingBuilt ?? 0);
            return this.CanBuild < build;
        }
    }
}

