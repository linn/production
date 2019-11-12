namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class LabelTypeRepository : IRepository<LabelType, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public LabelTypeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public LabelType FindById(string key)
        {
            return this.serviceDbContext.LabelTypes.Where(f => f.LabelTypeCode == key).ToList().FirstOrDefault();
        }

        public IQueryable<LabelType> FindAll()
        {
            return this.serviceDbContext.LabelTypes;
        }

        public void Add(LabelType entity)
        {
            this.serviceDbContext.LabelTypes.Add(entity);
        }

        public void Remove(LabelType entity)
        {
            throw new NotImplementedException();
        }

        public LabelType FindBy(Expression<Func<LabelType, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LabelType> FilterBy(Expression<Func<LabelType, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
