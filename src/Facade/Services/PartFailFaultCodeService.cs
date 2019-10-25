namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class PartFailFaultCodeService : FacadeService<
        PartFailFaultCode, string, PartFailFaultCodeResource, PartFailFaultCodeResource>
    {
        public PartFailFaultCodeService(IRepository<PartFailFaultCode, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override PartFailFaultCode CreateFromResource(PartFailFaultCodeResource resource)
        {
            return new PartFailFaultCode
                       {
                           FaultCode = resource.FaultCode,
                           Description = resource.FaultDescription
                       };
        }

        protected override void UpdateFromResource(PartFailFaultCode entity, PartFailFaultCodeResource updateResource)
        {
            entity.Description = updateResource.FaultDescription;
        }

        protected override Expression<Func<PartFailFaultCode, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}