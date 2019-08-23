namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class AssemblyFailsService : FacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource>
    {
        public AssemblyFailsService(IRepository<AssemblyFail, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override AssemblyFail CreateFromResource(AssemblyFailResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(AssemblyFail entity, AssemblyFailResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<AssemblyFail, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}