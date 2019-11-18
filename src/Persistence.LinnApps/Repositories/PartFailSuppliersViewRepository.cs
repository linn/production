namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class PartFailSuppliersViewRepository : IQueryRepository<PartFailSupplierView>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartFailSuppliersViewRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartFailSupplierView FindBy(Expression<Func<PartFailSupplierView, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartFailSupplierView> FilterBy(Expression<Func<PartFailSupplierView, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartFailSupplierView> FindAll()
        {
            return this.serviceDbContext.PartFailSuppliersView;
        }
    }
}