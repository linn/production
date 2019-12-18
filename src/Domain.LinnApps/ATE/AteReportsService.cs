namespace Linn.Production.Domain.LinnApps.ATE
{
    using System;

    using Linn.Common.Reporting.Models;

    public class AteReportsService : IAteReportsService
    {
        public ResultsModel GetStatusReport(
            DateTime resourceFromDate,
            DateTime resourceToDate,
            string resourceSmtOrPcb,
            string resourcePlaceFound)
        {
            throw new NotImplementedException();
        }

        public ResultsModel GetDetailsReport(DateTime fromDate, DateTime toDate, string selectBy, string value)
        {
            throw new NotImplementedException();
        }
    }
}