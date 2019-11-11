namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class SupplierService : FacadeService<Supplier, int, SupplierResource, SupplierResource>
    {
        public SupplierService(IRepository<Supplier, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Supplier CreateFromResource(SupplierResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Supplier entity, SupplierResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Supplier, bool>> SearchExpression(string searchTerm)
        {
            return supplier => supplier.DateClosed != null;
        }
    }
}