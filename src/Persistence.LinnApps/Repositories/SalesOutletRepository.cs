namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class SalesOutletRepository : IQueryRepository<SalesOutlet>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SalesOutletRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SalesOutlet FindBy(Expression<Func<SalesOutlet, bool>> expression)
        {
            return this.serviceDbContext.SalesOutlets.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<SalesOutlet> FilterBy(Expression<Func<SalesOutlet, bool>> expression)
        {
            return this.serviceDbContext.SalesOutlets.Where(expression);
        }
    }
}