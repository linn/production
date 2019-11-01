namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class WorksOrderLabelsRepository : IRepository<WorksOrderLabel, WorksOrderLabelKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WorksOrderLabelsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public WorksOrderLabel FindById(WorksOrderLabelKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WorksOrderLabel> FindAll()
        {
            return this.serviceDbContext.WorksOrderLabels;
        }

        public void Add(WorksOrderLabel entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(WorksOrderLabel entity)
        {
            throw new NotImplementedException();
        }

        public WorksOrderLabel FindBy(Expression<Func<WorksOrderLabel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WorksOrderLabel> FilterBy(Expression<Func<WorksOrderLabel, bool>> expression)
        {
            return this.serviceDbContext.WorksOrderLabels.Where(expression);
        }
    }
}