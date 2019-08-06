namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using System;
    using System.Collections.Generic;

    public interface IBuildsSummaryReportDatabaseService
    {
        IEnumerable<BuildsSummary> GetBuildsSummaries(DateTime from, DateTime to, bool monthly);
    }
}