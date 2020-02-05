namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Services;

    using Microsoft.EntityFrameworkCore;

    public class PurchaseOrderRepository : IRepository<PurchaseOrder, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PurchaseOrderRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PurchaseOrder FindById(int key)
        {
            return this.serviceDbContext.PurchaseOrders.Where(o => o.OrderNumber == key)
                .Include(o => o.Details).ThenInclude(o => o.Part)
                .Include(o => o.OrderAddress)
                .ToList().FirstOrDefault();
        }

        public IQueryable<PurchaseOrder> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(PurchaseOrder entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(PurchaseOrder entity)
        {
            throw new NotImplementedException();
        }

        public PurchaseOrder FindBy(Expression<Func<PurchaseOrder, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PurchaseOrder> FilterBy(Expression<Func<PurchaseOrder, bool>> expression)
        {
            return this.serviceDbContext.PurchaseOrders.AsNoTracking().Include(o => o.Details).Where(expression);
        }
    }
}