namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class PurchaseOrdersReceivedRepository : IQueryRepository<PurchaseOrdersReceived>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PurchaseOrdersReceivedRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PurchaseOrdersReceived FindBy(Expression<Func<PurchaseOrdersReceived, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PurchaseOrdersReceived> FilterBy(Expression<Func<PurchaseOrdersReceived, bool>> expression)
        {
            return this.serviceDbContext.PurchaseOrdersReceivedView.Where(expression);
        }

        public IQueryable<PurchaseOrdersReceived> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}