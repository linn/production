namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;

    public class PartFailErrorTypeRepository : IRepository<PartFailErrorType, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartFailErrorTypeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartFailErrorType FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartFailErrorType> FindAll()
        {
            return this.serviceDbContext.PartFailErrorTypes;
        }

        public void Add(PartFailErrorType entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(PartFailErrorType entity)
        {
            throw new NotImplementedException();
        }

        public PartFailErrorType FindBy(Expression<Func<PartFailErrorType, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartFailErrorType> FilterBy(Expression<Func<PartFailErrorType, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}