namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class WwdDetailQueryRepository : IQueryRepository<WwdDetail>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WwdDetailQueryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public WwdDetail FindBy(Expression<Func<WwdDetail, bool>> expression)
        {
            return this.serviceDbContext.WwdDetails.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<WwdDetail> FilterBy(Expression<Func<WwdDetail, bool>> expression)
        {
            return this.serviceDbContext.WwdDetails.Where(expression);
        }

        public IQueryable<WwdDetail> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}