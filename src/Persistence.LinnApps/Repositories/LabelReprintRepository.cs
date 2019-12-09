namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Proxy;

    public class LabelReprintRepository : IRepository<LabelReprint, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        private readonly IDatabaseService databaseService;

        public LabelReprintRepository(ServiceDbContext serviceDbContext, IDatabaseService databaseService)
        {
            this.serviceDbContext = serviceDbContext;
            this.databaseService = databaseService;
        }

        public LabelReprint FindById(int key)
        {
            return this.serviceDbContext.LabelReprints.Where(a => a.LabelReprintId == key).ToList().FirstOrDefault();
        }

        public IQueryable<LabelReprint> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(LabelReprint entity)
        {
            entity.LabelReprintId = this.databaseService.GetNextVal("LAB_REP_SEQ");
            this.serviceDbContext.LabelReprints.Add(entity);
        }

        public void Remove(LabelReprint entity)
        {
            throw new NotImplementedException();
        }

        public LabelReprint FindBy(Expression<Func<LabelReprint, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LabelReprint> FilterBy(Expression<Func<LabelReprint, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}