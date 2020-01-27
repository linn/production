namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class WswShortageRepository : IQueryRepository<WswShortage>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WswShortageRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }


        public WswShortage FindBy(Expression<Func<WswShortage, bool>> expression)
        {
            return this.serviceDbContext.WswShortages.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<WswShortage> FilterBy(Expression<Func<WswShortage, bool>> expression)
        {
            return this.serviceDbContext.WswShortages.Where(expression);
        }

        public IQueryable<WswShortage> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}