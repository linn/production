namespace Linn.Production.Domain.LinnApps.ATE
{
    using System;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;

    public interface IAteReportsService
    {
        ResultsModel GetStatusReport(
            DateTime fromDate,
            DateTime toDate,
            string smtOrPcb,
            string placeFound,
            AteReportGroupBy groupBy);

        ResultsModel GetDetailsReport(DateTime fromDate, DateTime toDate, string selectBy, string value);
    }
}