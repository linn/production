namespace Linn.Production.Domain.LinnApps.Repositories
{
    using System;
    using System.Collections.Generic;

    public interface IBuildsRepository
    {
        IEnumerable<BuildsSummary> GetBuildsByDepartment(DateTime from, DateTime to, bool monthly = false);
    }
}