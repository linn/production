namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class SalesBackOrderAnalysisRepository : IQueryRepository<SalesBackOrderAnalysis>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SalesBackOrderAnalysisRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SalesBackOrderAnalysis FindBy(Expression<Func<SalesBackOrderAnalysis, bool>> expression)
        {
            return this.serviceDbContext.SalesBackOrderAnalysis.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<SalesBackOrderAnalysis> FilterBy(Expression<Func<SalesBackOrderAnalysis, bool>> expression)
        {
            return this.serviceDbContext.SalesBackOrderAnalysis.Where(expression);
        }
    }
}