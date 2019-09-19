namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class ManufacturingRoutesRepository : IRepository<ManufacturingRoute, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ManufacturingRoutesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ManufacturingRoute FindById(string key)
        {
            return this.serviceDbContext.ManufacturingRoutes.Include(x => x.Operations).Where(f => f.RouteCode == key).ToList().FirstOrDefault();
        }

        public IQueryable<ManufacturingRoute> FindAll()
        {
            return this.serviceDbContext.ManufacturingRoutes.Include(x => x.Operations);
        }

        public void Add(ManufacturingRoute entity)
        {
            this.serviceDbContext.ManufacturingRoutes.Add(entity);
        }

        public void Remove(ManufacturingRoute entity)
        {
            throw new NotImplementedException();
        }

        public ManufacturingRoute FindBy(Expression<Func<ManufacturingRoute, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ManufacturingRoute> FilterBy(Expression<Func<ManufacturingRoute, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
