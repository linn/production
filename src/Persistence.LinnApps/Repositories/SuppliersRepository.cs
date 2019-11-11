namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class SuppliersRepository : IRepository<Supplier, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SuppliersRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
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
            throw new NotImplementedException();
        }

        public Supplier FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Supplier> FindAll()
        {
            var wot = this.serviceDbContext.Suppliers;

            return this.serviceDbContext.Suppliers.Where(s => s.DateClosed != null);
        }

        public void Add(Supplier entity)
        {
            throw new NotImplementedException();
        }
    }
}