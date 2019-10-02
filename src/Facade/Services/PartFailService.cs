namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class PartFailService : FacadeService<PartFail, int, PartFailResource, PartFailResource>
    {
        public PartFailService(IRepository<PartFail, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override PartFail CreateFromResource(PartFailResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PartFail entity, PartFailResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PartFail, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}