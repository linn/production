namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.Repositories;

    public class BuildsSummariesRepository : IBuildsSummariesRepository
    {
        private readonly ServiceDbContext serviceDbContext;
        private readonly ILrpPack lrpPack;

        private readonly ILinnWeekPack weekPack;

        public BuildsSummariesRepository(ServiceDbContext serviceDbContext, ILrpPack lrpPack, ILinnWeekPack weekPack)
        {
            this.serviceDbContext = serviceDbContext;
            this.lrpPack = lrpPack;
            this.weekPack = weekPack;
        }

        public IEnumerable<BuildsSummary> GetBuildsSummaries(DateTime from, DateTime to, bool monthly = false)
        {
            return this.serviceDbContext.Builds
                .Where(b => b.BuildDate > from && b.BuildDate < to)
                .GroupBy(
                    b => new
                             {
                                 b.DepartmentCode,
                                 Weekend = monthly
                                               ? new DateTime(
                                                   b.BuildDate.Year,
                                                   b.BuildDate.Month,
                                                   DateTime.DaysInMonth(b.BuildDate.Year, b.BuildDate.Month))
                                               : this.weekPack.GetLinnWeekEndDate(b.BuildDate)
                             }).Select(
                    x => new BuildsSummary
                             {
                                 DepartmentCode = x.Key.DepartmentCode,
                                 Value = x.Sum(b => (b.LabourPrice + b.MaterialPrice)),
                                 DaysToBuild = x.Sum(b => this.lrpPack.GetDaysToBuildPart(b.PartNumber, b.Quantity)),
                                 WeekEnd = monthly
                                               ? new DateTime(
                                                   x.Key.Weekend.Year,
                                                   x.Key.Weekend.Month,
                                                   DateTime.DaysInMonth(x.Key.Weekend.Year, x.Key.Weekend.Month))
                                               : x.Key.Weekend
                             }).OrderBy(s => s.WeekEnd);
        }
    }
}
