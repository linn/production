namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Domain.LinnApps;
    using Domain.LinnApps.RemoteServices;
    using Domain.LinnApps.Repositories;

    public class BuildsRepository : IBuildsRepository
    {
        private readonly ServiceDbContext serviceDbContext;
        private readonly ILrpPack lrpPack;

        private readonly ILinnWeekPack weekPack;

        public BuildsRepository(ServiceDbContext serviceDbContext, ILrpPack lrpPack, ILinnWeekPack weekPack)
        {
            this.serviceDbContext = serviceDbContext;
            this.lrpPack = lrpPack;
            this.weekPack = weekPack;
        }

        public IQueryable<BuildSummary> GetBuildsByDepartment(DateTime from, DateTime to)
        {
           var groups = this.serviceDbContext.Builds.Where(b => b.BuildDate.Date >= from && b.BuildDate.Date <= to)
                .GroupBy(b => new { b.DepartmentCode, Weekend = this.weekPack.GetLinnWeekEndDate(b.BuildDate.Date) });

            var result = new List<BuildSummary>();
            foreach (var grouping in groups)
            {
                result.Add(new BuildSummary
                               {
                                    Department = grouping.Key.DepartmentCode,
                                    Value = grouping.Sum(b => (b.LabourPrice + b.MaterialPrice)),
                                    DaysToBuild = grouping.Sum(b => this.lrpPack.GetDaysToBuildPart(b.PartNumber, b.Quantity)),
                                    WeekEnd = grouping.Key.Weekend
                               });
            }

            return result.AsQueryable();
        }
    }
}
