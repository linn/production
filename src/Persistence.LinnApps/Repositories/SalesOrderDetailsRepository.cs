namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.SalesOrders;

    public class SalesOrderDetailsRepository : IQueryRepository<SalesOrderDetails>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SalesOrderDetailsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SalesOrderDetails FindBy(Expression<Func<SalesOrderDetails, bool>> expression)
        {
            return this.serviceDbContext.SalesOrderDetails.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<SalesOrderDetails> FilterBy(Expression<Func<SalesOrderDetails, bool>> expression)
        {
            return this.serviceDbContext.SalesOrderDetails.Where(expression);
        }
    }
}