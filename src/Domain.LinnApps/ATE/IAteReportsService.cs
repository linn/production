namespace Linn.Production.Domain.LinnApps.ATE
{
    using System;

    using Linn.Common.Reporting.Models;

    public interface IAteReportsService
    {
        ResultsModel GetStatusReport(
            DateTime resourceFromDate,
            DateTime resourceToDate,
            string resourceSmtOrPcb,
            string resourcePlaceFound);

        ResultsModel GetDetailsReport(DateTime fromDate, DateTime toDate, string selectBy, string value);
    }
}