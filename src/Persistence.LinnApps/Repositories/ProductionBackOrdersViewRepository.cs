namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class ProductionBackOrdersViewRepository : IQueryRepository<ProductionBackOrdersView>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProductionBackOrdersViewRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProductionBackOrdersView FindBy(Expression<Func<ProductionBackOrdersView, bool>> expression)
        {
            return this.serviceDbContext.ProductionBackOrdersView.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<ProductionBackOrdersView> FilterBy(Expression<Func<ProductionBackOrdersView, bool>> expression)
        {
            return this.serviceDbContext.ProductionBackOrdersView.Where(expression);
        }

        public IQueryable<ProductionBackOrdersView> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}