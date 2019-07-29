namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Linn.Common.Persistence;
    using Linn.Production.Domain;

    public class ProductionMeasuresRepository : IRepository<ProductionMeasures, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProductionMeasuresRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProductionMeasures FindById(string key)
        {
            return this.serviceDbContext.ProductionMeasures.Where(m => m.Cit.Code == key).Include(s => s.Cit).ToList()
                .FirstOrDefault();
        }

        public IQueryable<ProductionMeasures> FindAll()
        {
            return this.serviceDbContext.ProductionMeasures.Include(s => s.Cit);
        }

        public void Add(ProductionMeasures entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ProductionMeasures entity)
        {
            throw new NotImplementedException();
        }

        public ProductionMeasures FindBy(Expression<Func<ProductionMeasures, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductionMeasures> FilterBy(Expression<Func<ProductionMeasures, bool>> expression)
        {
            return this.serviceDbContext.ProductionMeasures.Where(expression).Include(s => s.Cit);
        }
    }
}