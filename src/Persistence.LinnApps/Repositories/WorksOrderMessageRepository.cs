namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class WorksOrderMessageRepository : IRepository<WorksOrderMessage, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WorksOrderMessageRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public void Add(WorksOrderMessage entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WorksOrderMessage> FilterBy(Expression<Func<WorksOrderMessage, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WorksOrderMessage> FindAll()
        {
            throw new NotImplementedException();
        }

        public WorksOrderMessage FindBy(Expression<Func<WorksOrderMessage, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public WorksOrderMessage FindById(string key)
        {
            return this.serviceDbContext.WorksOrderMessages.Where(m => m.PartNumber == key).ToList().FirstOrDefault();
        }

        public void Remove(WorksOrderMessage entity)
        {
            throw new NotImplementedException();
        }
    }
}
