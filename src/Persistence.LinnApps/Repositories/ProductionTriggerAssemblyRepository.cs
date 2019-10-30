namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Repositories;
    using Linn.Production.Domain.LinnApps.Triggers;

    public class ProductionTriggerAssemblyRepository : IQueryRepository<ProductionTriggerAssembly>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProductionTriggerAssemblyRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProductionTriggerAssembly FindBy(Expression<Func<ProductionTriggerAssembly, bool>> expression)
        {
            // Oracle driver generates FETCH FIRST 1 ROWS ONLY SQL if you just use FirstOrDefault which is Oracle 11/12 compatible not doesn't work with 10g
            return this.serviceDbContext.ProductionTriggerAssemblies.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<ProductionTriggerAssembly> FilterBy(Expression<Func<ProductionTriggerAssembly, bool>> expression)
        {
            return this.serviceDbContext.ProductionTriggerAssemblies.Where(expression);
        }

        public IQueryable<ProductionTriggerAssembly> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
