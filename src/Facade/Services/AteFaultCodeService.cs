namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class AteFaultCodeService : FacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource>
    {
        public AteFaultCodeService(IRepository<AteFaultCode, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override AteFaultCode CreateFromResource(AteFaultCodeResource resource)
        {
            return new AteFaultCode(resource.FaultCode)
                       {
                           Description = resource.Description,
                           DateInvalid = string.IsNullOrEmpty(resource.DateInvalid)
                                             ? (DateTime?)null
                                             : DateTime.Parse(resource.DateInvalid)
                       };
        }

        protected override void UpdateFromResource(AteFaultCode ateFaultCode, AteFaultCodeResource updateResource)
        {
            ateFaultCode.Description = updateResource.Description;
            ateFaultCode.DateInvalid = string.IsNullOrEmpty(updateResource.DateInvalid)
                                           ? (DateTime?)null
                                           : DateTime.Parse(updateResource.DateInvalid);
        }

        protected override Expression<Func<AteFaultCode, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}