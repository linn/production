namespace Linn.Production.Domain.LinnApps.Repositories
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Persistence;

    public interface ILinnWeekRepository : IRepository<LinnWeek, int>
    {
        IEnumerable<LinnWeek> GetWeeks(DateTime startDate, DateTime endDate);

        LinnWeek GetWeek(DateTime date);
    }
}