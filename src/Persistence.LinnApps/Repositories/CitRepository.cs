namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;

    public class CitRepository : IRepository<Cit, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public CitRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Cit FindById(string key)
        {
            return this.serviceDbContext.Cits.Where(c => c.Code == key).ToList().FirstOrDefault();
        }

        public IQueryable<Cit> FindAll()
        {
            return this.serviceDbContext.Cits.OrderBy(c => c.SortOrder);
        }

        public void Add(Cit entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Cit entity)
        {
            throw new NotImplementedException();
        }

        public Cit FindBy(Expression<Func<Cit, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Cit> FilterBy(Expression<Func<Cit, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

