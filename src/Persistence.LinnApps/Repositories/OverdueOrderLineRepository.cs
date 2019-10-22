namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class OverdueOrderLineRepository : IQueryRepository<OverdueOrderLine>
    {
        private readonly ServiceDbContext serviceDbContext;

        public OverdueOrderLineRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public OverdueOrderLine FindBy(Expression<Func<OverdueOrderLine, bool>> expression)
        {
            return this.serviceDbContext.OverdueOrderLines.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<OverdueOrderLine> FilterBy(Expression<Func<OverdueOrderLine, bool>> expression)
        {
            return this.serviceDbContext.OverdueOrderLines.Where(expression);
        }

        public IQueryable<OverdueOrderLine> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}