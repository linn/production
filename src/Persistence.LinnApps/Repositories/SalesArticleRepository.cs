namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class SalesArticleRepository : IQueryRepository<SalesArticle>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SalesArticleRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SalesArticle FindBy(Expression<Func<SalesArticle, bool>> expression)
        {
            return this.serviceDbContext.SalesArticles.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<SalesArticle> FilterBy(Expression<Func<SalesArticle, bool>> expression)
        {
            return this.serviceDbContext.SalesArticles.Where(expression);
        }
    }
}