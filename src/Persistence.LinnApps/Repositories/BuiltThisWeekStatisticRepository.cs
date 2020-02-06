namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Models;

    public class BuiltThisWeekStatisticRepository : IQueryRepository<BuiltThisWeekStatistic>
    {
        private readonly ServiceDbContext serviceDbContext;

        public BuiltThisWeekStatisticRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public BuiltThisWeekStatistic FindBy(Expression<Func<BuiltThisWeekStatistic, bool>> expression)
        {
            return this.serviceDbContext.BuiltThisWeekStatistics.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<BuiltThisWeekStatistic> FilterBy(Expression<Func<BuiltThisWeekStatistic, bool>> expression)
        {
            return this.serviceDbContext.BuiltThisWeekStatistics.Where(expression);
        }

        public IQueryable<BuiltThisWeekStatistic> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}