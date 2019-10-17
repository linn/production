namespace Linn.Production.Domain.LinnApps.Triggers
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;

    public class ProductionTriggersReport
    {
        public ProductionTriggersReport()
        {
            // for testing
        }

        public ProductionTriggersReport(string jobref, PtlMaster ptlMaster, Cit cit, IQueryRepository<ProductionTrigger> repository)
        {
            this.PtlMaster = ptlMaster.LastFullRunJobref == jobref ? ptlMaster : null;
            this.Cit = cit;

            this.Triggers = repository.FilterBy(t => t.Jobref == jobref && t.Citcode == cit.Code).OrderBy(t => t.SortOrder).ThenBy(t => t.EarliestRequestedDate);
        }

        public PtlMaster PtlMaster { get; set; }

        public Cit Cit { get; set; }

        public ProductionTriggerReportType ReportType { get; set; }

        public IEnumerable<ProductionTrigger> Triggers { get; set; }
    }
}