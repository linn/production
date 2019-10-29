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
            return new PartFailErrorType
                       {
                           ErrorType = resource.ErrorType,
                           DateInvalid = resource.DateInvalid != null ? DateTime.Parse(resource.DateInvalid) : (DateTime?)null
                       };
        }

        protected override void UpdateFromResource(PartFailErrorType entity, PartFailErrorTypeResource updateResource)
        {
            entity.DateInvalid = DateTime.Parse(updateResource.DateInvalid);
        }

        protected override Expression<Func<PartFailErrorType, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}