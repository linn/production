namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class CitService : FacadeService<Cit, string, CitResource, CitResource>
    {
        public CitService(IRepository<Cit, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Cit CreateFromResource(CitResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Cit entity, CitResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Cit, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}