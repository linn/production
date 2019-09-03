namespace Linn.Production.Resources
{
    using System.Collections.Generic;

    public class ProductionTriggersReportResource
    {
        public string PtlJobref { get; set; }

        public string PtlRunDateTime { get; set; }

        public string CitCode { get; set; }

        public string CitName { get; set; }

        public string ReportFormat { get; set; }

        public IEnumerable<ProductionTriggerSummaryResource> Triggers { get; set; }
    }
}