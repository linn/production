namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class ManufacturingOperationsRepository : IRepository<ManufacturingOperation, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ManufacturingOperationsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ManufacturingOperation FindById(int key)
        {
            return this.serviceDbContext.ManufacturingOperations
                .Where(f => f.ManufacturingId == key).ToList().FirstOrDefault();
        }

        public IQueryable<ManufacturingOperation> FindAll()
        {
            return this.serviceDbContext.ManufacturingOperations;
        }

        public void Add(ManufacturingOperation entity)
        {
            this.serviceDbContext.ManufacturingOperations.Add(entity);
        }

        public void Remove(ManufacturingOperation entity)
        {
            this.serviceDbContext.ManufacturingOperations.Remove(entity);
        }

        public ManufacturingOperation FindBy(Expression<Func<ManufacturingOperation, bool>> expression)
        {
            return this.serviceDbContext.ManufacturingOperations.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<ManufacturingOperation> FilterBy(Expression<Func<ManufacturingOperation, bool>> expression)
        {
            return this.serviceDbContext.ManufacturingOperations.Where(expression);
        }
    }
}
