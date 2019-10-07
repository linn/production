namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;

    public interface IOutstandingWorksOrdersReportService
    {
        ResultsModel GetOutstandingWorksOrders(string reportType, string searchParameter);
    }
}
