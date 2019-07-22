namespace Linn.Production.Domain.LinnApps.Repositories
{
    using System;
    using System.Linq;

    using Linn.Production.Domain.LinnApps;

    public interface IBuildsRepository
    {
        IQueryable<BuildSummary> GetBuildsByDepartment(DateTime from, DateTime to);
    }
}