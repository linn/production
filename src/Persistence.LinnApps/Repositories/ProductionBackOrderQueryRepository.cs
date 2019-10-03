namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Domain.LinnApps.Repositories;

    public class ProductionBackOrderQueryRepository : IQueryRepository<ProductionBackOrder>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProductionBackOrderQueryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProductionBackOrder FindBy(Expression<Func<ProductionBackOrder, bool>> expression)
        {
            return this.serviceDbContext.ProductionBackOrders.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<ProductionBackOrder> FilterBy(Expression<Func<ProductionBackOrder, bool>> expression)
        {
            return this.serviceDbContext.ProductionBackOrders.Where(expression);
        }
    }
}