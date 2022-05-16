namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;

    public interface IPartsReportService
    {
        ResultsModel PartFailDetailsReport(
            int? supplierId,
            string fromDate,
            string toDate,
            string errorType,
            string faultCode,
            string partNumber,
            string department);
    }
}