namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;

    using Linn.Common.Reporting.Models;

    public interface IBuildsDetailReportService
    {
        ResultsModel GetBuildsDetailReport(
            DateTime from,
            DateTime to,
            string department,
            string quantityOrValue,
            bool monthly = false);
    }
}