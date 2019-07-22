namespace Domain.LinnApps.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Reporting.Models;

    public interface IBuildsByDepartmentReportService
    {
        ResultsModel GetBuildsSummaryReport(DateTime from, DateTime to);
    }
}
