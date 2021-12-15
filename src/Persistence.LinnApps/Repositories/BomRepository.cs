namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class BomRepository : IQueryRepository<Bom>
    {
        private readonly ServiceDbContext serviceDbContext;

        public BomRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Bom FindBy(Expression<Func<Bom, bool>> expression)
        {
            return this.serviceDbContext.Boms.Where(expression)
                .AsNoTracking()
                .Include(b => b.Details).ThenInclude(d => d.Part).ToList()
                .FirstOrDefault();
        }

        public IQueryable<Bom> FilterBy(Expression<Func<Bom, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Bom> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
