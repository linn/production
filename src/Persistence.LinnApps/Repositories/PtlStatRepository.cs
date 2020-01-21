namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class PtlStatRepository : IQueryRepository<PtlStat>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PtlStatRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PtlStat FindBy(Expression<Func<PtlStat, bool>> expression)
        {
            return this.serviceDbContext.PtlStats.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<PtlStat> FilterBy(Expression<Func<PtlStat, bool>> expression)
        {
            return this.serviceDbContext.PtlStats.Where(expression);
        }

        public IQueryable<PtlStat> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}