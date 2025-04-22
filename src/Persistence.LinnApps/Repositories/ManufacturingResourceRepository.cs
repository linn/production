namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Persistence.LinnApps;
    using Microsoft.EntityFrameworkCore;

    public class ManufacturingResourceRepository : IRepository<ManufacturingResource, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ManufacturingResourceRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ManufacturingResource FindById(string key)
        {
            return this.serviceDbContext.ManufacturingResources
                .Where(f => f.ResourceCode == key).ToList().FirstOrDefault();
        }

        public IQueryable<ManufacturingResource> FindAll()
        {
            return this.serviceDbContext.ManufacturingResources.Where(f => f.DateInvalid == null);
        }

        public void Add(ManufacturingResource entity)
        {
            this.serviceDbContext.ManufacturingResources.Add(entity);
        }

        public void Remove(ManufacturingResource entity)
        {
            throw new NotImplementedException();
        }

        public ManufacturingResource FindBy(Expression<Func<ManufacturingResource, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ManufacturingResource> FilterBy(Expression<Func<ManufacturingResource, bool>> expression)
        {
            return this.serviceDbContext.ManufacturingResources.AsNoTracking().Where(expression);
        }
    }
}
