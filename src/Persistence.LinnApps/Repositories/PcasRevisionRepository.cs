namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class PcasRevisionRepository : IRepository<PcasRevision, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PcasRevisionRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PcasRevision FindById(string key)
        {
            return this.serviceDbContext.PcasRevisions.Where(p => p.BoardCode == key).ToList().FirstOrDefault();
        }

        public IQueryable<PcasRevision> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(PcasRevision entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(PcasRevision entity)
        {
            throw new NotImplementedException();
        }

        public PcasRevision FindBy(Expression<Func<PcasRevision, bool>> expression)
        {
            return this.serviceDbContext.PcasRevisions.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<PcasRevision> FilterBy(Expression<Func<PcasRevision, bool>> expression)
        {
            return this.serviceDbContext.PcasRevisions.Where(expression);
        }
    }
}
