namespace Domain.LinnApps.Repositories
{
    using System;
    using System.Linq;

    public interface IBuildsRepository
    {
        IQueryable<BuildSummary> GetBuildsByDepartment(DateTime from, DateTime to);
    }
}