namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    public class PcasRevisionService : FacadeService<PcasRevision, string, PcasRevisionResource, PcasRevisionResource>
    {
        public PcasRevisionService(IRepository<PcasRevision, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override PcasRevision CreateFromResource(PcasRevisionResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PcasRevision entity, PcasRevisionResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PcasRevision, bool>> SearchExpression(string searchTerm)
        {
            return w => w.PcasPartNumber.Equals(searchTerm);
        }
    }
}