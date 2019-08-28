namespace Linn.Production.Domain.LinnApps.Triggers
{
    using System.Collections.Generic;
    using Linn.Production.Domain.LinnApps.Measures;

    public class ProductionTriggersReport
    {
        public PtlMaster PtlMaster { get; set; }

        public Cit Cit { get; set; }

        public ProductionTriggerReportType ReportType { get; set; }

        public IEnumerable<ProductionTrigger> Triggers { get; set; }
    }
}