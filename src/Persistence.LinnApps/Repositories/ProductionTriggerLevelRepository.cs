namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class ProductionTriggerLevelRepository : IRepository<ProductionTriggerLevel, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProductionTriggerLevelRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProductionTriggerLevel FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductionTriggerLevel> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ProductionTriggerLevel entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ProductionTriggerLevel entity)
        {
            this.serviceDbContext.ProductionTriggerLevels.Remove(entity);
        }

        public ProductionTriggerLevel FindBy(Expression<Func<ProductionTriggerLevel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductionTriggerLevel> FilterBy(Expression<Func<ProductionTriggerLevel, bool>> expression)
        {
            return this.serviceDbContext.ProductionTriggerLevels.Where(expression).OrderBy(l => l.PartNumber);
        }
    }
}