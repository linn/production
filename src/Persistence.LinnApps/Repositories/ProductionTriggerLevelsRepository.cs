namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class ProductionTriggerLevelsRepository : IRepository<ProductionTriggerLevel, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProductionTriggerLevelsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProductionTriggerLevel FindById(string key)
        {
            return this.serviceDbContext.ProductionTriggerLevels
                .Where(p => p.PartNumber == key)
                .Include(a => a.Cit)
                .ThenInclude(b => b.CitLeader)
                .ToList()
                .FirstOrDefault();
        }

        public IQueryable<ProductionTriggerLevel> FindAll()
        {
            return this.serviceDbContext.ProductionTriggerLevels;
        }

        public void Add(ProductionTriggerLevel entity)
        {
            this.serviceDbContext.ProductionTriggerLevels.Add(entity);
        }

        public void Remove(ProductionTriggerLevel entity)
        {
            this.serviceDbContext.ProductionTriggerLevels.Remove(entity);
        }

        public ProductionTriggerLevel FindBy(Expression<Func<ProductionTriggerLevel, bool>> expression)
        {
            return this.serviceDbContext.ProductionTriggerLevels.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<ProductionTriggerLevel> FilterBy(Expression<Func<ProductionTriggerLevel, bool>> expression)
        {
            return this.serviceDbContext.ProductionTriggerLevels.Where(expression);
        }
    }
}
