namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Production.Domain.LinnApps.Repositories;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class StoragePlaceRepository : IQueryRepository<StoragePlace>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StoragePlaceRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public IQueryable<StoragePlace> FindAll()
        {
            return this.serviceDbContext.StoragePlaces.GroupBy(x => x.StoragePlaceId)
                .Where(g => g.Count() == 1)
                .Select(g => g.First());
        }

        public StoragePlace FindBy(Expression<Func<StoragePlace, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoragePlace> FilterBy(Expression<Func<StoragePlace, bool>> expression)
        {
            return this.serviceDbContext.StoragePlaces.Where(expression);
        }
    }
}