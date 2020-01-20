namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;

    public interface IDeliveryPerformanceReportService
    {
        ResultsModel GetDeliveryPerformanceByPriority(string citCode);

        ResultsModel GetDeliveryPerformanceDetail(string citCode, int priority);
    }
}