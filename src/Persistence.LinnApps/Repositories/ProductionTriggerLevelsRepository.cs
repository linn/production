namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class ProductionTriggerLevelsRepository : IRepository<ProductionTriggerLevel, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProductionTriggerLevelsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProductionTriggerLevel FindById(string key)
        {
            return this.serviceDbContext.ProductionTriggerLevels.Where(p => p.PartNumber == key).ToList()
                .FirstOrDefault();
        }

        public IQueryable<ProductionTriggerLevel> FindAll()
        {
            return this.serviceDbContext.ProductionTriggerLevels;
        }

        public void Add(ProductionTriggerLevel entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ProductionTriggerLevel entity)
        {
            throw new NotImplementedException();
        }

        public ProductionTriggerLevel FindBy(Expression<Func<ProductionTriggerLevel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductionTriggerLevel> FilterBy(Expression<Func<ProductionTriggerLevel, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
