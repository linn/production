namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class SalesAccountRepository : IQueryRepository<SalesAccount>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SalesAccountRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SalesAccount FindBy(Expression<Func<SalesAccount, bool>> expression)
        {
            return this.serviceDbContext.SalesAccounts.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<SalesAccount> FilterBy(Expression<Func<SalesAccount, bool>> expression)
        {
            return this.serviceDbContext.SalesAccounts.Where(expression);
        }
    }
}