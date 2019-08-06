namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Reporting.Models;

    public interface IBuildsSummaryReportService
    {
        IEnumerable<ResultsModel> GetBuildsSummaryReports(DateTime from, DateTime to, bool monthly = false);
    }
}
