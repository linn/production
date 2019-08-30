namespace Linn.Production.Domain.LinnApps.Triggers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Repositories;

    public class ProductionTriggersReport
    {
        public ProductionTriggersReport()
        {
            // for testing        
        }

        public ProductionTriggersReport(string jobref, PtlMaster ptlMaster, Cit cit, ProductionTriggerReportType reportType, IQueryRepository<ProductionTrigger> repository)
        {
            this.PtlMaster = ptlMaster.LastFullRunJobref == jobref ? ptlMaster : null;
            this.Cit = cit;
            this.ReportType = reportType;

            var triggers = (reportType == ProductionTriggerReportType.Full)
                ? repository.FilterBy(t => t.Jobref == jobref && t.Citcode == cit.Code)
                : repository.FilterBy(t => t.Jobref == jobref && t.Citcode == cit.Code && t.ReportType == "BRIEF");

            this.Triggers = triggers.OrderBy(t => t.SortOrder).ThenBy(t => t.EarliestRequestedDate);
        }

        public PtlMaster PtlMaster { get; set; }

        public Cit Cit { get; set; }

        public ProductionTriggerReportType ReportType { get; set; }

        public IEnumerable<ProductionTrigger> Triggers { get; set; }
    }
}