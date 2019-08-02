using System;
using System.Collections.Generic;

namespace Linn.Production.Domain.LinnApps.Reports
{
    public interface IBuildsSummaryReportDatabaseService
    {
        IEnumerable<BuildsSummary> GetBuildsSummaries(DateTime from, DateTime to, bool monthly);
    }
}