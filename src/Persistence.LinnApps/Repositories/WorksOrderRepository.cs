namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Proxy;

    public class WorksOrderRepository : IRepository<WorksOrder, int>
    {
        private readonly IDatabaseService linnappsDatabaseService;

        private readonly ServiceDbContext serviceDbContext;

        public WorksOrderRepository(ServiceDbContext serviceDbContext, IDatabaseService linnappsDatabaseService)
        {
            this.serviceDbContext = serviceDbContext;
            this.linnappsDatabaseService = linnappsDatabaseService;
        }

        public WorksOrder FindById(int key)
        {
            return this.serviceDbContext.WorksOrders.Where(w => w.OrderNumber == key).ToList().FirstOrDefault();
        }

        public IQueryable<WorksOrder> FindAll()
        {
            return this.serviceDbContext.WorksOrders;
        }

        public void Add(WorksOrder entity)
        {
            entity.OrderNumber = this.linnappsDatabaseService.GetIdSequence("WO_ORDER_SEQ");
            this.serviceDbContext.WorksOrders.Add(entity);
        }

        public void Remove(WorksOrder entity)
        {
            throw new NotImplementedException();
        }

        public WorksOrder FindBy(Expression<Func<WorksOrder, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WorksOrder> FilterBy(Expression<Func<WorksOrder, bool>> expression)
        {
            return this.serviceDbContext.WorksOrders.Where(expression);
        }
    }
}
