namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class AddressRepository : IQueryRepository<Address>
    {
        private readonly ServiceDbContext serviceDbContext;

        public AddressRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Address FindBy(Expression<Func<Address, bool>> expression)
        {
            return this.serviceDbContext.Addresses.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<Address> FilterBy(Expression<Func<Address, bool>> expression)
        {
            return this.serviceDbContext.Addresses.Where(expression);
        }
    }
}