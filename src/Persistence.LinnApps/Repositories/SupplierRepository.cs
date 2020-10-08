namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class SupplierRepository : IRepository<Supplier, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SupplierRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Supplier FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Supplier> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Supplier entity)
        {
            throw new NotImplementedException();

        }

        public void Remove(Supplier entity)
        {
            throw new NotImplementedException();
        }

        public Supplier FindBy(Expression<Func<Supplier, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Supplier> FilterBy(Expression<Func<Supplier, bool>> expression)
        {
          return this.serviceDbContext.Suppliers.Where(expression);
        }
    }
}
