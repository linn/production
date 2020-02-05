namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Persistence.LinnApps;

    public class ProductionDaysRequiredRepository : IQueryRepository<ProductionDaysRequired>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProductionDaysRequiredRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProductionDaysRequired FindBy(Expression<Func<ProductionDaysRequired, bool>> expression)
        {
            return this.serviceDbContext.ProductionDaysRequired.Where(expression).FirstOrDefault();
        }

        public IQueryable<ProductionDaysRequired> FilterBy(Expression<Func<ProductionDaysRequired, bool>> expression)
        {
            return this.serviceDbContext.ProductionDaysRequired.Where(expression);
        }

        public IQueryable<ProductionDaysRequired> FindAll()
        {
            return this.serviceDbContext.ProductionDaysRequired;
        }
    }
}