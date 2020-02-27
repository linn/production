namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using Linn.Common.Reporting.Models;

    public interface IMetalWorkTimingsReportService
    {
        ResultsModel GetTimingsReport(
            DateTime from,
            DateTime to);
    }
}
