namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class SernosBuiltRepository : IQueryRepository<SernosBuilt>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SernosBuiltRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SernosBuilt FindBy(Expression<Func<SernosBuilt, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SernosBuilt> FilterBy(Expression<Func<SernosBuilt, bool>> expression)
        {
            return this.serviceDbContext.SernosBuiltView.Where(expression);
        }

        public IQueryable<SernosBuilt> FindAll()
        {
            return this.serviceDbContext.SernosBuiltView;
        }
    }
}