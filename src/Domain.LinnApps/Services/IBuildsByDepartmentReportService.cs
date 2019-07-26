namespace Linn.Production.Domain.LinnApps.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Linn.Common.Reporting.Models;

    public interface IBuildsByDepartmentReportService
    {
        IEnumerable<ResultsModel> GetBuildsSummaryReports(DateTime from, DateTime to, bool monthly = false);
    }
}
