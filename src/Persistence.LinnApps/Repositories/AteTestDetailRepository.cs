namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;

    using Microsoft.EntityFrameworkCore;

    public class AteTestDetailRepository : IRepository<AteTestDetail, AteTestDetailKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public AteTestDetailRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public AteTestDetail FindById(AteTestDetailKey key)
        {
            return this.serviceDbContext.AteTestDetails
                .Where(d => d.TestId == key.TestId && d.ItemNumber == key.ItemNumber)
                .Include(d => d.PcbOperator)
                .ToList().FirstOrDefault();
        }

        public IQueryable<AteTestDetail> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(AteTestDetail entity)
        {
            this.serviceDbContext.AteTestDetails.Add(entity);
        }

        public void Remove(AteTestDetail entity)
        {
            this.serviceDbContext.AteTestDetails.Remove(entity);
        }

        public AteTestDetail FindBy(Expression<Func<AteTestDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AteTestDetail> FilterBy(Expression<Func<AteTestDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}