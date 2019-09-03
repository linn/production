namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class AssemblyFailFaultCodesService : FacadeService<AssemblyFailFaultCode, string, 
        AssemblyFailFaultCodeResource, AssemblyFailFaultCodeResource>
    {
        public AssemblyFailFaultCodesService(IRepository<AssemblyFailFaultCode, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override AssemblyFailFaultCode CreateFromResource(AssemblyFailFaultCodeResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(AssemblyFailFaultCode entity, AssemblyFailFaultCodeResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<AssemblyFailFaultCode, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}