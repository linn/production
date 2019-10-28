namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class PartsFacadeService : FacadeService<Part, string, PartResource, PartResource>
    {
        public PartsFacadeService(IRepository<Part, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Part CreateFromResource(PartResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Part entity, PartResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Part, bool>> SearchExpression(string searchTerm)
        {
            return p => p.PartNumber.Contains(searchTerm.ToUpper())
                        || p.Description.ToUpper().Contains(searchTerm.ToUpper());
        }
    }
}