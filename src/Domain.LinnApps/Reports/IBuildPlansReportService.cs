namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;

    public interface IBuildPlansReportService
    {
        ResultsModel BuildPlansReport(string buildPlanName, int weeks, string citName);
    }
}
