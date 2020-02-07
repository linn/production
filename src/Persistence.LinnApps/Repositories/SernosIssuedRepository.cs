namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class SernosIssuedRepository : IQueryRepository<SernosIssued>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SernosIssuedRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SernosIssued FindBy(Expression<Func<SernosIssued, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SernosIssued> FilterBy(Expression<Func<SernosIssued, bool>> expression)
        {
            return this.serviceDbContext.SernosIssuedView.Where(expression);
        }

        public IQueryable<SernosIssued> FindAll()
        {
            return this.serviceDbContext.SernosIssuedView;
        }
    }
}