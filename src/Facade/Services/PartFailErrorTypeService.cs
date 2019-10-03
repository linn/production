namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class PartFailErrorTypeService : FacadeService<PartFailErrorType, string, PartFailErrorTypeResource, PartFailErrorTypeResource>
    {
        public PartFailErrorTypeService(IRepository<PartFailErrorType, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override PartFailErrorType CreateFromResource(PartFailErrorTypeResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PartFailErrorType entity, PartFailErrorTypeResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PartFailErrorType, bool>> SearchExpression(string searchTerm)
        {
            return t => t.DateInvalid == null;
        }
    }
}