namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Products;

    public class ProductDataRepository : IRepository<ProductData, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProductDataRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProductData FindById(int key)
        {
            return this.serviceDbContext.ProductData.Where(p => p.ProductId == key).ToList().FirstOrDefault();
        }

        public IQueryable<ProductData> FindAll()
        {
            return this.serviceDbContext.ProductData;
        }

        public void Add(ProductData entity)
        {
            this.serviceDbContext.ProductData.Add(entity);
        }

        public void Remove(ProductData entity)
        {
            throw new NotImplementedException();
        }

        public ProductData FindBy(Expression<Func<ProductData, bool>> expression)
        {
            return this.serviceDbContext.ProductData.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<ProductData> FilterBy(Expression<Func<ProductData, bool>> expression)
        {
            return this.serviceDbContext.ProductData.Where(expression);
        }
    }
}
