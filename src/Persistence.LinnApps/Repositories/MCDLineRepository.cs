namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Persistence.LinnApps;

    public class MCDLineRepository : IQueryRepository<MCDLine>
    {
        private readonly ServiceDbContext serviceDbContext;

        public MCDLineRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public MCDLine FindBy(Expression<Func<MCDLine, bool>> expression)
        {
            return this.serviceDbContext.MCDLines.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<MCDLine> FilterBy(Expression<Func<MCDLine, bool>> expression)
        {
            return this.serviceDbContext.MCDLines.Where(expression);
        }
    }
}