namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Persistence.LinnApps;

    public class FailedPartsRepository : IQueryRepository<FailedParts>
    {
        private readonly ServiceDbContext serviceDbContext;

        public FailedPartsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public FailedParts FindBy(Expression<Func<FailedParts, bool>> expression)
        {
            return this.serviceDbContext.FailedParts.Where(expression).FirstOrDefault();
        }

        public IQueryable<FailedParts> FilterBy(Expression<Func<FailedParts, bool>> expression)
        {
            return this.serviceDbContext.FailedParts.Where(expression);
        }

        public IQueryable<FailedParts> FindAll()
        {
            return this.serviceDbContext.FailedParts;
        }
    }
}