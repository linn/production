namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class AddressRepository : IRepository<Address, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public AddressRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Address FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Address> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Address entity)
        {
            throw new NotImplementedException();

        }

        public void Remove(Address entity)
        {
            throw new NotImplementedException();
        }

        public Address FindBy(Expression<Func<Address, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Address> FilterBy(Expression<Func<Address, bool>> expression)
        {
          return this.serviceDbContext.Addresses.Where(expression);
        }
    }
}
