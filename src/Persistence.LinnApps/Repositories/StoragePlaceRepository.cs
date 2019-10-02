namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class StoragePlaceRepository : IRepository<StoragePlace, string>
    {
        public StoragePlace FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoragePlace> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(StoragePlace entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(StoragePlace entity)
        {
            throw new NotImplementedException();
        }

        public StoragePlace FindBy(Expression<Func<StoragePlace, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoragePlace> FilterBy(Expression<Func<StoragePlace, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}