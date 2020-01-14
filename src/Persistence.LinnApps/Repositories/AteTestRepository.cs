namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;

    using Microsoft.EntityFrameworkCore;

    public class AteTestRepository : IRepository<AteTest, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public AteTestRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public AteTest FindById(int key)
        {
            return this.serviceDbContext
                .AteTests.Where(t => t.TestId == key)
                .Include(w => w.WorksOrder).Include(w => w.WorksOrder.Part)
                .Include(w => w.User)
                .Include(t => t.PcbOperator)
                .Include(t => t.Details)
                .Where(d => d.DateInvalid == null)
                .ToList().FirstOrDefault();
        }

        public IQueryable<AteTest> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(AteTest entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(AteTest entity)
        {
            throw new NotImplementedException();
        }

        public AteTest FindBy(Expression<Func<AteTest, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AteTest> FilterBy(Expression<Func<AteTest, bool>> expression)
        {
            return this.serviceDbContext.AteTests
                .Where(expression)
                .Include(w => w.WorksOrder).Include(w => w.WorksOrder.Part)
                .Include(w => w.User)
                .Include(t => t.PcbOperator)
                .Include(t => t.Details)
                .Where(d => d.DateInvalid == null);
        }
    }
}