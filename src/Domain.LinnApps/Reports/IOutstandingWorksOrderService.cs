namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;
    using Linn.Production.Resources;

    public interface IOutstandingWorksOrdersReportService
    {
        ResultsModel GetOutstandingWorksOrders(OutstandingWorksOrdersRequestResource options);
    }
}
