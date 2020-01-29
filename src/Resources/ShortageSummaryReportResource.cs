namespace Linn.Production.Resources
{
    using System.Collections.Generic;

    public class ShortageSummaryReportResource
    {
        public IEnumerable<ShortageSummaryResource> ReportResults { get; set; }
    }
}