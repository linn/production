namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class BomDetailRepository : IQueryRepository<BomDetail>
    {
        private readonly ServiceDbContext serviceDbContext;

        public BomDetailRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public BomDetail FindBy(Expression<Func<BomDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BomDetail> FilterBy(Expression<Func<BomDetail, bool>> expression)
        {
            return this.serviceDbContext.BomDetails.Where(expression);
        }

        public IQueryable<BomDetail> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
