namespace Linn.Production.Domain.LinnApps.Services
{
    using System;

    using Linn.Common.Reporting.Models;

    public interface IBuildsByDepartmentReportService
    {
        ResultsModel GetBuildsSummaryReport(DateTime from, DateTime to);
    }
}
