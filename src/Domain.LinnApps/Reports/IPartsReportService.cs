namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;

    public interface IPartsReportService
    {
        ResultsModel PartFailDetailsReport(
            int? supplierId,
            string fromWeek,
            string toWeek,
            string errorType,
            string faultCode,
            string partNumber,
            string department);
    }
}