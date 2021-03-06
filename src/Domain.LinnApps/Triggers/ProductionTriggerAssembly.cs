﻿namespace Linn.Production.Domain.LinnApps.Triggers
{
    public class ProductionTriggerAssembly
    {
        public string Jobref { get; set; }

        public string PartNumber { get; set; }

        public string AssemblyNumber { get; set; }

        public int? QtyUsed { get; set; }

        public int? BomLevel { get; set; }

        public decimal? NettSalesOrders { get; set; }

        public int? QtyBeingBuilt { get; set; }

        public int? RemainingBuild { get; set; }

        public int? ReqtForInternalAndTriggerLevelBT { get; set; }

        public int? ReqtForPriorityBuildBE { get; set; }

        public bool HasReqt()
        {
            return this.NettSalesOrders > 0 || this.QtyBeingBuilt > 0 || this.ReqtForInternalAndTriggerLevelBT > 0;
        }
    }
}
