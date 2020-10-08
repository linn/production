namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class AddressService : FacadeService<Address, int, AddressResource, AddressResource>,
                                  IFacadeWithSearchReturnTen<Address, int, AddressResource, AddressResource>
    {
        private readonly IRepository<Address, int> repository;

        public AddressService(
            IRepository<Address, int> repository,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<Address>> SearchReturnTen(string searchTerm)
        {
            try
            {
                return new SuccessResult<IEnumerable<Address>>(this.repository.FilterBy(this.SearchExpression(searchTerm)).ToList().Take(10));
            }
            catch (NotImplementedException)
            {
                return new BadRequestResult<IEnumerable<Address>>("Search is not implemented");
            }
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
            return w => !w.DateInvalid.HasValue && (w.Id.ToString().Contains(searchTerm) || w.Addressee.ToUpper().Contains(searchTerm.ToUpper()));
        }
    }
}
