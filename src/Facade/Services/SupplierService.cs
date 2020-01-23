namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class SupplierService : FacadeService<Supplier, int, SupplierResource,
        SupplierResource>, IFacadeWithSearchReturnTen<Supplier, int, SupplierResource,
                                       SupplierResource>
    {
        private readonly IRepository<Supplier, int> repository;

        public SupplierService(
            IRepository<Supplier, int> repository,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }
        public IResult<IEnumerable<Supplier>> SearchReturnTen(string searchTerm)
        {
            try
            {
                return new SuccessResult<IEnumerable<Supplier>>(this.repository.FilterBy(this.SearchExpression(searchTerm)).ToList().Take(10));
            }
            catch (NotImplementedException ex)
            {
                return new BadRequestResult<IEnumerable<Supplier>>("Search is not implemented");
            }
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
            return w => w.SupplierId.ToString().Contains(searchTerm) || w.SupplierName.Contains(searchTerm);
        }
    }
}
