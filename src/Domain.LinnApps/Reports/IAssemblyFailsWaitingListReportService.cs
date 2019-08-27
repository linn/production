namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;

    public interface IAssemblyFailsWaitingListReportService
    {
        ResultsModel GetAssemblyFailsWaitingListReport();
    }
}