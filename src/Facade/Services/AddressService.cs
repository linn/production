namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class AddressService : FacadeService<Address, int, AddressResource,
        AddressResource>
    {
        public AddressService(
            IRepository<Address, int> repository,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Address CreateFromResource(AddressResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Address entity, AddressResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Address, bool>> SearchExpression(string searchTerm)
        {
            return w => w.Id.ToString().Contains(searchTerm) || w.Addressee.Contains(searchTerm);
        }
    }
}
