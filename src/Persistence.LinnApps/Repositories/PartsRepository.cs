namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class PartsRepository : IRepository<Part, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Part FindById(string key)
        {
            return this.serviceDbContext.Parts.Where(p => p.PartNumber == key).ToList().FirstOrDefault();
        }

        public IQueryable<Part> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Part entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Part entity)
        {
            throw new NotImplementedException();
        }

        public Part FindBy(Expression<Func<Part, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Part> FilterBy(Expression<Func<Part, bool>> expression)
        {
            return this.serviceDbContext.Parts.Where(expression);
        }
    }
}