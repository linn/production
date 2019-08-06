namespace Linn.Production.Domain.LinnApps.Repositories
{
    using System;
    using System.Collections.Generic;

    public interface IBuildsSummariesRepository
    {
        IEnumerable<BuildsSummary> GetBuildsSummaries(DateTime from, DateTime to, bool monthly = false);
    }
}