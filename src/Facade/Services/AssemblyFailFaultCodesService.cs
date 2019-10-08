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
            return new AssemblyFailFaultCode
                       {
                           FaultCode = resource.FaultCode,
                           Description = resource.Description,
                           Explanation = resource.Explanation,
                           DateInvalid = resource.DateInvalid != null
                                             ? DateTime.Parse(resource.DateInvalid)
                                             : (DateTime?)null
                       };
        }

        protected override void UpdateFromResource(AssemblyFailFaultCode assemblyFailFaultCode, AssemblyFailFaultCodeResource resource)
        {
            assemblyFailFaultCode.Description = resource.Description;
            assemblyFailFaultCode.DateInvalid = resource.DateInvalid != null ? DateTime.Parse(resource.DateInvalid) : (DateTime?)null;
            assemblyFailFaultCode.Explanation = resource.Explanation;
        }

        protected override Expression<Func<AssemblyFailFaultCode, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}