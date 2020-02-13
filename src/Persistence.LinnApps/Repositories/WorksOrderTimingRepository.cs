namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class WorksOrderTimingRepository : IRepository<WorksOrderTiming, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WorksOrderTimingRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public WorksOrderTiming FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WorksOrderTiming> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(WorksOrderTiming entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(WorksOrderTiming entity)
        {
            throw new NotImplementedException();
        }

        public WorksOrderTiming FindBy(Expression<Func<WorksOrderTiming, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WorksOrderTiming> FilterBy(Expression<Func<WorksOrderTiming, bool>> expression)
        {
            return this.serviceDbContext.WorksOrderTimings.Where(expression);
        }
    }
}
