namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Production.Domain.LinnApps.Models;

    public interface IShortageSummaryReportService
    {
        ShortageSummary ShortageSummaryByCit(string citCode, string ptlJobref);
    }
}