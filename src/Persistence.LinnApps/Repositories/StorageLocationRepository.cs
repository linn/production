namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class StorageLocationRepository : IRepository<StorageLocation, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StorageLocationRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StorageLocation FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StorageLocation> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(StorageLocation entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(StorageLocation entity)
        {
            throw new NotImplementedException();
        }

        public StorageLocation FindBy(Expression<Func<StorageLocation, bool>> expression)
        {
            return this.serviceDbContext.StorageLocations.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<StorageLocation> FilterBy(Expression<Func<StorageLocation, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}